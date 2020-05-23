DELETE FROM `weenie` WHERE `class_Id` = 261010;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261010, 'ruschkresident', 10, '2019-09-13 00:00:00') /* Creature */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261010,   1,         16) /* ItemType - Creature */
     , (261010,   2,         81) /* CreatureType - Ruschk */
     , (261010,   3,          2) /* PaletteTemplate - Blue */
     , (261010,   6,         -1) /* ItemsCapacity */
     , (261010,   7,         -1) /* ContainersCapacity */
     , (261010,  16,          1) /* ItemUseable - No */
     , (261010,  25,        160) /* Level */
     , (261010,  27,          0) /* ArmorType - None */
     , (261010,  40,          2) /* CombatMode - Melee */
     , (261010,  68,          9) /* TargetingTactic - Random, TopDamager */
     , (261010,  72,          1) /* FriendType - Olthoi */
     , (261010,  93,       1032) /* PhysicsState - ReportCollisions, Gravity */
     , (261010, 101,        131) /* AiAllowedCombatStyle - Unarmed, OneHanded, ThrownWeapon */
     , (261010, 133,          2) /* ShowableOnRadar - ShowMovement */
     , (261010, 140,          1) /* AiOptions - CanOpenDoors */
     , (261010, 146,     4500000) /* XpOverride */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261010,   1, True ) /* Stuck */
     , (261010,  11, False) /* IgnoreCollisions */
     , (261010,  12, True ) /* ReportCollisions */
     , (261010,  13, False) /* Ethereal */
     , (261010,  14, True ) /* GravityStatus */
     , (261010,  19, True ) /* Attackable */
     , (261010, 101, True ) /* CanGenerateRare */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261010,   1,       5) /* HeartbeatInterval */
     , (261010,   2,       0) /* HeartbeatTimestamp */
     , (261010,   3, 0.0900000035762787) /* HealthRate */
     , (261010,   4,     3.5) /* StaminaRate */
     , (261010,   5, 1.20000004768372) /* ManaRate */
     , (261010,  12,       0) /* Shade */
     , (261010,  13,     3.0) /* ArmorModVsSlash */
     , (261010,  14,     3.0) /* ArmorModVsPierce */
     , (261010,  15,     3.0) /* ArmorModVsBludgeon */
     , (261010,  16,     3.0) /* ArmorModVsCold */
     , (261010,  17,     3.0) /* ArmorModVsFire */
     , (261010,  18,     3.0) /* ArmorModVsAcid */
     , (261010,  19,     3.0) /* ArmorModVsElectric */
     , (261010,  31,      10) /* VisualAwarenessRange */
     , (261010,  34,       1) /* PowerupTime */
     , (261010,  36,       1) /* ChargeSpeed */
     , (261010,  39,    1.25) /* DefaultScale */
     , (261010,  64,       0.5) /* ResistSlash */
     , (261010,  65,       0.5) /* ResistPierce */
     , (261010,  66,       0.5) /* ResistBludgeon */
     , (261010,  67,       0.5) /* ResistFire */
     , (261010,  68,       0.5) /* ResistCold */
     , (261010,  69,       0.5) /* ResistAcid */
     , (261010,  70,       0.5) /* ResistElectric */
     , (261010,  71,       1) /* ResistHealthBoost */
     , (261010,  72,     0.5) /* ResistStaminaDrain */
     , (261010,  73,       1) /* ResistStaminaBoost */
     , (261010,  74,     0.5) /* ResistManaDrain */
     , (261010,  75,       1) /* ResistManaBoost */
     , (261010, 104,      10) /* ObviousRadarRange */
     , (261010, 125,     0.5) /* ResistHealthDrain */
     , (261010, 166,     0.8) /* ResistNether */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261010,   1, 'Ruschk Resident') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261010,   1,   33559104) /* Setup */
     , (261010,   2,  150994951) /* MotionTable */
     , (261010,   3,  536871101) /* SoundTable */
     , (261010,   4,  805306372) /* CombatTable */
     , (261010,   6,   67115447) /* PaletteBase */
     , (261010,   7,  268436946) /* ClothingBase */
     , (261010,   8,  100677373) /* Icon */
     , (261010,  22,  872415364) /* PhysicsEffectTable */
     , (261010,  32,        488) /* WieldedTreasureType - 
                                   Wield Stone Mace (29997) | Probability: 20%
                                   Wield Bone Dagger (30002) | Probability: 20%
                                   Wield Stone Hatchet (30007) | Probability: 20%
                                   Wield Stone Spear (29987) | Probability: 20%
                                   Wield Bone Sword (29992) | Probability: 20% */
     , (261010,  35,        998) /* DeathTreasureType - Loot Tier: 7 -- epics only no sets */;

INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)
VALUES (261010,   1, 300, 0, 0) /* Strength */
     , (261010,   2, 250, 0, 0) /* Endurance */
     , (261010,   3, 220, 0, 0) /* Quickness */
     , (261010,   4, 210, 0, 0) /* Coordination */
     , (261010,   5, 180, 0, 0) /* Focus */
     , (261010,   6, 190, 0, 0) /* Self */;

INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)
VALUES (261010,   1,   2500, 0, 0, 3000) /* MaxHealth */
     , (261010,   3,   800, 0, 0, 1030) /* MaxStamina */
     , (261010,   5,   500, 0, 0, 670) /* MaxMana */;

INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)
VALUES (261010, 46, 0, 3, 0, 500, 0, 0) /* FinesseWeapons      Specialized */
     , (261010,  6, 0, 3, 0, 450, 0, 0) /* MeleeDefense        Specialized */
     , (261010,  7, 0, 3, 0, 450, 0, 0) /* MissileDefense      Specialized */
     , (261010, 44, 0, 3, 0, 500, 0, 0) /* HeavyWeapons        Specialized */
     , (261010, 47, 0, 3, 0, 300, 0, 0) /* MissileWeapons      Specialized */
     , (261010, 45, 0, 3, 0, 500, 0, 0) /* LightWeapons        Specialized */
     , (261010, 15, 0, 3, 0, 410, 0, 0) /* MagicDefense        Specialized */
     , (261010, 22, 0, 2, 0,  20, 0, 0) /* Jump                Trained */
     , (261010, 24, 0, 2, 0,  20, 0, 0) /* Run                 Trained */
     , (261010, 33, 0, 3, 0, 400, 0, 0) /* LifeMagic           Specialized */
     , (261010, 34, 0, 3, 0, 420, 0, 0) /* WarMagic            Specialized */;

INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)
VALUES (261010,  0,  4,  0,    0,  150,  150,  150,  150,  150,  150,  150,  150,    0, 1, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0) /* Head */
     , (261010,  1,  4,  0,    0,  150,  150,  150,  150,  150,  150,  150,  150,    0, 2, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0) /* Chest */
     , (261010,  2,  4,  0,    0,  150,  150,  150,  150,  150,  150,  150,  150,    0, 3,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0) /* Abdomen */
     , (261010,  3,  4,  0,    0,  150,  150,  150,  150,  150,  150,  150,  150,    0, 1, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0) /* UpperArm */
     , (261010,  4,  4,  0,    0,  150,  150,  150,  150,  150,  150,  150,  150,    0, 2,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0) /* LowerArm */
     , (261010,  5,  4, 100,  0.5,  150,  150,  150,  150,  150,  150,  150,  150,    0, 2,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0) /* Hand */
     , (261010,  6,  4,  0,    0,  150,  150,  150,  150,  150,  150,  150,  150,    0, 3,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18) /* UpperLeg */
     , (261010,  7,  4,  0,    0,  150,  150,  150,  150,  150,  150,  150,  150,    0, 3,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6) /* LowerLeg */
     , (261010,  8,  4,  75,  0.4,  150,  150,  150,  150,  150,  150,  150,  150,    0, 3,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22) /* Foot */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (261010,  2053,  2.010)  /* Armor Self 7 */
     , (261010,  1065,  2.015)  /* Cold Vulnerability Other VI */
     , (261010,  1327,  2.015)  /* Imperil Other VI */
     , (261010,  2730,  2.025)  /* Frost Arc VI */
     , (261010,  2166,  2.008)  /* Tusker gift */
     , (261010,  2174,  2.008)  /* archers gift */;

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (261010,  5 /* HeartBeat */,   0.05, NULL, 2147483708 /* HandCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435540 /* Twitch4 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (261010,  5 /* HeartBeat */,   0.14, NULL, 2147483708 /* HandCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435539 /* Twitch3 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (261010,  5 /* HeartBeat */,   0.19, NULL, 2147483708 /* HandCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435538 /* Twitch2 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (261010,  5 /* HeartBeat */,    0.2, NULL, 2147483708 /* HandCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435537 /* Twitch1 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (261010,  5 /* HeartBeat */,    0.1, NULL, 2147483710 /* SwordCombat */, 1090519043 /* Ready */, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,   5 /* Motion */, 0, 1, 268435537 /* Twitch1 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (261010, 21 /* ResistSpell */,   0.65, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  18 /* DirectBroadcast */, 0, 1, NULL, 'Ruschk mumbles something incoherent...an icy chill comes over you.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)
VALUES (261010, 9,  20630,  0, 0, 0.04, False) /* Create MMD (20630) for ContainTreasure */
     , (261010, 9,     0,  0, 0, 0.96, False) /* Create nothing for ContainTreasure */
     , (261010, 9,  60002,  0, 0, 0.02, False) /* Create PK Trophy (60002) for ContainTreasure */
     , (261010, 9,     0,  0, 0, 0.98, False) /* Create nothing for ContainTreasure */
     , (261010, 9,  38456,  0, 0, 0.0004, False) /* Create PK Trophy (60002) for ContainTreasure */
     , (261010, 9,     0,  0, 0, 0.9996, False) /* Create nothing for ContainTreasure */;
