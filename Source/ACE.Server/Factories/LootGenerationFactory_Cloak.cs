using ACE.Common;
using ACE.Database.Models.World;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Managers;
using ACE.Server.WorldObjects;

namespace ACE.Server.Factories
{
    public static partial class LootGenerationFactory
    {
        private static WorldObject CreateCloaks(TreasureDeath profile, int tier, bool mutate = true)
        {
            int chance;
            uint cloaksType;

            if (tier < 5) return null;

            // TODO: drop percentage tweaks between types within a given tier, if needed
            switch (tier)
            {
                case 5:
                    chance = ThreadSafeRandom.Next(1, 8);  // Example between color type
                    if (chance <= 2)
                        cloaksType = Cloaks.Cloak;
                    else if (chance <= 4)
                        cloaksType = Cloaks.ChevronCloak;
                    else if (chance <= 6)
                        cloaksType = Cloaks.BorderedCloak;
                    else
                        cloaksType = Cloaks.HalvedCloak;                   
                    break;
                case 6:
                    chance = ThreadSafeRandom.Next(1, 12);  // Example between color type
                    if (chance <= 2)
                        cloaksType = Cloaks.Cloak;
                    else if (chance <= 4)
                        cloaksType = Cloaks.ChevronInvertedCloak;
                    else if (chance <= 6)
                        cloaksType = Cloaks.HalvedCloak;
                    else if (chance <= 8)
                        cloaksType = Cloaks.TrimmedCloak;
                    else if (chance <= 10)
                        cloaksType = Cloaks.QuarteredCloak;
                    else
                        cloaksType = Cloaks.ChevronInvertedCloak;
                    break;
                case 7:
                    chance = ThreadSafeRandom.Next(1, 1000);  // Example between color type
                    if (chance <= 300)
                        cloaksType = Cloaks.Cloak;
                    else if (chance <= 500)
                        cloaksType = Cloaks.ChevronInvertedCloak;
                    else if (chance <= 600)
                        cloaksType = Cloaks.ChevronCloak;
                    else if (chance <= 700)
                        cloaksType = Cloaks.TrimmedCloak;
                    else if (chance <= 800)
                        cloaksType = Cloaks.QuarteredCloak;
                    else if (chance <= 970)
                        cloaksType = Cloaks.BorderedCloak;
                    else if (chance <= 990)
                        cloaksType = Cloaks.HalvedCloak;
                    else
                        cloaksType = Cloaks.RynthidFieldCloak;
                    break;
                case 8:
                    chance = ThreadSafeRandom.Next(1, 1000);  // Example between color type
                    if (chance <= 400)
                        cloaksType = Cloaks.Cloak;
                    else if (chance <= 700)
                        cloaksType = Cloaks.ChevronInvertedCloak;
                    else if (chance <= 800)
                        cloaksType = Cloaks.HalvedCloak;
                    else if (chance <= 900)
                        cloaksType = Cloaks.TrimmedCloak;
                    else if (chance <= 975)
                        cloaksType = Cloaks.QuarteredCloak;
                    else if (chance <= 980)
                        cloaksType = Cloaks.CreepingBlightCloak;
                    else if (chance <= 985)
                        cloaksType = Cloaks.HouseMhoireCloak;
                    else if (chance <= 995)
                        cloaksType = Cloaks.RynthidFieldCloak;
                    else if (chance <= 999)
                        cloaksType = Cloaks.MukkirWingsCloak;
                    else
                        cloaksType = Cloaks.RynthidTentaclesCloak;
                    break;
                default:
                    chance = ThreadSafeRandom.Next(1, 8); // Example % between color type
                    if (chance <= 2)
                        cloaksType = Cloaks.Cloak;
                    else if (chance <= 4)
                        cloaksType = Cloaks.ChevronCloak;
                    else if (chance <= 6)
                        cloaksType = Cloaks.BorderedCloak;
                    else
                        cloaksType = Cloaks.HalvedCloak;                   
                    break;
            }

            WorldObject wo = WorldObjectFactory.CreateNewWorldObject(cloaksType) as Clothing;

            if (wo != null && mutate)
                MutateCloaks(wo, profile, tier);

            return wo;
        }

        private static void MutateCloaks(WorldObject wo, TreasureDeath profile, int tier)
        {
            const uint cloaksIconOverlayOne   = 100690996;
            const uint cloaksIconOverlayTwo   = 100690997;
            const uint cloaksIconOverlayThree = 100690998;
            const uint cloaksIconOverlayFour  = 100690999;
            const uint cloaksIconOverlayFive  = 100691000;

            // Initial roll for an cloak level 1 through 3
            wo.ItemMaxLevel = 1;

            var rng = ThreadSafeRandom.Next(1, 7);

            if (rng > 4)
            {
                if (rng > 6)
                    wo.ItemMaxLevel = 3;
                else
                    wo.ItemMaxLevel = 2;
            }

            // Perform an additional roll check for a chance at a higher cloak level for tiers 6+
            if (tier > 6)
            {
                if (ThreadSafeRandom.Next(1, 50) == 1)
                {
                    wo.ItemMaxLevel = 4;
                    if (tier > 7 && ThreadSafeRandom.Next(1, 5) == 1)
                    {
                        wo.ItemMaxLevel = 5;
                    }
                }
            }

            // Initial roll for an cloak damage ratings level 1 through 3
            wo.DamageRating = 0;
            wo.DamageResistRating = 0;

            var rng2 = ThreadSafeRandom.Next(1, 100);

            if (rng2 > 30)
            {
                if (rng2 > 50)
                    wo.DamageResistRating = ThreadSafeRandom.Next(0, 2);
                else
                    wo.DamageRating = ThreadSafeRandom.Next(0, 2);
            }

            // Perform an additional roll check for a chance at a higher cloak damage rating for tiers 6+
            if (tier > 6)
            {
                if (ThreadSafeRandom.Next(1, 200) == 1)
                {
                    wo.DamageResistRating = 3;
                    if (tier > 7 && ThreadSafeRandom.Next(1, 10) == 1)
                    {
                        wo.DamageRating = 3;
                    }
                }
            }

            switch (wo.ItemMaxLevel)
            {
                case 1:
                    wo.IconOverlayId = cloaksIconOverlayOne;
                    wo.WieldDifficulty = 30;
                    break;
                case 2:
                    wo.IconOverlayId = cloaksIconOverlayTwo;
                    wo.WieldDifficulty = 60;
                    break;
                case 3:
                    wo.IconOverlayId = cloaksIconOverlayThree;
                    wo.WieldDifficulty = 90;
                    break;
                case 4:
                    wo.IconOverlayId = cloaksIconOverlayFour;
                    wo.WieldDifficulty = 120;
                    break;
                default:
                    wo.IconOverlayId = cloaksIconOverlayFive;
                    wo.WieldDifficulty = 150;
                    break;
            }

            switch (wo.DamageRating)
            {
                case 0:
                    wo.GearDamage = 0;
                    break;
                case 1:
                    wo.GearDamage = 1;
                    break;
                case 2:
                    wo.GearDamage = 2;
                    break;
                case 3:
                    wo.GearDamage = 3;
                    break;
            }

            switch (wo.DamageResistRating)
            {
                case 0:
                    wo.GearDamageResist = 0;
                    break;
                case 1:
                    wo.GearDamageResist = 1;
                    break;
                case 2:
                    wo.GearDamageResist = 2;
                    break;
                case 3:
                    wo.GearDamageResist = 3;
                    break;
            }

            // rng select a weaving set 49-88
            var equipSet = ThreadSafeRandom.Next(49, 88);
            wo.EquipmentSetId = (ACE.Entity.Enum.EquipmentSet)equipSet;

            // rng select a surge spell 1783-1789 rings, 5361 void ring, 5753-5756 skill buf/dbf, 6188-6196 ring II, rings based on player war skill instead of spellcraft
            var surgeSpell = ThreadSafeRandom.Next(6188, 6196);
            wo.ProcSpell = (uint)(ACE.Entity.Enum.SpellId)surgeSpell;

            int materialType = GetMaterialType(wo, profile.Tier);
            if (materialType > 0)
            wo.MaterialType = (MaterialType)materialType;

            int workmanship = GetWorkmanship(profile.Tier);
            wo.ItemWorkmanship = workmanship;

            int gemCount = ThreadSafeRandom.Next(1, 6);
            int gemType = ThreadSafeRandom.Next(10, 50);
            wo.GemCount = gemCount;
            wo.GemType = (MaterialType)gemType;
            double materialMod = LootTables.getMaterialValueModifier(wo);
            double gemMaterialMod = LootTables.getGemMaterialValueModifier(wo);
            var value = GetValue(profile.Tier, workmanship, gemMaterialMod, materialMod);
            wo.Value = value;

        }

        private static bool GetMutateCloaksData(uint wcid)
        {
            return LootTables.CloaksWcids.Contains(wcid);
        }
    }
}
