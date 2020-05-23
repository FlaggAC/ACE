DELETE FROM `weenie` WHERE `class_Id` = 151001;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (151001, 'sanctuarygolem', 10, '2019-09-13 00:00:00') /* Creature */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (151001,   1,         16) /* ItemType - Creature */
     , (151001,   2,         13) /* CreatureType - Golem */
     , (151001,   3,          2) /* PaletteTemplate - Blue */
     , (151001,   6,         -1) /* ItemsCapacity */
     , (151001,   7,         -1) /* ContainersCapacity */
     , (151001,  16,          1) /* ItemUseable - No */
     , (151001,  25,        400) /* Level */
     , (151001,  27,          0) /* ArmorType - None */
     , (151001,  40,          2) /* CombatMode - Melee */
     , (151001,  68,          9) /* TargetingTactic - Random, TopDamager */
     , (151001,  93,       1032) /* PhysicsState - ReportCollisions, Gravity */
     , (151001, 133,          2) /* ShowableOnRadar - ShowMovement */
     , (151001, 146,   15000000) /* XpOverride */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (151001,   1, True ) /* Stuck */
     , (151001,   6, True ) /* AiUsesMana */
     , (151001,  11, False) /* IgnoreCollisions */
     , (151001,  12, True ) /* ReportCollisions */
     , (151001,  13, False) /* Ethereal */
     , (151001,  14, True ) /* GravityStatus */
     , (151001,  19, True ) /* Attackable */
     , (151001,  50, True ) /* NeverFailCasting */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (151001,   1,       5) /* HeartbeatInterval */
     , (151001,   2,       0) /* HeartbeatTimestamp */
     , (151001,   3,     0.9) /* HealthRate */
     , (151001,   4,     0.5) /* StaminaRate */
     , (151001,   5,       2) /* ManaRate */
     , (151001,   6,     0.1) /* HealthUponResurrection */
     , (151001,   7,    0.25) /* StaminaUponResurrection */
     , (151001,   8,     0.3) /* ManaUponResurrection */
     , (151001,  12,     0.5) /* Shade */
     , (151001,  13,     1.7) /* ArmorModVsSlash */
     , (151001,  14,     1.7) /* ArmorModVsPierce */
     , (151001,  15,     1.7) /* ArmorModVsBludgeon */
     , (151001,  16,     1.7) /* ArmorModVsCold */
     , (151001,  17,     1.7) /* ArmorModVsFire */
     , (151001,  18,     1.7) /* ArmorModVsAcid */
     , (151001,  19,     1.7) /* ArmorModVsElectric */
     , (151001,  31,      15) /* VisualAwarenessRange */
     , (151001,  34,       1) /* PowerupTime */
     , (151001,  39,     1.0) /* DefaultScale */
     , (151001,  64,     1.4) /* ResistSlash */
     , (151001,  65,     1.4) /* ResistPierce */
     , (151001,  66,     1.4) /* ResistBludgeon */
     , (151001,  67,     1.4) /* ResistFire */
     , (151001,  68,     1.4) /* ResistCold */
     , (151001,  69,     1.4) /* ResistAcid */
     , (151001,  70,     1.4) /* ResistElectric */
     , (151001,  71,       1) /* ResistHealthBoost */
     , (151001,  72,       1) /* ResistStaminaDrain */
     , (151001,  73,       1) /* ResistStaminaBoost */
     , (151001,  74,       1) /* ResistManaDrain */
     , (151001,  75,       1) /* ResistManaBoost */
     , (151001,  80,       3) /* AiUseMagicDelay */
     , (151001, 104,       5) /* ObviousRadarRange */
     , (151001, 122,       2) /* AiAcquireHealth */
     , (151001, 125,       1) /* ResistHealthDrain */
     , (151001, 166,       1.8) /* ResistNether */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (151001,   1, 'Sanctuary Golem') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (151001,   1,   33558578) /* Setup */
     , (151001,   2,  150995073) /* MotionTable */
     , (151001,   3,  536871066) /* SoundTable */
     , (151001,   4,  805306376) /* CombatTable */
     , (151001,   6,   67114905) /* PaletteBase */
     , (151001,   7,  268436838) /* ClothingBase */
     , (151001,   8,  100667940) /* Icon */
     , (151001,  22,  872415322) /* PhysicsEffectTable */
     , (151001,  35,       1000) /* DeathTreasureType - Loot Tier: 7 */;

INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)
VALUES (151001,   1, 500, 0, 0) /* Strength */
     , (151001,   2, 850, 0, 0) /* Endurance */
     , (151001,   3, 520, 0, 0) /* Quickness */
     , (151001,   4, 620, 0, 0) /* Coordination */
     , (151001,   5, 850, 0, 0) /* Focus */
     , (151001,   6, 900, 0, 0) /* Self */;

INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)
VALUES (151001,   1,   22000, 0, 0, 22000) /* MaxHealth */
     , (151001,   3,   8000, 0, 0, 8000) /* MaxStamina */
     , (151001,   5,   10000, 0, 0, 10000) /* MaxMana */;

INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)
VALUES (151001,  6, 0, 3, 0, 280, 0, 1982.44181784858) /* MeleeDefense        Specialized */
     , (151001,  7, 0, 3, 0, 358, 0, 1982.44181784858) /* MissileDefense      Specialized */
     , (151001, 13, 0, 3, 0, 250, 0, 1982.44181784858) /* UnarmedCombat       Specialized */
     , (151001, 14, 0, 2, 0, 300, 0, 1982.44181784858) /* ArcaneLore          Trained */
     , (151001, 15, 0, 3, 0, 150, 0, 1982.44181784858) /* MagicDefense        Specialized */
     , (151001, 20, 0, 2, 0,  50, 0, 1982.44181784858) /* Deception           Trained */
     , (151001, 22, 0, 2, 0,  10, 0, 1982.44181784858) /* Jump                Trained */
     , (151001, 24, 0, 2, 0,  50, 0, 1982.44181784858) /* Run                 Trained */
     , (151001, 31, 0, 3, 0, 9000, 0, 1982.44181784858) /* CreatureEnchantment Specialized */
     , (151001, 33, 0, 3, 0, 9000, 0, 1982.44181784858) /* LifeMagic           Specialized */
     , (151001, 34, 0, 3, 0, 15, 0, 1982.44181784858) /* WarMagic            Specialized */;

INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)
VALUES (151001,  0,  4,  0,    0,  400,  480,  440,  360,  480,  560,  440,  560,    0, 1, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0) /* Head */
     , (151001,  1,  4,  0,    0,  400,  480,  440,  360,  480,  560,  440,  560,    0, 2, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0) /* Chest */
     , (151001,  2,  4,  0,    0,  400,  480,  440,  360,  480,  560,  440,  560,    0, 3,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0) /* Abdomen */
     , (151001,  3,  4,  0,    0,  400,  480,  440,  360,  480,  560,  440,  560,    0, 1, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0) /* UpperArm */
     , (151001,  4,  4,  0,    0,  400,  480,  440,  360,  480,  560,  440,  560,    0, 2,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0) /* LowerArm */
     , (151001,  5,  4, 180, 0.55,  400,  480,  440,  360,  480,  560,  440,  560,    0, 2,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0) /* Hand */
     , (151001,  6,  4,  0,    0,  400,  480,  440,  360,  480,  560,  440,  560,    0, 3,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18) /* UpperLeg */
     , (151001,  7,  4,  0,    0,  400,  480,  440,  360,  480,  560,  440,  560,    0, 3,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6) /* LowerLeg */
     , (151001,  8,  4, 150, 0.55,  400,  480,  440,  360,  480,  560,  440,  560,    0, 3,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22) /* Foot */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (151001,  1161,   2.02)  /* Heal Self VI */
     , (151001,  1343,   2.03)  /* Weakness Other VI */
     , (151001,  1839,   2.01)  /* Blistering Creeper */
     , (151001,  4312,   2.03)  /* Incantation Imperil */
     , (151001,  2122,   2.08)  /* Disintegration */
     , (151001,  4473,   2.03)  /* Incantation Acid Vuln */
     , (151001,  2717,   2.10)  /* Acid Arc VII */
     , (151001,  4432,   2.01)  /* Incantation of Acid Streak */;


INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (151001,  5 /* HeartBeat */,  0.075, NULL, 2147483708 /* HandCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435537 /* Twitch1 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (151001,  5 /* HeartBeat */,      1, NULL, 2147483709 /* NonCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 1090519043 /* Ready */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
     , (@parent_id,  1,   5 /* Motion */, 0, 1, 1090519060 /* Sleeping */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)
VALUES (151001, 9, 20630,  5, 0, 0.10, False) /* MMD */
     , (151001, 9,     0,  0, 0, 0.90, False) /* Create nothing for ContainTreasure */
     , (151001, 9,  6353,  0, 0, 0.05, False) /* Create Pyreal Mote (6353) for ContainTreasure */
     , (151001, 9,     0,  0, 0, 0.95, False) /* Create nothing for ContainTreasure */
     , (151001, 9,  9511,  0, 0, 0.10, False) /* Golden Gromnie */
     , (151001, 9,     0,  0, 0, 0.90, False) /* Create nothing for ContainTreasure */
     , (151001, 9,  60002,  0, 0, 0.05, False) /* Pk Trophy */
     , (151001, 9,     0,  0, 0, 0.95, False) /* Create nothing for ContainTreasure */;
