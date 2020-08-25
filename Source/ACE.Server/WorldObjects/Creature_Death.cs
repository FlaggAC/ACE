using System;
using System.Collections.Generic;
using System.Linq;
using ACE.Common;
using ACE.Database;
using ACE.Database.Models.World;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Factories;
using ACE.Server.Managers;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;


namespace ACE.Server.WorldObjects
{
    partial class Creature
    {
        public TreasureDeath DeathTreasure { get => DeathTreasureType.HasValue ? DatabaseManager.World.GetCachedDeathTreasure(DeathTreasureType.Value) : null; }
        public bool Enltrophy = false;
        
        private bool onDeathEntered = false;

        /// <summary>
        /// Called when a monster or player dies, in conjunction with Die()
        /// </summary>
        /// <param name="lastDamager">The last damager that landed the death blow</param>
        /// <param name="damageType">The damage type for the death message</param>
        /// <param name="criticalHit">True if the death blow was a critical hit, generates a critical death message</param>
        public virtual DeathMessage OnDeath(DamageHistoryInfo lastDamager, DamageType damageType, bool criticalHit = false)
        {
            if (onDeathEntered)
                return GetDeathMessage(lastDamager, damageType, criticalHit);

            onDeathEntered = true;

            IsTurning = false;
            IsMoving = false;

            //QuestManager.OnDeath(lastDamager?.TryGetAttacker());

            if (KillQuest != null)
                OnDeath_HandleKillTask(KillQuest);
            if (KillQuest2 != null)
                OnDeath_HandleKillTask(KillQuest2);
            if (KillQuest3 != null)
                OnDeath_HandleKillTask(KillQuest3);

            if (!IsOnNoDeathXPLandblock)
                OnDeath_GrantXP();

            return GetDeathMessage(lastDamager, damageType, criticalHit);
        }


        public DeathMessage GetDeathMessage(DamageHistoryInfo lastDamager, DamageType damageType, bool criticalHit = false)
        {
            var deathMessage = Strings.GetDeathMessage(damageType, criticalHit);

            if (lastDamager == null || lastDamager.Guid == Guid)
                return Strings.General[1];

            // if killed by a player, send them a message
            if (lastDamager.IsPlayer)
            {
                if (criticalHit && this is Player)
                    deathMessage = Strings.PKCritical[0];

                var killerMsg = string.Format(deathMessage.Killer, Name);

                var playerKiller = lastDamager.TryGetAttacker() as Player;

                if (playerKiller != null)
                    playerKiller.Session.Network.EnqueueSend(new GameEventKillerNotification(playerKiller.Session, killerMsg));
            }
            return deathMessage;
        }

        /// <summary>
        /// Kills a player/creature and performs the full death sequence
        /// </summary>
        public void Die()
        {
            Die(DamageHistory.LastDamager, DamageHistory.TopDamager);
        }

        private bool dieEntered = false;

        /// <summary>
        /// Performs the full death sequence for non-Player creatures
        /// </summary>
        protected virtual void Die(DamageHistoryInfo lastDamager, DamageHistoryInfo topDamager)
        {
            if (dieEntered) return;

            dieEntered = true;

            UpdateVital(Health, 0);

            if (topDamager != null)
            {
                KillerId = topDamager.Guid.Full;
                var killer = topDamager.TryGetAttacker() as Player;

                if (topDamager.IsPlayer)
                {
                    var topDamagerPlayer = topDamager.TryGetAttacker();
                    if (topDamagerPlayer != null)
                        topDamagerPlayer.CreatureKills = (topDamagerPlayer.CreatureKills ?? 0) + 1;

                    

                    if (killer != null && CreatureType == ACE.Entity.Enum.CreatureType.Drudge && Level <= 80)
                    {
                        var packslots = killer.GetFreeInventorySlots();
                        var lessergreaterdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("lessergreaterdrudgeslaying");
                        

                        if (killer.LowDrudgeSlayerCount >= ThreadSafeRandom.Next(200, 1000))
                        {               

                            var slayerng = ThreadSafeRandom.Next(1, 10);

                            if (slayerng <= 6) // %60 for Lesser Lesser
                            {
                                var lesserlesserdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("lesserlesserdrudgeslaying");
                                
                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.TryCreateInInventoryWithNetworking(lesserlesserdrudgeslaying);
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Lesser Lesser Gem of Drudge Slaying", ChatMessageType.Broadcast));                                    
                                }

                            }
                            else if (slayerng >= 7 && slayerng <= 9) // 20% For Normal Lesser
                            {
                                var lesserdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("lesserdrudgeslaying");

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.TryCreateInInventoryWithNetworking(lesserdrudgeslaying);
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Lesser Gem of Drudge Slaying", ChatMessageType.Broadcast));                                    
                                }                               
                            }
                            else if (slayerng == 10) // 10% For Lesser Greater
                            {
                                

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.TryCreateInInventoryWithNetworking(lessergreaterdrudgeslaying);
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Lesser Greater Gem of Drudge Slaying", ChatMessageType.Broadcast));                                    
                                }                              
                            }

                            if (packslots < 1)
                            {
                                killer.LowDrudgeSlayerCount = killer.LowDrudgeSlayerCount;
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You retain your kill count... Make space in your inventory!", ChatMessageType.Broadcast));
                            }
                            else
                            {
                                killer.LowDrudgeSlayerCount = 0;
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your knowledge of Drudge's has increased subtantially, you feel that you know enough to kill them more effectively.", ChatMessageType.Broadcast));
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You manifest this power into a gem into your inventory", ChatMessageType.Broadcast));
                            }                            
                            //killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Lesser Gem of Drudge Slaying", ChatMessageType.Broadcast));
                        }
                        else
                        {                  

                            killer.LowDrudgeSlayerCount++;
                        }
                    } // LESSER DRUDGE SLAYER (Drudges level 80 or less)

                    if (killer != null && CreatureType == ACE.Entity.Enum.CreatureType.Drudge && Level >= 100 && Level <= 135)
                    {
                        var packslots = killer.GetFreeInventorySlots();

                        if (killer.MidDrudgeSlayerCount >= ThreadSafeRandom.Next(1000, 2000))
                        {
                            killer.MidDrudgeSlayerCount = 0;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your knowledge of Drudge's has increased subtantially, you feel that you know enough to kill them more effectively.", ChatMessageType.Broadcast));
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You manifest this power into a gem into your inventory", ChatMessageType.Broadcast));
                            //killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Lesser Gem of Drudge Slaying", ChatMessageType.Broadcast));

                            var slayerng = ThreadSafeRandom.Next(1, 10);

                            if (slayerng <= 6) // %50 for Lesser Lesser
                            {
                                var moderatelesserdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("moderatelesserdrudgeslaying");

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Moderate Lesser Gem of Drudge Slaying", ChatMessageType.Broadcast));
                                    killer.TryCreateInInventoryWithNetworking(moderatelesserdrudgeslaying);
                                }

                                    
                            }
                            else if (slayerng >= 7 && slayerng <= 9) // 30% For Normal Lesser
                            {
                                var moderatedrudgeslaying = WorldObjectFactory.CreateNewWorldObject("moderatedrudgeslaying");

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Moderate Gem of Drudge Slaying", ChatMessageType.Broadcast));
                                    killer.TryCreateInInventoryWithNetworking(moderatedrudgeslaying);
                                }
                                    
                            }
                            else if (slayerng == 10) // 10% For Lesser Greater
                            {
                                var moderategreaterdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("moderategreaterdrudgeslaying");

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Moderate Greater Gem of Drudge Slaying", ChatMessageType.Broadcast));
                                    killer.TryCreateInInventoryWithNetworking(moderategreaterdrudgeslaying);
                                }                                    
                            }

                            if (packslots < 1)
                            {
                                killer.MidDrudgeSlayerCount = killer.MidDrudgeSlayerCount;
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You retain your kill count... Make space in your inventory!", ChatMessageType.Broadcast));
                            }
                            else
                            {
                                killer.MidDrudgeSlayerCount = 0;
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your knowledge of Drudge's has increased subtantially, you feel that you know enough to kill them more effectively.", ChatMessageType.Broadcast));
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You manifest this power into a gem into your inventory", ChatMessageType.Broadcast));
                            }
                        }
                        else
                        {
                            killer.MidDrudgeSlayerCount++;
                        }
                    } // MODERATE DRUDGE SLAYER (Drudges level 100-135)

                    if (killer != null && CreatureType == ACE.Entity.Enum.CreatureType.Drudge && Level >= 160)
                    {
                        var packslots = killer.GetFreeInventorySlots();
                        if (killer.HighDrudgeSlayerCount >= ThreadSafeRandom.Next(2000,5000))
                        {
                            killer.HighDrudgeSlayerCount = 0;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your knowledge of Drudge's has increased subtantially, you feel that you know enough to kill them more effectively.", ChatMessageType.Broadcast));
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You manifest this power into a gem into your inventory", ChatMessageType.Broadcast));
                            //killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Lesser Gem of Drudge Slaying", ChatMessageType.Broadcast));

                            var slayerng = ThreadSafeRandom.Next(1, 10);

                            if (slayerng <= 6) // %50 for Lesser Lesser
                            {
                                var greaterlesserdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("greaterlesserdrudgeslaying");

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Greater Lesser Gem of Drudge Slaying", ChatMessageType.Broadcast));
                                    killer.TryCreateInInventoryWithNetworking(greaterlesserdrudgeslaying);
                                }
                                    
                            }
                            else if (slayerng >= 7 && slayerng <= 9) // 30% For Normal Lesser
                            {
                                var greaterdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("greaterdrudgeslaying");

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Greater Gem of Drudge Slaying", ChatMessageType.Broadcast));
                                    killer.TryCreateInInventoryWithNetworking(greaterdrudgeslaying);
                                }

                                    
                            }
                            else if (slayerng == 10) // 10% For Lesser Greater
                            {
                                var greatergreaterdrudgeslaying = WorldObjectFactory.CreateNewWorldObject("greatergreaterdrudgeslaying");

                                if (packslots < 1)
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You dont have enough pack slots open to receive the item...", ChatMessageType.Broadcast));
                                }
                                else
                                {
                                    killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You receive a Greater Greater Gem of Drudge Slaying", ChatMessageType.Broadcast));
                                    killer.TryCreateInInventoryWithNetworking(greatergreaterdrudgeslaying);
                                }                                    
                            }

                            if (packslots < 1)
                            {
                                killer.HighDrudgeSlayerCount = killer.HighDrudgeSlayerCount;
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You retain your kill count... Make space in your inventory!", ChatMessageType.Broadcast));
                            }
                            else
                            {
                                killer.HighDrudgeSlayerCount = 0;
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your knowledge of Drudge's has increased subtantially, you feel that you know enough to kill them more effectively.", ChatMessageType.Broadcast));
                                killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"You manifest this power into a gem into your inventory", ChatMessageType.Broadcast));
                            }
                        }
                        else
                        {
                            killer.HighDrudgeSlayerCount++;
                        }
                    } // GREATER DRUDGE SLAYER (Drudges level 160+)

                    var trophypercentenl = ThreadSafeRandom.Next(1, 20);
                    if (killer != null && Level >= 130)
                    {                       
                        if (killer.Enlightenment == 1 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 5.0f && WeenieClassId != 151001) // 1% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 2 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 10.0f && WeenieClassId != 151001) // 2% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 3 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 15.0f && WeenieClassId != 151001) // 3% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 4 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 20.0f && WeenieClassId != 151001) // 4% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 5 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 25.0f && WeenieClassId != 151001) // 5% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 6 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 30.0f && WeenieClassId != 151001) // 6% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 7 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 35.0f && WeenieClassId != 151001) // 7% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 8 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 40.0f && WeenieClassId != 151001) // 8% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment == 9 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 45.0f && WeenieClassId != 151001) // 9% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                        else if (killer.Enlightenment >= 10 && ThreadSafeRandom.Next(1.0f, 500.0f) <= 50.0f && WeenieClassId != 151001) // 10% on all other Mobs if Enlightened
                        {
                            Enltrophy = true;
                            killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your enlightened state of mind is so powerful that you generated a Pk Trophy on the monster you just killed!", ChatMessageType.Broadcast));
                        }// 0.4% on all other Mobs if Enlightened
                    }// Trophy Enlightenment

                    /*
                    //handles elite split mod.
                    if (IsElite && SplitMod)
                    {
                        var id = WeenieClassId;

                        for (var i = 0; i < 8; i++)
                        {
                            var splitmob = WorldObjectFactory.CreateNewWorldObject(id) as Creature;
                            splitmob.Location = new Position(Location);

                            var name = splitmob.GetProperty(PropertyString.Name);

                            splitmob.Name = "Split " + name;
                            splitmob.SetProperty(PropertyBool.IsElite, false);
                            splitmob.SetProperty(PropertyBool.EliteTrigger, false);
                            splitmob.SetProperty(PropertyBool.IsMini, true);
                            splitmob.ResistAcid = 2;
                            splitmob.ResistFire = 2;
                            splitmob.ResistCold = 2;
                            splitmob.ResistElectric = 2;
                            splitmob.ResistPierce = 2;
                            splitmob.ResistBludgeon = 2;
                            splitmob.ResistSlash = 2;
                            splitmob.ResistNether = 2;
                            splitmob.ObjScale = (splitmob.ObjScale ?? 1.0f) - 0.3f;
                            splitmob.Tolerance = Tolerance.None; // will make all mobs without tolerence immediately target something and attack
                            splitmob.EnterWorld();
                        }

                        killer.Session.Network.EnqueueSend(new GameMessageSystemChat($"The Elite Mob Explodes into smaller versions of itself!", ChatMessageType.Broadcast));
                    }
                    */
                }
            }

            CurrentMotionState = new Motion(MotionStance.NonCombat, MotionCommand.Ready);
            //IsMonster = false;

            PhysicsObj.StopCompletely(true);

            // broadcast death animation
            var motionDeath = new Motion(MotionStance.NonCombat, MotionCommand.Dead);
            var deathAnimLength = ExecuteMotion(motionDeath);

            EmoteManager.OnDeath(lastDamager);

            var dieChain = new ActionChain();

            // wait for death animation to finish
            //var deathAnimLength = DatManager.PortalDat.ReadFromDat<MotionTable>(MotionTableId).GetAnimationLength(MotionCommand.Dead);
            dieChain.AddDelaySeconds(deathAnimLength);

            dieChain.AddAction(this, () =>
            {
                CreateCorpse(topDamager);
                Destroy();
            });

            dieChain.EnqueueChain();
        }

        /// <summary>
        /// Called when an admin player uses the /smite command
        /// to instantly kill a creature
        /// </summary>
        public void Smite(WorldObject smiter, bool useTakeDamage = false)
        {
            if (useTakeDamage)
            {
                // deal remaining damage
                TakeDamage(smiter, DamageType.Bludgeon, Health.Current);
            }
            else
            {
                OnDeath();
                var smiterInfo = new DamageHistoryInfo(smiter);
                Die(smiterInfo, smiterInfo);
            }
        }

        public void OnDeath()
        {
            OnDeath(null, DamageType.Undef);
        }

        /// <summary>
        /// Grants XP to players in damage history
        /// </summary>
        public void OnDeath_GrantXP()
        {
            if (this is Player && PlayerKillerStatus == PlayerKillerStatus.PKLite)
                return;

            var totalHealth = DamageHistory.TotalHealth;

            if (totalHealth == 0)
                return;

            foreach (var kvp in DamageHistory.TotalDamage)
            {
                var damager = kvp.Value.TryGetAttacker();

                var playerDamager = damager as Player;

                if (playerDamager == null && kvp.Value.PetOwner != null)
                    playerDamager = kvp.Value.TryGetPetOwner();

                if (playerDamager == null)
                    continue;

                var totalDamage = kvp.Value.TotalDamage;

                var damagePercent = totalDamage / totalHealth;

                var totalXP = (XpOverride ?? 0) * damagePercent;

                playerDamager.EarnXP((long)Math.Round(totalXP), XpType.Kill);

                // handle luminance
                if (LuminanceAward != null)
                {
                    var totalLuminance = (long)Math.Round(LuminanceAward.Value * damagePercent);
                    playerDamager.EarnLuminance(totalLuminance, XpType.Kill);
                }
            }
        }

        /// <summary>
        /// Handles the KillTask for a killed creature
        /// </summary>
        public void OnDeath_HandleKillTask(string killQuest)
        {
            var receivers = KillTask_GetEligibleReceivers(killQuest);

            foreach (var receiver in receivers)
            {
                var damager = receiver.Value.TryGetAttacker();

                var player = damager as Player;

                if (player == null && receiver.Value.PetOwner != null)
                    player = receiver.Value.TryGetPetOwner();

                if (player != null)
                    player.QuestManager.HandleKillTask(killQuest, this);
            }
        }

        /// <summary>
        /// Returns a flattened structure of eligible Players, Fellows, and CombatPets
        /// </summary>
        public Dictionary<ObjectGuid, DamageHistoryInfo> KillTask_GetEligibleReceivers(string killQuest)
        {
            // http://acpedia.org/wiki/Announcements_-_2012/12_-_A_Growing_Twilight#Release_Notes

            var questName = QuestManager.GetQuestName(killQuest);

            // we are using DamageHistoryInfo here, instead of Creature or WorldObjectInfo
            // WeakReference<CombatPet> may be null for expired CombatPets, but we still need the WeakReference<PetOwner> references

            var receivers = new Dictionary<ObjectGuid, DamageHistoryInfo>();

            foreach (var kvp in DamageHistory.TotalDamage)
            {
                if (kvp.Value.TotalDamage <= 0)
                    continue;

                var damager = kvp.Value.TryGetAttacker();

                var playerDamager = damager as Player;

                if (playerDamager == null && kvp.Value.PetOwner != null)
                {
                    // handle combat pets
                    playerDamager = kvp.Value.TryGetPetOwner();

                    if (playerDamager != null && playerDamager.QuestManager.HasQuest(questName))
                    {
                        // only add combat pet to eligible receivers if player has quest, and allow_summoning_killtask_multicredit = true (default, retail)
                        if (DamageHistory.HasDamager(playerDamager, true) && PropertyManager.GetBool("allow_summoning_killtask_multicredit").Item)
                            receivers[kvp.Value.Guid] = kvp.Value;
                        else
                            receivers[playerDamager.Guid] = new DamageHistoryInfo(playerDamager);
                    }

                    // regardless if combat pet is eligible, we still want to continue traversing to the pet owner, and possibly fellows

                    // in a scenario where combat pet does 100% damage:

                    // - regardless if allow_summoning_killtask_multicredit is enabled/disabled, it should continue traversing into pet owner and possibly their fellows

                    // - if pet owner doesn't have kill task, and fellow_kt_killer=false, any fellows with the task should still receive 1 credit
                }

                if (playerDamager == null)
                    continue;

                // factors:
                // - has quest
                // - is killer (last damager, top damager, or any damager? in current context, considering it to be any damager)
                // - has fellowship
                // - server option: fellow_kt_killer
                // - server option: fellow_kt_landblock

                if (playerDamager.QuestManager.HasQuest(questName))
                {
                    // just add a fake DamageHistoryInfo for reference
                    receivers[playerDamager.Guid] = new DamageHistoryInfo(playerDamager);
                }
                else if (PropertyManager.GetBool("fellow_kt_killer").Item)
                {
                    // if this option is enabled (retail default), the killer is required to have kill task
                    // for it to share with fellowship
                    continue;
                }

                // we want to add fellowship members in a flattened structure
                // in this inner loop, instead of the outer loop

                // scenarios:

                // i am a summoner in a fellowship with 1 other player
                // we both have a killtask

                // - my combatpet does 100% damage to the monster
                // result: i get 1 killtask credit, and my fellow gets 1 killtask credit

                // - my combatpet does 50% damage to monster, and i do 50% damage
                // result: i get 2 killtask credits (1 if allow_summoning_killtask_multicredit server option is disabled), and my fellow gets 1 killtask credit

                // - my combatpet does 33% damage to monster, i do 33% damage, and fellow does 33% damage
                // result: same as previous scenario

                // 2 players not in a fellowship both have a killtask
                // they each do 50% damage to monster

                // result: both players receive killtask credit

                if (playerDamager.Fellowship == null)
                    continue;

                // share with fellows in kill task range
                var fellows = playerDamager.Fellowship.WithinRange(playerDamager);

                foreach (var fellow in fellows)
                {
                    if (fellow.QuestManager.HasQuest(questName))
                        receivers[fellow.Guid] = new DamageHistoryInfo(fellow);
                }
            }
            return receivers;
        }

        /// <summary>
        /// Create a corpse for both creatures and players currently
        /// </summary>
        protected void CreateCorpse(DamageHistoryInfo killer)
        {
            if (NoCorpse)
            {
                var loot = GenerateTreasure(killer, null);

                foreach(var item in loot)
                {
                    item.Location = new Position(Location);
                    LandblockManager.AddObject(item);
                }
                return;
            }

            var cachedWeenie = DatabaseManager.World.GetCachedWeenie("corpse");

            var corpse = WorldObjectFactory.CreateNewWorldObject(cachedWeenie) as Corpse;

            var prefix = "Corpse";

            if (TreasureCorpse)
            {
                // Hardcoded values from PCAPs of Treasure Pile Corpses, everything else lines up exactly with existing corpse weenie
                corpse.SetupTableId  = 0x02000EC4;
                corpse.MotionTableId = 0x0900019B;
                corpse.SoundTableId  = 0x200000C2;
                corpse.ObjScale      = 0.4f;

                prefix = "Treasure";
            }
            else
            {
                corpse.SetupTableId = SetupTableId;
                corpse.MotionTableId = MotionTableId;
                //corpse.SoundTableId = SoundTableId; // Do not change sound table for corpses
                corpse.PaletteBaseDID = PaletteBaseDID;
                corpse.ClothingBase = ClothingBase;
                corpse.PhysicsTableId = PhysicsTableId;

                if (ObjScale.HasValue)
                    corpse.ObjScale = ObjScale;
                if (PaletteTemplate.HasValue)
                    corpse.PaletteTemplate = PaletteTemplate;
                if (Shade.HasValue)
                    corpse.Shade = Shade;
                //if (Translucency.HasValue) // Shadows have Translucency but their corpses do not, videographic evidence can be found on YouTube.
                //corpse.Translucency = Translucency;


                // Pull and save objdesc for correct corpse apperance at time of death
                var objDesc = CalculateObjDesc();

                corpse.Biota.PropertiesAnimPart = objDesc.AnimPartChanges.Clone(corpse.BiotaDatabaseLock);

                corpse.Biota.PropertiesPalette = objDesc.SubPalettes.Clone(corpse.BiotaDatabaseLock);

                corpse.Biota.PropertiesTextureMap = objDesc.TextureChanges.Clone(corpse.BiotaDatabaseLock);
            }

            // use the physics location for accuracy,
            // especially while jumping
            corpse.Location = PhysicsObj.Position.ACEPosition();

            corpse.VictimId = Guid.Full;
            corpse.Name = $"{prefix} of {Name}";

            // set 'killed by' for looting rights
            var killerName = "misadventure";
            if (killer != null)
            {
                if (!(Generator != null && Generator.Guid == killer.Guid) && Guid != killer.Guid)
                {
                    if (!string.IsNullOrWhiteSpace(killer.Name))
                        killerName = killer.Name.TrimStart('+');  // vtank requires + to be stripped for regex matching.

                    corpse.KillerId = killer.Guid.Full;

                    if (killer.PetOwner != null)
                    {
                        var petOwner = killer.TryGetPetOwner();
                        if (petOwner != null)
                            corpse.KillerId = petOwner.Guid.Full;
                    }
                }
            }

            corpse.LongDesc = $"Killed by {killerName}.";

            bool saveCorpse = false;

            var player = this as Player;

            if (player != null)
            {
                corpse.SetPosition(PositionType.Location, corpse.Location);
                var dropped = player.CalculateDeathItems(corpse);
                corpse.RecalculateDecayTime(player);

                if (dropped.Count > 0)
                    saveCorpse = true;

                if ((player.Location.Cell & 0xFFFF) < 0x100)
                {
                    player.SetPosition(PositionType.LastOutsideDeath, new Position(corpse.Location));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePosition(player, PositionType.LastOutsideDeath, corpse.Location));

                    if (dropped.Count > 0)
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your corpse is located at ({corpse.Location.GetMapCoordStr()}).", ChatMessageType.Broadcast));
                }

                var isPKdeath = player.IsPKDeath(killer);
                var isPKLdeath = player.IsPKLiteDeath(killer);

                if (isPKdeath)
                    corpse.PkLevel = PKLevel.PK;

                if (!isPKdeath && !isPKLdeath)
                {
                    var miserAug = player.AugmentationLessDeathItemLoss * 5;
                    if (miserAug > 0)
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Your augmentation has reduced the number of items you can lose by {miserAug}!", ChatMessageType.Broadcast));
                }

                if (dropped.Count == 0 && !isPKLdeath)
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"You have retained all your items. You do not need to recover your corpse!", ChatMessageType.Broadcast));
            }
            else
            {
                corpse.IsMonster = true;
                GenerateTreasure(killer, corpse);

                if (killer != null && killer.IsPlayer)
                {
                    if (Level >= 100)
                    {
                        CanGenerateRare = true;
                    }
                    else
                    {
                        var killerPlayer = killer.TryGetAttacker();
                        if (killerPlayer != null && Level > killerPlayer.Level)
                            CanGenerateRare = true;
                    }
                }
                else
                    CanGenerateRare = false;
            }

            corpse.RemoveProperty(PropertyInt.Value);

            if (CanGenerateRare && killer != null)
                corpse.TryGenerateRare(killer);

            corpse.InitPhysicsObj();

            // persist the original creature velocity (only used for falling) to corpse
            corpse.PhysicsObj.Velocity = PhysicsObj.Velocity;

            corpse.EnterWorld();

            if (player != null)
            {
                if (corpse.PhysicsObj == null || corpse.PhysicsObj.Position == null)
                    log.Debug($"[CORPSE] {Name}'s corpse (0x{corpse.Guid}) failed to spawn! Tried at {player.Location.ToLOCString()}");
                else
                    log.Debug($"[CORPSE] {Name}'s corpse (0x{corpse.Guid}) is located at {corpse.PhysicsObj.Position}");
            }

            if (saveCorpse)
            {
                corpse.SaveBiotaToDatabase();

                foreach (var item in corpse.Inventory.Values)
                    item.SaveBiotaToDatabase();
            }
        }

        public bool CanGenerateRare
        {
            get => GetProperty(PropertyBool.CanGenerateRare) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.CanGenerateRare); else SetProperty(PropertyBool.CanGenerateRare, value); }
        }

        /// <summary>
        /// Transfers generated treasure from creature to corpse
        /// </summary>
        private List<WorldObject> GenerateTreasure(DamageHistoryInfo killer, Corpse corpse)
        {
            var droppedItems = new List<WorldObject>();
            var OnceRare = 1;
            var OncePass = 0;
            var OncePass2 = 0;
            // create death treasure from loot generation factory
            if (DeathTreasure != null)
            {
                List<WorldObject> items = LootGenerationFactory.CreateRandomLootObjects(DeathTreasure);
                foreach (WorldObject wo in items)
                {
                    if (corpse != null)
                    {
                        corpse.TryAddToInventory(wo);

                        if (Enltrophy)
                        {
                            var amntrandom = ThreadSafeRandom.Next(1, 4);
                            var pktrophy = WorldObjectFactory.CreateNewWorldObject("pktrophy");
                            pktrophy.SetStackSize(amntrandom);
                            corpse.TryAddToInventory(pktrophy);
                            Enltrophy = false;
                        }// enlightenment trophy drop

                        if (IsRare && IsDead && OnceRare == 1)
                        {
                            int tier2 = 0;
                            int tier3 = 0;
                            int tier = 0;

                            var rarenum = ThreadSafeRandom.Next(1, 3);

                            //even spread % up to 3 rares
                            if (rarenum == 1)
                            {
                                tier = ThreadSafeRandom.Next(1, 6);
                                var tierRares = LootGenerationFactory.RareWCIDs[tier].ToList();
                                var rng = ThreadSafeRandom.Next(0, tierRares.Count - 1);
                                var rareWCID = tierRares[rng];
                                var rare = WorldObjectFactory.CreateNewWorldObject((uint)rareWCID);

                                corpse.TryAddToInventory(rare);

                                EnqueueBroadcast(new GameMessageSystemChat($"{killer.Name} has killed an Elite with the rare mod, it has generated {rarenum} rare! {rare.Name}!", ChatMessageType.System));
                            }
                            else if (rarenum == 2)
                            {
                                tier = ThreadSafeRandom.Next(1, 6);
                                var tierRares = LootGenerationFactory.RareWCIDs[tier].ToList();
                                var rng = ThreadSafeRandom.Next(0, tierRares.Count - 1);
                                var rareWCID = tierRares[rng];
                                var rare = WorldObjectFactory.CreateNewWorldObject((uint)rareWCID);


                                tier2 = ThreadSafeRandom.Next(1, 6);
                                var tierRares2 = LootGenerationFactory.RareWCIDs[tier2].ToList();
                                var rng2 = ThreadSafeRandom.Next(0, tierRares2.Count - 1);
                                var rareWCID2 = tierRares2[rng2];
                                var rare2 = WorldObjectFactory.CreateNewWorldObject((uint)rareWCID2);

                                corpse.TryAddToInventory(rare);
                                corpse.TryAddToInventory(rare2);

                                EnqueueBroadcast(new GameMessageSystemChat($"{killer.Name} has killed an Elite with the rare mod, it has generated {rarenum} rares! {rare.Name}, and {rare2.Name}!", ChatMessageType.System));
                            }
                            else if (rarenum == 3)
                            {
                                tier = ThreadSafeRandom.Next(1, 5);
                                var tierRares = LootGenerationFactory.RareWCIDs[tier].ToList();
                                var rng = ThreadSafeRandom.Next(0, tierRares.Count - 1);
                                var rareWCID = tierRares[rng];
                                var rare = WorldObjectFactory.CreateNewWorldObject((uint)rareWCID);


                                tier2 = ThreadSafeRandom.Next(1, 5);
                                var tierRares2 = LootGenerationFactory.RareWCIDs[tier2].ToList();
                                var rng2 = ThreadSafeRandom.Next(0, tierRares2.Count - 1);
                                var rareWCID2 = tierRares2[rng2];
                                var rare2 = WorldObjectFactory.CreateNewWorldObject((uint)rareWCID2);


                                tier3 = ThreadSafeRandom.Next(1, 5);
                                var tierRares3 = LootGenerationFactory.RareWCIDs[tier3].ToList();
                                var rng3 = ThreadSafeRandom.Next(0, tierRares3.Count - 1);
                                var rareWCID3 = tierRares3[rng3];
                                var rare3 = WorldObjectFactory.CreateNewWorldObject((uint)rareWCID3);

                                corpse.TryAddToInventory(rare);
                                corpse.TryAddToInventory(rare2);
                                corpse.TryAddToInventory(rare3);

                                EnqueueBroadcast(new GameMessageSystemChat($"{killer.Name} has killed an Elite with the rare mod, it has generated {rarenum} rares! {rare.Name}, {rare2.Name}, and {rare3.Name}", ChatMessageType.System));
                            }

                            OnceRare++;
                        }// rare mobs drop up to 3 rares                       

                        if (IsElite && OncePass == 0 && Level > 99)
                        {
                            OncePass = 1;
                            var fiveusemfk = WorldObjectFactory.CreateNewWorldObject("ace38920-reinforcedmanaforgekey");
                            var amntrandom = ThreadSafeRandom.Next(1, 6);
                            var pktrophy = WorldObjectFactory.CreateNewWorldObject("pktrophy");
                            pktrophy.SetStackSize(amntrandom);
                            corpse.TryAddToInventory(pktrophy);
                            corpse.TryAddToInventory(fiveusemfk);
                        }// Elites level 100+ will drop a reinforced manaforge key.

                        if (IsElite && OncePass2 == 0 && Level > 150)
                        {
                            OncePass2 = 1;

                            var rng1 = ThreadSafeRandom.Next(0f, 1.0f);
                            var rng2 = ThreadSafeRandom.Next(0f, 1.0f);
                            var rng3 = ThreadSafeRandom.Next(0f, 1.0f);

                            int id = LootGenerationFactory.CreateLevel8SpellComp();
                            var spellcomp81 = WorldObjectFactory.CreateNewWorldObject((uint)id);
                            int id2 = LootGenerationFactory.CreateLevel8SpellComp();
                            var spellcomp82 = WorldObjectFactory.CreateNewWorldObject((uint)id2);
                            int id3 = LootGenerationFactory.CreateLevel8SpellComp();
                            var spellcomp83 = WorldObjectFactory.CreateNewWorldObject((uint)id3);
                            int id4 = LootGenerationFactory.CreateLevel8SpellComp();
                            var spellcomp84 = WorldObjectFactory.CreateNewWorldObject((uint)id4);
                            int id5 = LootGenerationFactory.CreateLevel8SpellComp();
                            var spellcomp85 = WorldObjectFactory.CreateNewWorldObject((uint)id5);


                            if (rng1 <= 1)
                            {
                                corpse.TryAddToInventory(spellcomp81);
                                corpse.TryAddToInventory(spellcomp82);
                                corpse.TryAddToInventory(spellcomp83);
                            }// 100% for 3 comps
                            if (rng2 <= .50)
                            {
                                corpse.TryAddToInventory(spellcomp84);
                            }// 50% for 4
                            if (rng3 <= .25)
                            {
                                corpse.TryAddToInventory(spellcomp85);
                            }// 25% for 5
                        }// elites level 150+ drop up to 5 level 8 spell comps
                    }
                    else
                        droppedItems.Add(wo);

                    DoCantripLogging(killer, wo);
                }
            }

            // contain and non-wielded treasure create
            if (Biota.PropertiesCreateList != null)
            {
                var createList = Biota.PropertiesCreateList.Where(i => (i.DestinationType & DestinationType.Contain) != 0 ||
                                (i.DestinationType & DestinationType.Treasure) != 0 && (i.DestinationType & DestinationType.Wield) == 0).ToList();

                var selected = CreateListSelect(createList);

                foreach (var item in selected)
                {
                    var wo = WorldObjectFactory.CreateNewWorldObject(item);

                    if (wo != null)
                    {
                        if (corpse != null)
                            corpse.TryAddToInventory(wo);
                        else
                            droppedItems.Add(wo);
                    }
                }
            }

            // move wielded treasure over, which also should include Wielded objects not marked for destroy on death.
            // allow server operators to configure this behavior due to errors in createlist post 16py data
            var dropFlags = PropertyManager.GetBool("creatures_drop_createlist_wield").Item ? DestinationType.WieldTreasure : DestinationType.Treasure;

            var wieldedTreasure = Inventory.Values.Concat(EquippedObjects.Values).Where(i => (i.DestinationType & dropFlags) != 0);
            foreach (var item in wieldedTreasure.ToList())
            {
                if (item.Bonded == BondedStatus.Destroy)
                    continue;

                if (TryDequipObjectWithBroadcasting(item.Guid, out var wo, out var wieldedLocation))
                    EnqueueBroadcast(new GameMessagePublicUpdateInstanceID(item, PropertyInstanceId.Wielder, ObjectGuid.Invalid));

                if (corpse != null)
                {
                    corpse.TryAddToInventory(item);
                    EnqueueBroadcast(new GameMessagePublicUpdateInstanceID(item, PropertyInstanceId.Container, corpse.Guid), new GameMessagePickupEvent(item));
                }
                else
                    droppedItems.Add(item);
            }

            return droppedItems;
        }

        public void DoCantripLogging(DamageHistoryInfo killer, WorldObject wo)
        {
            var epicCantrips = wo.EpicCantrips;
            var legendaryCantrips = wo.LegendaryCantrips;

            if (epicCantrips.Count > 0)
                log.Debug($"[LOOT][EPIC] {Name} ({Guid}) generated item with {epicCantrips.Count} epic{(epicCantrips.Count > 1 ? "s" : "")} - {wo.Name} ({wo.Guid}) - {GetSpellList(epicCantrips)} - killed by {killer?.Name} ({killer?.Guid})");

            if (legendaryCantrips.Count > 0)
                log.Debug($"[LOOT][LEGENDARY] {Name} ({Guid}) generated item with {legendaryCantrips.Count} legendar{(legendaryCantrips.Count > 1 ? "ies" : "y")} - {wo.Name} ({wo.Guid}) - {GetSpellList(legendaryCantrips)} - killed by {killer?.Name} ({killer?.Guid})");
        }

        public static string GetSpellList(Dictionary<int, float> spellTable)
        {
            var spells = new List<Server.Entity.Spell>();

            foreach (var kvp in spellTable)
                spells.Add(new Server.Entity.Spell(kvp.Key, false));

            return string.Join(", ", spells.Select(i => i.Name));
        }

        public bool IsOnNoDeathXPLandblock => Location != null ? NoDeathXP_Landblocks.Contains(Location.LandblockId.Landblock) : false;

        /// <summary>
        /// A list of landblocks the player gains no xp from creature kills
        /// </summary>
        public static HashSet<ushort> NoDeathXP_Landblocks = new HashSet<ushort>()
        {
            0x00B0,     // Colosseum Arena One
            0x00B1,     // Colosseum Arena Two
            0x00B2,     // Colosseum Arena Three
            0x00B3,     // Colosseum Arena Four
            0x00B4,     // Colosseum Arena Five
            0x5960,     // Gauntlet Arena One (Celestial Hand)
            0x5961,     // Gauntlet Arena Two (Celestial Hand)
            0x5962,     // Gauntlet Arena One (Eldritch Web)
            0x5963,     // Gauntlet Arena Two (Eldritch Web)
            0x5964,     // Gauntlet Arena One (Radiant Blood)
            0x5965,     // Gauntlet Arena Two (Radiant Blood)
            0x596B,     // Gauntlet Staging Area (All Societies)
        };
    }
}
