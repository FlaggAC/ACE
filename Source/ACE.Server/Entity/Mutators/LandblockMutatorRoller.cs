using ACE.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.Entity.Mutators
{

    public static class LandblockMutatorRoller
    {
        static Dictionary<int, float> TierMagnitudes = new Dictionary<int, float>()
        {
            { 1, 1.1f },
            { 2, 1.2f },
            { 3, 1.3f },
            { 4, 1.4f },
            { 5, 1.5f },
            { 6, 1.6f },
            { 7, 1.7f },
            { 8, 1.8f },
            { 9, 1.9f },
            { 10, 2f },
            { 11, 2.2f },
            { 12, 2.4f },
            { 13, 2.6f },
            { 14, 2.8f },
            { 15, 3f }
        };

        static List<Func<float, float, LandblockMutator>> RandomMutatorAction = new List<Func<float, float, LandblockMutator>>
        {
            { (mag, roll) => new PlayerMutators.ArcherAddedDamageMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.ArcherDamageMultMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.ArmorMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.CritDamageRatingMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.CritRatingMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.DeathPenaltyVitaeOverride(mag, roll) },
            //{ (mag, roll) => new MutatorsForLandblock.ExplosionCorpseMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.FoodMod(mag, roll) },
            //{ (mag, roll) => new MutatorsForLandblock.ForceEnchantment(mag, roll) },
            { (mag, roll) => new PlayerMutators.GlobalDamageDealtMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.HealingKitAddedSkillMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.HealingKitMultiplierMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.MeleeDamageAddedMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.PlayerMeleeDamageMultiplier(mag, roll) },
         //   { (mag, roll) => new MutatorsForLandblock.ApplyDoT(mag, roll) },
            { (mag, roll) => new PlayerMutators.SpellProjectileMultMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.SpellMinimumDamageAddedMod(mag, roll) },
            { (mag, roll) => new PlayerMutators.SpellMaximumDamageAddedMod(mag, roll) },
        };

        /*
         * 
         * Change mutators to include groups filtered by player role - use action filter
         * Include mutators for mobs
         * 
         * Map Mutators
         * 
         * 1. Player buffs or debuffs (affect each player individually)
         * 
         * 
         * 
         * 
         * 2. Mob buffs 
         * 
         * 
         * 
         * 3. Global Auras
         * DoT
         * Recall spells % chance to fizzle
         * 
         * 
         * 
         * 4. Cosmetic Auras (stuff that has no game effect but changes appearances, purely for entertainment purposes)
         * Mob size increased
         * Player size decreased
         * 
         * 5. Encounters - Shrines, Items, Extra Bosses, Extra Packs, NPCs
         * 
         * 
         * ENCOUNTERS
         * Treasure Chest
         * Buff Shrine
         * 
         * Dungeon List
         * Dungeon of Corpses
            Halls of Lost Light
            Heart of Innocence
            Creepy Canyons
            80+ brood hive
            Drudge Fight
            Eater Pit
            Northern Infiltrator Keep
            Subterannean Farm
            Whispering Caverns
            Fiuns (abayer)
            Golem Sanctuary
            Tusker Assault
            Fenmalain Chamber (shadows)
            Seat of the New Singularity
            Sepulcher of the Hopeslayer
            Lair of the Eviscerators
            Aerbax Citadel
            Paradox-touched Olthoi Queen's Lair
            Harraag's Lair
            Under Drudge Fort
            Prodigal Harbinger's Antechamber
            Hidden Laboratory
            Mosswart Holding
            Prodigal Shadow Child's Lair
         * 
         */

        public static MutatorsForLandblock RollMutators(int tier = 1)
        {
            if (tier < 1)
                tier = 1;
            if (tier > 15)
                tier = 15;

            int numMods;
            if (tier <= 5)
                numMods = 3;
            else if (tier <= 10)
                numMods = 6;
            else
                numMods = 8;


            MutatorsForLandblock mutators = new MutatorsForLandblock();
            for (int i = 0; i < numMods; i++)
            {
                var difficultyMagnitude = TierMagnitudes[tier];
                var roll = ThreadSafeRandom.Next(0, 1);
                var mutator = GetRandomMutator(difficultyMagnitude, roll);
                mutators.AddMutatorToLandblock(mutator);
            }

            return mutators;
        }

        private static LandblockMutator GetRandomMutator(float difficultyMagnitude, int roll)
        {
            var idx = (int)Math.Round((double)ThreadSafeRandom.Next(0, RandomMutatorAction.Count - 1));
            return RandomMutatorAction[idx](difficultyMagnitude, roll);
        }
    }
}
