using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using log4net;

using ACE.Common;
using ACE.Common.Extensions;
using ACE.Database;
using ACE.Database.Models.World;
using ACE.DatLoader;
using ACE.DatLoader.FileTypes;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Factories;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.WorldObjects;

namespace ACE.Server.Managers
{
    public partial class RecipeManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Recipe GetRecipe(Player player, WorldObject source, WorldObject target)
        {
            // PY16 recipes
            var cookbook = DatabaseManager.World.GetCachedCookbook(source.WeenieClassId, target.WeenieClassId);
            if (cookbook != null)
                return cookbook.Recipe;

            // if none exists, try finding new recipe
            return GetNewRecipe(player, source, target);
        }

        public static void UseObjectOnTarget(Player player, WorldObject source, WorldObject target, bool confirmed = false)
        {
            if (player.IsBusy)
            {
                player.SendUseDoneEvent(WeenieError.YoureTooBusy);
                return;
            }

            if (source == target)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"The {source.NameWithMaterial} cannot be combined with itself.", ChatMessageType.Craft));
                player.Session.Network.EnqueueSend(new GameEventCommunicationTransientString(player.Session, $"You can't use the {source.NameWithMaterial} on itself."));
                player.SendUseDoneEvent();
                return;
            }

            var recipe = GetRecipe(player, source, target);

            if (recipe == null)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"The {source.NameWithMaterial} cannot be used on the {target.NameWithMaterial}.", ChatMessageType.Craft));
                player.SendUseDoneEvent();
                return;
            }

            // verify requirements
            if (!VerifyRequirements(recipe, player, source, target))
            {
                player.SendUseDoneEvent(WeenieError.YouDoNotPassCraftingRequirements);
                return;
            }

            if (source.ItemType == ItemType.TinkeringMaterial)
            {
                HandleTinkering(player, source, target);
                return;
            }

            var percentSuccess = GetRecipeChance(player, source, target, recipe);

            if (percentSuccess == null)
            {
                player.SendUseDoneEvent();
                return;
            }

            var showDialog = HasDifficulty(recipe) && player.GetCharacterOption(CharacterOption.UseCraftingChanceOfSuccessDialog);

            if (showDialog && !confirmed)
            {
                ShowDialog(player, source, target, (float)percentSuccess);
                return;
            }

            ActionChain craftChain = new ActionChain();

            var animTime = 0.0f;

            player.IsBusy = true;

            if (player.CombatMode != CombatMode.NonCombat)
            {
                var stanceTime = player.SetCombatMode(CombatMode.NonCombat);
                craftChain.AddDelaySeconds(stanceTime);

                animTime += stanceTime;
            }

            animTime += player.EnqueueMotion(craftChain, MotionCommand.ClapHands);

            craftChain.AddAction(player, () => HandleRecipe(player, source, target, recipe, (float)percentSuccess));

            player.EnqueueMotion(craftChain, MotionCommand.Ready);

            craftChain.AddAction(player, () =>
            {
                if (!showDialog)
                    player.SendUseDoneEvent();

                player.IsBusy = false;
            });

            craftChain.EnqueueChain();

            player.NextUseTime = DateTime.UtcNow.AddSeconds(animTime);
        }

        public static bool HasDifficulty(Recipe recipe)
        {
            return recipe.Skill > 0 && recipe.Difficulty > 0;
        }

        public static float? GetRecipeChance(Player player, WorldObject source, WorldObject target, Recipe recipe)
        {
            // only for regular recipes atm, tinkering / imbues handled separately
            // todo: refactor this more
            if (!HasDifficulty(recipe))
                return 1.0f;

            var playerSkill = player.GetCreatureSkill((Skill)recipe.Skill);

            if (playerSkill == null)
            {
                // this shouldn't happen, but sanity check for unexpected nulls
                log.Warn($"RecipeManager.GetRecipeChance({player.Name}, {source.Name}, {target.Name}): recipe {recipe.Id} missing skill");
                return null;
            }

            // check for pre-MoA skill
            // convert into appropriate post-MoA skill
            // pre-MoA melee weapons: get highest melee weapons skill
            var newSkill = player.ConvertToMoASkill(playerSkill.Skill);

            playerSkill = player.GetCreatureSkill(newSkill);

            //Console.WriteLine("Required skill: " + skill.Skill);

            if (playerSkill.AdvancementClass < SkillAdvancementClass.Trained)
            {
                player.SendWeenieError(WeenieError.YouAreNotTrainedInThatTradeSkill);
                return null;
            }

            //Console.WriteLine("Skill difficulty: " + recipe.Recipe.Difficulty);

            var chance = (float)SkillCheck.GetSkillChance(playerSkill.Current, recipe.Difficulty);

            return chance;
        }

        public static void HandleRecipe(Player player, WorldObject source, WorldObject target, Recipe recipe, float successChance)
        {
            // re-verify
            if (!VerifyRequirements(recipe, player, source, target))
            {
                player.SendWeenieError(WeenieError.YouDoNotPassCraftingRequirements);
                return;
            }

            var success = ThreadSafeRandom.Next(0.0f, 1.0f) <= successChance;

            CreateDestroyItems(player, recipe, source, target, success);

            // this code was intended for dyes, but UpdateObj seems to remove crafting components
            // from shortcut bar, if they are hotkeyed
            // more specifity for this, only if relevant properties are modified?
            var shortcuts = player.GetShortcuts();

            if (!shortcuts.Select(i => i.ObjectId).Contains(target.Guid.Full))
            {
                var updateObj = new GameMessageUpdateObject(target);
                var updateDesc = new GameMessageObjDescEvent(player);

                if (target.CurrentWieldedLocation != null)
                    player.EnqueueBroadcast(updateObj, updateDesc);
                else
                    player.Session.Network.EnqueueSend(updateObj);
            }
        }

        public static float DoMotion(Player player, MotionCommand motionCommand)
        {
            var motion = new Motion(MotionStance.NonCombat, motionCommand);
            player.EnqueueBroadcastMotion(motion);

            var motionTable = DatManager.PortalDat.ReadFromDat<MotionTable>(player.MotionTableId);
            var craftAnimationLength = motionTable.GetAnimationLength(motionCommand);
            return craftAnimationLength;
        }

        public static void ShowDialog(Player player, WorldObject source, WorldObject target, float successChance, bool tinkering = false, int numAugs = 0)
        {
            var percent = successChance * 100;

            // retail messages:

            // You determine that you have a 100 percent chance to succeed.
            // You determine that you have a 99 percent chance to succeed.
            // You determine that you have a 38 percent chance to succeed. 5 percent is due to your augmentation.

            var floorMsg = $"You determine that you have a {percent.Round()}% chance to succeed.";
            if (numAugs > 0)
                floorMsg += $"\n{numAugs * 5}% is due to your augmentation.";

            player.ConfirmationManager.EnqueueSend(new Confirmation_CraftInteration(player.Guid, source.Guid, target.Guid, tinkering), floorMsg);

            if (PropertyManager.GetBool("craft_exact_msg").Item)
            {
                var exactMsg = $"You have a {percent}% chance of using {source.NameWithMaterial} on {target.NameWithMaterial}.";

                player.Session.Network.EnqueueSend(new GameMessageSystemChat(exactMsg, ChatMessageType.Craft));
            }
            player.SendUseDoneEvent();
        }

        public static void HandleTinkering(Player player, WorldObject tool, WorldObject target, bool confirmed = false)
        {
            double successChance;
            bool incItemTinkered = true;

            Console.WriteLine($"{player.Name}.HandleTinkering({tool.NameWithMaterial}, {target.NameWithMaterial})");

            // calculate % success chance

            var toolWorkmanship = tool.Workmanship ?? 0;
            var itemWorkmanship = target.Workmanship ?? 0;

            var tinkeredCount = target.NumTimesTinkered;

            var materialType = tool.MaterialType ?? MaterialType.Unknown;
            var salvageMod = GetMaterialMod(materialType);

            var workmanshipMod = 1.0f;
            if (toolWorkmanship >= itemWorkmanship)
                workmanshipMod = 2.0f;

            var recipe = GetRecipe(player, tool, target);
            var recipeSkill = (Skill)recipe.Skill;
            var skill = player.GetCreatureSkill(recipeSkill);

            // require skill check for everything except ivory / leather / sandstone
            if (UseSkillCheck(materialType))
            {
                // tinkering skill must be trained
                if (skill.AdvancementClass < SkillAdvancementClass.Trained)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You are not trained in {skill.Skill.ToSentence()}.", ChatMessageType.Broadcast));
                    player.SendUseDoneEvent();
                    return;
                }

                // thanks to Endy's Tinkering Calculator for this formula!
                var attemptMod = TinkeringDifficulty[tinkeredCount];

                var difficulty = (int)Math.Floor(((salvageMod * 5.0f) + (itemWorkmanship * salvageMod * 2.0f) - (toolWorkmanship * workmanshipMod * salvageMod / 5.0f)) * attemptMod);

                if (player.Enlightenment >= 1 && player.Enlightenment <= 2)
                {
                    successChance = SkillCheck.GetSkillChance((int)skill.Current, difficulty) + 0.08f;
                }
                else if (player.Enlightenment >= 3 && player.Enlightenment <= 4)
                {
                    successChance = SkillCheck.GetSkillChance((int)skill.Current, difficulty) + 0.16f;
                }
                else if (player.Enlightenment >= 5 && player.Enlightenment <= 6)
                {
                    successChance = SkillCheck.GetSkillChance((int)skill.Current, difficulty) + 0.24f;
                }
                else if (player.Enlightenment >= 7)
                {
                    successChance = SkillCheck.GetSkillChance((int)skill.Current, difficulty) + 0.32f;
                }
                else
                {
                    successChance = SkillCheck.GetSkillChance((int)skill.Current, difficulty);
                }

                // imbue: divide success by 3
                if (recipe.SalvageType == 2)
                {
                    if (player.Enlightenment >= 1 && player.Enlightenment <= 2)
                    {
                        successChance /= 3.0f;
                        successChance += 0.08f;
                        if (player.AugmentationBonusImbueChance > 0)
                            successChance += player.AugmentationBonusImbueChance * 0.05f;
                    }
                    else if (player.Enlightenment >= 3 && player.Enlightenment <= 4)
                    {
                        successChance /= 3.0f;
                        successChance += 0.16f;
                        if (player.AugmentationBonusImbueChance > 0)
                            successChance += player.AugmentationBonusImbueChance * 0.05f;
                    }
                    else if (player.Enlightenment >= 5 && player.Enlightenment <= 6)
                    {
                        successChance /= 3.0f;
                        successChance += 0.24f;
                        if (player.AugmentationBonusImbueChance > 0)
                            successChance += player.AugmentationBonusImbueChance * 0.05f;
                    }
                    else if (player.Enlightenment >= 7)
                    {
                        successChance /= 3.0f;
                        successChance += 0.32f;
                        if (player.AugmentationBonusImbueChance > 0)
                            successChance += player.AugmentationBonusImbueChance * 0.05f;
                    }
                    else
                    {
                        successChance /= 3.0f;
                        if (player.AugmentationBonusImbueChance > 0)
                            successChance += player.AugmentationBonusImbueChance * 0.05f;
                    }
                }

                // handle rare foolproof material
                if (tool.WeenieClassId >= 30094 && tool.WeenieClassId <= 30106)
                    successChance = 1.0f;

                // check for player option: 'Use Crafting Chance of Success Dialog'
                if (player.GetCharacterOption(CharacterOption.UseCraftingChanceOfSuccessDialog) && !confirmed)
                {
                    var numAugs = recipe.SalvageType == 2 ? player.AugmentationBonusImbueChance : 0;

                    ShowDialog(player, tool, target, (float)successChance, true, numAugs);
                    return;
                }
            }
            else
            {
                // ivory / leather / sandstone always succeeds, and doesn't consume one of the ten tinking slots
                successChance = 1.0f;
                incItemTinkered = false;
            }

            player.IsBusy = true;

            var animLength = DoMotion(player, MotionCommand.ClapHands);

            var actionChain = new ActionChain();
            actionChain.AddDelaySeconds(animLength);
            actionChain.AddAction(player, () =>
            {
                DoTinkering(player, tool, target, recipe, (float)successChance, incItemTinkered);
                DoMotion(player, MotionCommand.Ready);
                player.IsBusy = false;
            });
            actionChain.EnqueueChain();

            player.NextUseTime = DateTime.UtcNow.AddSeconds(animLength);
        }

        public static void DoTinkering(Player player, WorldObject tool, WorldObject target, Recipe recipe, float chance, bool incItemTinkered)
        {
            var success = ThreadSafeRandom.Next(0.0f, 1.0f) <= chance;

            var sourceName = Regex.Replace(tool.NameWithMaterial, @" \(\d+\)$", "");

            if (success)
            {
                Tinkering_ModifyItem(player, tool, target, incItemTinkered);

                // send local broadcast
                if (incItemTinkered)
                    player.EnqueueBroadcast(new GameMessageSystemChat($"{player.Name} successfully applies the {sourceName} (workmanship {(tool.Workmanship ?? 0):#.00}) to the {target.NameWithMaterial}.", ChatMessageType.Craft), WorldObject.LocalBroadcastRange, ChatMessageType.Craft);
            }
            else if (incItemTinkered)
                player.EnqueueBroadcast(new GameMessageSystemChat($"{player.Name} fails to apply the {sourceName} (workmanship {(tool.Workmanship ?? 0):#.00}) to the {target.NameWithMaterial}. The target is destroyed.", ChatMessageType.Craft), WorldObject.LocalBroadcastRange, ChatMessageType.Craft);

            CreateDestroyItems(player, recipe, tool, target, success, !incItemTinkered);

            if (!player.GetCharacterOption(CharacterOption.UseCraftingChanceOfSuccessDialog) || !UseSkillCheck(tool.MaterialType ?? 0))
                player.SendUseDoneEvent();
        }

        public static void Tinkering_ModifyItem(Player player, WorldObject tool, WorldObject target, bool incItemTinkered = true)
        {
            var recipe = GetRecipe(player, tool, target);

            if (tool.MaterialType == null) return;

            var materialType = tool.MaterialType.Value;

            var xtramodroll = ThreadSafeRandom.Next(1, 300); // helps determine which mod occurs
            var alamountlow = ThreadSafeRandom.Next(10, 30); // common base bonus AL
            var modchance = ThreadSafeRandom.Next(1, 200);// the chance that a mod even will roll
            var resistroll = ThreadSafeRandom.Next(1, 187);
            var meleedmg = ThreadSafeRandom.Next(2, 6); // the roll for flat dmg for iron
            var bowmoddmg = ThreadSafeRandom.Next(0.01f, 0.02f); // 1-2% bonus
            var wanddamage = ThreadSafeRandom.Next(0.001f, 0.004f); // .1% - .4% bonus
            var splitmodchance = ThreadSafeRandom.Next(1, 187);
            var retainlore = target.GetProperty(PropertyInt.ItemDifficulty); // ensure that the lore difficulty doesnt increase when spells are added.

            switch (materialType)
            {
                // armor tinkering
                case MaterialType.Steel:
                    if (modchance <= 50) // 25% chance that an an extra mod bonus occurs
                    {
                        if (xtramodroll <= 200)
                        {
                            var alresult = 25 + alamountlow;
                            target.ArmorLevel += 25 + alamountlow;
                            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained {alamountlow} extra AL! New Target AL {target.ArmorLevel}(+{alresult})", ChatMessageType.Broadcast));
                        }
                        else if (xtramodroll >= 201 && xtramodroll <= 251) // rolls for resistance mods.
                        {
                            if (resistroll >= 1 && resistroll <= 20) // pierce
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsPierce = Math.Min((target.ArmorModVsPierce ?? 0) + 0.2f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 20% Piercing resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 21 && resistroll <= 41) // slash
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsSlash = Math.Min((target.ArmorModVsSlash ?? 0) + 0.2f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 20% Slashing resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 42 && resistroll <= 62) // bludge
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsBludgeon = Math.Min((target.ArmorModVsBludgeon ?? 0) + 0.2f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 20% Bludgeon resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 63 && resistroll <= 83) // acid
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsAcid = Math.Min((target.ArmorModVsAcid ?? 0) + 0.4f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 40% Acid resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 84 && resistroll <= 104) // fire
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsFire = Math.Min((target.ArmorModVsFire ?? 0) + 0.4f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 40% Fire resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 105 && resistroll <= 125) // cold
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsCold = Math.Min((target.ArmorModVsCold ?? 0) + 0.4f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 40% Cold resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 126 && resistroll <= 146) // lightning
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsElectric = Math.Min((target.ArmorModVsElectric ?? 0) + 0.4f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 40% Lightning resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 147 && resistroll <= 167) // Nether
                            {
                                target.ArmorLevel += 25;
                                target.ArmorModVsNether = Math.Min((target.ArmorModVsNether ?? 0) + 0.4f, 2.0f);
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained an extra 40% Nether resistance to your armor. {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                            }
                        }
                        else if (xtramodroll >= 252 && xtramodroll <= 297)
                        {
                            target.ArmorLevel += 25;
                            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target AL {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                        }

                        else if (xtramodroll == 298)
                        {
                            target.ArmorLevel += 80;
                            PlayerManager.BroadcastToAll(new GameMessageSystemChat($"{player.Name} Rolled a {xtramodroll}!! They just got super lucky applying steel to an item! Triple Value! New Target AL {target.ArmorLevel}(+80)", ChatMessageType.Broadcast));
                        }
                        else if (xtramodroll >= 299)
                        {
                            target.SetProperty(PropertyInt.Bonded, 1);
                            PlayerManager.BroadcastToAll(new GameMessageSystemChat($"{player.Name} Rolled a perfect {xtramodroll}!! They just got super lucky applying steel to their {target.NameWithMaterial}! The item is now bonded", ChatMessageType.Broadcast));
                        }
                    }
                    else
                    {
                        target.ArmorLevel += 25;
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target AL {target.ArmorLevel}(+25)", ChatMessageType.Broadcast));
                    }
                    break;
                case MaterialType.Alabaster:
                    target.ArmorModVsPierce = Math.Min((target.ArmorModVsPierce ?? 0) + 0.2f, 2.0f);
                    break;
                case MaterialType.Bronze:
                    target.ArmorModVsSlash = Math.Min((target.ArmorModVsSlash ?? 0) + 0.2f, 2.0f);
                    break;
                case MaterialType.Marble:
                    target.ArmorModVsBludgeon = Math.Min((target.ArmorModVsBludgeon ?? 0) + 0.2f, 2.0f);
                    break;
                case MaterialType.ArmoredilloHide:
                    target.ArmorModVsAcid = Math.Min((target.ArmorModVsAcid ?? 0) + 0.4f, 2.0f);
                    break;
                case MaterialType.Ceramic:
                    target.ArmorModVsFire = Math.Min((target.ArmorModVsFire ?? 0) + 0.4f, 2.0f);
                    break;
                case MaterialType.Wool:
                    target.ArmorModVsCold = Math.Min((target.ArmorModVsCold ?? 0) + 0.4f, 2.0f);
                    break;
                case MaterialType.ReedSharkHide:
                    target.ArmorModVsElectric = Math.Min((target.ArmorModVsElectric ?? 0) + 0.4f, 2.0f);
                    break;
                case MaterialType.Peridot:
                    AddImbuedEffect(player, target, ImbuedEffectType.MeleeDefense);
                    break;
                case MaterialType.YellowTopaz:
                    AddImbuedEffect(player, target, ImbuedEffectType.MissileDefense);
                    break;
                case MaterialType.Zircon:
                    AddImbuedEffect(player, target, ImbuedEffectType.MagicDefense);
                    break;

                // item tinkering
                case MaterialType.Pine:
                    target.Value = (int)Math.Round((target.Value ?? 1) * 0.75f);
                    break;
                case MaterialType.Gold:
                    target.Value = (int)Math.Round((target.Value ?? 1) * 1.25f);
                    break;
                case MaterialType.Linen:
                    target.EncumbranceVal = (int)Math.Round((target.EncumbranceVal ?? 1) * 0.75f);
                    break;
                case MaterialType.Ivory:
                    // Recipe already handles this correctly
                    //target.SetProperty(PropertyInt.Attuned, 0);
                    break;
                case MaterialType.Leather:
                    target.Retained = true;
                    break;
                case MaterialType.Sandstone:
                    target.Retained = false;
                    break;
                case MaterialType.Moonstone:
                    target.ItemMaxMana += 500;
                    break;
                case MaterialType.Teak:
                    target.HeritageGroup = HeritageGroup.Aluvian;
                    break;
                case MaterialType.Ebony:
                    target.HeritageGroup = HeritageGroup.Gharundim;
                    break;
                case MaterialType.Porcelain:
                    target.HeritageGroup = HeritageGroup.Sho;
                    break;
                case MaterialType.Satin:
                    target.HeritageGroup = HeritageGroup.Viamontian;
                    break;
                case MaterialType.Copper:

                    if (target.WieldSkillType != (int)Skill.MissileDefense)
                        return;

                    // change wield requirement: missile defense -> melee defense
                    target.WieldSkillType = (int)Skill.MeleeDefense;
                    target.ItemSkillLimit = (int)Skill.MeleeDefense;      // recipe requirements check for this field

                    // increase the wield difficulty
                    if (target.WieldDifficulty != null)
                    {
                        target.WieldDifficulty = target.WieldDifficulty switch
                        {
                            // todo: figure out the exact formula for this conversion
                            160 => 200,
                            205 => 250,
                            245 => 300,
                            270 => 325,
                            290 => 350,
                            305 => 370,
                            330 => 400,
                            340 => 410,
                            _ => (int)Math.Round(target.WieldDifficulty.Value * 1.25f)
                        };
                    }
                    break;

                case MaterialType.Silver:

                    if (target.WieldSkillType != (int)Skill.MeleeDefense)
                        return;

                    // change wield requirement: melee defense -> missile defense
                    target.WieldSkillType = (int)Skill.MissileDefense;
                    target.ItemSkillLimit = (int)Skill.MissileDefense;      // recipe requirements check for this field

                    // decrease the wield difficulty
                    if (target.WieldDifficulty != null)
                    {
                        target.WieldDifficulty = target.WieldDifficulty switch
                        {
                            // todo: figure out the exact formula for this conversion
                            200 => 160,
                            250 => 205,
                            300 => 245,
                            325 => 270,
                            350 => 290,
                            370 => 305,
                            400 => 330,
                            410 => 340,
                            _ => (int)Math.Round(target.WieldDifficulty.Value * 0.8f)
                        };
                    }
                    break;

                case MaterialType.Silk:

                    // remove allegiance rank limit, increase item difficulty by spellcraft?
                    target.ItemAllegianceRankLimit = null;
                    target.ItemDifficulty = (target.ItemDifficulty ?? 0) + target.ItemSpellcraft;
                    break;

                // armatures / trinkets
                // these are handled in recipe mod
                case MaterialType.Amber:
                case MaterialType.Diamond:
                case MaterialType.GromnieHide:
                case MaterialType.Pyreal:
                case MaterialType.Ruby:
                case MaterialType.Sapphire:
                    return;

                // magic item tinkering

                case MaterialType.Sunstone:
                    AddImbuedEffect(player, target, ImbuedEffectType.ArmorRending);
                    break;
                case MaterialType.FireOpal:
                    AddImbuedEffect(player, target, ImbuedEffectType.CripplingBlow);
                    break;
                case MaterialType.BlackOpal:
                    AddImbuedEffect(player, target, ImbuedEffectType.CriticalStrike);
                    break;
                case MaterialType.Opal:
                    target.ManaConversionMod += 0.01f;
                    break;
                case MaterialType.GreenGarnet:
                    if (modchance <= 60) // 30% chance to even roll a mod to begin with
                    {
                        // basic roll + 0.4-0.9% dmg
                        if (xtramodroll <= 180) // 33.3%
                        {
                            if (target.GetProperty(PropertyInt.DamageType) == null)
                            {
                                if (splitmodchance >= 1 && splitmodchance <= 26)
                                {
                                    target.SetProperty(PropertyInt.DamageType, 1);
                                    target.SetProperty(PropertyFloat.ElementalDamageMod, 1.00);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It now does bonus Slashing Damage", ChatMessageType.Broadcast));
                                }
                                else if (splitmodchance >= 27 && splitmodchance <= 53)
                                {
                                    target.SetProperty(PropertyInt.DamageType, 2);
                                    target.SetProperty(PropertyFloat.ElementalDamageMod, 1.00);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It now does bonus Piercing Damage", ChatMessageType.Broadcast));
                                }
                                else if (splitmodchance >= 54 && splitmodchance <= 81)
                                {
                                    target.SetProperty(PropertyInt.DamageType, 4);
                                    target.SetProperty(PropertyFloat.ElementalDamageMod, 1.00);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It now does bonus Bludgeon Damage", ChatMessageType.Broadcast));
                                }
                                else if (splitmodchance >= 82 && splitmodchance <= 108)
                                {
                                    target.SetProperty(PropertyInt.DamageType, 8);
                                    target.SetProperty(PropertyFloat.ElementalDamageMod, 1.00);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It now does bonus Cold Damage", ChatMessageType.Broadcast));
                                }
                                else if (splitmodchance >= 109 && splitmodchance <= 136)
                                {
                                    target.SetProperty(PropertyInt.DamageType, 16);
                                    target.SetProperty(PropertyFloat.ElementalDamageMod, 1.00);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It now does bonus Fire Damage", ChatMessageType.Broadcast));
                                }
                                else if (splitmodchance >= 137 && splitmodchance <= 164)
                                {
                                    target.SetProperty(PropertyInt.DamageType, 32);
                                    target.SetProperty(PropertyFloat.ElementalDamageMod, 1.00);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It now does bonus Acid Damage", ChatMessageType.Broadcast));
                                }
                                else if (splitmodchance >= 165 && splitmodchance <= 187)
                                {
                                    target.SetProperty(PropertyInt.DamageType, 64);
                                    target.SetProperty(PropertyFloat.ElementalDamageMod, 1.00);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It now does bonus Lightning Damage", ChatMessageType.Broadcast));
                                }
                            }// turns non elemental wands into elemental type.
                            target.ElementalDamageMod += 0.01f + wanddamage;
                            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained {wanddamage:N3}% extra Elemental damage modifier!", ChatMessageType.Broadcast));
                        }//60%
                        // fail roll
                        else if (xtramodroll >= 181 && xtramodroll <= 190)
                        {
                            target.ElementalDamageMod += 0.01f;
                            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                        }// 3%
                        // choose 1 mag d, melee d, Elemental Dmg.
                        else if (xtramodroll >= 191 && xtramodroll <= 209)
                        {
                            if (resistroll >= 1 && resistroll <= 60) // mag resist
                            {
                                if (target.WeaponMagicDefense == null)
                                {
                                    target.SetProperty(PropertyFloat.WeaponMagicDefense, 1.01);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}, Not bad! You gained bonus Magic Defense {target.WeaponMagicDefense:N2}(+1%)", ChatMessageType.Broadcast));
                                    target.ElementalDamageMod += 0.01f;
                                }
                                else
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    target.WeaponMagicDefense += 0.01;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}, Not bad! You gained bonus Magic Defense {target.WeaponMagicDefense:N2}(+1%)", ChatMessageType.Broadcast));
                                }
                            }// 1% mag d
                            else if (resistroll >= 61 && resistroll <= 120)
                            {
                                target.ElementalDamageMod += 0.01f;
                                target.WeaponDefense += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. You got bonus Melee Defense {target.WeaponDefense:N2}(+1%)", ChatMessageType.Broadcast));
                            }// melee d
                            else if (resistroll >= 121 && resistroll <= 187)
                            {
                                if (target.GetProperty(PropertyInt.DamageType) != null)
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    target.SetProperty(PropertyFloat.ManaConversionMod, 0.10);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. You got bonus Mana Conversion Mod! +10% Mana C.", ChatMessageType.Broadcast));
                                }// 10% mana c
                                else
                                {
                                    if (splitmodchance >= 1 && splitmodchance <= 26)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 1);
                                        target.SetProperty(PropertyFloat.ElementalDamageMod, 1.05);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It has gained +5% to PVE and +2.5 to PVP with Slashing Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 27 && splitmodchance <= 53)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 2);
                                        target.SetProperty(PropertyFloat.ElementalDamageMod, 1.05);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It has gained +5% to PVE and +2.5 to PVP with Piercing Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 54 && splitmodchance <= 81)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 4);
                                        target.SetProperty(PropertyFloat.ElementalDamageMod, 1.05);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It has gained +5% to PVE and +2.5 to PVP with Bludgeon Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 82 && splitmodchance <= 108)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 8);
                                        target.SetProperty(PropertyFloat.ElementalDamageMod, 1.05);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It has gained +5% to PVE and +2.5 to PVP with Cold Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 109 && splitmodchance <= 136)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 16);
                                        target.SetProperty(PropertyFloat.ElementalDamageMod, 1.05);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It has gained +5% to PVE and +2.5 to PVP with Fire Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 137 && splitmodchance <= 164)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 32);
                                        target.SetProperty(PropertyFloat.ElementalDamageMod, 1.05);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It has gained +5% to PVE and +2.5 to PVP with Acid Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 165 && splitmodchance <= 187)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 64);
                                        target.SetProperty(PropertyFloat.ElementalDamageMod, 1.05);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your {target.NameWithMaterial} has just been upgraded into a random elemental type. It has gained +5% to PVE and +2.5 to PVP with Lightning Damage", ChatMessageType.Broadcast));
                                    }
                                }// upgrade to element +5% /2.5%
                            }// Mana C + upgrade to element                          
                        }// 6%
                        // Resistance Cleaving
                        else if (xtramodroll >= 210 && xtramodroll <= 234)
                        {
                            var dmgtype = target.GetProperty(PropertyInt.DamageType);

                            if (dmgtype == 1 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.SlashRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 1);
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You got lucky applying Green Garnet to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Slashing", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 2 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.PierceRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 2);
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You got lucky applying Green Garnet to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Piercing", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 4 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.BludgeonRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 4);
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You got lucky applying Green Garnet to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Bludgeon", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 8 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.ColdRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 8);
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You got lucky applying Green Garnet to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Cold", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 16 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.FireRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 16);
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You got lucky applying Green Garnet to your{target.NameWithMaterial}! The item now has Resistance Cleaving: Fire", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 32 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.AcidRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 32);
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You got lucky applying Green Garnet to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Acid", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 64 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.ElectricRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 64);
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You got lucky applying Green Garnet to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Electric", ChatMessageType.Broadcast));
                            }
                            else if (target.GetProperty(PropertyInt.ResistanceModifierType) != null)
                            {
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                            }
                            else
                            {
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                            }
                        }// 8%
                        // Special Properties BS, CB, ETC
                        else if (xtramodroll == 235) // 2.6%
                        {
                            // biting Strike
                            /*
                            if (resistroll >= 1 && resistroll <= 94 && target.GetProperty(PropertyFloat.CriticalFrequency) == null)
                            {
                                target.ElementalDamageMod += 0.01f;
                                target.SetProperty(PropertyFloat.CriticalFrequency, 0.10); // flat 10%?
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! The item now has Biting Strike!", ChatMessageType.Broadcast));
                            }
                            // Critical Blow
                            else if (resistroll >= 95 && resistroll <= 187 && target.GetProperty(PropertyFloat.CriticalMultiplier) == null)
                            {
                                target.ElementalDamageMod += 0.01f;
                                target.SetProperty(PropertyFloat.CriticalMultiplier, 1.2); // is this really 1.2x? 20%?
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! The item now has Crushing Blow!", ChatMessageType.Broadcast));
                            }
                            else*/
                            //{
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                            //}
                        }// 0.3%
                        // Chance for major/epic/legendary Attributes
                        else if (xtramodroll >= 236 && xtramodroll <= 263) // 13.3%
                        {
                            if (resistroll >= 1 && resistroll <= 30)
                            {
                                if (!target.Biota.SpellIsKnown(2576, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3965, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6107, target.BiotaDatabaseLock))// if no minor add minor
                                {                                    
                                    AddSpell(player, target, SpellId.CANTRIPSTRENGTH2); // add Major Str
                                    target.Biota.TryRemoveKnownSpell(2583, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Strength to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2576, target.BiotaDatabaseLock )) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSTRENGTH3); // Epic Str
                                    target.Biota.TryRemoveKnownSpell(2576, target.BiotaDatabaseLock); // remove major                                    
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Strength upgraded to Epic Strength! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3965, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripStrength4); // Legendary Str
                                    target.Biota.TryRemoveKnownSpell(3965, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Strength upgraded to Legendary Strength!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Strength!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6107, target.BiotaDatabaseLock))
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Strength *done
                            else if (resistroll >= 31 && resistroll <= 61)
                            {
                                if (!target.Biota.SpellIsKnown(2573, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4226, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6104, target.BiotaDatabaseLock))// if no minor add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPENDURANCE2); // major end
                                    target.Biota.TryRemoveKnownSpell(2580, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Endurance to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2573, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPENDURANCE3); // epic end
                                    target.Biota.TryRemoveKnownSpell(2573, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Endurance upgraded to Epic Endurance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4226, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripEndurance4); // leg end
                                    target.Biota.TryRemoveKnownSpell(4226, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Endurance upgraded to Legendary Endurance!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Endurance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6104, target.BiotaDatabaseLock))
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Endurance *done
                            else if (resistroll >= 62 && resistroll <= 92)
                            {
                                if (!target.Biota.SpellIsKnown(2572, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3963, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6103, target.BiotaDatabaseLock))// if no minor add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPCOORDINATION2); // major coord
                                    target.Biota.TryRemoveKnownSpell(2579, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Coordination to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2572, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPCOORDINATION3); // epic coord
                                    target.Biota.TryRemoveKnownSpell(2572, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Coordination upgraded to Epic Coordination!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3963, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripCoordination4); // leg coord
                                    target.Biota.TryRemoveKnownSpell(3963, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Coordination upgraded to Legendary Coordination!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Coordination!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6103, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Coordination *done
                            else if (resistroll >= 93 && resistroll <= 123)
                            {
                                if (!target.Biota.SpellIsKnown(2575, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4019, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6106, target.BiotaDatabaseLock))// if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPQUICKNESS2); // Major quick
                                    target.Biota.TryRemoveKnownSpell(2582, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Quickness to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2575, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPQUICKNESS3); // Epic quick
                                    target.Biota.TryRemoveKnownSpell(2575, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Quickness upgraded to Epic Quickness!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4019, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripQuickness4); // Leg quick
                                    target.Biota.TryRemoveKnownSpell(4019, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Quickness upgraded to Legendary Quickness!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Quickness!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6106, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Quickness *done
                            else if (resistroll >= 124 && resistroll <= 154)
                            {
                                if (!target.Biota.SpellIsKnown(2574, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3964, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6105, target.BiotaDatabaseLock))// if no cantrip add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPFOCUS2); // major foc
                                    target.Biota.TryRemoveKnownSpell(2581, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Focus to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2574, target.BiotaDatabaseLock)) // if minor upgrade to major
                                {
                                    AddSpell(player, target, SpellId.CANTRIPFOCUS3); // epic foc
                                    target.Biota.TryRemoveKnownSpell(2574, target.BiotaDatabaseLock); // remove Major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Focus upgraded to Epic Focus!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3964, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CantripFocus4); // LEG foc
                                    target.Biota.TryRemoveKnownSpell(3964, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Focus upgraded to Legendary Focus!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Focus!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6105, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Focus *done
                            else if (resistroll >= 155 && resistroll <= 187)
                            {
                                if (!target.Biota.SpellIsKnown(2577, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4227, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6101, target.BiotaDatabaseLock))// if no cantrip add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWILLPOWER2); // Major will
                                    target.Biota.TryRemoveKnownSpell(2584, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Willpower to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2577, target.BiotaDatabaseLock)) // if minor upgrade to major
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWILLPOWER3); // EPIC will
                                    target.Biota.TryRemoveKnownSpell(2577, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Willpower upgraded to Epic Willpower!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4227, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CantripWillpower4); // LEG will
                                    target.Biota.TryRemoveKnownSpell(4227, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Willpower upgraded to Legendary Willpower!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Willpower!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6101, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Willpower *done
                        }// 9% attribute cantrips                        
                        else if (xtramodroll >= 264 && xtramodroll <= 273) // 10%
                        {
                            if (target.WeaponSkill == Skill.VoidMagic) // Checks for weapon type.
                            {
                                if (!target.Biota.SpellIsKnown(5428, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(5429, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(6074, target.BiotaDatabaseLock)) // if has no major/epic/leg
                                {
                                    AddSpell(player, target, SpellId.CantripVoidMagicAptitude2); // adds major
                                    target.ElementalDamageMod += 0.01f;
                                    target.Biota.TryRemoveKnownSpell(5427, target.BiotaDatabaseLock); // removes minor
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Void Magic Aptitude to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have added Major Void Magic Aptitude to it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(5428, target.BiotaDatabaseLock)) // if major already exists upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CantripVoidMagicAptitude3); // adds epic
                                    target.ElementalDamageMod += 0.01f;
                                    target.Biota.TryRemoveKnownSpell(5428, target.BiotaDatabaseLock); // removes major
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Void Magic Aptitude upgraded to Epic Void Magic Aptitude!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Major Cantrip, it is now Epic Void Magic Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(5429, target.BiotaDatabaseLock)) // if epic already exists upgrade to legendary
                                {
                                    AddSpell(player, target, SpellId.CantripVoidMagicAptitude4); // adds legendary
                                    target.ElementalDamageMod += 0.01f;
                                    target.Biota.TryRemoveKnownSpell(5429, target.BiotaDatabaseLock); // removes epic
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Void Magic Aptitude upgraded to Legendary Void Magic Aptitude!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Void Magic Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6074, target.BiotaDatabaseLock)) // if Legendary exists throw normal roll
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Void Magic *done

                            if (target.WeaponSkill == Skill.WarMagic) // Checks for weapon type.
                            {
                                if (!target.Biota.SpellIsKnown(2534, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(4715, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(6075, target.BiotaDatabaseLock)) // if has no major/epic/leg
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWARMAGICAPTITUDE2); // adds major
                                    target.ElementalDamageMod += 0.01f;
                                    target.Biota.TryRemoveKnownSpell(2569, target.BiotaDatabaseLock); // removes minor
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major War Magic Aptitude to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have added Major War Magic Aptitude to it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2534, target.BiotaDatabaseLock)) // if major already exists upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWARMAGICAPTITUDE3); // adds epic
                                    target.ElementalDamageMod += 0.01f;
                                    target.Biota.TryRemoveKnownSpell(2534, target.BiotaDatabaseLock); // removes major
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major War Magic Aptitude upgraded to Epic War Magic Aptitude!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Major Cantrip, it is now Epic War Magic Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4715, target.BiotaDatabaseLock)) // if epic already exists upgrade to legendary
                                {
                                    AddSpell(player, target, SpellId.CantripWarMagicAptitude4); // adds legendary
                                    target.ElementalDamageMod += 0.01f;
                                    target.Biota.TryRemoveKnownSpell(4715, target.BiotaDatabaseLock); // removes epic
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic War Magic Aptitude upgraded to Legendary War Magic Aptitude!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary War Magic Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6074, target.BiotaDatabaseLock)) // if Legendary exists throw normal roll
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// War Magic *done

                            if (target.WeaponSkill != Skill.VoidMagic || target.WeaponSkill != Skill.WarMagic)
                            {
                                target.ElementalDamageMod += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                            }// no wields cannot gain skill type cantrips??? -shrug-

                        }// 3% Weapon skill Cantrips                        
                        else if (xtramodroll >= 274 && xtramodroll <= 300) // 9.67%
                        {
                            if (resistroll >= 1 && resistroll <= 31)
                            {
                                if (!target.Biota.SpellIsKnown(3250, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(4670, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(6098, target.BiotaDatabaseLock))// if no major add major and remove minor.
                                {
                                    if (target.Biota.SpellIsKnown(3251, target.BiotaDatabaseLock))
                                    {
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You upgraded the Minor Spirit Thirst to Major Spirit Thirst on your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    }// conditional message if minor was detected
                                    else
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Spirit Thirst to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));

                                    AddSpell(player, target, SpellId.CantripSpiritThirst2); // Major Spirit Thirst
                                    target.Biota.TryRemoveKnownSpell(3251, target.BiotaDatabaseLock); // remove minor if any

                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They added Major Spirit Thirst to it!", ChatMessageType.Broadcast));

                                    target.ElementalDamageMod += 0.01f;
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3250, target.BiotaDatabaseLock)) // if major upgrade to Epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSPIRITTHIRST3); // Epic ST
                                    target.Biota.TryRemoveKnownSpell(3250, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Spirit Thirst upgraded to Epic Spirit Thirst!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They added Epic Spirit Thirst to it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4670, target.BiotaDatabaseLock)) // if Epic upgrade to Legendary
                                {
                                    AddSpell(player, target, SpellId.CantripSpiritThirst4); // Legendary st
                                    target.Biota.TryRemoveKnownSpell(4670, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Spirit Thirst upgraded to Legendary Spirit Thirst!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Spirit Thirst!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6098, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// spirit Thirst *done
                            else if (resistroll >= 32 && resistroll <= 62)
                            {
                                if (!target.Biota.SpellIsKnown(3200, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6086, target.BiotaDatabaseLock)
                                     && !target.Biota.SpellIsKnown(6087, target.BiotaDatabaseLock))// if no major add major and remove minor.
                                {
                                    if (target.Biota.SpellIsKnown(3199, target.BiotaDatabaseLock))
                                    {
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You upgraded the Minor Hermetic Link to Major Hermetic Link on your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    }// conditional message if minor was detected
                                    else
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Hermetic Link to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));

                                    AddSpell(player, target, SpellId.CantripHermeticLink2); // Major Hermetic Link
                                    target.Biota.TryRemoveKnownSpell(3199, target.BiotaDatabaseLock); // remove minor if any

                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They added Major Hermetic Link to it!", ChatMessageType.Broadcast));

                                    target.ElementalDamageMod += 0.01f;
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3200, target.BiotaDatabaseLock)) // if major upgrade to Epic
                                {
                                    AddSpell(player, target, SpellId.CantripHermeticLink3); // Epic HL
                                    target.Biota.TryRemoveKnownSpell(3200, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Hermetic Link upgraded to Epic Hermetic Link!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They added Epic Hermetic Link to it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6086, target.BiotaDatabaseLock)) // if Epic upgrade to Legendary
                                {
                                    AddSpell(player, target, SpellId.CantripHermeticLink4); // Legendary HL
                                    target.Biota.TryRemoveKnownSpell(6086, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Spirit Thirst upgraded to Legendary Hermetic Link!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Hermetic Link!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6087, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Hermetic Link *done                            
                            else if (resistroll >= 63 && resistroll <= 93)
                            {
                                if (!target.Biota.SpellIsKnown(2588, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4663, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6091, target.BiotaDatabaseLock))// if minor or nothing add or upgrade to major
                                {
                                    if (target.Biota.SpellIsKnown(2600, target.BiotaDatabaseLock))
                                    {
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You upgraded the Minor Defender to Major Defender on your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    }// conditional message if minor was detected
                                    else
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Defender to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));

                                    AddSpell(player, target, SpellId.CANTRIPDEFENDER2); // Major Def
                                    target.Biota.TryRemoveKnownSpell(2600, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They added Major Defender to it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2588, target.BiotaDatabaseLock)) // if Major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPDEFENDER3); // epic Def
                                    target.Biota.TryRemoveKnownSpell(2588, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Defender upgraded to Epic Defender!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They upgraded Major Defender to Epic Defender on it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4663, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripDefender4); // Leg def
                                    target.Biota.TryRemoveKnownSpell(4663, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Defender upgraded to Legendary Defender!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They upgraded Epic Defender to Legendary Defender on it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6091, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Defender *done
                            else if (resistroll >= 94 && resistroll <= 124)
                            {
                                if (!target.Biota.SpellIsKnown(2524, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4704, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6063, target.BiotaDatabaseLock))// if no minor add minor
                                {
                                    if (target.Biota.SpellIsKnown(2559, target.BiotaDatabaseLock))
                                    {
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You upgraded the Minor Magic Resistance to Major Magic Resistance on your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    }// conditional message if minor was detected
                                    else
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Magic Resistance to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));


                                    AddSpell(player, target, SpellId.CANTRIPMAGICRESISTANCE2); // Major Mag D
                                    target.Biota.TryRemoveKnownSpell(2559, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have added Major Magic Resistance to it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;

                                }
                                else if (target.Biota.SpellIsKnown(2524, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPMAGICRESISTANCE3); // epic Mag D
                                    target.Biota.TryRemoveKnownSpell(2524, target.BiotaDatabaseLock); // remove major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Magic Resistance upgraded to Epic Magic Resistance!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Major Cantrip, it is now Epic Magic Resistance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4704, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CantripMagicResistance4); // Leg Mag D
                                    target.Biota.TryRemoveKnownSpell(4704, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Magic Resistance upgraded to Legendary Magic Resistance!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Magic Resistance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6063, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Magic Resistance *done
                            else if (resistroll >= 125 && resistroll <= 187)
                            {
                                if (!target.Biota.SpellIsKnown(2515, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4696, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6055, target.BiotaDatabaseLock))// if no minor add minor
                                {
                                    if (target.Biota.SpellIsKnown(2550, target.BiotaDatabaseLock))
                                    {
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You upgraded Minor Invulnerability to Major Invulnerability to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    }// conditional message if minor was detected
                                    else
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Invulnerability to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));


                                    AddSpell(player, target, SpellId.CANTRIPINVULNERABILITY2); // Major Invul
                                    target.Biota.TryRemoveKnownSpell(2550, target.BiotaDatabaseLock); // remove minor
                                    target.ElementalDamageMod += 0.01f;
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have added Major Invulnerability to it!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2515, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPINVULNERABILITY3); // Epic Invul
                                    target.Biota.TryRemoveKnownSpell(2515, target.BiotaDatabaseLock); // remove Major
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Invulnerability upgraded to Epic Invulnerability!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Major Cantrip, it is now Epic Invulnerability!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4696, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripInvulnerability4); // Leg Invul
                                    target.Biota.TryRemoveKnownSpell(4696, target.BiotaDatabaseLock); // remove epic
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Invulnerability upgraded to Legendary Invulnerability!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Green Garnet to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Invulnerability!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6055, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.ElementalDamageMod += 0.01f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                                }
                            }// Invulnerability *done

                        }// 8.6% weapon cantrips ST/HL/Defender/Invul/Magres
                    }
                    else
                    {
                        target.ElementalDamageMod += 0.01f;
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time.", ChatMessageType.Broadcast));
                    }
                    break;

                // these are handled in recipe mods already

                case MaterialType.SmokeyQuartz:
                    //AddSpell(player, target, SpellId.CANTRIPCOORDINATION1);
                    break;
                case MaterialType.RoseQuartz:
                    //AddSpell(player, target, SpellId.CANTRIPQUICKNESS1);
                    break;
                case MaterialType.RedJade:
                    //AddSpell(player, target, SpellId.CANTRIPHEALTHGAIN1);
                    break;
                case MaterialType.Malachite:
                    //AddSpell(player, target, SpellId.WarriorsVigor);
                    break;
                case MaterialType.LavenderJade:
                    //AddSpell(player, target, SpellId.CANTRIPMANAGAIN1);
                    break;
                case MaterialType.LapisLazuli:
                    //AddSpell(player, target, SpellId.CANTRIPWILLPOWER1);
                    break;
                case MaterialType.Hematite:
                    //AddSpell(player, target, SpellId.WarriorsVitality);
                    break;
                case MaterialType.Citrine:
                    //AddSpell(player, target, SpellId.CANTRIPSTAMINAGAIN1);
                    break;
                case MaterialType.Carnelian:
                    //AddSpell(player, target, SpellId.CANTRIPSTRENGTH1);
                    break;
                case MaterialType.Bloodstone:
                    //AddSpell(player, target, SpellId.CANTRIPENDURANCE1);
                    break;
                case MaterialType.Azurite:
                    //AddSpell(player, target, SpellId.WizardsIntellect);
                    break;
                case MaterialType.Agate:
                    //AddSpell(player, target, SpellId.CANTRIPFOCUS1);
                    break;

                // weapon tinkering

                case MaterialType.Iron:
                    if (modchance <= 70) // 38% chance to even roll a mod to begin with
                    {
                        // basic roll + 1-4 flat dmg
                        if (xtramodroll <= 180) // 33.3%
                        {
                            string variancenote = null;
                            if (target.Damage >= 80)
                            {
                                target.DamageVariance += 0.05;
                                variancenote = " You've also gained 5% variance.";
                                if (target.DamageVariance >= 1.0)
                                {
                                    target.DamageVariance = 0.9;
                                    variancenote = " Your weapon has reached maximum variance.";
                                }
                            }

                            var dmgresult = 3 + meleedmg;
                            target.Damage += 3 + meleedmg;
                            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained {meleedmg} extra Flat damage! New Target Damage {target.Damage}(+{dmgresult}){variancenote}", ChatMessageType.Broadcast));

                        }// 60%
                        else if (xtramodroll >= 181 && xtramodroll <= 190)
                        {
                            target.Damage += 3;
                            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                        }// 3%
                        // choose 1 mag d, melee d, attack mod.
                        else if (xtramodroll >= 191 && xtramodroll <= 209)
                        {
                            if (resistroll >= 1 && resistroll <= 60) // mag resist
                            {
                                target.Damage += 3;
                                target.WeaponMagicDefense += .05;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}, Not bad! You gained bonus Magic Defense {target.WeaponMagicDefense:N2}(+5%)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 61 && resistroll <= 120) // melee d
                            {
                                target.Damage += 3;
                                target.WeaponDefense += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. You got bonus Melee Defense {target.WeaponDefense:N2}(+1%)", ChatMessageType.Broadcast));
                            }
                            if (resistroll >= 121 && resistroll <= 187) // attack mod
                            {
                                target.Damage += 3;
                                target.WeaponOffense += 0.05f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. You got bonus Attack Mod {target.WeaponOffense:N2}(+5%)", ChatMessageType.Broadcast));
                            }
                        }// 6%
                        // Resistance Cleaving
                        else if (xtramodroll >= 210 && xtramodroll <= 234)
                        {
                            var dmgtype = target.GetProperty(PropertyInt.DamageType);

                            if (dmgtype == 1 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.SlashRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 1);
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Slashing", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 2 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.PierceRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 2);
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Piercing", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 3 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.PierceRending) && !target.HasImbuedEffect(ImbuedEffectType.SlashRending))
                            {
                                var halfchance = ThreadSafeRandom.Next(1, 100);
                                if (halfchance >= 1 && halfchance <= 50)
                                {
                                    target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                    target.SetProperty(PropertyInt.ResistanceModifierType, 1);
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Slashing", ChatMessageType.Broadcast));
                                }
                                else if (halfchance >= 51 && halfchance <= 100)
                                {
                                    target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                    target.SetProperty(PropertyInt.ResistanceModifierType, 2);
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Piercing", ChatMessageType.Broadcast));
                                }
                            }
                            else if (dmgtype == 4 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.BludgeonRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 4);
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Bludgeon", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 8 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.ColdRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 8);
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Cold", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 16 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.FireRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 16);
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Fire", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 32 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.AcidRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 32);
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Acid", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 64 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.ElectricRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 64);
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying iron to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Electric", ChatMessageType.Broadcast));
                            }
                            else if (target.GetProperty(PropertyInt.ResistanceModifierType) != null)
                            {
                                string variancenote = null;
                                if (target.Damage >= 80)
                                {
                                    target.DamageVariance += 0.05;
                                    variancenote = " You've also gained 5% variance.";
                                    if (target.DamageVariance >= 1.0)
                                    {
                                        target.DamageVariance = 0.9;
                                        variancenote = " Your weapon has reached maximum variance.";
                                    }
                                }
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                            }
                            else
                            {
                                string variancenote = null;
                                if (target.Damage >= 80)
                                {
                                    target.DamageVariance += 0.05;
                                    variancenote = " You've also gained 5% variance.";
                                    if (target.DamageVariance >= 1.0)
                                    {
                                        target.DamageVariance = 0.9;
                                        variancenote = " Your weapon has reached maximum variance.";
                                    }
                                }
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                            }
                        }//8%
                        // Special Properties Armor Cleave, BS, CB, ETC
                        else if (xtramodroll == 235) // 2.6%
                        {
                            /*
                            int procs = 0;
                            if (target.GetProperty(PropertyBool.IgnoreMagicArmor) != null)
                                procs++;
                            if (target.GetProperty(PropertyBool.IgnoreMagicResist) != null)
                                procs++;
                            if (target.GetProperty(PropertyFloat.CriticalFrequency) != null)
                                procs++;
                            if (target.GetProperty(PropertyFloat.CriticalMultiplier) != null)
                                procs++;
                            if (target.GetProperty(PropertyInt.Cleaving) != null)
                                procs++;
                            if (target.GetProperty(PropertyInt.AttackType) < 32 && target.WeaponSkill != Skill.TwoHandedCombat)
                                procs++;

                            //hollow properties
                            if (resistroll >= 1 && resistroll <= 30 && target.GetProperty(PropertyBool.IgnoreMagicArmor) == null)
                            {
                                target.Damage += 3;
                                target.SetProperty(PropertyBool.IgnoreMagicArmor, true);
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! The item now ignores Partial armor values!", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 31 && resistroll <= 61 && target.GetProperty(PropertyBool.IgnoreMagicResist) == null)
                            {
                                target.Damage += 3;
                                target.SetProperty(PropertyBool.IgnoreMagicResist, true);
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! The item now has ignores partial magical protections!", ChatMessageType.Broadcast));
                            }
                            // biting Strike
                            if (resistroll >= 62 && resistroll <= 92 && target.GetProperty(PropertyFloat.CriticalFrequency) == null)
                            {
                                target.Damage += 3;
                                target.SetProperty(PropertyFloat.CriticalFrequency, 0.13);
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! The item now has Biting Strike!", ChatMessageType.Broadcast));
                            }
                            // Critical Blow
                            else if (resistroll >= 93 && resistroll <= 123 && target.GetProperty(PropertyFloat.CriticalMultiplier) == null)
                            {
                                target.Damage += 3;
                                target.SetProperty(PropertyFloat.CriticalMultiplier, 3);
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! The item now has Crushing Blow!", ChatMessageType.Broadcast));
                            }*/
                            // cleaving
                            if (resistroll >= 124 && resistroll <= 154 && target.GetProperty(PropertyInt.Cleaving) < 2 && target.WeaponSkill != Skill.TwoHandedCombat)
                            {
                                target.SetProperty(PropertyInt.Cleaving, 2);
                                target.Damage += 3;
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! The item now has Cleaving!", ChatMessageType.Broadcast));
                            }
                            // Multistrike
                            /*
                            else if (resistroll >= 155 && resistroll <= 187 && target.GetProperty(PropertyInt.AttackType) < 32 && target.WeaponSkill != Skill.TwoHandedCombat)
                            {
                                target.SetProperty(PropertyInt.AttackType, 32);
                                target.Damage += 3;
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! The item now has Multistrike - Double Slash", ChatMessageType.Broadcast));
                            } */                           
                            else
                            {
                                string variancenote = null;
                                if (target.Damage >= 80)
                                {
                                    target.DamageVariance += 0.05;
                                    variancenote = " You've also gained 5% variance.";
                                    if (target.DamageVariance >= 1.0)
                                    {
                                        target.DamageVariance = 0.9;
                                        variancenote = " Your weapon has reached maximum variance.";
                                    }
                                }
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                            }
                        }// 0.3%
                        // Chance for minor/major/epic/legendary Attributes
                        else if (xtramodroll >= 236 && xtramodroll <= 263) // 13.3%
                        {
                            if (resistroll >= 1 && resistroll <= 30)
                            {
                                if (!target.Biota.SpellIsKnown(2576, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3965, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6107, target.BiotaDatabaseLock))// if no minor add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSTRENGTH2); // add Major Str
                                    target.Biota.TryRemoveKnownSpell(2583, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Strength to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2576, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSTRENGTH3); // Epic Str
                                    target.Biota.TryRemoveKnownSpell(2576, target.BiotaDatabaseLock); // remove major                                    
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Strength upgraded to Epic Strength! New Bonus Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3965, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripStrength4); // Legendary Str
                                    target.Biota.TryRemoveKnownSpell(3965, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Strength upgraded to Legendary Strength!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Strength!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6107, target.BiotaDatabaseLock))
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Strength *done
                            else if (resistroll >= 31 && resistroll <= 61)
                            {
                                if (!target.Biota.SpellIsKnown(2573, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4226, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6104, target.BiotaDatabaseLock))// if no minor add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPENDURANCE2); // major end
                                    target.Biota.TryRemoveKnownSpell(2580, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Endurance to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2573, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPENDURANCE3); // epic end
                                    target.Biota.TryRemoveKnownSpell(2573, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Endurance upgraded to Epic Endurance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4226, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripEndurance4); // leg end
                                    target.Biota.TryRemoveKnownSpell(4226, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Endurance upgraded to Legendary Endurance!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Endurance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6104, target.BiotaDatabaseLock))
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Endurance *done
                            else if (resistroll >= 62 && resistroll <= 92)
                            {
                                if (!target.Biota.SpellIsKnown(2572, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3963, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6103, target.BiotaDatabaseLock))// if no minor add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPCOORDINATION2); // major coord
                                    target.Biota.TryRemoveKnownSpell(2579, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Coordination to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2572, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPCOORDINATION3); // epic coord
                                    target.Biota.TryRemoveKnownSpell(2572, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Coordination upgraded to Epic Coordination!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3963, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripCoordination4); // leg coord
                                    target.Biota.TryRemoveKnownSpell(3963, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Coordination upgraded to Legendary Coordination!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Coordination!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6103, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Coordination *done
                            else if (resistroll >= 93 && resistroll <= 123)
                            {
                                if (!target.Biota.SpellIsKnown(2575, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4019, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6106, target.BiotaDatabaseLock))// if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPQUICKNESS2); // Major quick
                                    target.Biota.TryRemoveKnownSpell(2582, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Quickness to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2575, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPQUICKNESS3); // Epic quick
                                    target.Biota.TryRemoveKnownSpell(2575, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Quickness upgraded to Epic Quickness!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4019, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripQuickness4); // Leg quick
                                    target.Biota.TryRemoveKnownSpell(4019, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Quickness upgraded to Legendary Quickness!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Quickness!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6106, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Quickness *done
                            else if (resistroll >= 124 && resistroll <= 154)
                            {
                                if (!target.Biota.SpellIsKnown(2574, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3964, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6105, target.BiotaDatabaseLock))// if no cantrip add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPFOCUS2); // major foc
                                    target.Biota.TryRemoveKnownSpell(2581, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Focus to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2574, target.BiotaDatabaseLock)) // if minor upgrade to major
                                {
                                    AddSpell(player, target, SpellId.CANTRIPFOCUS3); // epic foc
                                    target.Biota.TryRemoveKnownSpell(2574, target.BiotaDatabaseLock); // remove Major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Focus upgraded to Epic Focus!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3964, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CantripFocus4); // LEG foc
                                    target.Biota.TryRemoveKnownSpell(3964, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Focus upgraded to Legendary Focus!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Focus!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6105, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Focus *done
                            else if (resistroll >= 155 && resistroll <= 187)
                            {
                                if (!target.Biota.SpellIsKnown(2577, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4227, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6101, target.BiotaDatabaseLock))// if no cantrip add minor
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWILLPOWER2); // Major will
                                    target.Biota.TryRemoveKnownSpell(2584, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not bad! You added Major Willpower to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2577, target.BiotaDatabaseLock)) // if minor upgrade to major
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWILLPOWER3); // EPIC will
                                    target.Biota.TryRemoveKnownSpell(2577, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Willpower upgraded to Epic Willpower!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4227, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CantripWillpower4); // LEG will
                                    target.Biota.TryRemoveKnownSpell(4227, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Willpower upgraded to Legendary Willpower!", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Willpower!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6101, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Willpower *done
                        }// 9%
                        // Break into Weapon type cantrips
                        else if (xtramodroll >= 264 && xtramodroll <= 273) // 10%
                        {
                            if (target.WeaponSkill == Skill.LightWeapons) // Checks for weapon type.
                            {
                                if (!target.Biota.SpellIsKnown(2530, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(4711, target.BiotaDatabaseLock)
                                       && !target.Biota.SpellIsKnown(6043, target.BiotaDatabaseLock)) // if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSTAFFAPTITUDE2); // adds major
                                    target.Biota.TryRemoveKnownSpell(2565, target.BiotaDatabaseLock); // removes minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Light Weapons Aptitude to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2530, target.BiotaDatabaseLock)) // if major already exists upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSTAFFAPTITUDE3); // adds epic
                                    target.Biota.TryRemoveKnownSpell(2530, target.BiotaDatabaseLock); // removes major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Light Weapons Aptitude upgraded to Epic Light Weapons Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4711, target.BiotaDatabaseLock)) // if epic already exists upgrade to legendary
                                {
                                    AddSpell(player, target, SpellId.CantripAxeAptitude4); // adds legendary
                                    target.Biota.TryRemoveKnownSpell(4711, target.BiotaDatabaseLock); // removes epic
                                    target.Damage += 3;
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Light Weapons Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6043, target.BiotaDatabaseLock)) // if Legendary exists throw normal roll
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }

                            }// Light Weapons *done
                            else if (target.WeaponSkill == Skill.HeavyWeapons) // Checks for weapon type.
                            {
                                if (!target.Biota.SpellIsKnown(2531, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(4712, target.BiotaDatabaseLock)
                                       && !target.Biota.SpellIsKnown(6072, target.BiotaDatabaseLock)) // if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSWORDAPTITUDE2); // adds major
                                    target.Biota.TryRemoveKnownSpell(2566, target.BiotaDatabaseLock); // removes minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Heavy Weapons Aptitude to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2531, target.BiotaDatabaseLock)) // if major already exists upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSWORDAPTITUDE3); // adds epic
                                    target.Biota.TryRemoveKnownSpell(2531, target.BiotaDatabaseLock); // removes major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Heavy Weapons Aptitude upgraded to Epic Heavy Weapons Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4712, target.BiotaDatabaseLock)) // if epic already exists upgrade to legendary
                                {
                                    AddSpell(player, target, SpellId.CantripSwordAptitude4); // adds legendary
                                    target.Biota.TryRemoveKnownSpell(4712, target.BiotaDatabaseLock); // removes epic
                                    target.Damage += 3;
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Heavy Weapons Aptitude to Legendary Heavy Weapons Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6072, target.BiotaDatabaseLock)) // if Legendary exists throw normal roll
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Heavy Weapons *done
                            else if (target.WeaponSkill == Skill.FinesseWeapons) // Checks for weapon type.
                            {
                                if (!target.Biota.SpellIsKnown(2509, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(4691, target.BiotaDatabaseLock)
                                       && !target.Biota.SpellIsKnown(6047, target.BiotaDatabaseLock)) // if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPDAGGERAPTITUDE2); // adds major
                                    target.Biota.TryRemoveKnownSpell(2544, target.BiotaDatabaseLock); // removes minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Finesse Weapons Aptitude to your {target.NameWithMaterial}!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2509, target.BiotaDatabaseLock)) // if major already exists upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPDAGGERAPTITUDE3); // adds epic
                                    target.Biota.TryRemoveKnownSpell(2509, target.BiotaDatabaseLock); // removes major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Finesse Weapons Aptitude upgraded to Epic Finesse Weapons Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4691, target.BiotaDatabaseLock)) // if epic already exists upgrade to legendary
                                {
                                    AddSpell(player, target, SpellId.CantripDaggerAptitude4); // adds legendary
                                    target.Biota.TryRemoveKnownSpell(4691, target.BiotaDatabaseLock); // removes epic
                                    target.Damage += 3;
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Finesse Weapons Aptitude to Legendary Finesse Weapons Aptitude!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6047, target.BiotaDatabaseLock)) // if Legendary exists throw normal roll
                                {
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                }
                            }// Finesse Weapons *done
                            else
                            {
                                string variancenote = null;
                                if (target.Damage >= 80)
                                {
                                    target.DamageVariance += 0.05;
                                    variancenote = " You've also gained 5% variance.";
                                    if (target.DamageVariance >= 1.0)
                                    {
                                        target.DamageVariance = 0.9;
                                        variancenote = " Your weapon has reached maximum variance.";
                                    }
                                }
                                target.Damage += 3;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                            }

                        }// 3%
                        else if (xtramodroll >= 274 && xtramodroll <= 300) // 9.67%
                        {
                            if (resistroll >= 1 && resistroll <= 30)
                            {
                                if (!target.Biota.SpellIsKnown(2586, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4661, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6089, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPBLOODTHIRST2); // major bt
                                    target.Biota.TryRemoveKnownSpell(2598, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Blood Thirst to your {target.NameWithMaterial}! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2586, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPBLOODTHIRST3); // epic bt
                                    target.Biota.TryRemoveKnownSpell(2586, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Blood Thirst upgraded to Epic Blood Thirst! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4661, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripBloodThirst4); // leg bt
                                    target.Biota.TryRemoveKnownSpell(4661, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Blood Thirst upgraded to Legendary Blood Thirst! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Blood Thirst!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6089, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Blood Thirst *done
                            else if (resistroll >= 31 && resistroll <= 61)
                            {
                                if (!target.Biota.SpellIsKnown(2596, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4672, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6100, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSWIFTHUNTER2); // Major SH
                                    target.Biota.TryRemoveKnownSpell(2608, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Swift Hunter to your {target.NameWithMaterial}! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2596, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSWIFTHUNTER3); // Epic SH
                                    target.Biota.TryRemoveKnownSpell(2596, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Swift Hunter upgraded to Epic Swift Hunter! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4672, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripSwiftHunter4); // Leg SH
                                    target.Biota.TryRemoveKnownSpell(4672, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Swift Hunter upgraded to Legendary Swift Hunter! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Swift Hunter!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6100, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Swift Hunter *done
                            else if (resistroll >= 62 && resistroll <= 92)
                            {
                                if (!target.Biota.SpellIsKnown(2603, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(2591, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4666, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6094, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPHEARTTHIRST2); // Major SH
                                    target.Biota.TryRemoveKnownSpell(2603, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Heart Thirst to your {target.NameWithMaterial}! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2591, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPHEARTTHIRST3); // Epic SH
                                    target.Biota.TryRemoveKnownSpell(2591, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Heart Thirst upgraded to Epic Heart Thirst! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4666, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripHeartThirst4); // Leg SH
                                    target.Biota.TryRemoveKnownSpell(4666, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Heart Thirst upgraded to Legendary Heart Thirst! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Heart Thirst!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6094, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Heart Thirst *done
                            else if (resistroll >= 93 && resistroll <= 123)
                            {
                                if (!target.Biota.SpellIsKnown(2588, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4663, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6091, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPDEFENDER2); // Major Def
                                    target.Biota.TryRemoveKnownSpell(2600, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Defender to your {target.NameWithMaterial}! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2588, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPDEFENDER3); // Epic Def
                                    target.Biota.TryRemoveKnownSpell(2588, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Defender upgraded to Epic Defender! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4663, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripDefender4); // Leg Def
                                    target.Biota.TryRemoveKnownSpell(4663, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Defender upgraded to Legendary Defender! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Defender!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6091, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Defender *done
                            else if (resistroll >= 124 && resistroll <= 154)
                            {
                                if (!target.Biota.SpellIsKnown(2559, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(2524, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4704, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6063, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPMAGICRESISTANCE2); // Major Mag D
                                    target.Biota.TryRemoveKnownSpell(2559, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Magic Resistance to your {target.NameWithMaterial}! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2524, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPMAGICRESISTANCE3); // Epic Mag D
                                    target.Biota.TryRemoveKnownSpell(2524, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Magic Resistance upgraded to Epic Magic Resistance! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4704, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripMagicResistance4); // Leg Mag D
                                    target.Biota.TryRemoveKnownSpell(4704, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Magic Resistance upgraded to Legendary Magic Resistance! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Magic Resistance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6063, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Magic Resistance *done
                            else if (resistroll >= 155 && resistroll <= 187)
                            {
                                if (!target.Biota.SpellIsKnown(2515, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4696, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6055, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPINVULNERABILITY2); // Major Invul
                                    target.Biota.TryRemoveKnownSpell(2550, target.BiotaDatabaseLock); // remove minor
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Invulnerability to your {target.NameWithMaterial}! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2515, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPINVULNERABILITY3); // Epic Invul
                                    target.Biota.TryRemoveKnownSpell(2515, target.BiotaDatabaseLock); // remove major
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Invulnerability upgraded to Epic Invulnerability! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4696, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripInvulnerability4); // Leg Invul
                                    target.Biota.TryRemoveKnownSpell(4696, target.BiotaDatabaseLock); // remove epic
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Invulnerability upgraded to Legendary Invulnerability! New Target Damage {target.Damage}(+3)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying iron to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Invulnerability!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6055, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    string variancenote = null;
                                    if (target.Damage >= 80)
                                    {
                                        target.DamageVariance += 0.05;
                                        variancenote = " You've also gained 5% variance.";
                                        if (target.DamageVariance >= 1.0)
                                        {
                                            target.DamageVariance = 0.9;
                                            variancenote = " Your weapon has reached maximum variance.";
                                        }
                                    }
                                    target.Damage += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                                }
                            }// Invulnerability *done

                        }// 8.6%
                    }
                    else
                    {
                        string variancenote = null;
                        if (target.Damage >= 80)
                        {
                            target.DamageVariance += 0.05;
                            variancenote = " You've also gained 5% variance.";
                            if (target.DamageVariance >= 1.0)
                            {
                                target.DamageVariance = 0.9;
                                variancenote = " Your weapon has reached maximum variance.";
                            }
                        }
                        target.Damage += 3;
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+3){variancenote}", ChatMessageType.Broadcast));
                    }
                    break;
                case MaterialType.Mahogany:
                    if (modchance <= 60) // 30% chance to even roll a mod to begin with
                    {
                        // basic roll + 1-4 flat dmg
                        if (xtramodroll <= 190) // 33.3%
                        {
                            var dmgresult = 0.04f + bowmoddmg;
                            target.DamageMod += 0.04f + bowmoddmg;
                            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Not so lucky, but you also gained {bowmoddmg:N2} extra Bow Damage Modifier! New Target Damage Mod {target.DamageMod:N2}(+{dmgresult:N2})", ChatMessageType.Broadcast));
                        }// 60%
                        // choose 1 mag d, melee d, Elemental Dmg.
                        else if (xtramodroll >= 191 && xtramodroll <= 209)
                        {
                            if (resistroll >= 1 && resistroll <= 60) // mag resist
                            {
                                if (target.WeaponMagicDefense == null)
                                {
                                    target.SetProperty(PropertyFloat.WeaponMagicDefense, 1.01);
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}, Not bad! You gained bonus Magic Defense {target.WeaponMagicDefense:N2}(+1%)", ChatMessageType.Broadcast));
                                    target.DamageMod += 0.04f;
                                }
                                else
                                {
                                    target.DamageMod += 0.04f;
                                    target.WeaponMagicDefense += 0.01;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}, Not bad! You gained bonus Magic Defense {target.WeaponMagicDefense:N2}(+1%)", ChatMessageType.Broadcast));
                                }
                            }
                            else if (resistroll >= 61 && resistroll <= 120) // melee d
                            {
                                target.DamageMod += 0.04f;
                                target.WeaponDefense += 0.01f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. You got bonus Melee Defense {target.WeaponDefense:N2}(+1%)", ChatMessageType.Broadcast));
                            }
                            else if (resistroll >= 121 && resistroll <= 187) // elemental bonus + upgrade to
                            {
                                if (target.GetProperty(PropertyInt.DamageType) != null)
                                {
                                    target.DamageMod += 0.04f;
                                    target.ElementalDamageBonus += 3;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. You got bonus Elemental Damage {target.ElementalDamageBonus:N0}(+3)", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    if (splitmodchance >= 1 && splitmodchance <= 26)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 1);
                                        target.SetProperty(PropertyInt.ElementalDamageBonus, 2);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your missile weapon has just been upgraded into a random elemental weapon. It has gained +2 Slashing Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 27 && splitmodchance <= 53)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 2);
                                        target.SetProperty(PropertyInt.ElementalDamageBonus, 2);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your missile weapon has just been upgraded into a random elemental weapon. It has gained +2 Piercing Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 54 && splitmodchance <= 81)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 4);
                                        target.SetProperty(PropertyInt.ElementalDamageBonus, 2);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your missile weapon has just been upgraded into a random elemental weapon. It has gained +2 Bludgeon Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 82 && splitmodchance <= 108)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 8);
                                        target.SetProperty(PropertyInt.ElementalDamageBonus, 2);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your missile weapon has just been upgraded into a random elemental weapon. It has gained +2 Cold Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 109 && splitmodchance <= 136)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 16);
                                        target.SetProperty(PropertyInt.ElementalDamageBonus, 2);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your missile weapon has just been upgraded into a random elemental weapon. It has gained +2 Fire Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 137 && splitmodchance <= 164)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 32);
                                        target.SetProperty(PropertyInt.ElementalDamageBonus, 2);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your missile weapon has just been upgraded into a random elemental weapon. It has gained +2 Acid Damage", ChatMessageType.Broadcast));
                                    }
                                    else if (splitmodchance >= 165 && splitmodchance <= 187)
                                    {
                                        target.SetProperty(PropertyInt.DamageType, 64);
                                        target.SetProperty(PropertyInt.ElementalDamageBonus, 2);
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You rolled {xtramodroll}. Your missile weapon has just been upgraded into a random elemental weapon. It has gained +2 Lightning Damage", ChatMessageType.Broadcast));
                                    }
                                    else
                                    {
                                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"BUG 1", ChatMessageType.Broadcast));
                                    }
                                }
                            }
                            else
                            {
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"BUG 3", ChatMessageType.Broadcast));
                            }
                        }// 6%
                        // Resistance Cleaving
                        else if (xtramodroll >= 210 && xtramodroll <= 234)
                        {
                            var dmgtype = target.GetProperty(PropertyInt.DamageType);

                            if (dmgtype == 1 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.SlashRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 1);
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Slashing", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 2 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.PierceRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 2);
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Piercing", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 3 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.SlashRending))
                            {
                                var halfchance = ThreadSafeRandom.Next(1, 100);
                                if (halfchance >= 1 && halfchance <= 50)
                                {
                                    target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                    target.SetProperty(PropertyInt.ResistanceModifierType, 1);
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Slashing", ChatMessageType.Broadcast));
                                }
                                else if (halfchance >= 51 && halfchance <= 100)
                                {
                                    target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                    target.SetProperty(PropertyInt.ResistanceModifierType, 2);
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Piercing", ChatMessageType.Broadcast));
                                }
                            }
                            else if (dmgtype == 4 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.BludgeonRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 4);
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Bludgeon", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 8 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.ColdRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 8);
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Cold", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 16 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.FireRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 16);
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Fire", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 32 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.AcidRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 32);
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Acid", ChatMessageType.Broadcast));
                            }
                            else if (dmgtype == 64 && target.GetProperty(PropertyInt.ResistanceModifierType) == null && !target.HasImbuedEffect(ImbuedEffectType.ElectricRending))
                            {
                                target.SetProperty(PropertyFloat.ResistanceModifier, 1);
                                target.SetProperty(PropertyInt.ResistanceModifierType, 64);
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[TINKERING] You just got lucky applying Mahogany to your {target.NameWithMaterial}! The item now has Resistance Cleaving: Electric", ChatMessageType.Broadcast));
                            }
                            else if (target.GetProperty(PropertyInt.ResistanceModifierType) != null)
                            {
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));

                            }
                            else
                            {
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                            }
                        }// 8%
                        // Special Properties Armor Cleave, BS, CB, ETC
                        else if (xtramodroll == 235) // 2.6%
                        {
                            //Armor rend
                            /*if (resistroll >= 1 && resistroll <= 61 && target.GetProperty(PropertyFloat.IgnoreArmor) != 1)
                            {
                                target.DamageMod += 0.04f;
                                target.SetProperty(PropertyFloat.IgnoreArmor, 1);
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogony to their {target.NameWithMaterial}! The item now has Armor Cleaving!", ChatMessageType.Broadcast));
                            }
                            // biting Strike
                            else if (resistroll >= 62 && resistroll <= 124 && target.GetProperty(PropertyFloat.CriticalFrequency) == null)
                            {
                                target.DamageMod += 0.04f;
                                target.SetProperty(PropertyFloat.CriticalFrequency, 0.15);
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogony to their {target.NameWithMaterial}! The item now has Biting Strike!", ChatMessageType.Broadcast));
                            }
                            // Critical Blow
                            else if (resistroll >= 125 && resistroll <= 187 && target.GetProperty(PropertyFloat.CriticalMultiplier) == null)
                            {
                                target.DamageMod += 0.04f;
                                target.SetProperty(PropertyFloat.CriticalMultiplier, 3);
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogony to their {target.NameWithMaterial}! The item now has Crushing Blow!", ChatMessageType.Broadcast));
                            }*/
                            //else
                            //{
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                            //}
                        }// 0.3%
                        // Chance for minor/major/epic/legendary Attributes
                        else if (xtramodroll >= 236 && xtramodroll <= 263) // 13.3%
                        {
                            if (resistroll >= 1 && resistroll <= 30)
                            {
                                if (!target.Biota.SpellIsKnown(2576, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3965, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6107, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSTRENGTH2); // add Major Str
                                    target.Biota.TryRemoveKnownSpell(2583, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Strength to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2576, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSTRENGTH3); // Epic Str
                                    target.Biota.TryRemoveKnownSpell(2576, target.BiotaDatabaseLock); // remove major                                    
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Strength upgraded to Epic Strength! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3965, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripStrength4); // Legendary Str
                                    target.Biota.TryRemoveKnownSpell(3965, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Strength upgraded to Legendary Strength! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Strength!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6107, target.BiotaDatabaseLock))
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Strength *done
                            else if (resistroll >= 31 && resistroll <= 61)
                            {
                                if (!target.Biota.SpellIsKnown(2573, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4226, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6104, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPENDURANCE2); // major end
                                    target.Biota.TryRemoveKnownSpell(2580, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Endurance to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2573, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPENDURANCE3); // epic end
                                    target.Biota.TryRemoveKnownSpell(2573, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Endurance upgraded to Epic Endurance! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4226, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripEndurance4); // leg end
                                    target.Biota.TryRemoveKnownSpell(4226, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Endurance upgraded to Legendary Endurance! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Endurance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6104, target.BiotaDatabaseLock))
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Endurance *done
                            else if (resistroll >= 62 && resistroll <= 92)
                            {
                                if (!target.Biota.SpellIsKnown(2572, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3963, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6103, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPCOORDINATION2); // major coord
                                    target.Biota.TryRemoveKnownSpell(2579, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Coordination to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2572, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPCOORDINATION3); // epic coord
                                    target.Biota.TryRemoveKnownSpell(2572, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Coordination upgraded to Epic Coordination! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3963, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripCoordination4); // leg coord
                                    target.Biota.TryRemoveKnownSpell(3963, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Coordination upgraded to Legendary Coordination! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Coordination!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6103, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Coordination *done
                            else if (resistroll >= 93 && resistroll <= 123)
                            {
                                if (!target.Biota.SpellIsKnown(2575, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4019, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6106, target.BiotaDatabaseLock))// if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPQUICKNESS2); // Major quick
                                    target.Biota.TryRemoveKnownSpell(2582, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Quickness to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2575, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPQUICKNESS3); // Epic quick
                                    target.Biota.TryRemoveKnownSpell(2575, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Quickness upgraded to Epic Quickness! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4019, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripQuickness4); // Leg quick
                                    target.Biota.TryRemoveKnownSpell(4019, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Quickness upgraded to Legendary Quickness! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Quickness!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6106, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+1)", ChatMessageType.Broadcast));
                                }
                            }// Quickness *done
                            else if (resistroll >= 124 && resistroll <= 154)
                            {
                                if (!target.Biota.SpellIsKnown(2574, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(3964, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6105, target.BiotaDatabaseLock))// if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPFOCUS2); // major foc
                                    target.Biota.TryRemoveKnownSpell(2581, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Focus to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2574, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPFOCUS3); // epic foc
                                    target.Biota.TryRemoveKnownSpell(2574, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Focus upgraded to Epic Focus! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(3964, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripFocus4); // legendary foc
                                    target.Biota.TryRemoveKnownSpell(3964, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Focus upgraded to Legendary Focus! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Focus!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6105, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Focus *done
                            else if (resistroll >= 155 && resistroll <= 187)
                            {
                                if (!target.Biota.SpellIsKnown(2577, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4227, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6101, target.BiotaDatabaseLock))// if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWILLPOWER2); // major will
                                    target.Biota.TryRemoveKnownSpell(2584, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Willpower to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2577, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPWILLPOWER3); // epic will
                                    target.Biota.TryRemoveKnownSpell(2577, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Willpower upgraded to Epic Willpower! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4227, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripWillpower4); // leg will
                                    target.Biota.TryRemoveKnownSpell(4227, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Willpower upgraded to Legendary Willpower! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Willpower!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6101, target.BiotaDatabaseLock)) // checks if has legendary, if it does throw no mod roll.
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage {target.Damage}(+1)", ChatMessageType.Broadcast));
                                }
                            }// Willpower *done
                        }// 9%
                        // Break into Weapon skill cantrip
                        else if (xtramodroll >= 264 && xtramodroll <= 273) // 10%
                        {
                            if (target.WeaponSkill == Skill.MissileWeapons) // Checks for weapon type.
                            {
                                if (!target.Biota.SpellIsKnown(2505, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(4687, target.BiotaDatabaseLock)
                                       && !target.Biota.SpellIsKnown(6044, target.BiotaDatabaseLock)) // if no cantrip add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPBOWAPTITUDE2); // adds major
                                    target.DamageMod += 0.04f;
                                    target.Biota.TryRemoveKnownSpell(2540, target.BiotaDatabaseLock); // removes minor
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Missile Weapons Aptitude to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2505, target.BiotaDatabaseLock)) // if major already exists upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPBOWAPTITUDE3); // adds epic
                                    target.DamageMod += 0.04f;
                                    target.Biota.TryRemoveKnownSpell(2505, target.BiotaDatabaseLock); // removes major
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Missile Weapons Aptitude upgraded to Epic Missile Weapons Aptitude! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4687, target.BiotaDatabaseLock)) // if epic already exists upgrade to legendary
                                {
                                    AddSpell(player, target, SpellId.CantripBowAptitude4); // adds legendary
                                    target.DamageMod += 0.04f;
                                    target.Biota.TryRemoveKnownSpell(4687, target.BiotaDatabaseLock); // removes epic
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Missile Weapons Aptitude! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6044, target.BiotaDatabaseLock)) // if Legendary exists throw normal roll
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }

                            }// Missile Weapons *done
                            else
                            {
                                target.DamageMod += 0.04f;
                                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                            } // for no wields
                        }// 3%
                        // weapon cantrips BT/SH/Defender/Invul/Magres
                        else if (xtramodroll >= 274 && xtramodroll <= 300) // 9.67%
                        {
                            if (resistroll >= 1 && resistroll <= 31)
                            {
                                if (!target.Biota.SpellIsKnown(2586, target.BiotaDatabaseLock)
                                    && !target.Biota.SpellIsKnown(4661, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6089, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPBLOODTHIRST2); // major bt
                                    target.Biota.TryRemoveKnownSpell(2598, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Blood Thirst to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2586, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPBLOODTHIRST3); // epic bt
                                    target.Biota.TryRemoveKnownSpell(2586, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Blood Thirst upgraded to Epic Blood Thirst! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4661, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripBloodThirst4); // leg bt
                                    target.Biota.TryRemoveKnownSpell(4661, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Blood Thirst upgraded to Legendary Blood Thirst! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Blood Thirst!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6089, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Blood Thirst *done
                            else if (resistroll >= 32 && resistroll <= 62)
                            {
                                if (!target.Biota.SpellIsKnown(2596, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4672, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6100, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSWIFTHUNTER2); // Major SH
                                    target.Biota.TryRemoveKnownSpell(2608, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Swift Hunter to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2596, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPSWIFTHUNTER3); // Epic SH
                                    target.Biota.TryRemoveKnownSpell(2596, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Swift Hunter upgraded to Epic Swift Hunter! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4672, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripSwiftHunter4); // Leg SH
                                    target.Biota.TryRemoveKnownSpell(4672, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Swift Hunter upgraded to Legendary Swift Hunter! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Swift Hunter!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6100, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Swift Hunter *done                            
                            else if (resistroll >= 63 && resistroll <= 93)
                            {
                                if (!target.Biota.SpellIsKnown(2588, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4663, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6091, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPDEFENDER2); // Major Def
                                    target.Biota.TryRemoveKnownSpell(2600, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Defender to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2588, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPDEFENDER3); // Epic Def
                                    target.Biota.TryRemoveKnownSpell(2588, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Defender upgraded to Epic Defender! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4663, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripDefender4); // Leg Def
                                    target.Biota.TryRemoveKnownSpell(4663, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Defender upgraded to Legendary Defender! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Defender!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6091, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Defender *done
                            else if (resistroll >= 94 && resistroll <= 124)
                            {
                                if (!target.Biota.SpellIsKnown(2524, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4704, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6063, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPMAGICRESISTANCE2); // Major Mag D
                                    target.Biota.TryRemoveKnownSpell(2559, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Magic Resistance to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2524, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPMAGICRESISTANCE3); // Epic Mag D
                                    target.Biota.TryRemoveKnownSpell(2524, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Magic Resistance upgraded to Epic Magic Resistance! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4704, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripMagicResistance4); // Leg Mag D
                                    target.Biota.TryRemoveKnownSpell(4704, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Magic Resistance upgraded to Legendary Magic Resistance! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Magic Resistance!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6063, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Magic Resistance *done
                            else if (resistroll >= 125 && resistroll <= 187)
                            {
                                if (!target.Biota.SpellIsKnown(2515, target.BiotaDatabaseLock)
                                   && !target.Biota.SpellIsKnown(4696, target.BiotaDatabaseLock) && !target.Biota.SpellIsKnown(6055, target.BiotaDatabaseLock))// if no minor add minor                                
                                {
                                    AddSpell(player, target, SpellId.CANTRIPINVULNERABILITY2); // Major Invul
                                    target.Biota.TryRemoveKnownSpell(2550, target.BiotaDatabaseLock); // remove minor
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! You added Major Invulnerability to your {target.NameWithMaterial}! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(2515, target.BiotaDatabaseLock)) // if major upgrade to epic
                                {
                                    AddSpell(player, target, SpellId.CANTRIPINVULNERABILITY3); // Epic Invul
                                    target.Biota.TryRemoveKnownSpell(2515, target.BiotaDatabaseLock); // remove major
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Major Invulnerability upgraded to Epic Invulnerability! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(4696, target.BiotaDatabaseLock)) // if epic upgrade to leg
                                {
                                    AddSpell(player, target, SpellId.CantripInvulnerability4); // Leg Invul
                                    target.Biota.TryRemoveKnownSpell(4696, target.BiotaDatabaseLock); // remove epic
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Rolled {xtramodroll}. Nice! Your {target.NameWithMaterial} had its Epic Invulnerability upgraded to Legendary Invulnerability! New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[TINKERING] {player.Name} just got super lucky applying Mahogany to their {target.NameWithMaterial}! They have upgraded their Epic Cantrip, it is now Legendary Invulnerability!", ChatMessageType.Broadcast));
                                    target.ItemDifficulty = retainlore;
                                }
                                else if (target.Biota.SpellIsKnown(6055, target.BiotaDatabaseLock)) // if has legendary throw no mod chance
                                {
                                    target.DamageMod += 0.04f;
                                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Bonus Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                                }
                            }// Invulnerability *done
                        }// 8.6%
                    }
                    else
                    {
                        target.DamageMod += 0.04f;
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"No mod chance roll. Better luck next time. New Target Damage Mod {target.DamageMod:N2}(+4%)", ChatMessageType.Broadcast));
                    }
                    break;
                case MaterialType.Granite:
                    target.DamageVariance *= 0.8f;
                    break;
                case MaterialType.Oak:
                    target.WeaponTime = Math.Max(0, (target.WeaponTime ?? 0) - 50);
                    break;
                case MaterialType.Brass:
                    target.WeaponDefense += 0.01f;
                    break;
                case MaterialType.Velvet:
                    target.WeaponOffense += 0.01f;
                    break;
                    //start customs
                case MaterialType.LessDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);

                    var lessRNG = ThreadSafeRandom.Next((float)1.25, (float)1.30);

                    target.SetProperty(PropertyFloat.SlayerDamageBonus, lessRNG);
                    break;
                case MaterialType.LessLessDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 1.15);
                    break;
                case MaterialType.LessGreaterDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 1.35);
                    break;
                case MaterialType.ModerateDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 1.60);
                    break;
                case MaterialType.ModerateLessDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 1.50);
                    break;
                case MaterialType.ModerateGreaterDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 1.70);
                    break;
                case MaterialType.GreaterLessDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 1.80);
                    break;
                case MaterialType.GreaterDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 1.90);
                    break;
                case MaterialType.GreaterGreaterDrudgeSlayer:
                    target.SetProperty(PropertyInt.SlayerCreatureType, 3);
                    target.SetProperty(PropertyFloat.SlayerDamageBonus, 2);
                    break;
                    // end customs
                // only 1 imbue can be applied per piece of armor?
                case MaterialType.Emerald:
                    AddImbuedEffect(player, target, ImbuedEffectType.AcidRending);
                    break;
                case MaterialType.WhiteSapphire:
                    AddImbuedEffect(player, target, ImbuedEffectType.BludgeonRending);
                    break;
                case MaterialType.Aquamarine:
                    AddImbuedEffect(player, target, ImbuedEffectType.ColdRending);
                    break;
                case MaterialType.Jet:
                    AddImbuedEffect(player, target, ImbuedEffectType.ElectricRending);
                    break;
                case MaterialType.RedGarnet:
                    AddImbuedEffect(player, target, ImbuedEffectType.FireRending);
                    break;
                case MaterialType.BlackGarnet:
                    AddImbuedEffect(player, target, ImbuedEffectType.PierceRending);
                    break;
                case MaterialType.ImperialTopaz:
                    AddImbuedEffect(player, target, ImbuedEffectType.SlashRending);
                    break;
                default:
                    log.Error($"{player.Name}.RecipeManager.Tinkering_ModifyItem({tool.Name} ({tool.Guid}), {target.Name} ({target.Guid})) - Unknown material type: {materialType}");
                    return;
            }

            // increase # of times tinkered, if appropriate
            if (incItemTinkered)
            {
                target.NumTimesTinkered++;

                if (target.TinkerLog != null)
                    target.TinkerLog += ",";
                target.TinkerLog += (int)materialType;
            }
        }

        public static void AddSpell(Player player, WorldObject target, SpellId spell, int difficulty = 25)
        {
            target.Biota.GetOrAddKnownSpell((int)spell, target.BiotaDatabaseLock, out _);
            target.ChangesDetected = true;

            if (difficulty != 0)
            {
                target.ItemSpellcraft = (target.ItemSpellcraft ?? 0) + difficulty;
                target.ItemDifficulty = (target.ItemDifficulty ?? 0) + difficulty;
            }
            if (target.UiEffects == null)
            {
                target.UiEffects = UiEffects.Magical;
                player.Session.Network.EnqueueSend(new GameMessagePublicUpdatePropertyInt(target, PropertyInt.UiEffects, (int)target.UiEffects));
            }
        }

        public static bool AddImbuedEffect(Player player, WorldObject target, ImbuedEffectType effect)
        {
            var imbuedEffects = GetImbuedEffects(target);

            if (imbuedEffects.HasFlag(effect))
                return false;     // already present

            imbuedEffects |= effect;

            if (target.GetProperty(PropertyInt.ImbuedEffect) == null)
                target.SetProperty(PropertyInt.ImbuedEffect, (int)effect);

            else if (target.GetProperty(PropertyInt.ImbuedEffect2) == null)
                target.SetProperty(PropertyInt.ImbuedEffect2, (int)effect);

            else if (target.GetProperty(PropertyInt.ImbuedEffect3) == null)
                target.SetProperty(PropertyInt.ImbuedEffect3, (int)effect);

            else if (target.GetProperty(PropertyInt.ImbuedEffect4) == null)
                target.SetProperty(PropertyInt.ImbuedEffect4, (int)effect);

            else if (target.GetProperty(PropertyInt.ImbuedEffect5) == null)
                target.SetProperty(PropertyInt.ImbuedEffect5, (int)effect);

            else
                return false;

            /*if (IconUnderlay.TryGetValue(effect, out var icon))
            {
                target.IconUnderlayId = icon;
                player.Session.Network.EnqueueSend(new GameMessagePublicUpdatePropertyDataID(target, PropertyDataId.IconUnderlay, target.IconUnderlayId.Value));
            }*/

            return true;
        }

        // derrick's input => output mappings
        public static Dictionary<ImbuedEffectType, uint> IconUnderlay = new Dictionary<ImbuedEffectType, uint>()
        {
            { ImbuedEffectType.ColdRending,     0x06003353 },
            { ImbuedEffectType.ElectricRending, 0x06003354 },
            { ImbuedEffectType.AcidRending,     0x06003355 },
            { ImbuedEffectType.ArmorRending,    0x06003356 },
            { ImbuedEffectType.CripplingBlow,   0x06003357 },
            { ImbuedEffectType.CriticalStrike,  0x06003358 },
            { ImbuedEffectType.FireRending,     0x06003359 },
            { ImbuedEffectType.BludgeonRending, 0x0600335a },
            { ImbuedEffectType.PierceRending,   0x0600335b },
            { ImbuedEffectType.SlashRending,    0x0600335c },
        };

        public static ImbuedEffectType GetImbuedEffects(WorldObject target)
        {
            var imbuedEffects = 0;

            imbuedEffects |= target.GetProperty(PropertyInt.ImbuedEffect) ?? 0;
            imbuedEffects |= target.GetProperty(PropertyInt.ImbuedEffect2) ?? 0;
            imbuedEffects |= target.GetProperty(PropertyInt.ImbuedEffect3) ?? 0;
            imbuedEffects |= target.GetProperty(PropertyInt.ImbuedEffect4) ?? 0;
            imbuedEffects |= target.GetProperty(PropertyInt.ImbuedEffect5) ?? 0;

            return (ImbuedEffectType)imbuedEffects;
        }

        /// <summary>
        /// Returns the modifier for a bag of salvaging material
        /// </summary>
        public static float GetMaterialMod(MaterialType material)
        {
            switch (material)
            {
                case MaterialType.Gold:
                case MaterialType.Oak:
                    return 10.0f;

                case MaterialType.Alabaster:
                case MaterialType.ArmoredilloHide:
                case MaterialType.Brass:
                case MaterialType.Bronze:
                case MaterialType.Ceramic:
                case MaterialType.Granite:
                case MaterialType.Linen:
                case MaterialType.Marble:
                case MaterialType.Moonstone:
                case MaterialType.Opal:
                case MaterialType.Pine:
                case MaterialType.ReedSharkHide:
                case MaterialType.Velvet:
                case MaterialType.Wool:
                    return 11.0f;

                case MaterialType.Ebony:
                case MaterialType.GreenGarnet:
                case MaterialType.Iron:
                case MaterialType.Mahogany:
                case MaterialType.Porcelain:
                case MaterialType.Satin:
                case MaterialType.Steel:
                case MaterialType.Teak:
                    return 12.0f;

                case MaterialType.Bloodstone:
                case MaterialType.Carnelian:
                case MaterialType.Citrine:
                case MaterialType.Hematite:
                case MaterialType.LavenderJade:
                case MaterialType.Malachite:
                case MaterialType.RedJade:
                case MaterialType.RoseQuartz:
                    return 25.0f;

                default:
                    return 20.0f;
            }
        }

        /// <summary>
        /// Thanks to Endy's Tinkering Calculator for these values!
        /// </summary>
        public static List<float> TinkeringDifficulty = new List<float>()
        {
            // attempt #
            1.0f,   // 1
            1.1f,   // 2
            1.3f,   // 3
            1.6f,   // 4
            2.0f,   // 5
            2.5f,   // 6
            3.0f,   // 7
            3.5f,   // 8
            4.0f,   // 9
            4.5f    // 10
        };

        public static bool VerifyRequirements(Recipe recipe, Player player, WorldObject source, WorldObject target)
        {
            if (!VerifyUse(player, source, target))
                return false;

            if (!VerifyRequirements(recipe, player, target, RequirementType.Target)) return false;

            if (!VerifyRequirements(recipe, player, source, RequirementType.Source)) return false;

            if (!VerifyRequirements(recipe, player, player, RequirementType.Player)) return false;

            return true;
        }

        public static bool VerifyUse(Player player, WorldObject source, WorldObject target)
        {
            var usable = source.ItemUseable ?? Usable.Undef;

            if (usable == Usable.Undef)
            {
                log.Warn($"{player.Name}.RecipeManager.VerifyUse({source.Name} ({source.Guid}), {target.Name} ({target.Guid})) - source not usable, falling back on defaults");

                // re-verify
                if (player.FindObject(source.Guid.Full, Player.SearchLocations.MyInventory) == null)
                    return false;

                // almost always MyInventory, but sometimes can be applied to equipped
                if (player.FindObject(target.Guid.Full, Player.SearchLocations.MyInventory | Player.SearchLocations.MyEquippedItems) == null)
                    return false;

                return true;
            }

            var sourceUse = usable.GetSourceFlags();
            var targetUse = usable.GetTargetFlags();

            return VerifyUse(player, source, sourceUse) && VerifyUse(player, target, targetUse);
        }

        public static bool VerifyUse(Player player, WorldObject obj, Usable usable)
        {
            var searchLocations = Player.SearchLocations.None;

            // TODO: figure out other Usable flags
            if (usable.HasFlag(Usable.Contained))
                searchLocations |= Player.SearchLocations.MyInventory;
            if (usable.HasFlag(Usable.Wielded))
                searchLocations |= Player.SearchLocations.MyEquippedItems;
            if (usable.HasFlag(Usable.Remote))
                searchLocations |= Player.SearchLocations.LocationsICanMove;    // TODO: moveto for this type

            return player.FindObject(obj.Guid.Full, searchLocations) != null;
        }

        public static bool Debug = false;

        public static bool VerifyRequirements(Recipe recipe, Player player, WorldObject obj, RequirementType reqType)
        {
            var boolReqs = recipe.RecipeRequirementsBool.Where(i => i.Index == (int)reqType).ToList();
            var intReqs = recipe.RecipeRequirementsInt.Where(i => i.Index == (int)reqType).ToList();
            var floatReqs = recipe.RecipeRequirementsFloat.Where(i => i.Index == (int)reqType).ToList();
            var strReqs = recipe.RecipeRequirementsString.Where(i => i.Index == (int)reqType).ToList();
            var iidReqs = recipe.RecipeRequirementsIID.Where(i => i.Index == (int)reqType).ToList();
            var didReqs = recipe.RecipeRequirementsDID.Where(i => i.Index == (int)reqType).ToList();

            var totalReqs = boolReqs.Count + intReqs.Count + floatReqs.Count + strReqs.Count + iidReqs.Count + didReqs.Count;

            if (Debug && totalReqs > 0)
                Console.WriteLine($"{reqType} Requirements: {totalReqs}");

            foreach (var requirement in boolReqs)
            {
                bool? value = obj.GetProperty((PropertyBool)requirement.Stat);
                double? normalized = value != null ? (double?)Convert.ToDouble(value.Value) : null;

                if (Debug)
                    Console.WriteLine($"PropertyBool.{(PropertyBool)requirement.Stat} {(CompareType)requirement.Enum} {requirement.Value}, current: {value}");

                if (!VerifyRequirement(player, (CompareType)requirement.Enum, normalized, Convert.ToDouble(requirement.Value), requirement.Message))
                    return false;
            }

            foreach (var requirement in intReqs)
            {
                int? value = obj.GetProperty((PropertyInt)requirement.Stat);
                double? normalized = value != null ? (double?)Convert.ToDouble(value.Value) : null;

                if (Debug)
                    Console.WriteLine($"PropertyInt.{(PropertyInt)requirement.Stat} {(CompareType)requirement.Enum} {requirement.Value}, current: {value}");

                if (!VerifyRequirement(player, (CompareType)requirement.Enum, normalized, Convert.ToDouble(requirement.Value), requirement.Message))
                    return false;
            }

            foreach (var requirement in floatReqs)
            {
                double? value = obj.GetProperty((PropertyFloat)requirement.Stat);

                if (Debug)
                    Console.WriteLine($"PropertyFloat.{(PropertyFloat)requirement.Stat} {(CompareType)requirement.Enum} {requirement.Value}, current: {value}");

                if (!VerifyRequirement(player, (CompareType)requirement.Enum, value, requirement.Value, requirement.Message))
                    return false;
            }

            foreach (var requirement in strReqs)
            {
                string value = obj.GetProperty((PropertyString)requirement.Stat);

                if (Debug)
                    Console.WriteLine($"PropertyString.{(PropertyString)requirement.Stat} {(CompareType)requirement.Enum} {requirement.Value}, current: {value}");

                if (!VerifyRequirement(player, (CompareType)requirement.Enum, value, requirement.Value, requirement.Message))
                    return false;
            }

            foreach (var requirement in iidReqs)
            {
                uint? value = obj.GetProperty((PropertyInstanceId)requirement.Stat);
                double? normalized = value != null ? (double?)Convert.ToDouble(value.Value) : null;

                if (Debug)
                    Console.WriteLine($"PropertyInstanceId.{(PropertyInstanceId)requirement.Stat} {(CompareType)requirement.Enum} {requirement.Value}, current: {value}");

                if (!VerifyRequirement(player, (CompareType)requirement.Enum, normalized, Convert.ToDouble(requirement.Value), requirement.Message))
                    return false;
            }

            foreach (var requirement in didReqs)
            {
                uint? value = obj.GetProperty((PropertyDataId)requirement.Stat);
                double? normalized = value != null ? (double?)Convert.ToDouble(value.Value) : null;

                if (Debug)
                    Console.WriteLine($"PropertyDataId.{(PropertyDataId)requirement.Stat} {(CompareType)requirement.Enum} {requirement.Value}, current: {value}");

                if (!VerifyRequirement(player, (CompareType)requirement.Enum, normalized, Convert.ToDouble(requirement.Value), requirement.Message))
                    return false;
            }

            if (Debug && totalReqs > 0)
                Console.WriteLine($"-----");

            return true;
        }

        public static bool VerifyRequirement(Player player, CompareType compareType, double? prop, double val, string failMsg)
        {
            var success = true;

            switch (compareType)
            {
                case CompareType.GreaterThan:
                    if ((prop ?? 0) > val)
                        success = false;
                    break;

                case CompareType.LessThanEqual:
                    if ((prop ?? 0) <= val)
                        success = false;
                    break;

                case CompareType.LessThan:
                    if ((prop ?? 0) < val)
                        success = false;
                    break;

                case CompareType.GreaterThanEqual:
                    if ((prop ?? 0) >= val)
                        success = false;
                    break;

                case CompareType.NotEqual:
                    if ((prop ?? 0) != val)
                        success = false;
                    break;

                case CompareType.NotEqualNotExist:
                    if (prop == null || prop.Value != val)
                        success = false;
                    break;

                case CompareType.Equal:
                    if ((prop ?? 0) == val)
                        success = false;
                    break;

                case CompareType.NotExist:
                    if (prop == null)
                        success = false;
                    break;

                case CompareType.Exist:
                    if (prop != null)
                        success = false;
                    break;
            }

            if (!success)
                player.Session.Network.EnqueueSend(new GameMessageSystemChat(failMsg, ChatMessageType.Craft));

            return success;
        }

        public static bool VerifyRequirement(Player player, CompareType compareType, string prop, string val, string failMsg)
        {
            var success = true;

            switch (compareType)
            {
                case CompareType.NotEqual:
                    if (!(prop ?? "").Equals(val))
                        success = false;
                    break;

                case CompareType.NotEqualNotExist:
                    if (prop == null || !prop.Equals(val))
                        success = false;
                    break;

                case CompareType.Equal:
                    if ((prop ?? "").Equals(val))
                        success = false;
                    break;

                case CompareType.NotExist:
                    if (prop == null)
                        success = false;
                    break;

                case CompareType.Exist:
                    if (prop != null)
                        success = false;
                    break;
            }
            if (!success)
                player.Session.Network.EnqueueSend(new GameMessageSystemChat(failMsg, ChatMessageType.Craft));

            return success;
        }

        public static void CreateDestroyItems(Player player, Recipe recipe, WorldObject source, WorldObject target, bool success, bool sendMsg = true)
        {
            var destroyTargetChance = success ? recipe.SuccessDestroyTargetChance : recipe.FailDestroyTargetChance;
            var destroySourceChance = success ? recipe.SuccessDestroySourceChance : recipe.FailDestroySourceChance;

            var destroyTarget = ThreadSafeRandom.Next(0.0f, 1.0f) <= destroyTargetChance;
            var destroySource = ThreadSafeRandom.Next(0.0f, 1.0f) <= destroySourceChance;

            var createItem = success ? recipe.SuccessWCID : recipe.FailWCID;
            var createAmount = success ? recipe.SuccessAmount : recipe.FailAmount;

            if (createItem > 0 && DatabaseManager.World.GetWeenie(createItem) == null)
            {
                log.Error($"RecipeManager.CreateDestroyItems: Recipe.Id({recipe.Id}) couldn't find {(success ? "Success" : "Fail")}WCID {createItem} in database.");
                player.Session.Network.EnqueueSend(new GameEventWeenieError(player.Session, WeenieError.CraftGeneralErrorUiMsg));
                return;
            }

            if (destroyTarget)
            {
                var destroyTargetAmount = success ? recipe.SuccessDestroyTargetAmount : recipe.FailDestroyTargetAmount;
                var destroyTargetMessage = success ? recipe.SuccessDestroyTargetMessage : recipe.FailDestroyTargetMessage;

                DestroyItem(player, recipe, target, destroyTargetAmount, destroyTargetMessage);
            }

            if (destroySource)
            {
                var destroySourceAmount = success ? recipe.SuccessDestroySourceAmount : recipe.FailDestroySourceAmount;
                var destroySourceMessage = success ? recipe.SuccessDestroySourceMessage : recipe.FailDestroySourceMessage;

                DestroyItem(player, recipe, source, destroySourceAmount, destroySourceMessage);
            }

            WorldObject result = null;

            if (createItem > 0)
                result = CreateItem(player, createItem, createAmount);

            ModifyItem(player, recipe, source, target, result, success);

            if (sendMsg)
            {
                // TODO: remove this in data for imbues

                // suppress message for imbues w/ a chance of failure here,
                // handled previously in local broadcast

                var message = success ? recipe.SuccessMessage : recipe.FailMessage;

                player.Session.Network.EnqueueSend(new GameMessageSystemChat(message, ChatMessageType.Craft));
            }
        }

        public static WorldObject CreateItem(Player player, uint wcid, uint amount)
        {
            var wo = WorldObjectFactory.CreateNewWorldObject(wcid);

            if (wo == null)
            {
                log.Warn($"RecipeManager.CreateItem({player.Name}, {wcid}, {amount}): failed to create {wcid}");
                return null;
            }

            if (amount > 1)
                wo.SetStackSize((int)amount);

            player.TryCreateInInventoryWithNetworking(wo);
            return wo;
        }

        public static void DestroyItem(Player player, Recipe recipe, WorldObject item, uint amount, string msg)
        {
            if (item.OwnerId == player.Guid.Full || player.GetInventoryItem(item.Guid) != null)
            {
                if (!player.TryConsumeFromInventoryWithNetworking(item, (int)amount))
                    log.Warn($"RecipeManager.DestroyItem({player.Name}, {item.Name}, {amount}, {msg}): failed to remove {item.Name}");
            }
            else if (item.WielderId == player.Guid.Full)
            {
                if (!player.TryDequipObjectWithNetworking(item.Guid, out _, Player.DequipObjectAction.ConsumeItem))
                    log.Warn($"RecipeManager.DestroyItem({player.Name}, {item.Name}, {amount}, {msg}): failed to remove {item.Name}");
            }
            else
            {
                item.Destroy();
            }
            if (!string.IsNullOrEmpty(msg))
            {
                var destroyMessage = new GameMessageSystemChat(msg, ChatMessageType.Craft);
                player.Session.Network.EnqueueSend(destroyMessage);
            }
        }

        public static WorldObject GetSourceMod(RecipeSourceType sourceType, Player player, WorldObject source)
        {
            switch (sourceType)
            {
                case RecipeSourceType.Player:
                    return player;
                case RecipeSourceType.Source:
                    return source;
            }
            //log.Warn($"RecipeManager.GetSourceMod({sourceType}, {player.Name}, {source.Name}) - unknown source type");
            return null;
        }

        public static WorldObject GetTargetMod(ModificationType type, WorldObject source, WorldObject target, Player player, WorldObject result)
        {
            switch (type)
            {
                case ModificationType.SuccessSource:
                case ModificationType.FailureSource:
                    return source;

                default:
                    return target;

                case ModificationType.SuccessPlayer:
                case ModificationType.FailurePlayer:
                    return player;

                case ModificationType.SuccessResult:
                case ModificationType.FailureResult:
                    return result ?? target;
            }
        }

        public static void ModifyItem(Player player, Recipe recipe, WorldObject source, WorldObject target, WorldObject result, bool success)
        {
            foreach (var mod in recipe.RecipeMod)
            {
                if (mod.ExecutesOnSuccess != success)
                    continue;

                // apply base mod
                switch (mod.DataId)
                {
                    // 	Fetish of the Dark Idols
                    case 0x38000046:
                        AddImbuedEffect(player, target, ImbuedEffectType.IgnoreSomeMagicProjectileDamage);
                        target.SetProperty(PropertyFloat.AbsorbMagicDamage, 0.25f);
                        break;
                }

                // adjust vitals, but all appear to be 0 in current database?

                // apply type mods
                foreach (var boolMod in mod.RecipeModsBool)
                    ModifyBool(player, boolMod, source, target, result);

                foreach (var intMod in mod.RecipeModsInt)
                    ModifyInt(player, intMod, source, target, result);

                foreach (var floatMod in mod.RecipeModsFloat)
                    ModifyFloat(player, floatMod, source, target, result);

                foreach (var stringMod in mod.RecipeModsString)
                    ModifyString(player, stringMod, source, target, result);

                foreach (var iidMod in mod.RecipeModsIID)
                    ModifyInstanceID(player, iidMod, source, target, result);

                foreach (var didMod in mod.RecipeModsDID)
                    ModifyDataID(player, didMod, source, target, result);
            }
        }

        public static void ModifyBool(Player player, RecipeModsBool boolMod, WorldObject source, WorldObject target, WorldObject result)
        {
            var op = (ModificationOperation)boolMod.Enum;
            var prop = (PropertyBool)boolMod.Stat;
            var value = boolMod.Value;

            var targetMod = GetTargetMod((ModificationType)boolMod.Index, source, target, player, result);

            // always SetValue?
            if (op != ModificationOperation.SetValue)
            {
                log.Warn($"RecipeManager.ModifyBool({source.Name}, {target.Name}): unhandled operation {op}");
                return;
            }
            player.UpdateProperty(targetMod, prop, value);

            if (Debug)
                Console.WriteLine($"{targetMod.Name}.SetProperty({prop}, {value}) - {op}");
        }

        public static void ModifyInt(Player player, RecipeModsInt intMod, WorldObject source, WorldObject target, WorldObject result)
        {
            var op = (ModificationOperation)intMod.Enum;
            var prop = (PropertyInt)intMod.Stat;
            var value = intMod.Value;

            var sourceMod = GetSourceMod((RecipeSourceType)intMod.Source, player, source);
            var targetMod = GetTargetMod((ModificationType)intMod.Index, source, target, player, result);

            switch (op)
            {
                case ModificationOperation.SetValue:
                    player.UpdateProperty(targetMod, prop, value);
                    if (Debug) Console.WriteLine($"{targetMod.Name}.SetProperty({prop}, {value}) - {op}");
                    break;
                case ModificationOperation.Add:
                    player.UpdateProperty(targetMod, prop, (targetMod.GetProperty(prop) ?? 0) + value);
                    if (Debug) Console.WriteLine($"{targetMod.Name}.IncProperty({prop}, {value}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToTarget:
                    player.UpdateProperty(target, prop, sourceMod.GetProperty(prop) ?? 0);
                    if (Debug) Console.WriteLine($"{target.Name}.SetProperty({prop}, {sourceMod.GetProperty(prop) ?? 0}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToResult:
                    player.UpdateProperty(result, prop, player.GetProperty(prop) ?? 0);     // ??
                    if (Debug) Console.WriteLine($"{result.Name}.SetProperty({prop}, {player.GetProperty(prop) ?? 0}) - {op}");
                    break;
                case ModificationOperation.AddSpell:
                    targetMod.Biota.GetOrAddKnownSpell(intMod.Stat, target.BiotaDatabaseLock, out var added);
                    if (added)
                        targetMod.ChangesDetected = true;
                    if (Debug) Console.WriteLine($"{targetMod.Name}.AddSpell({intMod.Stat}) - {op}");
                    break;
                default:
                    log.Warn($"RecipeManager.ModifyInt({source.Name}, {target.Name}): unhandled operation {op}");
                    break;
            }
        }

        public static void ModifyFloat(Player player, RecipeModsFloat floatMod, WorldObject source, WorldObject target, WorldObject result)
        {
            var op = (ModificationOperation)floatMod.Enum;
            var prop = (PropertyFloat)floatMod.Stat;
            var value = floatMod.Value;

            var sourceMod = GetSourceMod((RecipeSourceType)floatMod.Source, player, source);
            var targetMod = GetTargetMod((ModificationType)floatMod.Index, source, target, player, result);

            switch (op)
            {
                case ModificationOperation.SetValue:
                    player.UpdateProperty(targetMod, prop, value);
                    if (Debug) Console.WriteLine($"{targetMod.Name}.SetProperty({prop}, {value}) - {op}");
                    break;
                case ModificationOperation.Add:
                    player.UpdateProperty(targetMod, prop, (targetMod.GetProperty(prop) ?? 0) + value);
                    if (Debug) Console.WriteLine($"{targetMod.Name}.IncProperty({prop}, {value}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToTarget:
                    player.UpdateProperty(target, prop, sourceMod.GetProperty(prop) ?? 0);
                    if (Debug) Console.WriteLine($"{target.Name}.SetProperty({prop}, {sourceMod.GetProperty(prop) ?? 0}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToResult:
                    player.UpdateProperty(result, prop, player.GetProperty(prop) ?? 0);
                    if (Debug) Console.WriteLine($"{result.Name}.SetProperty({prop}, {player.GetProperty(prop) ?? 0}) - {op}");
                    break;
                default:
                    log.Warn($"RecipeManager.ModifyFloat({source.Name}, {target.Name}): unhandled operation {op}");
                    break;
            }
        }

        public static void ModifyString(Player player, RecipeModsString stringMod, WorldObject source, WorldObject target, WorldObject result)
        {
            var op = (ModificationOperation)stringMod.Enum;
            var prop = (PropertyString)stringMod.Stat;
            var value = stringMod.Value;

            var sourceMod = GetSourceMod((RecipeSourceType)stringMod.Source, player, source);
            var targetMod = GetTargetMod((ModificationType)stringMod.Index, source, target, player, result);

            switch (op)
            {
                case ModificationOperation.SetValue:
                    player.UpdateProperty(targetMod, prop, value);
                    if (Debug) Console.WriteLine($"{targetMod.Name}.SetProperty({prop}, {value}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToTarget:
                    player.UpdateProperty(target, prop, sourceMod.GetProperty(prop) ?? sourceMod.Name);
                    if (Debug) Console.WriteLine($"{target.Name}.SetProperty({prop}, {sourceMod.GetProperty(prop) ?? sourceMod.Name}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToResult:
                    player.UpdateProperty(result, prop, player.GetProperty(prop) ?? player.Name);
                    if (Debug) Console.WriteLine($"{result.Name}.SetProperty({prop}, {player.GetProperty(prop) ?? player.Name}) - {op}");
                    break;
                default:
                    log.Warn($"RecipeManager.ModifyString({source.Name}, {target.Name}): unhandled operation {op}");
                    break;
            }
        }

        public static void ModifyInstanceID(Player player, RecipeModsIID iidMod, WorldObject source, WorldObject target, WorldObject result)
        {
            var op = (ModificationOperation)iidMod.Enum;
            var prop = (PropertyInstanceId)iidMod.Stat;
            var value = iidMod.Value;

            var sourceMod = GetSourceMod((RecipeSourceType)iidMod.Source, player, source);
            var targetMod = GetTargetMod((ModificationType)iidMod.Index, source, target, player, result);

            switch (op)
            {
                case ModificationOperation.SetValue:
                    player.UpdateProperty(targetMod, prop, value);
                    if (Debug) Console.WriteLine($"{targetMod.Name}.SetProperty({prop}, {value}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToTarget:
                    player.UpdateProperty(target, prop, ModifyInstanceIDRuleSet(prop, sourceMod, targetMod));
                    if (Debug) Console.WriteLine($"{target.Name}.SetProperty({prop}, {ModifyInstanceIDRuleSet(prop, sourceMod, targetMod)}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToResult:
                    player.UpdateProperty(result, prop, ModifyInstanceIDRuleSet(prop, player, targetMod));     // ??
                    if (Debug) Console.WriteLine($"{result.Name}.SetProperty({prop}, {ModifyInstanceIDRuleSet(prop, player, targetMod)}) - {op}");
                    break;
                default:
                    log.Warn($"RecipeManager.ModifyInstanceID({source.Name}, {target.Name}): unhandled operation {op}");
                    break;
            }
        }

        private static uint ModifyInstanceIDRuleSet(PropertyInstanceId property, WorldObject sourceMod, WorldObject targetMod)
        {
            switch (property)
            {
                case PropertyInstanceId.AllowedWielder:
                case PropertyInstanceId.AllowedActivator:
                    return sourceMod.Guid.Full;
                default:
                    break;
            }

            return sourceMod.GetProperty(property) ?? 0;
        }

        public static void ModifyDataID(Player player, RecipeModsDID didMod, WorldObject source, WorldObject target, WorldObject result)
        {
            var op = (ModificationOperation)didMod.Enum;
            var prop = (PropertyDataId)didMod.Stat;
            var value = didMod.Value;

            var sourceMod = GetSourceMod((RecipeSourceType)didMod.Source, player, source);
            var targetMod = GetTargetMod((ModificationType)didMod.Index, source, target, player, result);

            switch (op)
            {
                case ModificationOperation.SetValue:
                    player.UpdateProperty(targetMod, prop, value);
                    if (Debug) Console.WriteLine($"{targetMod.Name}.SetProperty({prop}, {value}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToTarget:
                    player.UpdateProperty(target, prop, sourceMod.GetProperty(prop) ?? 0);
                    if (Debug) Console.WriteLine($"{target.Name}.SetProperty({prop}, {sourceMod.GetProperty(prop) ?? 0}) - {op}");
                    break;
                case ModificationOperation.CopyFromSourceToResult:
                    player.UpdateProperty(result, prop, player.GetProperty(prop) ?? 0);
                    if (Debug) Console.WriteLine($"{result.Name}.SetProperty({prop}, {player.GetProperty(prop) ?? 0}) - {op}");
                    break;
                default:
                    log.Warn($"RecipeManager.ModifyDataID({source.Name}, {target.Name}): unhandled operation {op}");
                    break;
            }
        }

        public static uint MaterialDualDID = 0x27000000;

        public static string GetMaterialName(MaterialType materialType)
        {

            if (materialType.IsCustom())
                return materialType.ToString();

            var dualDIDs = DatManager.PortalDat.ReadFromDat<DualDidMapper>(MaterialDualDID);

            if (!dualDIDs.ClientEnumToName.TryGetValue((uint)materialType, out var materialName))
            {
                log.Error($"RecipeManager.GetMaterialName({materialType}): couldn't find material name");
                return materialType.ToString();
            }
            return materialName.Replace("_", " ");
        }

        /// <summary>
        /// Returns TRUE if this material requies a skill check
        /// </summary>
        public static bool UseSkillCheck(MaterialType material)
        {
            return material != MaterialType.Ivory && material != MaterialType.Leather && material != MaterialType.Sandstone && material != MaterialType.LessDrudgeSlayer
                && material != MaterialType.LessLessDrudgeSlayer && material != MaterialType.LessGreaterDrudgeSlayer && material != MaterialType.ModerateDrudgeSlayer
                && material != MaterialType.ModerateLessDrudgeSlayer && material != MaterialType.ModerateGreaterDrudgeSlayer && material != MaterialType.GreaterDrudgeSlayer
                && material != MaterialType.GreaterLessDrudgeSlayer && material != MaterialType.GreaterGreaterDrudgeSlayer;
        }
    }
}
