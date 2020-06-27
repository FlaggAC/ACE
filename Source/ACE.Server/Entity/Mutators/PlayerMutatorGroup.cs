using ACE.Server.Managers;
using ACE.Server.WorldObjects;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACE.Server.Entity.Mutators
{
    public abstract class PlayerMutatorGroup
    {
        public string Value { get; set; }
        public Func<Player, bool> CustomFunc { get; protected set; }
    }

    public class AnyPlayerGroup : PlayerMutatorGroup
    {
        static Func<Player, bool> func = (p) => true;
        public AnyPlayerGroup()
        {
            CustomFunc = func;
        }
    }

    public class SpecificPlayerGroup : PlayerMutatorGroup
    {
        public SpecificPlayerGroup(string playerName)
        {
            Value = playerName;
            CustomFunc = (p) => p.Name == Value;
        }
    }

    public class FellowshipPlayerGroup : PlayerMutatorGroup
    {
        public FellowshipPlayerGroup(string playerName)
        {
            Value = playerName;
            CustomFunc = (p) =>
                p.Fellowship != null &&
                p.Fellowship
                .FellowshipMembers
                .Values
                .Any(wr => wr.TryGetTarget(out var fellowplayer) && fellowplayer.Name == Value);
        }
    }

    public class AllegiancePlayerGroup : PlayerMutatorGroup
    {
        public uint MonarchGuid { get; private set; }

        public AllegiancePlayerGroup(uint monarchGuid)
        {
            MonarchGuid = monarchGuid;
            Value = PlayerManager.FindByGuid(monarchGuid).Name;
            CustomFunc = (p) => p.MonarchId == MonarchGuid;
        }
    }
}
