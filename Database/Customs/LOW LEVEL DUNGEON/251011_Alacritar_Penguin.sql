DELETE FROM `weenie` WHERE `class_Id` = 251011;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (251011, 'alacritarpenguin', 10, '2019-04-09 23:37:09') /* Creature */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (251011,   1,         16) /* ItemType - Creature */
     , (251011,   2,         80) /* CreatureType - Penguin */
     , (251011,   3,          8) /* PaletteTemplate - Green */
     , (251011,   6,         -1) /* ItemsCapacity */
     , (251011,   7,         -1) /* ContainersCapacity */
     , (251011,  16,          1) /* ItemUseable - No */
     , (251011,  25,         99) /* Level */
     , (251011,  27,          0) /* ArmorType - None */
     , (251011,  40,          2) /* CombatMode - Melee */
     , (251011,  68,          9) /* TargetingTactic - Random, TopDamager */
     , (251011,  72,         80) /* FriendType - Penguin */
     , (251011,  93,       1032) /* PhysicsState - ReportCollisions, Gravity */
     , (251011, 101,        131) /* AiAllowedCombatStyle - Unarmed, OneHanded, ThrownWeapon */
     , (251011, 133,          2) /* ShowableOnRadar - ShowMovement */
     , (251011, 140,          1) /* AiOptions - CanOpenDoors */
     , (251011, 146,     205000) /* XpOverride */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (251011,   1, True ) /* Stuck */
     , (251011,  11, False) /* IgnoreCollisions */
     , (251011,  12, True ) /* ReportCollisions */
     , (251011,  13, False) /* Ethereal */
     , (251011,  14, True ) /* GravityStatus */
     , (251011,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (251011,   1,       5) /* HeartbeatInterval */
     , (251011,   2,       0) /* HeartbeatTimestamp */
     , (251011,   3,     0.5) /* HealthRate */
     , (251011,   4,       3) /* StaminaRate */
     , (251011,   5,       1) /* ManaRate */
     , (251011,  12,       0) /* Shade */
     , (251011,  13, 0.2) /* ArmorModVsSlash */
     , (251011,  14,       0.2) /* ArmorModVsPierce */
     , (251011,  15, 0.2) /* ArmorModVsBludgeon */
     , (251011,  16, 0.2) /* ArmorModVsCold */
     , (251011,  17, 0.2) /* ArmorModVsFire */
     , (251011,  18,       1) /* ArmorModVsAcid */
     , (251011,  19, 0.2) /* ArmorModVsElectric */
     , (251011,  31,       15) /* VisualAwarenessRange */
     , (251011,  34,       1) /* PowerupTime */
     , (251011,  36,       1) /* ChargeSpeed */
     , (251011,  39,       2.5) /* DefaultScale */
     , (251011,  64,     0.5) /* ResistSlash */
     , (251011,  65,     0.5) /* ResistPierce */
     , (251011,  66,     0.5) /* ResistBludgeon */
     , (251011,  67,     0.5) /* ResistFire */
     , (251011,  68,     0.5) /* ResistCold */
     , (251011,  69,     0.5) /* ResistAcid */
     , (251011,  70,     0.5) /* ResistElectric */
     , (251011,  71,       1) /* ResistHealthBoost */
     , (251011,  72,     0.5) /* ResistStaminaDrain */
     , (251011,  73,       1) /* ResistStaminaBoost */
     , (251011,  74,     0.5) /* ResistManaDrain */
     , (251011,  75,       1) /* ResistManaBoost */
     , (251011,  80,       4) /* AiUseMagicDelay */
     , (251011, 104,      12) /* ObviousRadarRange */
     , (251011, 125,     0.5) /* ResistHealthDrain */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (251011,   1, 'Alacritar Penguin') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (251011,   1,   33559122) /* Setup */
     , (251011,   2,  150995323) /* MotionTable */
     , (251011,   3,  536871098) /* SoundTable */
     , (251011,   4,  805306432) /* CombatTable */
     , (251011,   6,   67115388) /* PaletteBase */
     , (251011,   7,  268436889) /* ClothingBase */
     , (251011,   8,  100677366) /* Icon */
     , (251011,  22,  872415411) /* PhysicsEffectTable */
     , (251011,  35,        448) /* DeathTreasureType - Loot Tier: 4 */;

INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)
VALUES (251011,   1, 360, 0, 0) /* Strength */
     , (251011,   2, 360, 0, 0) /* Endurance */
     , (251011,   3, 360, 0, 0) /* Quickness */
     , (251011,   4, 250, 0, 0) /* Coordination */
     , (251011,   5,  20, 0, 0) /* Focus */
     , (251011,   6,  20, 0, 0) /* Self */;

INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)
VALUES (251011,   1,   700, 0, 0, 850) /* MaxHealth */
     , (251011,   3,   400, 0, 0, 800) /* MaxStamina */
     , (251011,   5,     0, 0, 0, 20) /* MaxMana */;

INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)
VALUES (251011,  6, 0, 3, 0,  70, 0, 0) /* MeleeDefense        Specialized */
     , (251011,  7, 0, 3, 0,  50, 0, 0) /* MissileDefense      Specialized */
     , (251011, 13, 0, 1, 0, 275, 0, 0) /* UnarmedCombat       Untrained */
     , (251011, 15, 0, 3, 0, 320, 0, 0) /* MagicDefense        Specialized */
     , (251011, 22, 0, 2, 0,   2, 0, 0) /* Jump                Trained */
     , (251011, 24, 0, 2, 0,   2, 0, 0) /* Run                 Trained */
     , (251011, 31, 0, 3, 0,  35, 0, 0) /* CreatureEnchantment Specialized */
     , (251011, 33, 0, 3, 0,  15, 0, 0) /* LifeMagic           Specialized */
     , (251011, 34, 0, 3, 0,  35, 0, 0) /* WarMagic            Specialized */;

INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)
VALUES (251011,  0,  2, 100,  0.6,  100,  100,  100,  100,  100,  100,  100,  100,  100, 1, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0) /* Head */
     , (251011,  1,  4, 100,  0.3,  100,  100,  100,  100,  100,  100,  100,  100,  100, 2, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0) /* Chest */
     , (251011,  2,  4, 100,  0.4,  100,  100,  100,  100,  100,  100,  100,  100,  100, 3,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0) /* Abdomen */
     , (251011,  3,  4, 100,  0.3,  100,  100,  100,  100,  100,  100,  100,  100,  100, 1, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0) /* UpperArm */
     , (251011,  4,  4, 100,  0.4,  100,  100,  100,  100,  100,  100,  100,  100,  100, 2,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0) /* LowerArm */
     , (251011,  5,  4, 120,  0.4,  100,  100,  100,  100,  100,  100,  100,  100,  100, 2,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0) /* Hand */
     , (251011,  6,  4, 100,  0.3,  100,  100,  100,  100,  100,  100,  100,  100,  100, 3,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18) /* UpperLeg */
     , (251011,  7,  4, 100,  0.4,  100,  100,  100,  100,  100,  100,  100,  100,  100, 3,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6) /* LowerLeg */
     , (251011,  8,  4, 100,  0.4,  100,  100,  100,  100,  100,  100,  100,  100,  100, 3,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22) /* Foot */;

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251011,  5 /* HeartBeat */,  0.085, NULL, 2147483709 /* NonCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435537 /* Twitch1 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251011,  5 /* HeartBeat */,    0.1, NULL, 2147483709 /* NonCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435538 /* Twitch2 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251011,  5 /* HeartBeat */,   0.15, NULL, 2147483709 /* NonCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435539 /* Twitch3 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251011,  3 /* On Death */,      1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  36 /* InqIntStat */, 0, 1, NULL, 'TestLevel', NULL, 101, 275, NULL, NULL, NULL, NULL, 25, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251011, 22 /* TestSuccess */,      1, NULL, NULL, NULL, 'TestLevel', NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  99 /* TeleTarget */, 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2847146009, 84, 7.1,  94,  0.996917, 0, 0, -0.0784591);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251011, 23 /* TestFailure */,      1, NULL, NULL, NULL, 'TestLevel', NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  62 /* AwardNoShareXP */, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)
VALUES (251011, 9, 23853,  0, 0, 0.00625, False) /* Create Seared Shard (23853) for ContainTreasure */
     , (251011, 9, 23854,  0, 0, 0.00625, False) /* Create Chilled Shard (23854) for ContainTreasure */
     , (251011, 9, 23849,  0, 0, 0.00625, False) /* Create Scored Shard (23849) for ContainTreasure */
     , (251011, 9, 23855,  0, 0, 0.00625, False) /* Create Charged Shard (23855) for ContainTreasure */
     , (251011, 9, 23851,  0, 0, 0.00625, False) /* Create Solid Shard (23851) for ContainTreasure */
     , (251011, 9, 23856,  0, 0, 0.00625, False) /* Create Hardened Shard (23856) for ContainTreasure */
     , (251011, 9, 23852,  0, 0, 0.00625, False) /* Create Plated Shard (23852) for ContainTreasure */
     , (251011, 9, 23850,  0, 0, 0.00625, False) /* Create Brilliant Shard (23850) for ContainTreasure */
     , (251011, 9,     0,  0, 0, 0.95, False) /* Create nothing for ContainTreasure */
     , (251011, 9,  20630,  0, 0, 0.04, False) /* Create MMD (20630) for ContainTreasure */
     , (251011, 9,     0,  0, 0, 0.96, False) /* Create nothing for ContainTreasure */
     , (251011, 9,  60002,  0, 0, 0.02, False) /* Create PK Trophy (60002) for ContainTreasure */
     , (251011, 9,     0,  0, 0, 0.98, False) /* Create nothing for ContainTreasure */
     , (251011, 9,  6056,  0, 0, 0.02, False) /* Create Small Shard (6056) for ContainTreasure */
     , (251011, 9,     0,  0, 0, 0.98, False) /* Create nothing for ContainTreasure */
     , (251011, 9, 45875,  1, 0, 0.02, False) /* Create Lucky Gold Letter (45875) for ContainTreasure */
     , (251011, 9,     0,  0, 0, 0.98, False) /* Create nothing for ContainTreasure */;
