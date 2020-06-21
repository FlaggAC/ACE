using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using log4net;

using ACE.Database;
using ACE.Entity.Enum;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Entity.Enum.Properties;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.WorldObjects;
using ACE.Entity;
using ACE.Common;

namespace ACE.Server.Command.Handlers
{
    public static class PlayerCommands
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // pop
        [CommandHandler("pop", AccessLevel.Player, CommandHandlerFlag.None, 0,
            "Show current world population",
            "")]
        public static void HandlePop(Session session, params string[] parameters)
        {
            if (PropertyManager.GetBool("command_pop_enabled").Item || session.AccessLevel != AccessLevel.Player)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Current world population: {PlayerManager.GetActualOnlineCount().ToString()}\n", ChatMessageType.Broadcast));
        }

        [CommandHandler("pkmode", AccessLevel.Player, CommandHandlerFlag.RequiresWorld,
            "Toggle Between Pk Mode and not Pk Mode",
            "")]
        public static void HandlePkMode(Session session, params string[] parameters)
        {
            var trophies = session.Player.GetInventoryItemsOfWCID(60002);
            int stacksizes = 0;

            var trophyarray = trophies.ToArray();

            foreach (var item in trophyarray)
            {
                stacksizes += (int)item.StackSize;
            }

            if (session.Player.Level < 200 && !session.Player.PKMode)
                session.Network.EnqueueSend(new GameMessageSystemChat($"You do not meet the requirements to use this command yet.", ChatMessageType.Broadcast));
            else
            {
                if (Time.GetUnixTime() < session.Player.GetProperty(PropertyFloat.PkModeTimer) && session.Player.GetProperty(PropertyFloat.PkModeTimer) != null)
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have switched between PKMode and Non-PKMode too fast. You must wait 5 minutes between each usage of the command.", ChatMessageType.Broadcast));
                else
                {
                    if (stacksizes < 24 && !session.Player.PKMode && !session.Player.PKMode)
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough PK Trophies in your inventory to enter PKMode. You have {stacksizes}, you need 25 or more.", ChatMessageType.Magic));
                    else
                    {
                        
                        if (session.Player.PKTimerActive)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You have been involved in a pk battle too recently to use this command", ChatMessageType.Magic));
                        else
                        {
                            if (parameters.Length == 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"You need to specify if pk mode is on or off ie. /pkmode on or /pkmode off", ChatMessageType.Broadcast));
                            }
                            else if (parameters[0].Equals("on", StringComparison.OrdinalIgnoreCase))
                            {
                                return; //Pk Mode has been disabled due to overwhelming dislike by the community. 
                                if (session.Player.PKMode == true)
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"You are already in PKMode", ChatMessageType.Broadcast));
                                else
                                {
                                    if (stacksizes > 24)
                                    {
                                        //consumes pk trophies and sets timer.
                                        session.Player.TryConsumeFromInventoryWithNetworking(60002, 25);
                                        session.Player.SetProperty(PropertyFloat.PKModeDuration, Time.GetFutureUnixTime(3600));
                                    }
                                    else
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"You cannot enter PKMode without the proper amount of PK Trophies.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[PKMODE] {session.Player.Name} has entered PKMode!", ChatMessageType.Tell));

                                    session.Player.SetProperty(PropertyBool.PKMode, true);

                                    //stores current non-pkmode values
                                    session.Player.NonPkModeStoredTotalExperience = session.Player.TotalExperience;
                                    session.Player.NonPkModeStoredLevel = session.Player.Level;
                                    session.Player.NonPkModeStoredAvailableExperience = session.Player.AvailableExperience;
                                    session.Player.NonPkModeStoredDelevelXP = session.Player.DelevelXp;
                                    session.Player.NonPkModeStoredAvailableCredits = session.Player.AvailableSkillCredits;

                                    //updates to pk-mode values
                                    if (session.Player.PkModeStoredLevel == null) // handles first toggle   ---> LEVEL
                                    {
                                        session.Player.UpdateProperty(session.Player, PropertyInt.Level, 1);
                                        session.Player.UpdateProperty(session.Player, PropertyInt.DeathLevel, 1);
                                    }
                                    else
                                    {
                                        session.Player.UpdateProperty(session.Player, PropertyInt.Level, session.Player.PkModeStoredLevel);
                                        session.Player.UpdateProperty(session.Player, PropertyInt.DeathLevel, session.Player.PkModeStoredLevel);
                                    }

                                    if (session.Player.PkModeStoredTotalExperience == null) // handles first toggle   ----> TOTAL EXP
                                        session.Player.UpdateProperty(session.Player, PropertyInt64.TotalExperience, 0);
                                    else
                                        session.Player.UpdateProperty(session.Player, PropertyInt64.TotalExperience, session.Player.PkModeStoredTotalExperience);

                                    if (session.Player.GetProperty(PropertyInt.PkModeStoredPkDmgRating) == null) // PKratings are generally 0 as theres no current way to get them.
                                        session.Player.PKDamageRating = 0;
                                    else
                                        session.Player.UpdateProperty(session.Player, PropertyInt.PKDamageRating, session.Player.PkModeStoredPkDmgRating);

                                    if (session.Player.GetProperty(PropertyInt.PkModeStoredPkDmgRedRating) == null) // PKratings are generally 0 as theres no current way to get them.
                                        session.Player.PKDamageResistRating = 0;
                                    else
                                        session.Player.UpdateProperty(session.Player, PropertyInt.PKDamageResistRating, session.Player.PkModeStoredPkDmgRedRating);

                                    // ALWAYS SETS AVAILABLE XP TO 0 for PKMODE as you never gain available xp in this mode.
                                    session.Player.UpdateProperty(session.Player, PropertyInt64.AvailableExperience, 0);

                                    // sets credits to 0 as you cant train skills in this mode
                                    session.Player.UpdateProperty(session.Player, PropertyInt.AvailableSkillCredits, 0);

                                    // ALWAYS SETS DELEVEL TO 0 for PKMODE
                                    session.Player.SetProperty(PropertyInt64.DelevelXp, 0);

                                    session.Player.EnqueueBroadcast(new GameMessageScript(session.Player.Guid, PlayScript.AttribUpRed, 1));

                                    session.Network.EnqueueSend(new GameMessageSystemChat($"PKMode is {(session.Player.PKMode ? "enabled" : "disabled")} You do not gain spendable XP in this mode, and you cannot train or specialize skills. Do /pkmode off after 5 minutes to return to normal.", ChatMessageType.Advancement));
                                    session.Player.SetProperty(PropertyFloat.PkModeTimer, Time.GetFutureUnixTime(300));
                                }
                            }
                            else if (parameters[0].Equals("off", StringComparison.OrdinalIgnoreCase))
                            {
                                if (session.Player.PKMode == false)
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"You are already in Non-PKMode", ChatMessageType.Broadcast));
                                else
                                {
                                    session.Player.SetProperty(PropertyBool.PKMode, false);

                                    //stores current pkmode values
                                    session.Player.PkModeStoredTotalExperience = session.Player.TotalExperience;
                                    session.Player.PkModeStoredLevel = session.Player.Level;
                                    session.Player.PkModeStoredAvailableExperience = session.Player.AvailableExperience;
                                    session.Player.PkModeStoredDelevelXP = session.Player.DelevelXp;
                                    session.Player.PkModeStoredPkDmgRating = session.Player.PKDamageRating;
                                    session.Player.PkModeStoredPkDmgRedRating = session.Player.PKDamageResistRating;

                                    //updates to non-pkmode values

                                    session.Player.UpdateProperty(session.Player, PropertyInt.Level, session.Player.NonPkModeStoredLevel);

                                    session.Player.UpdateProperty(session.Player, PropertyInt.DeathLevel, session.Player.NonPkModeStoredLevel);

                                    session.Player.UpdateProperty(session.Player, PropertyInt64.TotalExperience, session.Player.NonPkModeStoredTotalExperience);

                                    session.Player.UpdateProperty(session.Player, PropertyInt64.AvailableExperience, session.Player.NonPkModeStoredAvailableExperience);

                                    session.Player.UpdateProperty(session.Player, PropertyInt64.DelevelXp, session.Player.NonPkModeStoredDelevelXP);

                                    session.Player.UpdateProperty(session.Player, PropertyInt.AvailableSkillCredits, session.Player.NonPkModeStoredAvailableCredits);

                                    session.Player.UpdateProperty(session.Player, PropertyInt.PKDamageRating, 0);

                                    session.Player.UpdateProperty(session.Player, PropertyInt.PKDamageResistRating, 0);

                                    session.Player.EnqueueBroadcast(new GameMessageScript(session.Player.Guid, PlayScript.AttribDownRed, 1));

                                    session.Network.EnqueueSend(new GameMessageSystemChat($"PKMode is {(session.Player.PKMode ? "enabled" : "disabled")} and you have returned back to normal leveling mode.", ChatMessageType.Broadcast));
                                    session.Player.SetProperty(PropertyFloat.PkModeTimer, Time.GetFutureUnixTime(300));
                                }
                            }
                        }
                    }
                }
            }
        }

        // delevelcheck + live xp feed.
        [CommandHandler("delevelxp", AccessLevel.Player, CommandHandlerFlag.RequiresWorld,
            "commands that control Xp for various actions in realtime, also to check delevelxp",
            "")]
        public static void Handledelevelxp(Session session, params string[] parameters)
        {
            {
                if (parameters.Length == 0)
                {
                    session.Player.XpShow = !session.Player.XpShow;

                    if (session.Player.GetProperty(PropertyBool.XpShow) == false)
                        session.Player.UpdateProperty(session.Player, PropertyBool.XpShow, true);
                    else
                        session.Player.UpdateProperty(session.Player, PropertyBool.XpShow, false);

                    session.Network.EnqueueSend(new GameMessageSystemChat($"Live XP display is {(session.Player.XpShow ? "enabled" : "disabled")}", ChatMessageType.Broadcast));
                }
                else
                {
                    if (parameters[0].Equals("on", StringComparison.OrdinalIgnoreCase))
                    {
                        session.Player.UpdateProperty(session.Player, PropertyBool.XpShow, true);
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Live XP display is {(session.Player.XpShow ? "enabled" : "disabled")}", ChatMessageType.Broadcast));
                    }
                    else if (parameters[0].Equals("check", StringComparison.OrdinalIgnoreCase))
                    {
                        var dlvlxp = session.Player.GetProperty(PropertyInt64.DelevelXp);

                        if (dlvlxp == null)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You have no delevel xp.", ChatMessageType.Broadcast));
                        }
                        else
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You need {dlvlxp:N0} xp to regain your levels back.", ChatMessageType.Broadcast));
                        }
                    }
                    else if (parameters[0].Equals("off", StringComparison.OrdinalIgnoreCase))
                    {
                        session.Player.UpdateProperty(session.Player, PropertyBool.XpShow, false);
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Live XP display is {(session.Player.XpShow ? "enabled" : "disabled")}", ChatMessageType.Broadcast));
                    }
                }
            }
        }

        // quest info (uses GDLe formatting to match plugin expectations)
        [CommandHandler("myquests", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows your quest log")]
        public static void HandleQuests(Session session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("quest_info_enabled").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("The command \"myquests\" is not currently enabled on this server.", ChatMessageType.Broadcast));
                return;
            }

            if (session.Player.QuestManager.Quests.Count == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("Quest list is empty.", ChatMessageType.Broadcast));
                return;
            }

            foreach (var playerQuest in session.Player.QuestManager.Quests)
            {
                var text = "";
                var questName = QuestManager.GetQuestName(playerQuest.QuestName);
                var quest = DatabaseManager.World.GetCachedQuest(questName);
                if (quest == null)
                {
                    //Console.WriteLine($"Couldn't find quest {playerQuest.QuestName}");
                    continue;
                }
                var minDelta = (uint)(quest.MinDelta * PropertyManager.GetDouble("quest_mindelta_rate").Item);
                text += $"{playerQuest.QuestName.ToLower()} - {playerQuest.NumTimesCompleted} solves ({playerQuest.LastTimeCompleted})";
                text += $"\"{quest.Message}\" {quest.MaxSolves} {minDelta}";

                session.Network.EnqueueSend(new GameMessageSystemChat(text, ChatMessageType.Broadcast));
            }
        }

        /// <summary>
        /// For characters/accounts who currently own multiple houses, used to select which house they want to keep
        /// </summary>
        [CommandHandler("house-select", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 1, "For characters/accounts who currently own multiple houses, used to select which house they want to keep")]
        public static void HandleHouseSelect(Session session, params string[] parameters)
        {
            HandleHouseSelect(session, false, parameters);
        }

        public static void HandleHouseSelect(Session session, bool confirmed, params string[] parameters)
        {
            if (!int.TryParse(parameters[0], out var houseIdx))
                return;

            // ensure current multihouse owner
            if (!session.Player.IsMultiHouseOwner(false))
            {
                log.Warn($"{session.Player.Name} tried to /house-select {houseIdx}, but they are not currently a multi-house owner!");
                return;
            }

            // get house info for this index
            var multihouses = session.Player.GetMultiHouses();

            if (houseIdx < 1 || houseIdx > multihouses.Count)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Please enter a number between 1 and {multihouses.Count}.", ChatMessageType.Broadcast));
                return;
            }

            var keepHouse = multihouses[houseIdx - 1];

            // show confirmation popup
            if (!confirmed)
            {
                var houseType = $"{keepHouse.HouseType}".ToLower();;
                var loc = HouseManager.GetCoords(keepHouse.SlumLord.Location);

                var msg = $"Are you sure you want to keep the {houseType} at\n{loc}?";
                session.Player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(session.Player.Guid, () => HandleHouseSelect(session, true, parameters)), msg);
                return;
            }

            // house to keep confirmed, abandon the other houses
            var abandonHouses = new List<House>(multihouses);
            abandonHouses.RemoveAt(houseIdx - 1);

            foreach (var abandonHouse in abandonHouses)
            {
                var house = session.Player.GetHouse(abandonHouse.Guid.Full);

                HouseManager.HandleEviction(house, house.HouseOwner ?? 0, true);
            }

            // set player properties for house to keep
            var player = PlayerManager.FindByGuid(keepHouse.HouseOwner ?? 0, out bool isOnline);
            if (player == null)
            {
                log.Error($"{session.Player.Name}.HandleHouseSelect({houseIdx}) - couldn't find HouseOwner {keepHouse.HouseOwner} for {keepHouse.Name} ({keepHouse.Guid})");
                return;
            }

            player.HouseId = keepHouse.HouseId;
            player.HouseInstance = keepHouse.Guid.Full;

            player.SaveBiotaToDatabase();

            // update house panel for current player
            var actionChain = new ActionChain();
            actionChain.AddDelaySeconds(3.0f);  // wait for slumlord inventory biotas above to save
            actionChain.AddAction(session.Player, session.Player.HandleActionQueryHouse);
            actionChain.EnqueueChain();

            Console.WriteLine("OK");
        }

        [CommandHandler("debugcast", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows debug information about the current magic casting state")]
        public static void HandleDebugCast(Session session, params string[] parameters)
        {
            session.Network.EnqueueSend(new GameMessageSystemChat(GetDebugCast(session), ChatMessageType.Broadcast));
        }

        public static string GetDebugCast(Session session)
        {
            var physicsObj = session.Player.PhysicsObj;

            var pendingActions = physicsObj.MovementManager.MoveToManager.PendingActions;
            var sequence = physicsObj.PartArray.Sequence;

            var str = session.Player.MagicState.ToString();
            str += $"\nIsMovingOrAnimating: {physicsObj.IsMovingOrAnimating}";
            str += $"\n- IsAnimating: {physicsObj.IsAnimating}";
            str += $"\n- IsFirstCyclic: {!physicsObj.PartArray.Sequence.is_first_cyclic()}";
            str += $"\n- CachedVelocity: {physicsObj.CachedVelocity != Vector3.Zero}";
            str += $"\n- Velocity: {physicsObj.Velocity != Vector3.Zero}";
            str += $"\n- InterpretedState.HasCommands: {physicsObj.MovementManager.MotionInterpreter.InterpretedState.HasCommands()}";
            str += $"\n- MoveToManager: {physicsObj.MovementManager.MoveToManager.Initialized}";
            str += $"\nCurCell: {physicsObj.CurCell?.ID:X8}";
            str += $"\nPendingActions: {pendingActions.Count}";
            str += $"\nAnimList: {string.Join(", ", sequence.AnimList.Select(i => i.Anim.ID.ToString("X8")))}";
            str += $"\nFirstCyclic: {sequence.FirstCyclic?.Value.Anim.ID:X8}";
            str += $"\nCurrAnim: {sequence.CurrAnim?.Value.Anim.ID:X8}";

            return str;
        }

        [CommandHandler("fixcast", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Fixes magic casting if locked up for an extended time")]
        public static void HandleFixCast(Session session, params string[] parameters)
        {
            var magicState = session.Player.MagicState;

            if (magicState.IsCasting && DateTime.UtcNow - magicState.StartTime > TimeSpan.FromSeconds(5))
            {
                var debugCast = GetDebugCast(session);

                DebugAnimQueue(session);

                session.Player.RecordCast.ShowInfo(debugCast);

                session.Network.EnqueueSend(new GameEventCommunicationTransientString(session, "Fixed casting state"));
                session.Player.SendUseDoneEvent();
                magicState.OnCastDone();
            }
        }

        public static void DebugAnimQueue(Session session)
        {
            session.Player.PhysicsObj.DebugAnim = true;

            for (var i = 0; i < 5; i++)
            {
                session.Player.RecordCast.Log($"DebugAnimQueue({i})");
                session.Player.PhysicsObj.UpdateTime = Physics.Common.PhysicsTimer.CurrentTime - 1.0f;
                session.Player.PhysicsObj.update_object();
            }

            session.Player.PhysicsObj.DebugAnim = false;
        }

        [CommandHandler("castmeter", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows the fast casting efficiency meter")]
        public static void HandleCastMeter(Session session, params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                session.Player.MagicState.CastMeter = !session.Player.MagicState.CastMeter;
            }
            else
            {
                if (parameters[0].Equals("on", StringComparison.OrdinalIgnoreCase))
                    session.Player.MagicState.CastMeter = true;
                else
                    session.Player.MagicState.CastMeter = false;
            }
            session.Network.EnqueueSend(new GameMessageSystemChat($"Cast efficiency meter {(session.Player.MagicState.CastMeter ? "enabled" : "disabled")}", ChatMessageType.Broadcast));
        }

        private static List<string> configList = new List<string>()
        {
            "Common settings:\nConfirmVolatileRareUse, MainPackPreferred, SalvageMultiple, SideBySideVitals, UseCraftSuccessDialog",
            "Interaction settings:\nAcceptLootPermits, AllowGive, AppearOffline, AutoAcceptFellowRequest, DragItemOnPlayerOpensSecureTrade, FellowshipShareLoot, FellowshipShareXP, IgnoreAllegianceRequests, IgnoreFellowshipRequests, IgnoreTradeRequests, UseDeception",
            "UI settings:\nCoordinatesOnRadar, DisableDistanceFog, DisableHouseRestrictionEffects, DisableMostWeatherEffects, FilterLanguage, LockUI, PersistentAtDay, ShowCloak, ShowHelm, ShowTooltips, SpellDuration, TimeStamp, ToggleRun, UseMouseTurning",
            "Chat settings:\nHearAllegianceChat, HearGeneralChat, HearLFGChat, HearRoleplayChat, HearSocietyChat, HearTradeChat, HearPKDeaths, StayInChatMode",
            "Combat settings:\nAdvancedCombatUI, AutoRepeatAttack, AutoTarget, LeadMissileTargets, UseChargeAttack, UseFastMissiles, ViewCombatTarget, VividTargetingIndicator",
            "Character display settings:\nDisplayAge, DisplayAllegianceLogonNotifications, DisplayChessRank, DisplayDateOfBirth, DisplayFishingSkill, DisplayNumberCharacterTitles, DisplayNumberDeaths"
        };

        /// <summary>
        /// Mapping of GDLE -> ACE CharacterOptions
        /// </summary>
        private static Dictionary<string, string> translateOptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Common
            { "ConfirmVolatileRareUse", "ConfirmUseOfRareGems" },
            { "MainPackPreferred", "UseMainPackAsDefaultForPickingUpItems" },
            { "SalvageMultiple", "SalvageMultipleMaterialsAtOnce" },
            { "SideBySideVitals", "SideBySideVitals" },
            { "UseCraftSuccessDialog", "UseCraftingChanceOfSuccessDialog" },

            // Interaction
            { "AcceptLootPermits", "AcceptCorpseLootingPermissions" },
            { "AllowGive", "LetOtherPlayersGiveYouItems" },
            { "AppearOffline", "AppearOffline" },
            { "AutoAcceptFellowRequest", "AutomaticallyAcceptFellowshipRequests" },
            { "DragItemOnPlayerOpensSecureTrade", "DragItemToPlayerOpensTrade" },
            { "FellowshipShareLoot", "ShareFellowshipLoot" },
            { "FellowshipShareXP", "ShareFellowshipExpAndLuminance" },
            { "IgnoreAllegianceRequests", "IgnoreAllegianceRequests" },
            { "IgnoreFellowshipRequests", "IgnoreFellowshipRequests" },
            { "IgnoreTradeRequests", "IgnoreAllTradeRequests" },
            { "UseDeception", "AttemptToDeceiveOtherPlayers" },

            // UI
            { "CoordinatesOnRadar", "ShowCoordinatesByTheRadar" },
            { "DisableDistanceFog", "DisableDistanceFog" },
            { "DisableHouseRestrictionEffects", "DisableHouseRestrictionEffects" },
            { "DisableMostWeatherEffects", "DisableMostWeatherEffects" },
            { "FilterLanguage", "FilterLanguage" },
            { "LockUI", "LockUI" },
            { "PersistentAtDay", "AlwaysDaylightOutdoors" },
            { "ShowCloak", "ShowYourCloak" },
            { "ShowHelm", "ShowYourHelmOrHeadGear" },
            { "ShowTooltips", "Display3dTooltips" },
            { "SpellDuration", "DisplaySpellDurations" },
            { "TimeStamp", "DisplayTimestamps" },
            { "ToggleRun", "RunAsDefaultMovement" },
            { "UseMouseTurning", "UseMouseTurning" },

            // Chat
            { "HearAllegianceChat", "ListenToAllegianceChat" },
            { "HearGeneralChat", "ListenToGeneralChat" },
            { "HearLFGChat", "ListenToLFGChat" },
            { "HearRoleplayChat", "ListentoRoleplayChat" },
            { "HearSocietyChat", "ListenToSocietyChat" },
            { "HearTradeChat", "ListenToTradeChat" },
            { "HearPKDeaths", "ListenToPKDeathMessages" },
            { "StayInChatMode", "StayInChatModeAfterSendingMessage" },

            // Combat
            { "AdvancedCombatUI", "AdvancedCombatInterface" },
            { "AutoRepeatAttack", "AutoRepeatAttacks" },
            { "AutoTarget", "AutoTarget" },
            { "LeadMissileTargets", "LeadMissileTargets" },
            { "UseChargeAttack", "UseChargeAttack" },
            { "UseFastMissiles", "UseFastMissiles" },
            { "ViewCombatTarget", "KeepCombatTargetsInView" },
            { "VividTargetingIndicator", "VividTargetingIndicator" },

            // Character Display
            { "DisplayAge", "AllowOthersToSeeYourAge" },
            { "DisplayAllegianceLogonNotifications", "ShowAllegianceLogons" },
            { "DisplayChessRank", "AllowOthersToSeeYourChessRank" },
            { "DisplayDateOfBirth", "AllowOthersToSeeYourDateOfBirth" },
            { "DisplayFishingSkill", "AllowOthersToSeeYourFishingSkill" },
            { "DisplayNumberCharacterTitles", "AllowOthersToSeeYourNumberOfTitles" },
            { "DisplayNumberDeaths", "AllowOthersToSeeYourNumberOfDeaths" },
        };

        /// <summary>
        /// Manually sets a character option on the server. Use /config list to see a list of settings.
        /// </summary>
        [CommandHandler("config", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 1, "Manually sets a character option on the server.\nUse /config list to see a list of settings.", "<setting> <on/off>")]
        public static void HandleConfig(Session session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("player_config_command").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("The command \"config\" is not currently enabled on this server.", ChatMessageType.Broadcast));
                return;
            }

            // /config list - show character options
            if (parameters[0].Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var line in configList)
                    session.Network.EnqueueSend(new GameMessageSystemChat(line, ChatMessageType.Broadcast));

                return;
            }

            // translate GDLE CharacterOptions for existing plugins
            if (!translateOptions.TryGetValue(parameters[0], out var param) || !Enum.TryParse(param, out CharacterOption characterOption))
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Unknown character option: {parameters[0]}", ChatMessageType.Broadcast));
                return;
            }

            var option = session.Player.GetCharacterOption(characterOption);

            // modes of operation:
            // on / off / toggle

            // - if none specified, default to toggle
            var mode = "toggle";

            if (parameters.Length > 1)
            {
                if (parameters[1].Equals("on", StringComparison.OrdinalIgnoreCase))
                    mode = "on";
                else if (parameters[1].Equals("off", StringComparison.OrdinalIgnoreCase))
                    mode = "off";
            }

            // set character option
            if (mode.Equals("on"))
                option = true;
            else if (mode.Equals("off"))
                option = false;
            else
                option = !option;

            session.Player.SetCharacterOption(characterOption, option);

            session.Network.EnqueueSend(new GameMessageSystemChat($"Character option {parameters[0]} is now {(option ? "on" : "off")}.", ChatMessageType.Broadcast));

            // update client
            session.Network.EnqueueSend(new GameEventPlayerDescription(session));
        }

        /// <summary>
        /// Force resend of all visible objects known to this player. Can fix rare cases of invisible object bugs.
        /// Can only be used once every 5 mins max.
        /// </summary>
        [CommandHandler("objsend", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Force resend of all visible objects known to this player. Can fix rare cases of invisible object bugs. Can only be used once every 5 mins max.")]
        public static void HandleObjSend(Session session, params string[] parameters)
        {
            // a good repro spot for this is the first room after the door in facility hub
            // in the portal drop / staircase room, the VisibleCells do not have the room after the door
            // however, the room after the door *does* have the portal drop / staircase room in its VisibleCells (the inverse relationship is imbalanced)
            // not sure how to fix this atm, seems like it triggers a client bug..

            if (DateTime.UtcNow - session.Player.PrevObjSend < TimeSpan.FromMinutes(5))
            {
                session.Player.SendTransientError("You have used this command too recently!");
                return;
            }

            var knownObjs = session.Player.GetKnownObjects();

            foreach (var knownObj in knownObjs)
            {
                session.Player.RemoveTrackedObject(knownObj, false);
                session.Player.TrackObject(knownObj);
            }
            session.Player.PrevObjSend = DateTime.UtcNow;
        }

        /*[CommandHandler("debugstance", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Debug logs the most recent stance history")]
       public static void HandleDebugStance(Session session, params string[] parameters)
       {
           session.Player.StanceLog.Show();
       }*/

    }
}
