DELETE FROM `weenie` WHERE `class_Id` = 251010;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (251010, 'alacritarshadow', 10, '2019-09-13 00:00:00') /* Creature */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (251010,   1,         16) /* ItemType - Creature */
     , (251010,   2,         22) /* CreatureType - Shadow */
     , (251010,   3,         39) /* PaletteTemplate - Black */
     , (251010,   6,         -1) /* ItemsCapacity */
     , (251010,   7,         -1) /* ContainersCapacity */
     , (251010,   8,         90) /* Mass */
     , (251010,  16,          1) /* ItemUseable - No */
     , (251010,  25,         60) /* Level */
     , (251010,  27,          0) /* ArmorType - None */
     , (251010,  68,          3) /* TargetingTactic - Random, Focused */
     , (251010,  93,    4195336) /* PhysicsState - ReportCollisions, Gravity, EdgeSlide */
     , (251010, 101,        183) /* AiAllowedCombatStyle - Unarmed, OneHanded, OneHandedAndShield, Bow, Crossbow, ThrownWeapon */
     , (251010, 113,          1) /* Gender - Male */
     , (251010, 133,          2) /* ShowableOnRadar - ShowMovement */
     , (251010, 140,          1) /* AiOptions - CanOpenDoors */
     , (251010, 146,       5800) /* XpOverride */
     , (251010, 188,          1) /* HeritageGroup - Aluvian */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (251010,   1, True ) /* Stuck */
     , (251010,   6, True ) /* AiUsesMana */
     , (251010,  11, False) /* IgnoreCollisions */
     , (251010,  12, True ) /* ReportCollisions */
     , (251010,  13, False) /* Ethereal */
     , (251010,  14, True ) /* GravityStatus */
     , (251010,  19, True ) /* Attackable */
     , (251010,  42, True ) /* AllowEdgeSlide */
     , (251010,  50, True ) /* NeverFailCasting */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (251010,   1,       5) /* HeartbeatInterval */
     , (251010,   2,       0) /* HeartbeatTimestamp */
     , (251010,   3, 0.699999988079071) /* HealthRate */
     , (251010,   4,     2.5) /* StaminaRate */
     , (251010,   5,       1) /* ManaRate */
     , (251010,  12,     0.5) /* Shade */
     , (251010,  13,       0.2) /* ArmorModVsSlash */
     , (251010,  14, 0.2) /* ArmorModVsPierce */
     , (251010,  15, 0.2) /* ArmorModVsBludgeon */
     , (251010,  16, 0.2) /* ArmorModVsCold */
     , (251010,  17,       0.2) /* ArmorModVsFire */
     , (251010,  18, 0.2) /* ArmorModVsAcid */
     , (251010,  19, 0.2) /* ArmorModVsElectric */
     , (251010,  31,      15) /* VisualAwarenessRange */
     , (251010,  34, 1.20000004768372) /* PowerupTime */
     , (251010,  36,       1) /* ChargeSpeed */
     , (251010,  39,       1) /* DefaultScale */
     , (251010,  64,       0.5) /* ResistSlash */
     , (251010,  65,       0.5) /* ResistPierce */
     , (251010,  66,      0.5) /* ResistBludgeon */
     , (251010,  67,      0.5) /* ResistFire */
     , (251010,  68,      0.5) /* ResistCold */
     , (251010,  69,      0.5) /* ResistAcid */
     , (251010,  70,     0.5) /* ResistElectric */
     , (251010,  71,       1) /* ResistHealthBoost */
     , (251010,  72,       1) /* ResistStaminaDrain */
     , (251010,  73,       1) /* ResistStaminaBoost */
     , (251010,  74,       1) /* ResistManaDrain */
     , (251010,  75,       1) /* ResistManaBoost */
     , (251010,  76,     0.5) /* Translucency */
     , (251010,  80,       3) /* AiUseMagicDelay */
     , (251010, 104,      10) /* ObviousRadarRange */
     , (251010, 122,       5) /* AiAcquireHealth */
     , (251010, 125,       1) /* ResistHealthDrain */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (251010,   1, 'Alacritar Shadow') /* Name */
     , (251010,   3, 'Male') /* Sex */
     , (251010,   4, 'Aluvian') /* HeritageGroup */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (251010,   1,   33554433) /* Setup */
     , (251010,   2,  150994945) /* MotionTable */
     , (251010,   3,  536870913) /* SoundTable */
     , (251010,   4,  805306368) /* CombatTable */
     , (251010,   6,   67108990) /* PaletteBase */
     , (251010,   7,  268435632) /* ClothingBase */
     , (251010,   8,  100670397) /* Icon */
     , (251010,   9,   83890445) /* EyesTexture */
     , (251010,  10,   83890551) /* NoseTexture */
     , (251010,  11,   83890646) /* MouthTexture */
     , (251010,  15,   67116986) /* HairPalette */
     , (251010,  16,   67109565) /* EyesPalette */
     , (251010,  17,   67109559) /* SkinPalette */
     , (251010,  22,  872415331) /* PhysicsEffectTable */
     , (251010,  32,        175) /* WieldedTreasureType - 
                                   Wield Yumi (23735) | Probability: 20%
                                   Wield 14x Fire Arrow (1437) | Probability: 100%
                                   Wield Yumi (23735) | Probability: 20%
                                   Wield 14x Arrow (300) | Probability: 100%
                                   Wield Katar (23675) | Probability: 10%
                                   Wield Kite Shield (23685) | Probability: 100%
                                   Wield Nekode (23681) | Probability: 10%
                                   Wield Kite Shield (23685) | Probability: 100%
                                   Wield Cestus (23638) | Probability: 10%
                                   Wield Kite Shield (23685) | Probability: 100%
                                   Wield Tachi (23701) | Probability: 35%
                                   Wield Kite Shield (23685) | Probability: 100%
                                   Wield Fire Yaoji (23719) | Probability: 35%
                                   Wield Kite Shield (23685) | Probability: 100% */
     , (251010,  35,        951) /* DeathTreasureType - Loot Tier: 4 */;

INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)
VALUES (251010,   1, 150, 0, 0) /* Strength */
     , (251010,   2, 100, 0, 0) /* Endurance */
     , (251010,   3, 130, 0, 0) /* Quickness */
     , (251010,   4, 130, 0, 0) /* Coordination */
     , (251010,   5, 110, 0, 0) /* Focus */
     , (251010,   6,  50, 0, 0) /* Self */;

INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)
VALUES (251010,   1,   90, 0, 0, 140) /* MaxHealth */
     , (251010,   3,   180, 0, 0, 310) /* MaxStamina */
     , (251010,   5,   200, 0, 0, 270) /* MaxMana */;

INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)
VALUES (251010, 45, 0, 3, 0, 150, 0, 0) /* LightWeapons        Specialized */
     , (251010, 47, 0, 3, 0, 150, 0, 0) /* MissileWeapons      Specialized */
     , (251010, 46, 0, 3, 0, 150, 0, 0) /* FinesseWeapons      Specialized */
     , (251010,  6, 0, 3, 0,  40, 0, 0) /* MeleeDefense        Specialized */
     , (251010,  7, 0, 3, 0,  40, 0, 0) /* MissileDefense      Specialized */
     , (251010, 44, 0, 3, 0, 150, 0, 0) /* HeavyWeapons        Specialized */
     , (251010, 14, 0, 2, 0, 200, 0, 0) /* ArcaneLore          Trained */
     , (251010, 15, 0, 3, 0,  50, 0, 0) /* MagicDefense        Specialized */
     , (251010, 20, 0, 3, 0,  90, 0, 0) /* Deception           Specialized */
     , (251010, 31, 0, 3, 0,  60, 0, 0) /* CreatureEnchantment Specialized */
     , (251010, 33, 0, 3, 0,  60, 0, 0) /* LifeMagic           Specialized */
     , (251010, 34, 0, 3, 0,  60, 0, 0) /* WarMagic            Specialized */;

INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)
VALUES (251010,  0,  4,  0,    0,  90,  90,   90,  90,   90,  90,   90,   90,    0, 1, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0) /* Head */
     , (251010,  1,  4,  0,    0,  90,  90,   90,  90,   90,  90,   90,   90,    0, 2, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0) /* Chest */
     , (251010,  2,  4,  0,    0,  90,  90,   90,  90,   90,  90,   90,   90,    0, 3,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0) /* Abdomen */
     , (251010,  3,  4,  0,    0,  90,  90,   90,  90,   90,  90,   90,   90,    0, 1, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0) /* UpperArm */
     , (251010,  4,  4,  0,    0,  90,  90,   90,  90,   90,  90,   90,   90,    0, 2,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0) /* LowerArm */
     , (251010,  5,  4, 25, 0.75,  90,  90,   90,  90,   90,  90,   90,   90,    0, 2,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0) /* Hand */
     , (251010,  6,  4,  0,    0,  90,  90,   90,  90,   90,  90,   90,   90,    0, 3,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18) /* UpperLeg */
     , (251010,  7,  4,  0,    0,  90,  90,   90,  90,   90,  90,   90,   90,    0, 3,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6) /* LowerLeg */
     , (251010,  8,  4, 30, 0.75,  90,  90,   90,  90,   90,  90,   90,   90,    0, 3,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22) /* Foot */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (251010,    27,  2.036)  /* Flame Bolt I */
     , (251010,    25,  2.036)  /* Imperil Other I */
     , (251010,    75,  2.036)  /* Lightning Bolt I */
     , (251010,    5357,  2.036)  /* Nether Streak I */;

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251010,  3 /* On Death */,      1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  36 /* InqIntStat */, 0, 1, NULL, 'TestLevel', NULL, 101, 275, NULL, NULL, NULL, NULL, 25, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251010, 22 /* TestSuccess */,      1, NULL, NULL, NULL, 'TestLevel', NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  99 /* TeleTarget */, 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2847146009, 84, 7.1,  94,  0.996917, 0, 0, -0.0784591);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (251010, 23 /* TestFailure */,      1, NULL, NULL, NULL, 'TestLevel', NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  62 /* AwardNoShareXP */, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)
VALUES (251010, 9,  6059,  0, 0, 0.06, False) /* Create Dark Sliver (6059) for ContainTreasure */
     , (251010, 9,     0,  0, 0, 0.94, False) /* Create nothing for ContainTreasure */
     , (251010, 9,  20630,  0, 0, 0.04, False) /* Create MMD (20630) for ContainTreasure */
     , (251010, 9,     0,  0, 0, 0.96, False) /* Create nothing for ContainTreasure */
     , (251010, 9,  60002,  0, 0, 0.03, False) /* Create PK Trophy (60002) for ContainTreasure */
     , (251010, 9,     0,  0, 0, 0.97, False) /* Create nothing for ContainTreasure */;
