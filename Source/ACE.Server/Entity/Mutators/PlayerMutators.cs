using ACE.Server.WorldObjects;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ACE.Server.Entity.Mutators
{
    public enum PlayerMutatorBuffType
    {
        None,
        Buff,
        Debuff
    }
    public class PlayerMutators
    {
        public abstract class PlayerMutator : LandblockMutator
        {
            static readonly PlayerMutatorGroup _AnyPlayerGroup = new AnyPlayerGroup(); //Default filter
            public PlayerMutatorGroup PlayersToAffect { get; set; } = _AnyPlayerGroup;
            public bool AffectsPlayer(Player p) => PlayersToAffect.CustomFunc(p);
            public abstract PlayerMutatorBuffType BuffType { get; }
        }

        /// <summary>
        /// Represents a player mutator that can be automatically aggregated
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        public abstract class PlayerMutator<TValue> : PlayerMutator
        {
            public abstract TValue AggregateKey { get; }
            public virtual MutatorAggregateGroup AggregateGroup => MutatorAggregateGroup.ManualAggregation;
        }

        public abstract class PlayerMultiplierMutator : PlayerMutator<float>
        {
            public float Multiplier { get; set; } = 1f;
            public override float AggregateKey => Multiplier;
            public override PlayerMutatorBuffType BuffType =>
                Multiplier >= 1f ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;
            public override MutatorAggregateGroup AggregateGroup => 
                Multiplier >= 1f ? MutatorAggregateGroup.TakeMaximum : MutatorAggregateGroup.TakeMinimum;
        }

        [MutatorAtom]
        public class XPMod : PlayerMultiplierMutator //done
        {
            internal override string Describe() => $"{Multiplier}x XP earned";
        }

        [MutatorAtom]
        public class DeathPenaltyVitaeOverride : PlayerMutator<float> //done
        {
            public float VitaePerDeath { get; set; } //The maximum in the set will take precedence
            public override float AggregateKey => VitaePerDeath;
            public override MutatorAggregateGroup AggregateGroup => MutatorAggregateGroup.TakeMaximum;
            public override PlayerMutatorBuffType BuffType => PlayerMutatorBuffType.Debuff;
            public DeathPenaltyVitaeOverride(float difficultyMagnitude, float roll)
            {
                VitaePerDeath = (int)Math.Round(difficultyMagnitude * .012 * .05);
                if (VitaePerDeath < .05f)
                    VitaePerDeath = .05f;
                if (VitaePerDeath > .60f)
                    VitaePerDeath = .60f;
            }

            internal override string Describe() => $"Vitae per death: {VitaePerDeath}%.";
        }

        [MutatorAtom]
        public class FoodMod : PlayerMultiplierMutator //done
        {
            public FoodMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                Multiplier = ReduceMulti(difficultyMagnitude, .1f);
            }

            internal override string Describe() => $"{Multiplier}x effect of food and drink";
        }

        [MutatorAtom]
        public class PlayerMeleeDamageMultiplier : PlayerMultiplierMutator //done
        {
            public PlayerMeleeDamageMultiplier(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                Multiplier = ReduceMulti(difficultyMagnitude, 0.1f * roll);
            }

            internal override string Describe() => $"You deal {Multiplier}x melee damage.";
        }

        [MutatorAtom]
        public class MeleeDamageAddedMod : PlayerMutator<int> //done
        {
            public int AddMaximumDamage { get; set; }
            public override int AggregateKey => AddMaximumDamage;
            public override MutatorAggregateGroup AggregateGroup =>
                AddMaximumDamage >= 0f ? MutatorAggregateGroup.TakeMaximum : MutatorAggregateGroup.TakeMinimum;
            public override PlayerMutatorBuffType BuffType => AddMaximumDamage >= 0f ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;

            public MeleeDamageAddedMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                AddMaximumDamage = -1 * AddWeighted(difficultyMagnitude, 10);
            }

            internal override string Describe() => $"You deal {AddMaximumDamage} additional melee damage.";
        }

        [MutatorAtom]
        public class SpellProjectileMultMod : PlayerMultiplierMutator //done
        {
            public SpellProjectileMultMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                Multiplier = ReduceMulti(difficultyMagnitude, 0.2f * roll);
            }
            internal override string Describe() => $"You deal {Multiplier}x spell damage.";
        }

        [MutatorAtom]
        public class SpellMinimumDamageAddedMod : PlayerMutator<int>
        {
            public override MutatorAggregateGroup AggregateGroup =>
                AddMinimumDamage >= 0f ? MutatorAggregateGroup.TakeMaximum : MutatorAggregateGroup.TakeMinimum;
            public override int AggregateKey => AddMinimumDamage;
            public int AddMinimumDamage { get; set; }
            public override PlayerMutatorBuffType BuffType => AddMinimumDamage >= 0f ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;

            public SpellMinimumDamageAddedMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                AddMinimumDamage = -1 * AddWeighted(difficultyMagnitude, 15 * roll);
            }

            internal override string Describe() => $"Your minimum spell damage is increased by {AddMinimumDamage}.";
        }

        public class SpellMaximumDamageAddedMod : PlayerMutator<int>
        {
            public override MutatorAggregateGroup AggregateGroup =>
                AddMaximumDamage >= 0f ? MutatorAggregateGroup.TakeMaximum : MutatorAggregateGroup.TakeMinimum;
            public override int AggregateKey => AddMaximumDamage;
            public int AddMaximumDamage { get; set; }
            public override PlayerMutatorBuffType BuffType => AddMaximumDamage >= 0f ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;

            public SpellMaximumDamageAddedMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                AddMaximumDamage = -1 * AddWeighted(difficultyMagnitude, 30 * roll);
            }

            internal override string Describe() => $"Your maximum spell damage is increased by {AddMaximumDamage}.";
        }

        [MutatorAtom]
        public class ArcherDamageMultMod : PlayerMultiplierMutator //done
        {
            public ArcherDamageMultMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                Multiplier = ReduceMulti(difficultyMagnitude, 0.2f * roll);
            }
            internal override string Describe() => $"You deal {Multiplier}x damage with missile weapons.";
        }

        [MutatorAtom]
        public class ArcherAddedDamageMod : PlayerMutator<int> //done
        {
            public int AddMaximumDamage { get; set; }
            public override int AggregateKey => AddMaximumDamage;
            public override MutatorAggregateGroup AggregateGroup =>
                AddMaximumDamage >= 0f ? MutatorAggregateGroup.TakeMaximum : MutatorAggregateGroup.TakeMinimum;
            public override PlayerMutatorBuffType BuffType => AddMaximumDamage >= 0f ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;
            public ArcherAddedDamageMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                AddMaximumDamage = -1 * AddWeighted(difficultyMagnitude, 10 * roll);
            }
            internal override string Describe() => $"You deal an additional {AddMaximumDamage} additional damage with missile weapons.";
        }

        [MutatorAtom]
        public class ForceEnchantment : PeriodicTick //done?
        {
            public ForceEnchantment(Spell spell)
                : base(MakeAction(spell))
            {

            }

            private static Action<Player> MakeAction(Spell spell)
            {
                int count = 0;
                return (player) =>
                {
                    if (--count <= 0)
                    {
                        count = 4;
                        player.EnchantmentManager.Add(spell, player);
                    }
                };
            }
        }

        [MutatorAtom]
        public class PeriodicTick : PlayerMutator //done
        {
            public override PlayerMutatorBuffType BuffType => PlayerMutatorBuffType.None;
            public Action<WorldObjects.Player> Action { get; private set; }
            public PeriodicTick(Action<WorldObjects.Player> action)
            {
                this.Action = WrapAction(action);
            }
        }

        [MutatorAtom]
        public class HealingKitMultiplierMod : PlayerMultiplierMutator //done
        {
            public HealingKitMultiplierMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                Multiplier = ReduceMulti(difficultyMagnitude, .1f);
            }
            internal override string Describe() => $"{Multiplier}x healing taken from healing kits";
        }

        [MutatorAtom]
        public class HealingKitAddedSkillMod : PlayerMutator<int> //done
        {
            public override PlayerMutatorBuffType BuffType => SkillBonus >= 0 ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;
            public int SkillBonus { get; set; }
            public override int AggregateKey => SkillBonus;
            public override MutatorAggregateGroup AggregateGroup =>
                SkillBonus >= 0f ? MutatorAggregateGroup.TakeMaximum : MutatorAggregateGroup.TakeMinimum;
            public HealingKitAddedSkillMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                SkillBonus = AddWeighted(difficultyMagnitude, -50f);
            }

            internal override string Describe() => $"{SkillBonus} added to healing skill";
        }

        [MutatorAtom]
        public class GlobalDamageDealtMod : PlayerMultiplierMutator //done
        {
            public GlobalDamageDealtMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                Multiplier = ReduceMulti(difficultyMagnitude, 0.1f * roll);
            }

            internal override string Describe() => $"You deal {Multiplier}x damage.";
        }

        [MutatorAtom]
        public class CritRatingMod : PlayerMutator<float> //done
        {
            public override PlayerMutatorBuffType BuffType => CritRatingAdded >= 0 ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;
            public float CritRatingAdded { get; set; }
            public override MutatorAggregateGroup AggregateGroup =>
                CritRatingAdded >= 1f ? MutatorAggregateGroup.StacksAdditivelyIncreasing : MutatorAggregateGroup.TakeMinimum;
            public override float AggregateKey => CritRatingAdded;
            public CritRatingMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                CritRatingAdded = -.01f * AddWeighted(5 * difficultyMagnitude, roll);
            }

            internal override string Describe()
            {
                return $"Your crit chance is reduced by {Math.Round(CritRatingAdded * 100, 1)}%.";
            }
        }

        [MutatorAtom]
        public class CritDamageRatingMod : PlayerMutator<float> //done
        {
            public override PlayerMutatorBuffType BuffType => CritMultiplierAdded >= 0 ? PlayerMutatorBuffType.Buff : PlayerMutatorBuffType.Debuff;
            public float CritMultiplierAdded { get; set; }
            public override float AggregateKey => CritMultiplierAdded;
            public override MutatorAggregateGroup AggregateGroup =>
                CritMultiplierAdded >= 1f ? MutatorAggregateGroup.StacksAdditivelyIncreasing : MutatorAggregateGroup.TakeMinimum;
            public CritDamageRatingMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                CritMultiplierAdded = -.01f * AddWeighted(5 * difficultyMagnitude, roll);
            }

            internal override string Describe()
            {
                return $"Your crit damage is reduced by {Math.Round(CritMultiplierAdded * 100, 1)}x.";
            }
        }

        [MutatorAtom]
        public class ArmorMod : PlayerMultiplierMutator //done
        {
            public ArmorMod(float difficultyMagnitude, float roll)
            {
                difficultyMagnitude = TrimDifficultyMagnitude(difficultyMagnitude);
                Multiplier = base.ReduceMulti(difficultyMagnitude, .1f * roll);
            }

            internal override string Describe()
            {
                return $"Your effective armor level is multiplied by {Multiplier}x.";
            }
        }
    }
}
