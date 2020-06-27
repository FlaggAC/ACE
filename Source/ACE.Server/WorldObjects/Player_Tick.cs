using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using ACE.Common;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Factories;
using ACE.Server.Managers;
using ACE.Server.Network.Enum;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Network.Sequence;
using ACE.Server.Network.Structure;
using ACE.Server.Physics;
using ACE.Server.Physics.Common;
using ACE.Server.Entity.Mutators;

namespace ACE.Server.WorldObjects
{
    partial class Player
    {
        private readonly ActionQueue actionQueue = new ActionQueue();

        private int initialAge;
        private DateTime initialAgeTime;

        private const double ageUpdateInterval = 7;
        private double nextAgeUpdateTime;


        public static HashSet<uint> noobzonekillblocks = new HashSet<uint>
                {
                    0x003F0140, 0x003F0141, 0x003F0142, 0x003F0143, 0x003F0144, 0x003F0145, 0x003F013C, 0x003F013D, 0x003F013E, 0x003F013F, 0x003F013B, 0x003F013A, 0x003F0139, 0x003F0138, 0x003F0137, 0x003F0132, 0x003F0133, 0x003F0134, 0x003F0135, 0x003F0136
                  , 0x003F018A, 0x003F01CF, 0x003F027B, 0x003F026E, 0x003F0267, 0x003F0266, 0x003F0263, 0x003F0264, 0x003F0255, 0x003F0253, 0x003F0252, 0x003F0251, 0x003F024C, 0x003F0260, 0x003F023C, 0x003F0242, 0x003F023F, 0x003F0234, 0x003F0228, 0x003F0225
                  , 0x003F0244, 0x003F025A, 0x003F02A6, 0x003F0298, 0x003F0299, 0x003F029A, 0x003F028C, 0x003F0291, 0x003F029C, 0x003F029F, 0x003F02A2, 0x003F02B4, 0x003F02B5, 0x003F02A0, 0x003F02A3, 0x003F0292, 0x003F0293, 0x003F0295, 0x003F028A, 0x003F0289
                  , 0x003F0286, 0x003F0287, 0x003F0288, 0x003F028B, 0x003F0285, 0x003F0283, 0x003F0284, 0x003F0282, 0x003F027F, 0x003F02A4, 0x003F02A5, 0x003F02A7, 0x003F0248, 0x003F0246, 0x003F0247, 0x003F025B, 0x003F025C, 0x003F025D, 0x003F0249, 0x003F024A
                  , 0x003F0239, 0x003F023B, 0x003F023A, 0x003F024B, 0x003F025F, 0x003F0265, 0x003F0264, 0x003F025E, 0x003F026D, 0x003F0268, 0x003F0269, 0x003F0272, 0x003F026F, 0x003F0278, 0x003F027C, 0x003F022F, 0x003F0222, 0x003F0223, 0x003F021A, 0x003F021B
                  , 0x003F021C, 0x003F021D, 0x003F0224, 0x003F0231, 0x003F017D, 0x003F0182, 0x003F0183, 0x003F0186, 0x003F0189, 0x003F0188, 0x003F0187, 0x003F0184, 0x003F0181, 0x003F0185, 0x003F014B, 0x003F0152, 0x003F0151, 0x003F014A, 0x003F0146, 0x003F0147
                  , 0x003F0148, 0x003F014C, 0x003F0153, 0x003F014D, 0x003F0150, 0x003F0149, 0x003F0235, 0x003F0236, 0x003F0237, 0x003F0238, 0x003F022E, 0x003F022D, 0x003F0230, 0x003F021E, 0x003F020F, 0x003F0201, 0x003F01FF, 0x003F0200, 0x003F0204, 0x003F0206
                  , 0x003F0207, 0x003F0205, 0x003F0210, 0x003F0203, 0x003F0202, 0x003F01FE, 0x003F01FC, 0x003F01FD, 0x003F01FB, 0x003F01F1, 0x003F01EA, 0x003F022D, 0x003F022C, 0x003F0221, 0x003F0220, 0x003F0218, 0x003F020E, 0x003F01C1, 0x003F01BE, 0x003F01C2
                  , 0x003F01BB, 0x003F01BC, 0x003F01BD, 0x003F01B9, 0x003F01B3, 0x003F01B2, 0x003F01B1, 0x003F01B5, 0x003F01BA, 0x003F01B8, 0x003F01B7, 0x003F017C, 0x003F017A, 0x003F0177, 0x003F0178, 0x003F0179, 0x003F0170, 0x003F016F, 0x003F016E, 0x003F0176
                  , 0x003F0124, 0x003F011C, 0x003F011D, 0x003F011E, 0x003F0126, 0x003F0125, 0x003F0219, 0x003F0217, 0x003F0216, 0x003F0214, 0x003F0215, 0x003F0213, 0x003F020B, 0x003F020D, 0x003F020C, 0x003F0209, 0x003F020A, 0x003F0208, 0x003F01F8, 0x003F01FA
                  , 0x003F01F9, 0x003F01F6, 0x003F01F7, 0x003F01F5, 0x003F01ED, 0x003F01B4, 0x003F017B, 0x003F0131, 0x003F010D, 0x003F010C, 0x003F0109, 0x003F0106, 0x003F0103, 0x003F0100, 0x003F0101, 0x003F0102, 0x003F0105, 0x003F0108, 0x003F010B, 0x003F010E
                  , 0x003F010A, 0x003F0107, 0x003F0104, 0x003F0114, 0x003F0110, 0x003F010F, 0x003F0113, 0x003F0111, 0x003F0115, 0x003F0116, 0x003F011B, 0x003F011A, 0x003F0122, 0x003F0123, 0x003F012B, 0x003F012A, 0x003F0130, 0x003F012F, 0x003F012C, 0x003F012D
                  , 0x003F0127, 0x003F0128, 0x003F011F, 0x003F0120, 0x003F0117, 0x003F0118, 0x003F0112
                };

        public void Player_Tick(double currentUnixTime)
        {
            actionQueue.RunActions();

            if (nextAgeUpdateTime <= currentUnixTime)
            {
                nextAgeUpdateTime = currentUnixTime + ageUpdateInterval;

                if (initialAgeTime == DateTime.MinValue)
                {
                    initialAge = Age ?? 1;
                    initialAgeTime = DateTime.UtcNow;
                }

                Age = initialAge + (int)(DateTime.UtcNow - initialAgeTime).TotalSeconds;

                // In retail, this is sent every 7 seconds. If you adjust ageUpdateInterval from 7, you'll need to re-add logic to send this every 7s (if you want to match retail)
                Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(this, PropertyInt.Age, Age ?? 1));
            }

            if (FellowVitalUpdate && Fellowship != null)
            {
                Fellowship.OnVitalUpdate(this);
                FellowVitalUpdate = false;
            }
        }

        private static readonly TimeSpan MaximumTeleportTime = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Called every ~5 seconds for Players
        /// </summary>
        public override void Heartbeat(double currentUnixTime)
        {
            NotifyLandblocks();

            LandblockMutatorTick();

            ManaConsumersTick();

            HandleTargetVitals();

            LifestoneProtectionTick();

            PK_DeathTick();

            GagsTick();

            if (Time.GetUnixTime() > GetProperty(PropertyFloat.PkDeathCountTimer))
                DeathCount = 0;

            if (noobzonekillblocks.Contains(Location.Cell) && Level > 100 && Account.AccessLevel < 2)
            {
                Session.Network.EnqueueSend(new GameMessageSystemChat($"You shouldn't be in this location for your level. Teleporting you to your lifestone.", ChatMessageType.Broadcast));
                PlayerManager.BroadcastToAuditChannel(Session.Player, $"{Name} has been teleported from noob dungeon aftering reaching level 101");
                WorldManager.ThreadSafeTeleport(this, Sanctuary);
            }

            PhysicsObj.ObjMaint.DestroyObjects();

            // Check if we're due for our periodic SavePlayer
            if (LastRequestedDatabaseSave == DateTime.MinValue)
                LastRequestedDatabaseSave = DateTime.UtcNow;

            if (LastRequestedDatabaseSave.AddSeconds(PlayerSaveIntervalSecs) <= DateTime.UtcNow)
                SavePlayerToDatabase();

            if (Teleporting && DateTime.UtcNow > Time.GetDateTimeFromTimestamp(LastTeleportStartTimestamp ?? 0).Add(MaximumTeleportTime))
            {
                if (Session != null)
                    Session.LogOffPlayer(true);
                else
                    LogOut();
            }

            base.Heartbeat(currentUnixTime);
        }

        private void LandblockMutatorTick()
        {
            var mutators = MutatorsForLandblock.GetForPlayer<PlayerMutators.PeriodicTick>(this);
            foreach(var mutator in mutators)
                mutator.Action(this);
        }

        public static float MaxSpeed = 50;
        public static float MaxSpeedSq = MaxSpeed * MaxSpeed;

        public static bool DebugPlayerMoveToStatePhysics = false;

        /// <summary>
        /// Flag indicates if player is doing full physics simulation
        /// </summary>
        public bool FastTick => true;

        /// <summary>
        /// For advanced spellcasting / players glitching around during powersliding,
        /// the reason for this retail bug is from 2 different functions for player movement
        /// 
        /// The client's self-player uses DoMotion/StopMotion
        /// The server and other players on the client use apply_raw_movement
        ///
        /// When a 3+ button powerslide is performed, this bugs out apply_raw_movement,
        /// and causes the player to spin in place. With DoMotion/StopMotion, it performs a powerslide.
        ///
        /// With this option enabled (retail defaults to false), the player's position on the server
        /// will match up closely with the player's client during powerslides.
        ///
        /// Since the client uses apply_raw_movement to simulate the movement of nearby players,
        /// the other players will still glitch around on screen, even with this option enabled.
        ///
        /// If you wish for the positions of other players to be less glitchy, the 'MoveToState_UpdatePosition_Threshold'
        /// can be lowered to achieve that
        /// </summary>

        public void OnMoveToState(MoveToState moveToState)
        {
            if (!FastTick)
                return;

            if (DebugPlayerMoveToStatePhysics)
                Console.WriteLine(moveToState.RawMotionState);

            if (RecordCast.Mode == RecordCastMode.Enabled)
                RecordCast.OnMoveToState(moveToState);

            if (!PhysicsObj.IsMovingOrAnimating)
                PhysicsObj.UpdateTime = PhysicsTimer.CurrentTime;

            if (!PropertyManager.GetBool("client_movement_formula").Item || moveToState.StandingLongJump)
                OnMoveToState_ServerMethod(moveToState);
            else
                OnMoveToState_ClientMethod(moveToState);

            if (MagicState.IsCasting && MagicState.PendingTurnRelease && moveToState.RawMotionState.TurnCommand == 0)
                OnTurnRelease();
        }

        public void OnMoveToState_ClientMethod(MoveToState moveToState)
        {
            var rawState = moveToState.RawMotionState;
            var prevState = LastMoveToState?.RawMotionState ?? RawMotionState.None;

            var mvp = new Physics.Animation.MovementParameters();
            mvp.HoldKeyToApply = rawState.CurrentHoldKey;

            if (!PhysicsObj.IsMovingOrAnimating)
                PhysicsObj.UpdateTime = PhysicsTimer.CurrentTime;

            // ForwardCommand
            if (rawState.ForwardCommand != MotionCommand.Invalid)
            {
                // press new key
                if (prevState.ForwardCommand == MotionCommand.Invalid)
                {
                    PhysicsObj.DoMotion((uint)MotionCommand.Ready, mvp);
                    PhysicsObj.DoMotion((uint)rawState.ForwardCommand, mvp);
                }
                // press alternate key
                else if (prevState.ForwardCommand != rawState.ForwardCommand)
                {
                    PhysicsObj.DoMotion((uint)rawState.ForwardCommand, mvp);
                }
            }
            else if (prevState.ForwardCommand != MotionCommand.Invalid)
            {
                // release key
                PhysicsObj.StopMotion((uint)prevState.ForwardCommand, mvp, true);
            }

            // StrafeCommand
            if (rawState.SidestepCommand != MotionCommand.Invalid)
            {
                // press new key
                if (prevState.SidestepCommand == MotionCommand.Invalid)
                {
                    PhysicsObj.DoMotion((uint)rawState.SidestepCommand, mvp);
                }
                // press alternate key
                else if (prevState.SidestepCommand != rawState.SidestepCommand)
                {
                    PhysicsObj.DoMotion((uint)rawState.SidestepCommand, mvp);
                }
            }
            else if (prevState.SidestepCommand != MotionCommand.Invalid)
            {
                // release key
                PhysicsObj.StopMotion((uint)prevState.SidestepCommand, mvp, true);
            }

            // TurnCommand
            if (rawState.TurnCommand != MotionCommand.Invalid)
            {
                // press new key
                if (prevState.TurnCommand == MotionCommand.Invalid)
                {
                    PhysicsObj.DoMotion((uint)rawState.TurnCommand, mvp);
                }
                // press alternate key
                else if (prevState.TurnCommand != rawState.TurnCommand)
                {
                    PhysicsObj.DoMotion((uint)rawState.TurnCommand, mvp);
                }
            }
            else if (prevState.TurnCommand != MotionCommand.Invalid)
            {
                // release key
                PhysicsObj.StopMotion((uint)prevState.TurnCommand, mvp, true);
            }
        }

        public void OnMoveToState_ServerMethod(MoveToState moveToState)
        {
            var minterp = PhysicsObj.get_minterp();
            minterp.RawState.SetState(moveToState.RawMotionState);

            if (moveToState.StandingLongJump)
            {
                minterp.RawState.ForwardCommand = (uint)MotionCommand.Ready;
                minterp.RawState.SideStepCommand = 0;
            }

            var allowJump = minterp.motion_allows_jump(minterp.InterpretedState.ForwardCommand) == WeenieError.None;

            //PhysicsObj.cancel_moveto();

            minterp.apply_raw_movement(true, allowJump);
        }

        public bool InUpdate;

        public override bool UpdateObjectPhysics()
        {
            try
            {
                stopwatch.Restart();

                bool landblockUpdate = false;

                InUpdate = true;

                // update position through physics engine
                if (RequestedLocation != null)
                {
                    landblockUpdate = UpdatePlayerPosition(RequestedLocation);
                    RequestedLocation = null;
                }

                if (FastTick && PhysicsObj.IsMovingOrAnimating || PhysicsObj.Velocity != Vector3.Zero)
                    UpdatePlayerPhysics();

                InUpdate = false;

                return landblockUpdate;
            }
            finally
            {
                var elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                ServerPerformanceMonitor.AddToCumulativeEvent(ServerPerformanceMonitor.CumulativeEventHistoryType.Player_Tick_UpdateObjectPhysics, elapsedSeconds);
                if (elapsedSeconds >= 1) // Yea, that ain't good....
                    log.Warn($"[PERFORMANCE][PHYSICS] {Guid}:{Name} took {(elapsedSeconds * 1000):N1} ms to process UpdateObjectPhysics() at loc: {Location}");
                else if (elapsedSeconds >= 0.010)
                    log.Debug($"[PERFORMANCE][PHYSICS] {Guid}:{Name} took {(elapsedSeconds * 1000):N1} ms to process UpdateObjectPhysics() at loc: {Location}");
            }
        }

        public void UpdatePlayerPhysics()
        {
            if (DebugPlayerMoveToStatePhysics)
                Console.WriteLine($"{Name}.UpdatePlayerPhysics({PhysicsObj.PartArray.Sequence.CurrAnim.Value.Anim.ID:X8})");

            //Console.WriteLine($"{PhysicsObj.Position.Frame.Origin}");
            //Console.WriteLine($"{PhysicsObj.Position.Frame.get_heading()}");

            PhysicsObj.update_object();

            // handle landblock update?

            // sync ace position?
            Location.Rotation = PhysicsObj.Position.Frame.Orientation;

            if (!FastTick) return;

            // ensure PKLogout position is synced up for other players
            if (PKLogout)
            {
                EnqueueBroadcast(new GameMessageUpdateMotion(this, new Motion(MotionStance.NonCombat, MotionCommand.Ready)));
                PhysicsObj.StopCompletely(true);

                if (!PhysicsObj.IsMovingOrAnimating)
                {
                    SyncLocation();
                    EnqueueBroadcast(new GameMessageUpdatePosition(this));
                }
            }

            // this fixes some differences between client movement (DoMotion/StopMotion) and server movement (apply_raw_movement)
            //
            // scenario: start casting a self-spell, and then immediately start holding the run forward key during the windup
            // on client: player will start running forward after the cast has completed
            // on server: player will stand still
            //
            if (!PhysicsObj.IsMovingOrAnimating && LastMoveToState != null)
            {
                // apply latest MoveToState, if applicable
                //if ((LastMoveToState.RawMotionState.Flags & (RawMotionFlags.ForwardCommand | RawMotionFlags.SideStepCommand | RawMotionFlags.TurnCommand)) != 0)
                if ((LastMoveToState.RawMotionState.Flags & RawMotionFlags.ForwardCommand) != 0)
                {
                    if (DebugPlayerMoveToStatePhysics)
                        Console.WriteLine("Re-applying movement: " + LastMoveToState.RawMotionState.Flags);

                    OnMoveToState(LastMoveToState);
                }
                LastMoveToState = null;
            }

            if (MagicState.IsCasting && MagicState.PendingTurnRelease)
                CheckTurn();
        }

        /// <summary>
        /// The maximum rate UpdatePosition packets from MoveToState will be broadcast for each player
        /// AutonomousPosition still always broadcasts UpdatePosition
        ///  
        /// The default value (1 second) was estimated from this retail video:
        /// https://youtu.be/o5lp7hWhtWQ?t=112
        /// 
        /// If you wish for players to glitch around less during powerslides, lower this value
        /// </summary>
        public static TimeSpan MoveToState_UpdatePosition_Threshold = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Used by physics engine to actually update a player position
        /// Automatically notifies clients of updated position
        /// </summary>
        /// <param name="newPosition">The new position being requested, before verification through physics engine</param>
        /// <returns>TRUE if object moves to a different landblock</returns>
        public bool UpdatePlayerPosition(ACE.Entity.Position newPosition, bool forceUpdate = false)
        {
            //Console.WriteLine($"{Name}.UpdatePlayerPhysics({newPosition}, {forceUpdate}, {Teleporting})");
            bool verifyContact = false;

            // possible bug: while teleporting, client can still send AutoPos packets from old landblock
            if (Teleporting && !forceUpdate) return false;

            // pre-validate movement
            if (!ValidateMovement(newPosition))
            {
                log.Error($"{Name}.UpdatePlayerPosition() - movement pre-validation failed from {Location} to {newPosition}");
                return false;
            }

            try
            {
                if (!forceUpdate) // This is needed beacuse this function might be called recursively
                    stopwatch.Restart();

                var success = true;

                if (PhysicsObj != null)
                {
                    var distSq = Location.SquaredDistanceTo(newPosition);

                    if (distSq > PhysicsGlobals.EpsilonSq)
                    {
                        /*var p = new Physics.Common.Position(newPosition);
                        var dist = PhysicsObj.Position.Distance(p);
                        Console.WriteLine($"Dist: {dist}");*/

                        if (newPosition.Landblock == 0x18A && Location.Landblock != 0x18A)
                            log.Info($"{Name} is getting swanky");

                        if (!Teleporting)
                        {
                            var blockDist = PhysicsObj.GetBlockDist(Location.ObjCellID, newPosition.ObjCellID);

                            // verify movement
                            if (distSq > MaxSpeedSq && blockDist > 1)
                            {
                                //Session.Network.EnqueueSend(new GameMessageSystemChat("Movement error", ChatMessageType.Broadcast));
                                log.Warn($"MOVEMENT SPEED: {Name} trying to move from {Location} to {newPosition}, speed: {Math.Sqrt(distSq)}");
                                return false;
                            }

                            // verify z-pos
                            if (blockDist == 0 && LastGroundPos != null && newPosition.Pos.Z - LastGroundPos.Pos.Z > 10 && DateTime.UtcNow - LastJumpTime > TimeSpan.FromSeconds(1) && GetCreatureSkill(Skill.Jump).Current < 1000)
                                verifyContact = true;
                        }

                        var curCell = LScape.get_landcell(newPosition.LongObjCellID);
                        if (curCell != null)
                        {
                            //if (PhysicsObj.CurCell == null || curCell.ID != PhysicsObj.CurCell.ID)
                                //PhysicsObj.change_cell_server(curCell);

                            PhysicsObj.set_request_pos(newPosition.Pos, newPosition.Rotation, newPosition.Instance, curCell, Location.ObjCellID);
                            if (FastTick)
                                success = PhysicsObj.update_object_server_new();
                            else
                                success = PhysicsObj.update_object_server();

                            if (PhysicsObj.CurCell == null && curCell.ID >> 16 != 0x18A)
                            {
                                PhysicsObj.CurCell = curCell;
                            }

                            if (verifyContact && IsJumping)
                            {
                                log.Warn($"z-pos hacking detected for {Name}, lastGroundPos: {LastGroundPos.ToLOCString()} - requestPos: {newPosition.ToLOCString()}");
                                Location = new ACE.Entity.Position(LastGroundPos);
                                Sequences.GetNextSequence(SequenceType.ObjectForcePosition);
                                SendUpdatePosition();
                                return false;
                            }

                            CheckMonsters();
                        }
                    }
                    else
                        PhysicsObj.Position.Frame.Orientation = newPosition.Rotation;
                }

                // double update path: landblock physics update -> updateplayerphysics() -> update_object_server() -> Teleport() -> updateplayerphysics() -> return to end of original branch
                if (Teleporting && !forceUpdate) return true;

                if (!success) return false;

                var landblockUpdate = Location.Landblock != newPosition.Landblock;

                Location = newPosition;

                if (RecordCast.Mode == RecordCastMode.Enabled)
                    RecordCast.Log($"CurPos: {Location.ToLOCString()}");

                if (RequestedLocationBroadcast || DateTime.UtcNow - LastUpdatePosition >= MoveToState_UpdatePosition_Threshold || FastTick && !PhysicsObj.TransientState.HasFlag(TransientStateFlags.OnWalkable))
                    SendUpdatePosition();
                else
                    Session.Network.EnqueueSend(new GameMessageUpdatePosition(this));

                if (!InUpdate)
                {
                    // todo: improve this logic
                    /*if (CurrentLandblock.Instance > 0)
                    {
                        ClearInstance(CurrentLandblock.LongId);
                    }*/

                    LandblockManager.RelocateObjectForPhysics(this, true);
                }

                return landblockUpdate;
            }
            finally
            {
                if (!forceUpdate) // This is needed beacuse this function might be called recursively
                {
                    var elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                    ServerPerformanceMonitor.AddToCumulativeEvent(ServerPerformanceMonitor.CumulativeEventHistoryType.Player_Tick_UpdateObjectPhysics, elapsedSeconds);
                    if (elapsedSeconds >= 0.100) // Yea, that ain't good....
                        log.Warn($"[PERFORMANCE][PHYSICS] {Guid}:{Name} took {(elapsedSeconds * 1000):N1} ms to process UpdatePlayerPosition() at loc: {Location}");
                    else if (elapsedSeconds >= 0.010)
                        log.Debug($"[PERFORMANCE][PHYSICS] {Guid}:{Name} took {(elapsedSeconds * 1000):N1} ms to process UpdatePlayerPosition() at loc: {Location}");
                }
            }
        }

        public bool ValidateMovement(ACE.Entity.Position newPosition)
        {
            if (CurrentLandblock == null)
                return false;

            if (!Teleporting && Location.Landblock != newPosition.Landblock)
            {
                if (Location.Indoors && newPosition.Indoors)
                    return false;

                if (!Location.Indoors && !newPosition.Indoors)
                    return true;

                if (CurrentLandblock.IsDungeon)
                {
                    var destBlock = LScape.get_landblock(newPosition.ObjCellID);
                    if (destBlock != null && destBlock.IsDungeon)
                        return false;
                }
            }
            return true;
        }

        private bool gagNoticeSent = false;

        public void GagsTick()
        {
            if (IsGagged)
            {
                if (!gagNoticeSent)
                {
                    SendGagNotice();
                    gagNoticeSent = true;
                }

                // check for gag expiration, if expired, remove gag.
                GagDuration -= CachedHeartbeatInterval;

                if (GagDuration <= 0)
                {
                    IsGagged = false;
                    GagTimestamp = 0;
                    GagDuration = 0;
                    SaveBiotaToDatabase();
                    SendUngagNotice();
                    gagNoticeSent = false;
                }
            }
        }

        /// <summary>
        /// Prepare new action to run on this player
        /// </summary>
        public override void EnqueueAction(IAction action)
        {
            actionQueue.EnqueueAction(action);
        }

        /// <summary>
        /// Called every ~5 secs for equipped mana consuming items
        /// </summary>
        public void ManaConsumersTick()
        {
            if (!EquippedObjectsLoaded)
                return;

            var EquippedManaConsumers = EquippedObjects.Where(k =>
                (k.Value.IsAffecting ?? false) &&
                //k.Value.ManaRate.HasValue &&
                k.Value.ItemMaxMana.HasValue &&
                k.Value.ItemCurMana.HasValue &&
                k.Value.ItemCurMana.Value > 0).ToList();

            foreach (var k in EquippedManaConsumers)
            {
                var item = k.Value;

                // this was a bug in lootgen until 7/11/2019, mostly for clothing/armor/shields
                // tons of existing items on servers are in this bugged state, where they aren't ticking mana.
                // this retroactively fixes them when equipped
                // items such as Impious Staff are excluded from this via IsAffecting

                if (item.ManaRate == null)
                {
                    item.ManaRate = LootGenerationFactory.GetManaRate(item);
                    log.Warn($"{Name}.ManaConsumersTick(): {k.Value.Name} ({k.Value.Guid}) fixed missing ManaRate");
                }

                var rate = item.ManaRate.Value;

                if (LumAugItemManaUsage != 0)
                    rate *= GetNegativeRatingMod(LumAugItemManaUsage * 5);

                if (!item.ItemManaConsumptionTimestamp.HasValue) item.ItemManaConsumptionTimestamp = DateTime.UtcNow;
                DateTime mostRecentBurn = item.ItemManaConsumptionTimestamp.Value;

                var timePerBurn = -1 / rate;

                var secondsSinceLastBurn = (DateTime.UtcNow - mostRecentBurn).TotalSeconds;

                var delta = secondsSinceLastBurn / timePerBurn;

                var deltaChopped = (int)Math.Floor(delta);
                var deltaExtra = delta - deltaChopped;

                if (deltaChopped <= 0)
                    continue;

                var timeToAdd = (int)Math.Floor(deltaChopped * timePerBurn);
                item.ItemManaConsumptionTimestamp = mostRecentBurn + new TimeSpan(0, 0, timeToAdd);
                var manaToBurn = Math.Min(item.ItemCurMana.Value, deltaChopped);
                deltaChopped = Math.Clamp(deltaChopped, 0, 10);
                item.ItemCurMana -= deltaChopped;

                if (item.ItemCurMana < 1 || item.ItemCurMana == null)
                {
                    item.IsAffecting = false;
                    var msg = new GameMessageSystemChat($"Your {item.Name} is out of Mana.", ChatMessageType.Magic);
                    var sound = new GameMessageSound(Guid, Sound.ItemManaDepleted);
                    Session.Network.EnqueueSend(msg, sound);
                    if (item.WielderId != null)
                    {
                        if (item.Biota.PropertiesSpellBook != null)
                        {
                            // unsure if these messages / sounds were ever sent in retail,
                            // or if it just purged the enchantments invisibly
                            // doing a delay here to prevent 'SpellExpired' sounds from overlapping with 'ItemManaDepleted'
                            var actionChain = new ActionChain();
                            actionChain.AddDelaySeconds(2.0f);
                            actionChain.AddAction(this, () =>
                            {
                                foreach (var spellId in item.Biota.GetKnownSpellsIds(item.BiotaDatabaseLock))
                                    RemoveItemSpell(item, (uint)spellId);
                            });
                            actionChain.EnqueueChain();
                        }
                    }
                }
                else
                {
                    // get time until empty
                    var secondsUntilEmpty = ((item.ItemCurMana - deltaExtra) * timePerBurn);
                    if (secondsUntilEmpty <= 120 && (!item.ItemManaDepletionMessageTimestamp.HasValue || (DateTime.UtcNow - item.ItemManaDepletionMessageTimestamp.Value).TotalSeconds > 120))
                    {
                        item.ItemManaDepletionMessageTimestamp = DateTime.UtcNow;
                        Session.Network.EnqueueSend(new GameMessageSystemChat($"Your {item.Name} is low on Mana.", ChatMessageType.Magic));
                    }
                }
            }
        }

        public override void HandleMotionDone(uint motionID, bool success)
        {
            //Console.WriteLine($"{Name}.HandleMotionDone({(MotionCommand)motionID}, {success})");

            if (FastTick && MagicState.IsCasting)
                HandleMotionDone_Magic(motionID, success);
        }
    }
}
