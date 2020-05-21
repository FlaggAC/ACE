using System.Numerics;

using log4net;

using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Managers;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Common;

namespace ACE.Server.WorldObjects
{
    public partial class Portal : WorldObject
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// A new biota be created taking all of its values from weenie.
        /// </summary>
        public Portal(Weenie weenie, ObjectGuid guid) : base(weenie, guid)
        {
            SetEphemeralValues();
        }

        /// <summary>
        /// Restore a WorldObject from the database.
        /// </summary>
        public Portal(Biota biota) : base(biota)
        {
            SetEphemeralValues();
        }

        protected void SetEphemeralValues()
        {
            ObjectDescriptionFlags |= ObjectDescriptionFlag.Portal;

            UpdatePortalDestination(Destination);
        }

        public override bool EnterWorld()
        {
            var success = base.EnterWorld();

            if (!success)
            {
                log.Error($"{Name} ({Guid}) failed to spawn @ {Location?.ToLOCString()}");
                return false;
            }

            if (RelativeDestination != null && Location != null && Destination == null)
            {
                var relativeDestination = new Position(Location);
                relativeDestination.Pos += new Vector3(RelativeDestination.PositionX, RelativeDestination.PositionY, RelativeDestination.PositionZ);
                relativeDestination.Rotation = new Quaternion(RelativeDestination.RotationX, relativeDestination.RotationY, relativeDestination.RotationZ, relativeDestination.RotationW);
                relativeDestination.LandblockId = new LandblockId(relativeDestination.GetCell());

                UpdatePortalDestination(relativeDestination);
            }

            return true;
        }

        public void UpdatePortalDestination(Position destination)
        {
            Destination = destination;

            if (PortalShowDestination ?? true)
            {
                AppraisalPortalDestination = Name;

                if (Destination != null)
                {
                    var destCoords = Destination.GetMapCoordStr();
                    if (destCoords != null)
                        AppraisalPortalDestination += $" ({destCoords}).";
                }
            }
        }

        public override void SetLinkProperties(WorldObject wo)
        {
            if (wo.IsLinkSpot)
                SetPosition(PositionType.Destination, new Position(wo.Location));
        }

        public bool IsGateway { get => WeenieClassId == 1955; }

        //public override void OnActivate(WorldObject activator)
        //{
        //    if (activator is Creature creature)
        //        EmoteManager.OnUse(creature);

        //    base.OnActivate(activator);
        //}

        public virtual void OnCollideObject(Player player)
        {
            OnActivate(player);
        }

        public override void OnCastSpell(WorldObject activator)
        {
            if (SpellDID.HasValue)
                base.OnCastSpell(activator);
            else
                ActOnUse(activator);
        }

        public override ActivationResult CheckUseRequirements(WorldObject activator)
        {
            if (!(activator is Player player))
                return new ActivationResult(false);

            if (player.Teleporting)
                return new ActivationResult(false);

            if (Destination == null && WeenieClassId != 777779)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Portal destination for portal ID {WeenieClassId} not yet implemented!", ChatMessageType.System));
                return new ActivationResult(false);
            }

            if (player.PKTimerActive && !PortalIgnoresPkAttackTimer)
            {
                return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouHaveBeenInPKBattleTooRecently));
            }

            if (!player.IgnorePortalRestrictions)
            {
                if (player.Level < MinLevel)
                {
                    // You are not powerful enough to interact with that portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouAreNotPowerfulEnoughToUsePortal));
                }

                if (WeenieClassId == 251000 && player.Enlightenment >= 1 || WeenieClassId == 251000 && player.PKMode)
                {                
                        // You are too powerful to interact with that portal!
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You cannot enter because you are enlightened or in PkMode.", ChatMessageType.System));
                        return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouAreTooPowerfulToUsePortal));
                    
                }

                // disallow entry if non pk
                if (WeenieClassId == 777779 && !player.IsPK)
                {
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.NonPKsMayNotUsePortal));
                }

                // picks a random location and adds as the portals destination
                if (WeenieClassId == 777779 && player.IsPK && !player.PKTimerActive)
                {                    
                    var randomdrop = ThreadSafeRandom.Next(1, 8);

                    if (randomdrop == 1)
                        Destination = new Position(0x576b017f, 0, -50, 18.004999160767f, 0, 0, 0.71442097425461f, -0.69971597194672f);
                    else if (randomdrop == 2)
                        Destination = new Position(0x576b0182, 30, 0, 18.004999160767f, 0, 0, -0.999965f, 0.008408f);
                    else if (randomdrop == 3)
                        Destination = new Position(0x576b0185, 30, -70, 18.004999160767f, 0, 0, 0.020795f, -0.999784f);
                    else if (randomdrop == 4)
                        Destination = new Position(0x576b0186, 50, 0, 18.004999160767f, 0, 0, 1, 0);
                    else if (randomdrop == 5)
                        Destination = new Position(0x576b0189, 49.91189956665f, -70.45719909668f, 18.004999160767f, 0, 0, -0.006596f, -0.999978f);
                    else if (randomdrop == 6)
                        Destination = new Position(0x576b018c, 80, -20, 18.004999160767f, 0, 0, 0.73168897628784f, 0.68163901567459f);
                    else if (randomdrop == 7)
                        Destination = new Position(0x576b018d, 80, -50, 18.004999160767f, 0, 0, 0.65998297929764f, 0.75128102302551f);
                    else if (randomdrop == 8)
                        Destination = new Position(0x576b017e, 0, -20, 18.004999160767f, 0, 0, 0.71442097425461f, -0.69971597194672f);

                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"you have been teleported to one of the random drop locations inside the Control Block Arena!", ChatMessageType.System));
                }

                // 1-100 and 150 dungeon deathcount restriction.
                if (WeenieClassId == 251000  && player.DeathCount > 3 || WeenieClassId == 261005 && player.DeathCount > 3 || WeenieClassId == 261006 && player.DeathCount > 3 || WeenieClassId == 261007 && player.DeathCount > 3 || WeenieClassId == 261008 && player.DeathCount > 3)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You have died too many times consecutively to enter this dungeon. You need to not die for 20 minutes to enter this dungeon.", ChatMessageType.System));
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouCanPossiblySucceed));
                }

                if (player.Level > MaxLevel && MaxLevel != 0)
                {
                    // You are too powerful to interact with that portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouAreTooPowerfulToUsePortal));
                }

                var playerPkLevel = player.PkLevel;

                if (PropertyManager.GetBool("pk_server").Item)
                    playerPkLevel = PKLevel.PK;
                else if (PropertyManager.GetBool("pkl_server").Item)
                    playerPkLevel = PKLevel.PKLite;

                if (PortalRestrictions == PortalBitmask.NotPassable)
                {
                    // Players may not interact with that portal.
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.PlayersMayNotUsePortal));
                }

                if (PortalRestrictions.HasFlag(PortalBitmask.NoPk) && (playerPkLevel == PKLevel.PK || player.PlayerKillerStatus == PlayerKillerStatus.PK))
                {
                    // Player killers may not interact with that portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.PKsMayNotUsePortal));
                }

                if (PortalRestrictions.HasFlag(PortalBitmask.NoPKLite) && (playerPkLevel == PKLevel.PKLite || player.PlayerKillerStatus == PlayerKillerStatus.PKLite))
                {
                    // Lite Player Killers may not interact with that portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.PKLiteMayNotUsePortal));
                }

                if (PortalRestrictions.HasFlag(PortalBitmask.NoNPK) && playerPkLevel == PKLevel.NPK)
                {
                    // Non-player killers may not interact with that portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.NonPKsMayNotUsePortal));
                }

                if (PortalRestrictions.HasFlag(PortalBitmask.OnlyOlthoiPCs) && !player.IsOlthoiPlayer())
                {
                    // Only Olthoi may pass through this portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.OnlyOlthoiMayUsePortal));
                }

                if (PortalRestrictions.HasFlag(PortalBitmask.NoOlthoiPCs) && player.IsOlthoiPlayer())
                {
                    // Olthoi may not pass through this portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.OlthoiMayNotUsePortal));
                }

                if (PortalRestrictions.HasFlag(PortalBitmask.NoVitae) && player.HasVitae)
                {
                    // You may not pass through this portal while Vitae weakens you!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouMayNotUsePortalWithVitae));
                }

                if (PortalRestrictions.HasFlag(PortalBitmask.NoNewAccounts) && !player.Account15Days)
                {
                    // This character must be two weeks old or have been created on an account at least two weeks old to use this portal!
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouMustBeTwoWeeksOldToUsePortal));
                }

                if (player.AccountRequirements < AccountRequirements)
                {
                    // You must purchase Asheron's Call -- Throne of Destiny to use this portal.
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.MustPurchaseThroneOfDestinyToUsePortal));
                }

                if ((AdvocateQuest ?? false) && !player.IsAdvocate)
                {
                    // You must be an Advocate to interact with that portal.
                    return new ActivationResult(new GameEventWeenieError(player.Session, WeenieError.YouMustBeAnAdvocateToUsePortal));
                }
            }

            if (QuestRestriction != null && !player.IgnorePortalRestrictions)
            {
                var hasQuest = player.QuestManager.HasQuest(QuestRestriction);
                var canSolve = player.QuestManager.CanSolve(QuestRestriction);

                var success = hasQuest && !canSolve;

                if (!success)
                {
                    player.QuestManager.HandlePortalQuestError(QuestRestriction);
                    return new ActivationResult(false);
                }
            }

            // handle quest initial flagging
            if (Quest != null)
            {
                player.QuestManager.Update(Quest);
            }

            return new ActivationResult(true);
        }

        public override void ActOnUse(WorldObject activator)
        {
            var player = activator as Player;
            if (player == null) return;

#if DEBUG
            // player.Session.Network.EnqueueSend(new GameMessageSystemChat("Portal sending player to destination", ChatMessageType.System));
#endif
            var portalDest = new Position(Destination);
            WorldObject.AdjustDungeon(portalDest);

            WorldManager.ThreadSafeTeleport(player, portalDest, new ActionEventDelegate(() =>
            {
                // If the portal just used is able to be recalled to,
                // save the destination coordinates to the LastPortal character position save table
                if (!NoRecall)
                    player.LastPortalDID = OriginalPortal == null ? WeenieClassId : OriginalPortal; // if walking through a summoned portal

                EmoteManager.OnPortal(player);
            }));
        }
    }
}
