using System;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.Managers;

namespace ACE.Server.WorldObjects
{
    partial class Creature
    {
        public double? ResistSlash
        {
            get => GetProperty(PropertyFloat.ResistSlash);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistSlash); else SetProperty(PropertyFloat.ResistSlash, value.Value); }
        }

        public double? ResistPierce
        {
            get => GetProperty(PropertyFloat.ResistPierce);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistPierce); else SetProperty(PropertyFloat.ResistPierce, value.Value); }
        }

        public double? ResistBludgeon
        {
            get => GetProperty(PropertyFloat.ResistBludgeon);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistBludgeon); else SetProperty(PropertyFloat.ResistBludgeon, value.Value); }
        }

        public double? ResistFire
        {
            get => GetProperty(PropertyFloat.ResistFire);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistFire); else SetProperty(PropertyFloat.ResistFire, value.Value); }
        }

        public double? ResistCold
        {
            get => GetProperty(PropertyFloat.ResistCold);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistCold); else SetProperty(PropertyFloat.ResistCold, value.Value); }
        }

        public double? ResistAcid
        {
            get => GetProperty(PropertyFloat.ResistAcid);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistAcid); else SetProperty(PropertyFloat.ResistAcid, value.Value); }
        }

        public double? ResistElectric
        {
            get => GetProperty(PropertyFloat.ResistElectric);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistElectric); else SetProperty(PropertyFloat.ResistElectric, value.Value); }
        }

        public double? ResistHealthDrain
        {
            get => GetProperty(PropertyFloat.ResistHealthBoost);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistHealthBoost); else SetProperty(PropertyFloat.ResistHealthBoost, value.Value); }
        }

        public double? ResistHealthBoost
        {
            get => GetProperty(PropertyFloat.ResistHealthBoost);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistHealthBoost); else SetProperty(PropertyFloat.ResistHealthBoost, value.Value); }
        }

        public double? ResistStaminaDrain
        {
            get => GetProperty(PropertyFloat.ResistStaminaDrain);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistStaminaDrain); else SetProperty(PropertyFloat.ResistStaminaDrain, value.Value); }
        }

        public double? ResistStaminaBoost
        {
            get => GetProperty(PropertyFloat.ResistStaminaBoost);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistStaminaBoost); else SetProperty(PropertyFloat.ResistStaminaBoost, value.Value); }
        }

        public double? ResistManaDrain
        {
            get => GetProperty(PropertyFloat.ResistManaDrain);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistManaDrain); else SetProperty(PropertyFloat.ResistManaDrain, value.Value); }
        }

        public double? ResistManaBoost
        {
            get => GetProperty(PropertyFloat.ResistManaBoost);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistManaBoost); else SetProperty(PropertyFloat.ResistManaBoost, value.Value); }
        }

        public double? ResistNether
        {
            get => GetProperty(PropertyFloat.ResistNether);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.ResistNether); else SetProperty(PropertyFloat.ResistNether, value.Value); }
        }

        public bool NonProjectileMagicImmune
        {
            get => GetProperty(PropertyBool.NonProjectileMagicImmune) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.NonProjectileMagicImmune); else SetProperty(PropertyBool.NonProjectileMagicImmune, value); }
        }

        public float GetResistanceMod(DamageType damageType, WorldObject attacker, WorldObject weapon, float weaponResistanceMod = 1.0f)
        {
            var ignoreMagicResist = (weapon?.IgnoreMagicResist ?? false) || (attacker?.IgnoreMagicResist ?? false);

            // hollow weapons also ignore player natural resistances
            if (ignoreMagicResist)
            {
                if (!(attacker is Player) || !(this is Player) || PropertyManager.GetDouble("ignore_magic_resist_pvp_scalar").Item == 1.0)
                    return weaponResistanceMod;
            }

            var protMod = EnchantmentManager.GetProtectionResistanceMod(damageType);
            var vulnMod = EnchantmentManager.GetVulnerabilityResistanceMod(damageType);

            var naturalResistMod = GetNaturalResistance();

            // protection mod becomes either life protection or natural resistance,
            // whichever is more powerful (more powerful = lower value here)
            if (protMod > naturalResistMod)
                protMod = naturalResistMod;

            // does this stack with natural resistance?
            if (this is Player player)
            {
                var resistAug = player.GetAugmentationResistance(damageType);
                if (resistAug > 0)
                {
                    var augFactor = Math.Min(1.0f, resistAug * 0.1f);
                    protMod *= 1.0f - augFactor;
                }
            }

            // vulnerability mod becomes either life vuln or weapon resistance mod,
            // whichever is more powerful
            if (vulnMod < weaponResistanceMod)
                vulnMod = weaponResistanceMod;

            if (ignoreMagicResist)
            {
                // convert to additive space
                var addProt = -ModToRating(protMod);
                var addVuln = ModToRating(vulnMod);

                // scale
                addProt = IgnoreMagicResistScaled(addProt);
                addVuln = IgnoreMagicResistScaled(addVuln);

                protMod = GetNegativeRatingMod(addProt);
                vulnMod = GetPositiveRatingMod(addVuln);
            }

            return protMod * vulnMod;
        }

        public virtual float GetNaturalResistance()
        {
            // overridden for players
            return 1.0f;
        }

        public double GetArmorVsType(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Slash:
                    return GetProperty(PropertyFloat.ArmorModVsSlash) ?? 1.0f;
                case DamageType.Pierce:
                    return GetProperty(PropertyFloat.ArmorModVsPierce) ?? 1.0f;
                case DamageType.Bludgeon:
                    return GetProperty(PropertyFloat.ArmorModVsBludgeon) ?? 1.0f;
                case DamageType.Fire:
                    return GetProperty(PropertyFloat.ArmorModVsFire) ?? 1.0f;
                case DamageType.Cold:
                    return GetProperty(PropertyFloat.ArmorModVsCold) ?? 1.0f;
                case DamageType.Acid:
                    return GetProperty(PropertyFloat.ArmorModVsAcid) ?? 1.0f;
                case DamageType.Electric:
                    return GetProperty(PropertyFloat.ArmorModVsElectric) ?? 1.0f;
                case DamageType.Nether:
                    return GetProperty(PropertyFloat.ArmorModVsNether) ?? 1.0f;
                default:
                    return 1.0f;
            }
        }

        public double GetResistanceMod(ResistanceType resistance, WorldObject attacker = null, WorldObject weapon = null, float weaponResistanceMod = 1.0f)
        {
            switch (resistance)
            {
                case ResistanceType.Slash:
                    return (ResistSlash ?? 1.0) * GetResistanceMod(DamageType.Slash, attacker, weapon, weaponResistanceMod);
                case ResistanceType.Pierce:
                    return (ResistPierce ?? 1.0) * GetResistanceMod(DamageType.Pierce, attacker, weapon, weaponResistanceMod);
                case ResistanceType.Bludgeon:
                    return (ResistBludgeon ?? 1.0) * GetResistanceMod(DamageType.Bludgeon, attacker, weapon, weaponResistanceMod);
                case ResistanceType.Fire:
                    return (ResistFire ?? 1.0) * GetResistanceMod(DamageType.Fire, attacker, weapon, weaponResistanceMod);
                case ResistanceType.Cold:
                    return (ResistCold ?? 1.0) * GetResistanceMod(DamageType.Cold, attacker, weapon, weaponResistanceMod);
                case ResistanceType.Acid:
                    return (ResistAcid ?? 1.0) * GetResistanceMod(DamageType.Acid, attacker, weapon, weaponResistanceMod);
                case ResistanceType.Electric:
                    return (ResistElectric ?? 1.0) * GetResistanceMod(DamageType.Electric, attacker, weapon, weaponResistanceMod);
                case ResistanceType.Nether:
                    return (ResistNether ?? 1.0) * GetResistanceMod(DamageType.Nether, attacker, weapon, weaponResistanceMod);
                case ResistanceType.HealthBoost:
                    return (ResistHealthBoost ?? 1.0) * GetHealingRatingMod();
                case ResistanceType.HealthDrain:
                    return (ResistHealthDrain ?? 1.0) * GetNaturalResistance() * GetLifeResistRatingMod();
                case ResistanceType.StaminaBoost:
                    return (ResistStaminaBoost ?? 1.0) * GetHealingRatingMod();     // does healing rating affect these?
                case ResistanceType.StaminaDrain:
                    return (ResistStaminaDrain ?? 1.0) * GetNaturalResistance();
                case ResistanceType.ManaBoost:
                    return (ResistManaBoost ?? 1.0) * GetHealingRatingMod();
                case ResistanceType.ManaDrain:
                    return (ResistManaDrain ?? 1.0) * GetNaturalResistance();
                default:
                    return 1.0;
            }
        }

        public double? HealthRate
        {
            get => GetProperty(PropertyFloat.HealthRate);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.HealthRate); else SetProperty(PropertyFloat.HealthRate, value.Value); }
        }

        public double? StaminaRate
        {
            get => GetProperty(PropertyFloat.StaminaRate);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.StaminaRate); else SetProperty(PropertyFloat.StaminaRate, value.Value); }
        }

        public double ResistSlashMod => (ResistSlash ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Slash);
        public double ResistPierceMod => (ResistPierce ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Pierce);
        public double ResistBludgeonMod => (ResistBludgeon ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Bludgeon);
        public double ResistFireMod => (ResistFire ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Fire);
        public double ResistColdMod => (ResistCold ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Cold);
        public double ResistAcidMod => (ResistAcid ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Acid);
        public double ResistElectricMod => (ResistElectric ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Electric);
        public double ResistNetherMod => (ResistNether ?? 1.0) * EnchantmentManager.GetResistanceMod(DamageType.Nether);

        public bool NoCorpse
        {
            get => GetProperty(PropertyBool.NoCorpse) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.NoCorpse); else SetProperty(PropertyBool.NoCorpse, value); }
        }

        public bool TreasureCorpse
        {
            get => GetProperty(PropertyBool.TreasureCorpse) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.TreasureCorpse); else SetProperty(PropertyBool.TreasureCorpse, value); }
        }

        public uint? DeathTreasureType
        {
            get => GetProperty(PropertyDataId.DeathTreasureType);
            set { if (!value.HasValue) RemoveProperty(PropertyDataId.DeathTreasureType); else SetProperty(PropertyDataId.DeathTreasureType, value.Value); }
        }

        public int? LuminanceAward
        {
            get => GetProperty(PropertyInt.LuminanceAward);
            set { if (!value.HasValue) RemoveProperty(PropertyInt.LuminanceAward); else SetProperty(PropertyInt.LuminanceAward, value.Value); }
        }

        public bool AiImmobile
        {
            get => GetProperty(PropertyBool.AiImmobile) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.AiImmobile); else SetProperty(PropertyBool.AiImmobile, value); }
        }

        public int? Overpower
        {
            get => GetProperty(PropertyInt.Overpower);
            set { if (!value.HasValue) RemoveProperty(PropertyInt.Overpower); else SetProperty(PropertyInt.Overpower, value.Value); }
        }

        public int? OverpowerResist
        {
            get => GetProperty(PropertyInt.OverpowerResist);
            set { if (!value.HasValue) RemoveProperty(PropertyInt.OverpowerResist); else SetProperty(PropertyInt.OverpowerResist, value.Value); }
        }

        public string KillQuest
        {
            get => GetProperty(PropertyString.KillQuest);
            set { if (value == null) RemoveProperty(PropertyString.KillQuest); else SetProperty(PropertyString.KillQuest, value); }
        }

        public string KillQuest2
        {
            get => GetProperty(PropertyString.KillQuest2);
            set { if (value == null) RemoveProperty(PropertyString.KillQuest2); else SetProperty(PropertyString.KillQuest2, value); }
        }

        public string KillQuest3
        {
            get => GetProperty(PropertyString.KillQuest3);
            set { if (value == null) RemoveProperty(PropertyString.KillQuest3); else SetProperty(PropertyString.KillQuest3, value); }
        }

        public bool EliteTrigger
        {
            get => GetProperty(PropertyBool.EliteTrigger) ?? true;
            set { if (!value) RemoveProperty(PropertyBool.EliteTrigger); else SetProperty(PropertyBool.EliteTrigger, value); }
        }
        public bool IsElite
        {
            get => GetProperty(PropertyBool.IsElite) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.IsElite); else SetProperty(PropertyBool.IsElite, value); }
        }
        public bool IsMini
        {
            get => GetProperty(PropertyBool.IsMini) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.IsMini); else SetProperty(PropertyBool.IsMini, value); }
        }
        public bool DiscoMod
        {
            get => GetProperty(PropertyBool.DiscoMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.DiscoMod); else SetProperty(PropertyBool.DiscoMod, value); }
        }
        public bool SplitMod
        {
            get => GetProperty(PropertyBool.SplitMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.SplitMod); else SetProperty(PropertyBool.SplitMod, value); }
        }

        public bool IsRare
        {
            get => GetProperty(PropertyBool.IsRare) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.IsRare); else SetProperty(PropertyBool.IsRare, value); }
        }

        public bool BeefyMod
        {
            get => GetProperty(PropertyBool.BeefyMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.BeefyMod); else SetProperty(PropertyBool.BeefyMod, value); }
        }

        public bool Warded
        {
            get => GetProperty(PropertyBool.Warded) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.Warded); else SetProperty(PropertyBool.Warded, value); }
        }

        public bool TogglePhys
        {
            get => GetProperty(PropertyBool.TogglePhys) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.TogglePhys); else SetProperty(PropertyBool.TogglePhys, value); }
        }
        public bool ToggleMis
        {
            get => GetProperty(PropertyBool.ToggleMis) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.ToggleMis); else SetProperty(PropertyBool.ToggleMis, value); }
        }
        public bool ToggleSpell
        {
            get => GetProperty(PropertyBool.ToggleSpell) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.ToggleSpell); else SetProperty(PropertyBool.ToggleSpell, value); }
        }

        public bool ToggleRNG
        {
            get => GetProperty(PropertyBool.ToggleRNG) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.ToggleRNG); else SetProperty(PropertyBool.ToggleRNG, value); }
        }

        public bool MeteorMod
        {
            get => GetProperty(PropertyBool.MeteorMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.MeteorMod); else SetProperty(PropertyBool.MeteorMod, value); }
        }

        public bool TeleMod
        {
            get => GetProperty(PropertyBool.TeleMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.MeteorMod); else SetProperty(PropertyBool.TeleMod, value); }
        }

        public bool NovaMod
        {
            get => GetProperty(PropertyBool.NovaMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.NovaMod); else SetProperty(PropertyBool.NovaMod, value); }
        }

        public bool SupportMod
        {
            get => GetProperty(PropertyBool.SupportMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.SupportMod); else SetProperty(PropertyBool.SupportMod, value); }
        }

        public bool MirrorMod
        {
            get => GetProperty(PropertyBool.MirrorMod) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.MirrorMod); else SetProperty(PropertyBool.MirrorMod, value); }
        }

        public bool MirrorMob
        {
            get => GetProperty(PropertyBool.MirrorMob) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.MirrorMob); else SetProperty(PropertyBool.MirrorMob, value); }
        }

        public double? EliteDungeonIdleTime
        {
            get => GetProperty(PropertyFloat.EliteDungeonIdleTime);
            set { if (!value.HasValue) RemoveProperty(PropertyFloat.EliteDungeonIdleTime); else SetProperty(PropertyFloat.EliteDungeonIdleTime, value.Value); }
        }

        public int? MirrorMobCount
        {
            get => GetProperty(PropertyInt.MirrorMobCount);
            set { if (!value.HasValue) RemoveProperty(PropertyInt.MirrorMobCount); else SetProperty(PropertyInt.MirrorMobCount, value.Value); }
        }
    }
}
