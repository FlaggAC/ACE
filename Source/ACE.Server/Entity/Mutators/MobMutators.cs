using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.Entity.Mutators
{
    public abstract class MobMutators
    {

        public abstract class MobMutator : LandblockMutator { }


        [MutatorAtom]
        public abstract class MobSpawnMutator : MobMutator
        {
            public Action<WorldObjects.Creature> OnSpawn { get; private set; }
            public MobSpawnMutator(Action<WorldObjects.Creature> onSpawn)
            {
                this.OnSpawn = WrapAction(onSpawn);
            }
        }
    }
}
