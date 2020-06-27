using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.Entity.Mutators
{
    class GlobalMutators
    {
        public class ItemQualityMod : LandblockMutator
        {
            public float Multiplier { get; set; } = 1f; //stacks additively
            /*
             * Affects:
             * Legendary cantrip change
             * Rating chance
             * Stats of items
             * Chance to have a top tier set vs a shitty set
             * */

            internal override string Describe()
            {
                return $"{Multiplier}x Item Quality";
            }
        }

        public class ItemQuantityMod : LandblockMutator
        {
            public float Multiplier { get; set; } = 1f; //stacks additively

            /*
             * Affects:
             * Stacks of items (up to the maximum stack size)
             * Number of items generated (up to a cap)
             */

            internal override string Describe()
            {
                return $"{Multiplier}x Item Quantity";
            }
        }
    }
}
