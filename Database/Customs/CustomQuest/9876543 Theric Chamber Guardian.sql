DELETE FROM `weenie` WHERE `class_Id` = 9876543;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (9876543, 'Theric Chamber Guardian', 10, '2019-09-13 00:00:00') /* Creature */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (9876543,   1,         16) /* ItemType - Creature */
     , (9876543,   2,         13) /* CreatureType - Golem */
     , (9876543,   6,        255) /* ItemsCapacity */
     , (9876543,   7,        255) /* ContainersCapacity */
     , (9876543,  16,          1) /* ItemUseable - No */
     , (9876543,  25,        750) /* Level */
     , (9876543,  27,         32) /* ArmorType - Metal */
     , (9876543,  40,          2) /* CombatMode - Melee */
     , (9876543,  67,         64) /* Tolerance - Retaliate */
     , (9876543,  68,         13) /* TargetingTactic - Random, LastDamager, TopDamager */
     , (9876543,  93,       1032) /* PhysicsState - ReportCollisions, Gravity */
     , (9876543, 101,        131) /* AiAllowedCombatStyle - Unarmed, OneHanded, ThrownWeapon */
     , (9876543, 133,          2) /* ShowableOnRadar - ShowMovement */
     , (9876543, 146,   86000000) /* XpOverride */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (9876543,   1, True ) /* Stuck */
     , (9876543,  11, False) /* IgnoreCollisions */
     , (9876543,  12, True ) /* ReportCollisions */
     , (9876543,  13, False) /* Ethereal */
     , (9876543,  14, True ) /* GravityStatus */
     , (9876543,  19, True ) /* Attackable */
     , (9876543,  66, True ) /* IgnoreMagicArmor */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (9876543,   1,       8) /* HeartbeatInterval */
     , (9876543,   2,       0) /* HeartbeatTimestamp */
     , (9876543,   3,     900) /* HealthRate */
     , (9876543,   4,     250) /* StaminaRate */
     , (9876543,   5,     150) /* ManaRate */
     , (9876543,  13,     1.6) /* ArmorModVsSlash */
     , (9876543,  14,     1.6) /* ArmorModVsPierce */
     , (9876543,  15,     1.6) /* ArmorModVsBludgeon */
     , (9876543,  16,     1.6) /* ArmorModVsCold */
     , (9876543,  17,     1.6) /* ArmorModVsFire */
     , (9876543,  18,     1.6) /* ArmorModVsAcid */
     , (9876543,  19,     1.6) /* ArmorModVsElectric */
     , (9876543,  20,       2) /* CombatSpeed */
     , (9876543,  31,      10) /* VisualAwarenessRange */
     , (9876543,  34,       1) /* PowerupTime */
     , (9876543,  36,       2) /* ChargeSpeed */
     , (9876543,  39,     1.0) /* DefaultScale */
     , (9876543,  64,     1.1) /* ResistSlash */
     , (9876543,  65,     1.1) /* ResistPierce */
     , (9876543,  66,     1.1) /* ResistBludgeon */
     , (9876543,  67,     1.1) /* ResistFire */
     , (9876543,  68,     1.1) /* ResistCold */
     , (9876543,  69,     1.1) /* ResistAcid */
     , (9876543,  70,     1.1) /* ResistElectric */
     , (9876543,  71,       1) /* ResistHealthBoost */
     , (9876543,  72,       1) /* ResistStaminaDrain */
     , (9876543,  73,       1) /* ResistStaminaBoost */
     , (9876543,  74,       1) /* ResistManaDrain */
     , (9876543,  75,       1) /* ResistManaBoost */
     , (9876543, 104,       8) /* ObviousRadarRange */
     , (9876543, 117,     0.7) /* FocusedProbability */
     , (9876543, 125,       1) /* ResistHealthDrain */
     , (9876543, 151,       1) /* IgnoreShield */
     , (9876543, 166,     1.1) /* ResistNether */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (9876543,   1, 'Theric Chamber Guardian') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (9876543,   1,   33560279) /* Setup */
     , (9876543,   2,  150995334) /* MotionTable */
     , (9876543,   3,  536870933) /* SoundTable */
     , (9876543,   4,  805306368) /* CombatTable */
     , (9876543,   7,  268436634) /* ClothingBase */
     , (9876543,   8,  100674350) /* Icon */
     , (9876543,  22,  872415269) /* PhysicsEffectTable */;

INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)
VALUES (9876543,   1, 490, 0, 0) /* Strength */
     , (9876543,   2, 1000, 0, 0) /* Endurance */
     , (9876543,   3, 430, 0, 0) /* Quickness */
     , (9876543,   4, 350, 0, 0) /* Coordination */
     , (9876543,   5, 450, 0, 0) /* Focus */
     , (9876543,   6, 500, 0, 0) /* Self */;

INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)
VALUES (9876543,   1, 100000, 0, 0, 100000) /* MaxHealth */
     , (9876543,   3, 19000, 0, 0, 20000) /* MaxStamina */
     , (9876543,   5,  9500, 0, 0, 10000) /* MaxMana */;

INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)
VALUES (9876543,  6, 0, 2, 0, 350, 0, 0) /* MeleeDefense        Trained */
     , (9876543,  7, 0, 2, 0, 300, 0, 0) /* MissileDefense      Trained */
     , (9876543, 15, 0, 2, 0, 300, 0, 0) /* MagicDefense        Trained */
     , (9876543, 41, 0, 3, 0, 370, 0, 0) /* TwoHandedCombat     Specialized */
     , (9876543, 34, 0, 3, 0, 500, 0, 0) /* WarMagic            Specialized */
     , (9876543, 33, 0, 3, 0, 9000, 0, 0) /* LifeMagic          Specialized */
     , (9876543, 44, 0, 3, 0, 370, 0, 0) /* HeavyWeapons        Specialized */;

INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)
VALUES (9876543,  0,  4,  0,    0,  650,  720,  720,  720,  560,  480,  430,  600,    0, 1, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0, 0.33,    0,    0) /* Head */
     , (9876543,  1,  4,  0,    0,  650,  720,  720,  720,  560,  480,  430,  600,    0, 2, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0, 0.44, 0.17,    0) /* Chest */
     , (9876543,  2,  4,  0,    0,  650,  720,  720,  720,  560,  480,  430,  600,    0, 3,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0,    0, 0.17,    0) /* Abdomen */
     , (9876543,  3,  4,  0,    0,  650,  720,  720,  720,  560,  480,  430,  600,    0, 1, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0, 0.23, 0.03,    0) /* UpperArm */
     , (9876543,  4,  4,  0,    0,  650,  720,  720,  720,  560,  480,  430,  600,    0, 2,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0,    0,  0.3,    0) /* LowerArm */
     , (9876543,  5,  4,  2, 0.75,  650,  720,  720,  720,  560,  480,  430,  600,    0, 2,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0,    0,  0.2,    0) /* Hand */
     , (9876543,  6,  4,  0,    0,  650,  720,  720,  720,  560,  480,  430,  600,    0, 3,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18,    0, 0.13, 0.18) /* UpperLeg */
     , (9876543,  7,  4,  0,    0,  650,  720,  720,  720,  560,  480,  430,  600,    0, 3,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6,    0,    0,  0.6) /* LowerLeg */
     , (9876543,  8,  4,  2, 0.75,  650,  720,  720,  720,  560,  480,  430,  600,    0, 3,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22,    0,    0, 0.22) /* Foot */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (9876543,  2142,   2.13)  /* Tempest */
     , (9876543,  1326,   2.05)  /* Imperil V */
     , (9876543,  2737,   2.10)  /* Lightning Arc VI */
     , (9876543,  2172,   2.05)  /* Lightning Vulnerability 7 */
     , (9876543,  6197,   2.10)  /* Lightning Ring */
     , (9876543,  2054,   2.04)  /* Focus Debuff 7 */
     , (9876543,    79,   2.07)  /* Lightning Bolt 5 */
     , (9876543,  4332,   2.01)  /* Nullify All Magic Other */;

INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)
VALUES (9876543, 10, 35297,  1, 0, 1, True) /* Create Greatsword of Flame and Light (35297) for WieldTreasure */
     , (9876543, 9, 77655558,  1, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* Create Pyreal Mote (6353) for ContainTreasure */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */
     , (9876543, 9, 77655558,  0, 0, 1, False) /* energy */
     , (9876543, 9,     0,  0, 0, 0, False) /* Create nothing for ContainTreasure */;
