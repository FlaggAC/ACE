using ACE.Server.WorldObjects;
using log4net;
using Org.BouncyCastle.Crypto;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ACE.Server.Entity.Mutators
{
    public static class MutatorAggregateHelpers
    {
        public static TSource MaxSource<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            Comparer<TResult> comparer = Comparer<TResult>.Default;
            TResult value = default!;
            TSource key = default!;
            if (value == null)
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    do
                    {
                        if (!e.MoveNext())
                        {
                            return key;
                        }

                        value = selector(e.Current);
                        key = e.Current;
                    }
                    while (value == null);

                    while (e.MoveNext())
                    {
                        TResult x = selector(e.Current);
                        if (x != null && comparer.Compare(x, value) > 0)
                        {
                            value = x;
                            key = e.Current;
                        }
                    }
                }
            }
            else
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (!e.MoveNext())
                    {
                        ThrowHelper.ThrowNoElementsException();
                    }

                    value = selector(e.Current);
                    key = e.Current;
                    while (e.MoveNext())
                    {
                        TResult x = selector(e.Current);
                        if (comparer.Compare(x, value) > 0)
                        {
                            value = x;
                            key = e.Current;
                        }
                    }
                }
            }

            return key;
        }

        public static TSource MinSource<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (selector == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.selector);
            }

            Comparer<TResult> comparer = Comparer<TResult>.Default;
            TResult value = default!;
            TSource key = default!;
            if (value == null)
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    do
                    {
                        if (!e.MoveNext())
                        {
                            return key;
                        }

                        value = selector(e.Current);
                        key = e.Current;
                    }
                    while (value == null);

                    while (e.MoveNext())
                    {
                        TResult x = selector(e.Current);
                        if (x != null && comparer.Compare(x, value) < 0)
                        {
                            value = x;
                            key = e.Current;
                        }
                    }
                }
            }
            else
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (!e.MoveNext())
                    {
                        ThrowHelper.ThrowNoElementsException();
                    }

                    value = selector(e.Current);
                    key = e.Current;
                    while (e.MoveNext())
                    {
                        TResult x = selector(e.Current);
                        if (comparer.Compare(x, value) < 0)
                        {
                            value = x;
                            key = e.Current;
                        }
                    }
                }
            }

            return key;
        }
    }

    /// <summary>
    /// Represents a mutator class which shall be applied with the same mechanism for all subclasses
    /// All mutator classes must contain this attribute exactly once in its inheritance chain
    /// </summary>
    public class MutatorAtomAttribute : Attribute { }

    public class MutatorsForLandblock //TODO FIX TO MAKE ONE MAX OF A GIVEN TYPE
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        List<LandblockMutator> AllMutators { get; } = new List<LandblockMutator>();

        private readonly object _mutatorMutex = new Object();

        //Each key of this dict must be a type that directly contains a MutatorAtomAttribute
        Dictionary<Type, List<LandblockMutator>> MutatorDict { get; } = new Dictionary<Type, List<LandblockMutator>>();
        public void AddMutatorToLandblock(LandblockMutator mutator)
        {
            var type = mutator.GetType();
            while (type.BaseType != typeof(LandblockMutator))
            {
                type = type.BaseType;
            }
            lock (_mutatorMutex)
            {
                if (!MutatorDict.ContainsKey(type))
                    MutatorDict.Add(type, new List<LandblockMutator>());
                MutatorDict[type].Add(mutator);
                AllMutators.Add(mutator);
            }
        }

        public IEnumerable<T> GetAll<T>()
            where T : LandblockMutator
        {
            if (typeof(T).GetCustomAttribute<MutatorAtomAttribute>(false) == null)
            {
                log.Warn($"MutatorsForLandblock.GetAll<{typeof(T).Name}> called on non-atom type.");
                return new List<T>();
            }
            lock (_mutatorMutex)
            {
                if (!MutatorDict.ContainsKey(typeof(T)))
                    return new ReadOnlyCollection<T>(new List<T>());
                var list = MutatorDict[typeof(T)];
                return (IEnumerable<T>)list.AsReadOnly().Cast<T>();
            }
        }

        public static IEnumerable<T> GetForPlayer<T>(Player currentPlayer)
            where T : PlayerMutators.PlayerMutator
        {
            if (currentPlayer == null)
                return new List<T>();
            var filter =
                currentPlayer
                .GetMutatorsForLocation()
                .GetAll<T>()
                .Where(x => ((PlayerMutators.PlayerMutator)x).AffectsPlayer(currentPlayer));
            return filter;
        }

        public static U? GetAggregatedMutatorForPlayer<T, U> (Player currentPlayer, PlayerMutatorBuffType buffType)
            where T : PlayerMutators.PlayerMutator<U>
            where U : struct
        {
            var filter = GetForPlayer<T>(currentPlayer).Where(x => x.BuffType == buffType);
            if (!filter.Any())
                return null;

            var aggregateGroup = filter.First().AggregateGroup;
            if (aggregateGroup == MutatorAggregateGroup.ManualAggregation) //Should not be called
            {
                log.Warn($"GetAggregatedMutatorForPlayer called with invalid aggregateGroup ${aggregateGroup}.");
                return null;
            }

            var filter2 = filter.Select(x => x.AggregateKey);
            
            if (typeof(U) == typeof(int))
                return (U?)(object)AggregateMutatorValues(filter2.Cast<int>(), aggregateGroup);
            else if (typeof(U) == typeof(float))
                return (U?)(object)AggregateMutatorValues(filter2.Cast<float>(), aggregateGroup);

            log.Warn($"GetAggregatedMutatorForPlayer called with invalid Type ${typeof(U).Name}.");
            return null;
        }

        private static int? AggregateMutatorValues(IEnumerable<int> values, MutatorAggregateGroup aggregateGroup) => aggregateGroup switch
        {
            var x when
            x == MutatorAggregateGroup.StacksAdditivelyAll ||
            x == MutatorAggregateGroup.StacksAdditivelyIncreasing ||
            x == MutatorAggregateGroup.StacksAdditivelyReduction => values.Sum(),
            var x when
            x == MutatorAggregateGroup.StacksMultiplicitivelyAll ||
            x == MutatorAggregateGroup.StacksMultiplicitivelyIncreasing ||
            x == MutatorAggregateGroup.StacksMultiplicitivelyReduction => values.Aggregate((x, y) => x * y),
            MutatorAggregateGroup.TakeMinimum => values.Min(),
            MutatorAggregateGroup.TakeMaximum => values.Max(),
            _ => null
        };

        private static float? AggregateMutatorValues(IEnumerable<float> values, MutatorAggregateGroup aggregateGroup) => aggregateGroup switch
        {
            var x when
            x == MutatorAggregateGroup.StacksAdditivelyAll ||
            x == MutatorAggregateGroup.StacksAdditivelyIncreasing ||
            x == MutatorAggregateGroup.StacksAdditivelyReduction => values.Sum(),
            var x when
            x == MutatorAggregateGroup.StacksMultiplicitivelyAll ||
            x == MutatorAggregateGroup.StacksMultiplicitivelyIncreasing ||
            x == MutatorAggregateGroup.StacksMultiplicitivelyReduction => values.Aggregate((x, y) => x * y),
            MutatorAggregateGroup.TakeMinimum => values.Min(),
            MutatorAggregateGroup.TakeMaximum => values.Max(),
            _ => null
        };

        public string DescribeMutators()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var mutator in AllMutators)
            {
                sb.Append(mutator.Describe() + "\n");
            }
            return sb.ToString();
        }

        public class SpellCastingMod : LandblockMutator
        {
            //Single or double collision detection
            //MaxAngle
            //GodMode/NoMoveFizzle
            //NoComponents
            //MoveToState Rebroadcast Interval
            //MoveToState Reprocess Interval - "im wondering if the server shouldnt even be processing the MoveToState position more than 1x per second"
        }

        public class ExplosionCorpseMod : LandblockMutator
        {
            public float DamageMultiplier { get; set; } = 1f;
            public float Probability { get; set; } = 0.25f;
        }
    }
}
