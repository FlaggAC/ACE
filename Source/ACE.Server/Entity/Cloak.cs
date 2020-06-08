using ACE.Common;
using ACE.Entity.Enum;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.WorldObjects;

namespace ACE.Server.Entity
{
    public class Cloaks
    {
        
        public const uint Cloak = 44840;
        public const uint ChevronCloak = 44849;
        public const uint ChevronInvertedCloak = 44851;
        public const uint BorderedCloak = 44853;
        public const uint HalvedCloak = 44854;
        public const uint TrimmedCloak = 44856;
        public const uint QuarteredCloak = 44857;
        public const uint CreepingBlightCloak = 44982;
        public const uint HouseMhoireCloak = 44983;
        public const uint MukkirWingsCloak = 52193;
        public const uint RynthidTentaclesCloak = 51868;
        public const uint RynthidFieldCloak = 51867;

        private static readonly float ChanceMod = 2.5f;

        /// <summary>
        /// Rolls for a chance at procing a cloak spell
        /// If successful, casts the spell
        /// </summary>
        public static bool TryProcSpell(Creature defender, WorldObject attacker, float damage_percent)
        {
            var cloak = defender.EquippedCloak;

            if (cloak == null)
                return false;

            if (!RollProc(damage_percent))
                return false;

            return HandleProc(defender, attacker, cloak);
        }

        /// <summary>
        /// Rolls for a chance at procing a cloak spell
        /// </summary>
        /// <param name="damage_percent">The percent of MaxHealth inflicted by an enemy's hit</param>
        /// <returns></returns>
        public static bool RollProc(float damage_percent)
        {
            // TODO: find retail formula
            var chance = damage_percent * ChanceMod;

            if (chance < 1.0f)
            {
                var rng = ThreadSafeRandom.Next(0.0f, 1.0f);
                if (chance < rng)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Casts the cloak proc spell
        /// </summary>
        public static bool HandleProc(Creature defender, WorldObject attacker, WorldObject cloak)
        {
            if (!cloak.HasProc) return false;

            var spell = new Spell(cloak.ProcSpell.Value);

            if (spell.NotFound)
            {
                if (defender is Player player)
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"{spell.Name} spell not implemented, yet!", ChatMessageType.System));

                return false;
            }

            var targetSelf = spell.Flags.HasFlag(SpellFlags.SelfTargeted);
            var untargeted = spell.NonComponentTargetType == ItemType.None;

            var target = attacker;
            if (untargeted)
                target = null;
            else if (targetSelf)
                target = defender;

            // cloak range?

            var msg = new GameMessageSystemChat($"The cloak of {defender.Name} weaves the power of {spell.Name}!", ChatMessageType.Spellcasting);

            defender.EnqueueBroadcast(msg, WorldObject.LocalBroadcastRange, ChatMessageType.Magic);

            defender.TryCastSpell(spell, target, defender, true, false);

            return true;
        }

        /// <summary>
        /// Returns TRUE if object is cloak
        /// </summary>
        public static bool IsCloak(WorldObject wo)
        {
            return wo.ValidLocations == EquipMask.Cloak;
        }
    }
}
