using System.Collections.Generic;
using System.Linq;
using ACE.Common;

using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Factories;
using ACE.Server.Managers;
using ACE.Server.Network.GameMessages.Messages;

namespace ACE.Server.WorldObjects
{
    partial class Creature
    {
        /// <summary>
        /// Called every ~5 seconds for Creatures
        /// </summary>
        public override void Heartbeat(double currentUnixTime)
        {
            var expireItems = new List<WorldObject>();

            // added where clause
            foreach (var wo in EquippedObjects.Values.Where(i => i.EnchantmentManager.HasEnchantments || i.Lifespan.HasValue))
            {
                // FIXME: wo.NextHeartbeatTime is double.MaxValue here
                //if (wo.NextHeartbeatTime <= currentUnixTime)
                    //wo.Heartbeat(currentUnixTime);

                // just go by parent heartbeats, only for enchantments?
                // TODO: handle players dropping / picking up items
                wo.EnchantmentManager.HeartBeat(CachedHeartbeatInterval);

                if (wo.IsLifespanSpent)
                    expireItems.Add(wo);
            }

            VitalHeartBeat();

            EmoteManager.HeartBeat();

            DamageHistory.TryPrune();

            // delete items when RemainingLifespan <= 0
            foreach (var expireItem in expireItems)
            {
                expireItem.DeleteObject(this);

                if (this is Player player)
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Its lifespan finished, your {expireItem.Name} crumbles to dust.", ChatMessageType.Broadcast));
            }

            // ensures that there is an actual value on the control block
            if (WeenieClassId == 777778 && WorldManager.Controlblock == null)
                LongDesc = "No allegiance currently owns the control block.";

            if (WeenieClassId == 777778 && Time.GetUnixTime() > WorldManager.ControlblockTimer && WorldManager.Controlblock != null)
            {
                EnqueueBroadcast(new GameMessageSystemChat($"[CONTROL BLOCK] Control for {WorldManager.Controlblock} has expired, it is now free to take!", ChatMessageType.System), LocalBroadcastRangeSq, ChatMessageType.System);
                WorldManager.Controlblock = null;
                LongDesc = "No allegiance currently owns the control block.";
            }

            // if mob is awake will set the timer as to not have the mob delete itself prematurely
            if (IsAwake && IsElite && Location.Indoors)
            {
                //gets the time an hour from now, checks every 5 seconds via creature tick if the monster is in combat.
                SetProperty(PropertyFloat.EliteDungeonIdleTime, Time.GetFutureUnixTime(3600)); // 3600 default 1 hour
            }

            // Elites that somehow get displaced will return home if idle and not at their home location.
            if (!IsAwake && Location != Home && IsElite && Location.Indoors)
            {
                MoveToHome();                
            }

            // handles deleting elites in dungeons
            if (!IsAwake && IsElite && Location.Indoors)
            {
                if (Time.GetUnixTime() > EliteDungeonIdleTime)
                {
                    EnqueueBroadcast(new GameMessageSystemChat($"[ELITE] {Name} has left dereth forever.", ChatMessageType.System), LocalBroadcastRangeSq, ChatMessageType.System);
                    DeathTreasureType = null;
                    Smite(this, true);
                    DeleteObject();
                }
            }
            
            // handles immunity shifting from warded mobs
            if (Warded && PlayersInRange(96) && IsAwake)
            {
                var RNG1 = ThreadSafeRandom.Next(1, 3);
                var RNG2 = ThreadSafeRandom.Next(1, 2);
                var Initialize = 1;

                if (!ToggleMis && !TogglePhys && !ToggleSpell && Initialize == 1)
                {
                    Initialize = 2;

                    // choose a random immunity to start with
                    if (RNG1 == 1)
                    {
                        SetProperty(PropertyBool.TogglePhys, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Melee Attacks", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                    else if (RNG1 == 2)
                    {
                        SetProperty(PropertyBool.ToggleMis, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Missile Attacks", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                    else if (RNG1 == 3)
                    {
                        SetProperty(PropertyBool.ToggleSpell, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Magic Projectiles", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                }
                // controls frequency of immunity shifts. currently 50% every 5 seconds via creature tick.
                var togglerng = ThreadSafeRandom.Next(1, 100);
                if (togglerng >= 50)
                    SetProperty(PropertyBool.ToggleRNG, true);
                else
                    SetProperty(PropertyBool.ToggleRNG, false);

                // switch from missile to another
                if (ToggleMis && ToggleRNG)
                {
                    SetProperty(PropertyBool.ToggleMis, false);

                    if (RNG2 == 1)
                    {
                        SetProperty(PropertyBool.TogglePhys, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Melee Attacks", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                    if (RNG2 == 2)
                    {
                        SetProperty(PropertyBool.ToggleSpell, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Magic Projectiles", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                }
                // switch from melee to another
                else if (TogglePhys && ToggleRNG)
                {
                    SetProperty(PropertyBool.TogglePhys, false);

                    if (RNG2 == 1)
                    {
                        SetProperty(PropertyBool.ToggleMis, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Missile Attacks", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                    if (RNG2 == 2)
                    {
                        SetProperty(PropertyBool.ToggleSpell, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Magic Projectiles", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                }

                // switch from magic projectiles to another
                else if (ToggleSpell && ToggleRNG)
                {
                    SetProperty(PropertyBool.ToggleSpell, false);

                    if (RNG2 == 1)
                    {
                        SetProperty(PropertyBool.ToggleMis, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Missile Attacks", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                    if (RNG2 == 2)
                    {
                        SetProperty(PropertyBool.TogglePhys, true);
                        EnqueueBroadcast(new GameMessageSystemChat($"{Name} Switched its Immunity to Melee Attacks", ChatMessageType.System), LocalBroadcastRange, ChatMessageType.System);
                    }
                }
            }

            var lastDamager = DamageHistory.LastDamager;
            var attackerwakeup = AttackTarget as Player;
            // Disco Mod -- Launches a series of projectile spells if monster is attacking.
            if (PlayersInRange(96) && IsElite && DiscoMod && attackerwakeup != null && IsAwake)
            {
                if (attackerwakeup == null)
                    return;

                var randomshootermodrng = ThreadSafeRandom.Next(1, 10);
                var RNGwar = ThreadSafeRandom.Next(1, 13);

                if (randomshootermodrng <= 7) // high chance for it to chain wars at people omg watchout!
                {
                    var actionChain = new ActionChain();
                    actionChain.AddDelaySeconds(2.5f);
                    actionChain.AddAction(this, () =>
                    {
                        if (Level > 99)
                        {
                            if (RNGwar == 1)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidArc5), this);
                            if (RNGwar == 2)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningBolt5), this);
                            if (RNGwar == 3)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningArc5), this);
                            if (RNGwar == 4)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FlameBolt5), this);
                            if (RNGwar == 5)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostArc5), this);
                            if (RNGwar == 6)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceArc5), this);
                            if (RNGwar == 7)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockArc5), this);
                            if (RNGwar == 8)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.BladeArc5), this);
                            if (RNGwar == 9)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceBolt5), this);
                            if (RNGwar == 10)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostBolt5), this);
                            if (RNGwar == 11)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockWave5), this);
                            if (RNGwar == 12)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.WhirlingBlade5), this);
                            if (RNGwar == 13)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidStream5), this);
                        }
                        else
                        {
                            if (RNGwar == 1)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidArc2), this);
                            if (RNGwar == 2)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningBolt2), this);
                            if (RNGwar == 3)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningArc2), this);
                            if (RNGwar == 4)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FlameBolt2), this);
                            if (RNGwar == 5)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostArc2), this);
                            if (RNGwar == 6)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceArc2), this);
                            if (RNGwar == 7)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockArc2), this);
                            if (RNGwar == 8)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.BladeArc2), this);
                            if (RNGwar == 9)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceBolt2), this);
                            if (RNGwar == 10)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostBolt2), this);
                            if (RNGwar == 11)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockWave2), this);
                            if (RNGwar == 12)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.WhirlingBlade2), this);
                            if (RNGwar == 13)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidStream2), this);
                        }
                    });
                    actionChain.AddDelaySeconds(2.5f);
                    actionChain.AddAction(this, () =>
                    {
                        if (Level > 99)
                        {
                            if (RNGwar == 1)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidArc5), this);
                            if (RNGwar == 2)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningBolt5), this);
                            if (RNGwar == 3)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningArc5), this);
                            if (RNGwar == 4)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FlameBolt5), this);
                            if (RNGwar == 5)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostArc5), this);
                            if (RNGwar == 6)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceArc5), this);
                            if (RNGwar == 7)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockArc5), this);
                            if (RNGwar == 8)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.BladeArc5), this);
                            if (RNGwar == 9)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceBolt5), this);
                            if (RNGwar == 10)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostBolt5), this);
                            if (RNGwar == 11)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockWave5), this);
                            if (RNGwar == 12)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.WhirlingBlade5), this);
                            if (RNGwar == 13)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidStream5), this);
                        }
                        else
                        {
                            if (RNGwar == 1)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidArc2), this);
                            if (RNGwar == 2)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningBolt2), this);
                            if (RNGwar == 3)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningArc2), this);
                            if (RNGwar == 4)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FlameBolt2), this);
                            if (RNGwar == 5)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostArc2), this);
                            if (RNGwar == 6)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceArc2), this);
                            if (RNGwar == 7)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockArc2), this);
                            if (RNGwar == 8)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.BladeArc2), this);
                            if (RNGwar == 9)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceBolt2), this);
                            if (RNGwar == 10)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostBolt2), this);
                            if (RNGwar == 11)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockWave2), this);
                            if (RNGwar == 12)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.WhirlingBlade2), this);
                            if (RNGwar == 13)
                                WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidStream2), this);
                        }
                    });
                    actionChain.EnqueueChain();
                }
            }

            // Meteor Mod -- Launches a series of spells that fall on top of players, if monster is attacking. If in dungeon default to Disco.
            if (PlayersInRange(96) && IsElite && MeteorMod && attackerwakeup != null && IsAwake)
            {
                if (attackerwakeup == null)
                    return;

                var randomshootermodrng = ThreadSafeRandom.Next(1, 10);
                var spell1 = ThreadSafeRandom.Next(1, 5);
                var spell2 = ThreadSafeRandom.Next(1, 5);

                if (randomshootermodrng <= 5) // 70% chance to cast meteor type spells
                {
                    
                        // choose randomly a meteor type
                        if (spell1 == 1)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostStrike), this);
                        else if (spell1 == 2)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FlameStrike), this);
                        else if (spell1 == 3)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningStrike), this);
                        else if (spell1 == 4)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.BladeStrike), this);
                        else if (spell1 == 5)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceStrike), this);
                   
                    
                }
            }

            // Nova Mod -- Constanty casts Ring Spells.
            if (PlayersInRange(96) && IsElite && NovaMod && attackerwakeup != null && IsAwake)
            {
                if (attackerwakeup == null)
                    return;

                var randomshootermodrng = ThreadSafeRandom.Next(1, 10);
                var spell1 = ThreadSafeRandom.Next(1, 8);

                if (randomshootermodrng <= 5) // Constant Stream of Ring Spells
                {
                    var actionChain = new ActionChain();

                        // choose randomly a meteor type
                        if (spell1 == 1)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.NetherRing), this);
                        else if (spell1 == 2)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.AcidRing), this);
                        else if (spell1 == 3)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.LightningRingLarge), this);
                        else if (spell1 == 4)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FrostRing), this);
                        else if (spell1 == 5)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.FlameRing), this);
                        else if (spell1 == 6)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ForceRing), this);
                        else if (spell1 == 7)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.BladeRing), this);
                        else if (spell1 == 8)
                            WarMagic(attackerwakeup, new Server.Entity.Spell(SpellId.ShockwaveRing), this);
                    
                }
            }

            var mirrormobtimer = GetProperty(PropertyFloat.MirrorCreationTime);
            // Deletes a MirrorMob after 15 seconds.
            if (MirrorMob && IsElite && Time.GetUnixTime() >= mirrormobtimer)
            {
                if (mirrormobtimer != null)
                    DeleteObject(this);
            }


            // Mirror Mod -- Creates Clones of itself up to a maximum.
            if (PlayersInRange(96) && IsElite && MirrorMod && attackerwakeup != null && IsAwake)
            {
                if (attackerwakeup == null)
                    return;

                var randommirrorproc = ThreadSafeRandom.Next(1, 10);


                if (randommirrorproc <= 5) // 50% to spawn a duplicate
                {
                    var id = WeenieClassId;

                    var modrng = ThreadSafeRandom.Next(1, 10);
                    var mirrormob = WorldObjectFactory.CreateNewWorldObject(id) as Creature;
                    // the location to spawn is same as parent mob.
                    mirrormob.Location = new Position(Location);
                    // has same name as parent mob, canot be an elite and cannot have elite trigger flags.
                    mirrormob.Name = Name;
                    mirrormob.SetProperty(PropertyBool.IsElite, true);                    
                    mirrormob.SetProperty(PropertyBool.MirrorMob, true);
                    mirrormob.SetProperty(PropertyBool.EliteTrigger, false);

                    if (modrng <= 2)
                    {
                        mirrormob.SetProperty(PropertyBool.SupportMod, true);

                        // set life magic if non
                        var lifeMagic = mirrormob.GetCreatureSkill(Skill.LifeMagic);
                        if (lifeMagic.InitLevel > 100 && lifeMagic.InitLevel < 200)
                            lifeMagic.InitLevel += 300;
                        else
                            lifeMagic.InitLevel += 500;
                    }

                    // handles the duration of each mirrored mob. lasting 5 seconds.
                    mirrormob.SetProperty(PropertyFloat.MirrorCreationTime, Time.GetFutureUnixTime(6));
                    // Clones have 100 hp
                    mirrormob.Health.StartingValue = 100;
                    // clones have the same stats as parent mob.
                    mirrormob.Strength.StartingValue = Strength.StartingValue;
                    mirrormob.Endurance.StartingValue = Endurance.StartingValue;
                    mirrormob.Coordination.StartingValue = Coordination.StartingValue;
                    mirrormob.Quickness.StartingValue = Quickness.StartingValue;
                    mirrormob.Focus.StartingValue = Focus.StartingValue;
                    mirrormob.Self.StartingValue = Self.StartingValue;
                    //ensures hp/stam/mana are at max values upon spawn in.
                    mirrormob.Health.StartingValue = mirrormob.Health.MaxValue;
                    mirrormob.Stamina.StartingValue = mirrormob.Stamina.MaxValue;
                    mirrormob.Mana.StartingValue = mirrormob.Mana.MaxValue;
                    // resistances are crap for clones.
                    mirrormob.ResistAcid = 2;
                    mirrormob.ResistFire = 2;
                    mirrormob.ResistCold = 2;
                    mirrormob.ResistElectric = 2;
                    mirrormob.ResistPierce = 2;
                    mirrormob.ResistBludgeon = 2;
                    mirrormob.ResistSlash = 2;
                    mirrormob.ResistNether = 2;
                    mirrormob.ObjScale = ObjScale;
                    mirrormob.DeathTreasureType = null;
                    mirrormob.IgnoreCollisions = true;
                    mirrormob.Tolerance = Tolerance.None; // will make all mobs without tolerence immediately target something and attack
                    mirrormob.EnterWorld();
                    MirrorMobCount += 1;                    

                }
            }

            // Support Mod -- Constanty casts Vulns and debuffs.
            if (PlayersInRange(96) && IsElite && SupportMod && attackerwakeup != null && IsAwake)
            {
                if (attackerwakeup == null)
                    return;

                var randomshootermodrng = ThreadSafeRandom.Next(1, 10);
                var spell1 = ThreadSafeRandom.Next(1, 17);
                var spell2 = ThreadSafeRandom.Next(1, 17);

                if (randomshootermodrng <= 5)
                {

                    // choose randomly a debuff type
                    if (Level > 99)
                    {
                        if (spell1 == 1)
                            CastSpell(new Server.Entity.Spell(SpellId.VulnerabilityOther5));
                        else if (spell1 == 2)
                            CastSpell(new Server.Entity.Spell(SpellId.LightningVulnerabilityOther5));
                        else if (spell1 == 3)
                            CastSpell(new Server.Entity.Spell(SpellId.ColdVulnerabilityOther5));
                        else if (spell1 == 4)
                            CastSpell(new Server.Entity.Spell(SpellId.FireVulnerabilityOther5));
                        else if (spell1 == 5)
                            CastSpell(new Server.Entity.Spell(SpellId.AcidVulnerabilityOther5));
                        else if (spell1 == 6)
                            CastSpell(new Server.Entity.Spell(SpellId.PiercingVulnerabilityOther5));
                        else if (spell1 == 7)
                            CastSpell(new Server.Entity.Spell(SpellId.BludgeonVulnerabilityOther5));
                        else if (spell1 == 8)
                            CastSpell(new Server.Entity.Spell(SpellId.BladeVulnerabilityOther5));
                        else if (spell1 == 9)
                            CastSpell(new Server.Entity.Spell(SpellId.ImperilOther5));
                        else if (spell1 == 10)
                            CastSpell(new Server.Entity.Spell(SpellId.MagicYieldOther5));
                        else if (spell1 == 11)
                            CastSpell(new Server.Entity.Spell(SpellId.SlownessOther5));
                        else if (spell1 == 12)
                            CastSpell(new Server.Entity.Spell(SpellId.WeaknessOther5));
                        else if (spell1 == 13)
                            CastSpell(new Server.Entity.Spell(SpellId.FrailtyOther5));
                        else if (spell1 == 14)
                            CastSpell(new Server.Entity.Spell(SpellId.ClumsinessOther5));
                        else if (spell1 == 15)
                            CastSpell(new Server.Entity.Spell(SpellId.FesterOther5));
                        else if (spell1 == 16)
                            CastSpell(new Server.Entity.Spell(SpellId.LifeMagicIneptitudeOther5));
                        else if (spell1 == 17)
                            CastSpell(new Server.Entity.Spell(SpellId.CreatureEnchantmentIneptitudeOther5));
                    }
                    else
                    {
                        if (spell1 == 1)
                            CastSpell(new Server.Entity.Spell(SpellId.VulnerabilityOther2));
                        else if (spell1 == 2)
                            CastSpell(new Server.Entity.Spell(SpellId.LightningVulnerabilityOther2));
                        else if (spell1 == 3)
                            CastSpell(new Server.Entity.Spell(SpellId.ColdVulnerabilityOther2));
                        else if (spell1 == 4)
                            CastSpell(new Server.Entity.Spell(SpellId.FireVulnerabilityOther2));
                        else if (spell1 == 5)
                            CastSpell(new Server.Entity.Spell(SpellId.AcidVulnerabilityOther2));
                        else if (spell1 == 6)
                            CastSpell(new Server.Entity.Spell(SpellId.PiercingVulnerabilityOther2));
                        else if (spell1 == 7)
                            CastSpell(new Server.Entity.Spell(SpellId.BludgeonVulnerabilityOther2));
                        else if (spell1 == 8)
                            CastSpell(new Server.Entity.Spell(SpellId.BladeVulnerabilityOther2));
                        else if (spell1 == 9)
                            CastSpell(new Server.Entity.Spell(SpellId.ImperilOther2));
                        else if (spell1 == 10)
                            CastSpell(new Server.Entity.Spell(SpellId.MagicYieldOther2));
                        else if (spell1 == 11)
                            CastSpell(new Server.Entity.Spell(SpellId.SlownessOther2));
                        else if (spell1 == 12)
                            CastSpell(new Server.Entity.Spell(SpellId.WeaknessOther2));
                        else if (spell1 == 13)
                            CastSpell(new Server.Entity.Spell(SpellId.FrailtyOther2));
                        else if (spell1 == 14)
                            CastSpell(new Server.Entity.Spell(SpellId.ClumsinessOther2));
                        else if (spell1 == 15)
                            CastSpell(new Server.Entity.Spell(SpellId.FesterOther2));
                        else if (spell1 == 16)
                            CastSpell(new Server.Entity.Spell(SpellId.LifeMagicIneptitudeOther2));
                        else if (spell1 == 17)
                            CastSpell(new Server.Entity.Spell(SpellId.CreatureEnchantmentIneptitudeOther2));
                    }
                }
            }


            // tele Mod -- Teleports to the players last position every 5 seconds and also sets HOME location. Effectively allowing a mob to never lose aggro. ?? change?
            if (PlayersInRange(96) && IsElite && TeleMod && attackerwakeup != null && IsAwake)
            {
                if (attackerwakeup == null)
                    return;

                var teleposition = new Position(attackerwakeup.Location);
                SetPosition(PositionType.Location, teleposition);
                //SetPosition(PositionType.Home, teleposition); // set home location possible abuse to towns
            }

            var generatorid = GetProperty(PropertyInstanceId.Generator);
            if (EliteTrigger && GetProperty(PropertyBool.Attackable) == true && GetProperty(PropertyInt.PlayerKillerStatus) != 16)
            {
                // controls the RNG % of spawning in Elites
                if (ThreadSafeRandom.Next(0.0000f, 1.0000f) <= 0.0050f && !Location.Indoors) // outside spawn
                {
                    SetProperty(PropertyBool.IsElite, true);
                }
                else if (ThreadSafeRandom.Next(0.0000f, 1.0000f) <= 0.0002f && Location.Indoors) // inside spawn
                {
                    SetProperty(PropertyBool.IsElite, true);
                }
                else
                {
                    SetProperty(PropertyBool.IsElite, false);
                    SetProperty(PropertyBool.EliteTrigger, false);
                }
            }

            if (IsElite && EliteTrigger && WeenieClassId != 151001 && WeenieClassId != 251011 && WeenieClassId != 251010 && WeenieClassId != 261010)
            {                
                // get scale
                var scale = GetProperty(PropertyFloat.DefaultScale);

                // get name
                var creaturename = GetProperty(PropertyString.Name);

                // get  resists
                var acid = GetProperty(PropertyFloat.ResistAcid);
                var fire = GetProperty(PropertyFloat.ResistFire);
                var cold = GetProperty(PropertyFloat.ResistCold);
                var light = GetProperty(PropertyFloat.ResistElectric);
                var pierce = GetProperty(PropertyFloat.ResistPierce);
                var bludge = GetProperty(PropertyFloat.ResistBludgeon);
                var slash = GetProperty(PropertyFloat.ResistSlash);
                var nether = GetProperty(PropertyFloat.ResistNether);

                // directly set resistances for elemental damages and randomizes values.

                var acidrng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var firerng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var coldrng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var lightrng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var piercerng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var bludgerng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var slashrng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var netherrng = ThreadSafeRandom.Next((float)0.00, (float)0.60); // Possible outcomes can shift a weakness or strength entirely.
                var resistcount = 0;

                ResistAcid = 0;
                ResistFire = 0;
                ResistCold = 0;
                ResistElectric = 0;
                ResistPierce = 0;
                ResistBludgeon = 0;
                ResistSlash = 0;
                ResistNether = 0;

                ResistAcid += acidrng;
                ResistFire += firerng;
                ResistCold += coldrng;
                ResistElectric += lightrng;
                ResistPierce += piercerng;
                ResistBludgeon += bludgerng;
                ResistSlash += slashrng;
                ResistNether += netherrng;

                if (ResistAcid <= 0.25)
                    resistcount += 1;
                if (ResistFire <= 0.25)
                    resistcount += 1;
                if (ResistCold <= 0.25)
                    resistcount += 1;
                if (ResistElectric <= 0.25)
                    resistcount += 1;
                if (ResistPierce <= 0.25)
                    resistcount += 1;
                if (ResistBludgeon <= 0.25)
                    resistcount += 1;
                if (ResistSlash <= 0.25)
                    resistcount += 1;
                if (ResistNether <= 0.25)
                    resistcount += 1;

                if (resistcount < 1)
                {
                    var addoneresistrng = ThreadSafeRandom.Next(1, 8);

                    if (addoneresistrng == 1)
                        ResistAcid = 0.25;
                    if (addoneresistrng == 2)
                        ResistFire = 0.25;
                    if (addoneresistrng == 3)
                        ResistCold = 0.25;
                    if (addoneresistrng == 4)
                        ResistElectric = 0.25;
                    if (addoneresistrng == 5)
                        ResistPierce = 0.25;
                    if (addoneresistrng == 6)
                        ResistSlash = 0.25;
                    if (addoneresistrng == 7)
                        ResistBludgeon = 0.25;
                    if (addoneresistrng == 8)
                        ResistNether = 0.25;
                }// ensures elites have at least one extra resistant resist

                // makes sure resistances can never go below 0.01 after reductions making sure a mob can never be invulnerable.
                if (ResistAcid < 0.01 || ResistAcid == null)
                    ResistAcid = 0.01;
                if (ResistFire < 0.01 || ResistFire == null)
                    ResistFire = 0.01;
                if (ResistCold < 0.01 || ResistCold == null)
                    ResistCold = 0.01;
                if (ResistElectric < 0.01 || ResistElectric == null)
                    ResistElectric = 0.01;
                if (ResistPierce < 0.01 || ResistPierce == null)
                    ResistPierce = 0.01;
                if (ResistBludgeon < 0.01 || ResistBludgeon == null)
                    ResistBludgeon = 0.01;
                if (ResistSlash < 0.01 || ResistSlash == null)
                    ResistSlash = 0.01;
                if (ResistNether < 0.01 || ResistNether == null)
                    ResistNether = 0.01;

                // get physical armor resists
                var armoracid = GetProperty(PropertyFloat.ArmorModVsAcid);
                var armorfire = GetProperty(PropertyFloat.ArmorModVsFire);
                var armorcold = GetProperty(PropertyFloat.ArmorModVsCold);
                var armorlight = GetProperty(PropertyFloat.ArmorModVsElectric);
                var armorpierce = GetProperty(PropertyFloat.ArmorModVsPierce);
                var armorbludgeon = GetProperty(PropertyFloat.ArmorModVsBludgeon);
                var armorslashing = GetProperty(PropertyFloat.ArmorModVsSlash);

                // directly set physical resist values
                ArmorModVsAcid = armoracid + 1.5;
                ArmorModVsFire = armorfire + 1.5;
                ArmorModVsCold = armorcold + 1.5;
                ArmorModVsElectric = armorlight + 1.5;
                ArmorModVsPierce = armorpierce + 1.5;
                ArmorModVsBludgeon = armorbludgeon + 1.5;
                ArmorModVsSlash = armorslashing + 1.5;

                // Scale increase
                /*if (scale != null && scale >= 1.1f)
                    SetProperty(PropertyFloat.DefaultScale, (double)scale + 0.2);
                else if (scale != null)
                    SetProperty(PropertyFloat.DefaultScale, (double)scale + 0.3);
                 */ // temporarily disable scale to confirm if fixes bug.
                // change radar color to dull green
                SetProperty(PropertyInt.RadarBlipColor, (int)ACE.Entity.Enum.RadarColor.Green);

                // set healthrate higher if life immune reduce health regen
                if (NonProjectileMagicImmune)
                    HealthRate = 50;
                else
                    HealthRate = 1000;

                ManaRate = 1000;
                StaminaRate = 1000;

                // Allow Unarmed to ignore banes.
                //IgnoreMagicArmor = true;

                // scale all stats randomly. MAX 200 - STR 500
                var strRNG = ThreadSafeRandom.Next(70, 500);
                var endRNG = ThreadSafeRandom.Next(70, 200);
                var quickRNG = ThreadSafeRandom.Next(70, 200);
                var coordRNG = ThreadSafeRandom.Next(70, 200);
                var focusRNG = ThreadSafeRandom.Next(70, 200);
                var willRNG = ThreadSafeRandom.Next(70, 200);
                // for mobs under level 100
                var strRNGL = ThreadSafeRandom.Next(25, 75);
                var endRNGL = ThreadSafeRandom.Next(25, 75);
                var quickRNGL = ThreadSafeRandom.Next(25, 75);
                var coordRNGL = ThreadSafeRandom.Next(25, 75);
                var focusRNGL = ThreadSafeRandom.Next(25, 75);
                var willRNGL = ThreadSafeRandom.Next(25, 75);


                if (Level > 99)
                {
                    Strength.StartingValue += (uint)strRNG;
                    Endurance.StartingValue += (uint)endRNG;
                    Quickness.StartingValue += (uint)quickRNG;
                    Coordination.StartingValue += (uint)coordRNG;
                    Focus.StartingValue += (uint)focusRNG;
                    Self.StartingValue += (uint)willRNG;
                }
                else
                {
                    Strength.StartingValue += (uint)strRNGL;
                    Endurance.StartingValue += (uint)endRNGL;
                    Quickness.StartingValue += (uint)quickRNGL;
                    Coordination.StartingValue += (uint)coordRNGL;
                    Focus.StartingValue += (uint)focusRNGL;
                    Self.StartingValue += (uint)willRNGL;
                }
                // handles mod rolling. Progressively harder to roll certain/more mods.
                var rollmods = ThreadSafeRandom.Next(1, 1000);
                var raremod = ThreadSafeRandom.Next(1, 15000);
                var rng1 = ThreadSafeRandom.Next(1, 650);
                var rng2 = ThreadSafeRandom.Next(1, 480);
                var rng3 = ThreadSafeRandom.Next(1, 450);
                var rng4 = ThreadSafeRandom.Next(1, 500);
                var rng5 = ThreadSafeRandom.Next(1, 400);
                var rng6 = ThreadSafeRandom.Next(1, 350);
                var rng7 = ThreadSafeRandom.Next(1, 500);
                var rng8 = ThreadSafeRandom.Next(1, 410);
                var rng9 = ThreadSafeRandom.Next(1, 310);

                var InitGen = GetProperty(PropertyInt.InitGeneratedObjects);
                var MaxGen = GetProperty(PropertyInt.MaxGeneratedObjects);
                var ModCount = 0;
                var ModCountHidden = 0;
                var DefenseMod = 0;
                var OffenseMod = 0;

                if (rollmods <= rng1 && ModCountHidden < 5)
                {
                    SetProperty(PropertyBool.DiscoMod, true);
                    ModCountHidden += 1;

                } // disco

                /*if (rollmods <= rng2 && ModCountHidden < 5)
                {
                    if (InitGeneratedObjects <= 1 || MaxGeneratedObjects <= 1 || InitGen == null || MaxGen == null)
                    {
                        SetProperty(PropertyBool.SplitMod, true);
                        ModCountHidden += 1;
                    }
                    else
                    {
                        SetProperty(PropertyBool.SplitMod, false);
                    }
                }// Split .. Generator creature types cannot split. Can cause massive lag to server.
                */
                if (rollmods <= rng3 && ModCountHidden < 5)
                {
                    SetProperty(PropertyBool.BeefyMod, true);
                    ModCountHidden += 1;

                }// beefy

                if (rollmods <= rng4 && ModCountHidden < 5)
                {
                    if (Location.Indoors)
                    {
                        SetProperty(PropertyBool.DiscoMod, true);
                        ModCountHidden += 1;
                    }
                    else
                    {
                        SetProperty(PropertyBool.MeteorMod, true);
                        ModCountHidden += 1;
                    }
                }// Meteor. Outside Conditional rolls Disco instead.

                if (rollmods <= rng5 && ModCountHidden < 5)
                {
                    SetProperty(PropertyBool.Warded, true);
                    ModCountHidden += 1;

                }// warded

                if (rollmods <= rng6 && ModCountHidden < 5)
                {
                    SetProperty(PropertyBool.TeleMod, true);
                    ModCountHidden += 1;

                }// telemod

                if (rollmods <= rng7 && ModCountHidden < 5)
                {
                    SetProperty(PropertyBool.NovaMod, true);
                    ModCountHidden += 1;

                }// Novamod

                if (rollmods <= rng8 && ModCountHidden < 5)
                {
                    SetProperty(PropertyBool.SupportMod, true);
                    ModCountHidden += 1;

                }// SupportMod

                /*if (rollmods <= rng9 && ModCountHidden < 5)
                {
                    SetProperty(PropertyBool.MirrorMod, true);
                    ModCountHidden += 1;

                }// mirrorMod
                */
                if (raremod <= 1 && Level >= 150 && !Location.Indoors && ModCountHidden >= 5)
                {
                    if (Location.Indoors)
                    {
                        SetProperty(PropertyBool.Warded, true);
                        ModCountHidden += 1;
                    }
                    else
                    {
                        SetProperty(PropertyBool.IsRare, true);
                    }
                }// rare

                // rollmods disco, splitting, rare, beefy, warded, Meteor, NOVA, TELEPORTING <--DONE 

                // count mods to make sure minimum has been met.
                if (DiscoMod || SplitMod || BeefyMod || MeteorMod || TeleMod || Warded || NovaMod || SupportMod || MirrorMod && ModCount == 0)
                {
                    if (DiscoMod)
                        OffenseMod += 1;
                    if (MeteorMod)
                        OffenseMod += 1;
                    if (NovaMod)
                        OffenseMod += 1;
                    if (SupportMod)
                        OffenseMod += 1;
                    if (MirrorMod)
                        OffenseMod += 1;
                    if (SplitMod)
                        DefenseMod += 1;
                    if (BeefyMod)
                        DefenseMod += 1;
                    if (TeleMod)
                        DefenseMod += 1;
                    if (Warded)
                        DefenseMod += 1;
                    if (IsRare)
                        ModCount += 1;
                }


                // if amount of mods rolled is 0 or 1, will add 1 defense or 1 offense mod. a mob MUST have at least 1 Defensive mod AND 1 Offensive Mod.
                // This will make sure that a mob never rolls only 1 defensive or offensive mod and bring most encounters up in difficulty
                if (OffenseMod < 1 || DefenseMod < 1)
                {
                    var RandomOffMod = ThreadSafeRandom.Next(1, 5);
                    var RandomDefMod = ThreadSafeRandom.Next(1, 4);

                    if (OffenseMod < 1)
                    {
                        if (RandomOffMod == 1)
                        {
                            SetProperty(PropertyBool.DiscoMod, true);
                        }
                        if (RandomOffMod == 2)
                        {
                            if (!Location.Indoors)
                                SetProperty(PropertyBool.MeteorMod, true);
                            else
                                SetProperty(PropertyBool.DiscoMod, true);
                        }
                        if (RandomOffMod == 3)
                        {
                            SetProperty(PropertyBool.NovaMod, true);
                        }
                        if (RandomOffMod == 4)
                        {
                            SetProperty(PropertyBool.SupportMod, true);
                        }
                       // if (RandomOffMod == 5)
                       // {
                       //     SetProperty(PropertyBool.MirrorMod, true);
                       // }
                        OffenseMod += 1;
                    }

                    if (DefenseMod < 1)
                    {
                        if (RandomDefMod == 1)
                        {
                            if (InitGeneratedObjects <= 1 || MaxGeneratedObjects <= 1 || InitGen == null || MaxGen == null)
                            {
                                SetProperty(PropertyBool.SplitMod, true);
                            }
                            else
                            {
                                SetProperty(PropertyBool.BeefyMod, true);
                            }
                        }
                        if (RandomDefMod == 2)
                        {
                            SetProperty(PropertyBool.BeefyMod, true);
                        }
                        if (RandomDefMod == 3)
                        {
                            SetProperty(PropertyBool.Warded, true);
                        }
                        if (RandomDefMod == 4)
                            SetProperty(PropertyBool.TeleMod, true);
                        DefenseMod += 1;
                    }
                }

                ModCount = OffenseMod + DefenseMod;

                // add elite to name if name exists and other various strings
                if (creaturename != null)
                {
                    string acidext = null;
                    string fireext = null;
                    string coldext = null;
                    string lightext = null;
                    string pierceext = null;
                    string bludgeext = null;
                    string slashext = null;
                    string nethertext = null;
                    if (ResistAcid <= 0.25)
                        acidext = "[+a]";
                    if (ResistFire <= 0.25)
                        fireext = "[+f]";
                    if (ResistCold <= 0.25)
                        coldext = "[+c]";
                    if (ResistElectric <= 0.25)
                        lightext = "[+e]";
                    if (ResistPierce <= 0.25)
                        pierceext = "[+p]";
                    if (ResistBludgeon <= 0.25)
                        bludgeext = "[+b]";
                    if (ResistSlash <= 0.25)
                        slashext = "[+s]";
                    if (ResistNether <= 0.25)
                        nethertext = "[+n]";

                    SetProperty(PropertyString.Name, "Elite " + creaturename + " " + acidext + fireext + coldext + lightext + pierceext + bludgeext + slashext + nethertext);

                    string Mods = "";


                    if (ModCount < 5)
                    {
                        if (DiscoMod)
                            Mods += " Disco,";
                        if (SplitMod)
                            Mods += " Split,";
                        if (IsRare)
                            Mods += " Rare,";
                        if (BeefyMod)
                            Mods += " Beefy,";
                        if (Warded)
                            Mods += " Ward,";
                        if (MeteorMod)
                            Mods += " Meteor,";
                        if (TeleMod)
                            Mods += " Tele,";
                        if (NovaMod)
                            Mods += " Nova,";
                        if (SupportMod)
                            Mods += " Support,";
                        if (MirrorMod)
                            Mods += " Mirror,";

                        Mods = Mods.TrimStart(' ');
                        Mods = Mods.TrimEnd(',');

                        SetProperty(PropertyString.Template, Mods);
                    }
                    else
                    {
                        if (DiscoMod)
                            Mods += " Disc,";
                        if (SplitMod)
                            Mods += " Spl,";
                        if (IsRare)
                            Mods += " Rar,";
                        if (BeefyMod)
                            Mods += " Beef,";
                        if (Warded)
                            Mods += " Ward,";
                        if (MeteorMod)
                            Mods += " Met,";
                        if (TeleMod)
                            Mods += " Tel,";
                        if (NovaMod)
                            Mods += " Nov,";
                        if (SupportMod)
                            Mods += " Sup,";
                        if (MirrorMod)
                            Mods += " Mirr,";

                        Mods = Mods.TrimStart(' ');
                        Mods = Mods.TrimEnd(',');

                        SetProperty(PropertyString.Template, Mods);

                    }
                }

                if (DiscoMod || MeteorMod || NovaMod)
                {
                    // set war magic if non
                    var warMagic = GetCreatureSkill(Skill.WarMagic);
                    if (warMagic.InitLevel > 100 && warMagic.InitLevel < 200)
                        warMagic.InitLevel += 300;
                    else
                        warMagic.InitLevel += 450;
                }
                if (SupportMod)
                {
                    // set life magic if non
                    var lifeMagic = GetCreatureSkill(Skill.LifeMagic);
                    if (lifeMagic.InitLevel > 100 && lifeMagic.InitLevel < 200)
                        lifeMagic.InitLevel += 300;
                    else
                        lifeMagic.InitLevel += 500;

                    // set critter magic if non
                    var creatureMagic = GetCreatureSkill(Skill.CreatureEnchantment);
                    if (creatureMagic.InitLevel > 100 && creatureMagic.InitLevel < 200)
                        creatureMagic.InitLevel += 300;
                    else
                        creatureMagic.InitLevel += 500;

                }

                
                // allows players to ID and see the elites stats
                var deception = GetCreatureSkill(Skill.Deception);
                if (deception.InitLevel > 0)
                    deception.InitLevel = 0;

                //handles the loot table that the elite mob has. Upgrades to next tier.
                var loottable = GetProperty(PropertyDataId.DeathTreasureType);
                //tier 1 to tier 2
                if (loottable == 36 || loottable == 30 || loottable == 81 || loottable == 465 || loottable == 24
                    || loottable == 414 || loottable == 142 || loottable == 83 || loottable == 397 || loottable == 127
                    || loottable == 453 || loottable == 156 || loottable == 18 || loottable == 191 || loottable == 12
                    || loottable == 459 || loottable == 6)
                    DeathTreasureType = 34; // tier 2

                // tier 2 to tier 3
                if (loottable == 356 || loottable == 96 || loottable == 94 || loottable == 91 || loottable == 395
                    || loottable == 65 || loottable == 390 || loottable == 69 || loottable == 134 || loottable == 77
                    || loottable == 188 || loottable == 132 || loottable == 197 || loottable == 232 || loottable == 242
                    || loottable == 173 || loottable == 172 || loottable == 151 || loottable == 145 || loottable == 143 || loottable == 164
                    || loottable == 230 || loottable == 316 || loottable == 138 || loottable == 193 || loottable == 405
                    || loottable == 16 || loottable == 34 || loottable == 10 || loottable == 22 || loottable == 28
                    || loottable == 463 || loottable == 457 || loottable == 451 || loottable == 410 || loottable == 4 || loottable == 63)
                    DeathTreasureType = 33; // tier 3

                // tier 3 to tier 4
                if (loottable == 171 || loottable == 166 || loottable == 253 || loottable == 162 || loottable == 258
                    || loottable == 159 || loottable == 262 || loottable == 265 || loottable == 147 || loottable == 144
                    || loottable == 462 || loottable == 21 || loottable == 313 || loottable == 246 || loottable == 3
                    || loottable == 184 || loottable == 182 || loottable == 181 || loottable == 180 || loottable == 227 || loottable == 9
                    || loottable == 231 || loottable == 233 || loottable == 177 || loottable == 236 || loottable == 365
                    || loottable == 239 || loottable == 174 || loottable == 244 || loottable == 15 || loottable == 238
                    || loottable == 64 || loottable == 346 || loottable == 106 || loottable == 450 || loottable == 95 || loottable == 33
                    || loottable == 90 || loottable == 89 || loottable == 88 || loottable == 86
                    || loottable == 359 || loottable == 48 || loottable == 72 || loottable == 363 || loottable == 411
                    || loottable == 62 || loottable == 408 || loottable == 344 || loottable == 343 || loottable == 342
                    || loottable == 140 || loottable == 27 || loottable == 456 || loottable == 126 || loottable == 123 || loottable == 116
                    || loottable == 314 || loottable == 115 || loottable == 111 || loottable == 340 || loottable == 113)
                    DeathTreasureType = 31; // tier 4

                // tier 4 to tier 5
                if (loottable == 19 || loottable == 108 || loottable == 7 || loottable == 97 || loottable == 161
                    || loottable == 59 || loottable == 179 || loottable == 13 || loottable == 1 || loottable == 148
                    || loottable == 176 || loottable == 31 || loottable == 25 || loottable == 150 || loottable == 190
                    || loottable == 261 || loottable == 263 || loottable == 354 || loottable == 347 || loottable == 345 || loottable == 406
                    || loottable == 267 || loottable == 308 || loottable == 309 || loottable == 448 || loottable == 339
                    || loottable == 412 || loottable == 319 || loottable == 320 || loottable == 328 || loottable == 260
                    || loottable == 259 || loottable == 329 || loottable == 201 || loottable == 217 || loottable == 221 || loottable == 235
                    || loottable == 240 || loottable == 243 || loottable == 245 || loottable == 252
                    || loottable == 254 || loottable == 962 || loottable == 352 || loottable == 951 || loottable == 360)
                    DeathTreasureType = 35; // tier 5

                // tier 5 to tier 6
                if (loottable == 87 || loottable == 357 || loottable == 358 || loottable == 407 || loottable == 375
                    || loottable == 391 || loottable == 362 || loottable == 409 || loottable == 413 || loottable == 11
                    || loottable == 988 || loottable == 17 || loottable == 460 || loottable == 454 || loottable == 23
                    || loottable == 464 || loottable == 458 || loottable == 29 || loottable == 452 || loottable == 35 || loottable == 5
                    || loottable == 200 || loottable == 146 || loottable == 266 || loottable == 264 || loottable == 149
                    || loottable == 154 || loottable == 155 || loottable == 157 || loottable == 256 || loottable == 312
                    || loottable == 255 || loottable == 204 || loottable == 183 || loottable == 257 || loottable == 315 || loottable == 334
                    || loottable == 355 || loottable == 317 || loottable == 318 || loottable == 353
                    || loottable == 348 || loottable == 341)
                    DeathTreasureType = 32; // tier 6

                // tier 6&7 to tier 7/8 (Golem Loot Table)
                if (loottable == 361 || loottable == 351 || loottable == 982 || loottable == 14 || loottable == 421
                    || loottable == 8 || loottable == 461 || loottable == 2 || loottable == 350 || loottable == 310
                    || loottable == 349 || loottable == 32 || loottable == 422 || loottable == 455 || loottable == 26
                    || loottable == 338 || loottable == 449 || loottable == 20 || loottable == 420 /*end t6*/ || loottable == 1004 || loottable == 1003
                    || loottable == 1002 || loottable == 999 || loottable == 2000 || loottable == 998 || loottable == 1001)
                    DeathTreasureType = 1000; // tier 7/8

                // Xp Scaled by mods. A mob always has at least 2 mods which typically means Double XP.
                var XP = GetProperty(PropertyInt.XpOverride);

                if (Level > 100)
                    XpOverride = (XP + 5000000) * ModCount;
                else
                    XpOverride = (XP + 150000) * ModCount;
                // multiples hp by 12 for increased soakability. Beefy Mod increases HP by a bit more.
                var hp = GetCreatureVital(PropertyAttribute2nd.Health);

                if (!BeefyMod) // default elite hp multipliers
                {
                    if (Level > 99)
                    {
                        hp.StartingValue *= 17;
                        hp.Current = hp.MaxValue;
                    }//over level 100 non beefy
                    else
                    {
                        hp.StartingValue *= 12;
                        hp.Current = hp.MaxValue;
                    }//under level 100 non beefy
                }// base value is x12 hp for all elites
                else
                {

                    if (Level > 99)
                    {
                        if (hp.StartingValue > 100000)
                        {
                            hp.StartingValue *= 15;
                            hp.Current = hp.MaxValue;
                        }
                        else
                        {
                            hp.StartingValue *= 30;
                            hp.Current = hp.MaxValue;
                        }
                    }// over level 100 beefy
                    else
                    {
                        hp.StartingValue *= 7;
                        hp.Current = hp.MaxValue;
                    }// under level beefy mod
                }// if hp is higher than 100,000 then only x20, else x50

                //Adjusts stamina/mana vitals to proper values after attribute adjustments.
                var stamina = GetCreatureVital(PropertyAttribute2nd.Stamina);
                var mana = GetCreatureVital(PropertyAttribute2nd.Mana);
                mana.Current = mana.MaxValue;
                stamina.Current = stamina.MaxValue;

                // elites can always generate rares
                CanGenerateRare = true;
                                
                EnqueueBroadcast(new GameMessageSystemChat($"[ELITE] {Name} has spawned as an elite near you with {ModCount} Mods. {Location.GetMapCoordStr()}", ChatMessageType.System), LocalBroadcastRangeSq, ChatMessageType.System);

                if (IsRare)
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ELITE] An elite monster with the RARE mod has spawned at {Location.GetMapCoordStr()}", ChatMessageType.Broadcast));               

                SetProperty(PropertyBool.EliteTrigger, false);

                //sets elite to non aggressive in dungeons and adds the initial IdleTime  for upgrades.
                if (Location.Indoors)
                {
                    Tolerance = Tolerance.Retaliate; // if in dungeon only retaliate
                    VisualAwarenessRange = 0; // Other Mobs will not wake it
                    MoveToHome(); // brief second where mobs spawn in and aggro -- once they upgrade they will stop aggro and run back to home until attacked by a player.
                    IsAwake = false;
                    SetProperty(PropertyFloat.EliteDungeonIdleTime, Time.GetFutureUnixTime(3600));
                }

                EnqueueBroadcastUpdateObject();
            }

            base.Heartbeat(currentUnixTime);
        }
    }
}
