using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ACE.Server.Entity.Mutators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACE.Server.Tests
{
    [TestClass]
    public class MutatorTests
    {
        [TestMethod]
        public void MutatorClassesContainExactlyOneAtomAttribute()
        {
            var superclass = typeof(LandblockMutator);
            var types = superclass.Assembly.GetTypes().Where(t => t.IsSubclassOf(superclass));

            foreach(var _iter in types)
            {
                Type type = _iter;
                int countAttr = 0;
                while(type != superclass)
                {
                    if (type.GetCustomAttribute<MutatorAtomAttribute>(false) != null)
                        countAttr++;
                    type = type.BaseType;
                }
                Assert.AreEqual(1, countAttr, $"{_iter.Name} must have exactly 1 MutatorAtomAttribute in its inheritance chain.");
            }
        }

        public void PlayerMutatorsHaveExactlyOneAggregatorKey()
        {

        }
    }
}
