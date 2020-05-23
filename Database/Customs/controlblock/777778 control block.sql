DELETE FROM `weenie` WHERE `class_Id` = 777778;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (777778, 'controlblock', 10, '2005-02-09 10:00:00') /* Creature */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (777778,   1,         16) /* ItemType - Creature */
     , (777778,   2,         63) /* CreatureType - Statue */
     , (777778,   6,         -1) /* ItemsCapacity */
     , (777778,   7,         -1) /* ContainersCapacity */
     , (777778,   8,        120) /* Mass */
     , (777778,  16,         32) /* ItemUseable - Remote */
     , (777778,  25,        239) /* Level */
     , (777778,  27,          0) /* ArmorType - None */
     , (777778,  93,    6292504) /* PhysicsState - ReportCollisions, IgnoreCollisions, Gravity, ReportCollisionsAsEnvironment, EdgeSlide */
     , (777778,  95,          3) /* RadarBlipColor - White */
     , (777778, 133,          0) /* ShowableOnRadar - Undefined */
     , (777778, 134,         16) /* PlayerKillerStatus - RubberGlue */
     , (777778, 146,      20166) /* XpOverride */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (777778,   1, True ) /* Stuck */
     , (777778,   6, False) /* AiUsesMana */
     , (777778,   8, False ) /* AllowGive */
     , (777778,  12, True ) /* ReportCollisions */
     , (777778,  13, False) /* Ethereal */
     , (777778,  19, False) /* Attackable */
     , (777778,  41, True ) /* ReportCollisionsAsEnvironment */
     , (777778,  42, True ) /* AllowEdgeSlide */
     , (777778,  50, True ) /* NeverFailCasting */
     , (777778,  52, True ) /* AiImmobile */
     , (777778,  82, True ) /* DontTurnOrMoveWhenGiving */
     , (777778,  83, True ) /* NpcLooksLikeObject */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (777778,   1,       5) /* HeartbeatInterval */
     , (777778,   2,       0) /* HeartbeatTimestamp */
     , (777778,   3,     1.1) /* HealthRate */
     , (777778,   4,     0.5) /* StaminaRate */
     , (777778,   5,       2) /* ManaRate */
     , (777778,  13,    0.79) /* ArmorModVsSlash */
     , (777778,  14,    0.79) /* ArmorModVsPierce */
     , (777778,  15,     0.8) /* ArmorModVsBludgeon */
     , (777778,  16,       1) /* ArmorModVsCold */
     , (777778,  17,       1) /* ArmorModVsFire */
     , (777778,  18,       1) /* ArmorModVsAcid */
     , (777778,  19,       1) /* ArmorModVsElectric */
     , (777778,  39,       1) /* DefaultScale */
     , (777778,  54,       3) /* UseRadius */
     , (777778,  64,       1) /* ResistSlash */
     , (777778,  65,       1) /* ResistPierce */
     , (777778,  66,       1) /* ResistBludgeon */
     , (777778,  67,       1) /* ResistFire */
     , (777778,  68,       1) /* ResistCold */
     , (777778,  69,       1) /* ResistAcid */
     , (777778,  70,       1) /* ResistElectric */
     , (777778,  71,       1) /* ResistHealthBoost */
     , (777778,  72,       1) /* ResistStaminaDrain */
     , (777778,  73,       1) /* ResistStaminaBoost */
     , (777778,  74,       1) /* ResistManaDrain */
     , (777778,  75,       1) /* ResistManaBoost */
     , (777778, 104,      10) /* ObviousRadarRange */
     , (777778, 125,       1) /* ResistHealthDrain */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (777778,   1, 'Control block') /* Name */
,      (777778,   16, 'beep') /* Long Desc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (777778,   1,   33559250) /* Setup */
     , (777778,   2,  150995329) /* MotionTable */
     , (777778,   3,  536870932) /* SoundTable */
     , (777778,   4,  805306368) /* CombatTable */
     , (777778,   8,  100677461) /* Icon */
     , (777778,  22,  872415274) /* PhysicsEffectTable */;

INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)
VALUES (777778,   1, 380, 0, 0) /* Strength */
     , (777778,   2, 340, 0, 0) /* Endurance */
     , (777778,   3, 250, 0, 0) /* Quickness */
     , (777778,   4, 330, 0, 0) /* Coordination */
     , (777778,   5, 250, 0, 0) /* Focus */
     , (777778,   6, 285, 0, 0) /* Self */;

INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)
VALUES (777778,   1,   200, 0, 0, 370) /* MaxHealth */
     , (777778,   3,   151, 0, 0, 491) /* MaxStamina */
     , (777778,   5,   201, 0, 0, 486) /* MaxMana */;

INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)
VALUES (777778, 32, 0, 3, 0, 400, 0, 2261.65994237663) /* ItemEnchantment     Specialized */;

INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)
VALUES (777778,  0,  8,  3,  0.5,   20,   16,   16,   16,   20,   20,   20,   20,    0, 1,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2) /* Head */
     , (777778, 16, 64,  3,  0.5,   20,   16,   16,   16,   20,   20,   20,   20,    0, 2,  0.4,  0.4,  0.4,  0.4,  0.4,  0.4,  0.4,  0.4,  0.4,  0.4,  0.4,  0.4) /* Torso */
     , (777778, 17, 64,  3, 0.75,   20,   16,   16,   16,   20,   20,   20,   20,    0, 2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2) /* Tail */
     , (777778, 21, 64,  3,  0.5,   10,    8,    8,    8,   10,   10,   10,   10,    0, 2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2,  0.2) /* Wings */;