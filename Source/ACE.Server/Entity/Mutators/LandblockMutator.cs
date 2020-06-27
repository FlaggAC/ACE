using ACE.Common;
using ACE.Entity.Enum;
using ACE.Server.WorldObjects;
using log4net;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ACE.Server.Entity.Mutators
{
    public abstract class LandblockMutator
    {
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected Action<T> WrapAction<T>(Action<T> a)
        {
            Action<T> newAction = t =>
            {
                try
                {
                    a(t);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            };
            return newAction;
        }

        protected float TrimDifficultyMagnitude(float difficultyMagnitude)
        {
            if (difficultyMagnitude < 0)
                difficultyMagnitude = 0;
            if (difficultyMagnitude > 10f)
                difficultyMagnitude = 10f;
            return difficultyMagnitude;
        }

        protected float ReduceMulti(float difficultyMagnitude, float weight) => 1f - (difficultyMagnitude * weight);
        protected int AddWeighted(float difficultyMagnitude, float weight) => (int)Math.Round(difficultyMagnitude * weight);

        internal virtual string Describe()
        {
            return this.GetType().Name;
        }


    }

    public enum MutatorAggregateGroup
    {
        ManualAggregation, //Default, unaggregated
        StacksMultiplicitivelyReduction,
        StacksMultiplicitivelyIncreasing,
        StacksMultiplicitivelyAll,
        StacksAdditivelyReduction,
        StacksAdditivelyIncreasing,
        StacksAdditivelyAll,
        TakeMinimum,
        TakeMaximum
    }
}

/*Player handicap
Damage modifier
Vitae Penalty
XP Wager
Trophy Wager
No Movement Fizzle
Movement Speed Penalty
Global DoT
Global Debuff of (X)
Area will contain extra <X> mob
Aetheria cannot proc
Extra Crit Rating
Extra Crit Damage Rating
Modifier to Healing Kits
Chugs Disabled
Melee-specific, mage-specific, or archer-specific damage
Proof sets and/or resistance augs disabled
Casting variable modifiers
Players treated as if they have legendary wards
*/
