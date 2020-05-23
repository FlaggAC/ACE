DELETE FROM `weenie` WHERE `class_Id` = 50200;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (50200, 'ace50200-mortimyer', 12, '2019-03-26 00:00:00') /* Vendor */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (50200,   1,         16) /* ItemType - Creature */
     , (50200,   2,         31) /* CreatureType - Human */
     , (50200,   6,        255) /* ItemsCapacity */
     , (50200,   7,        255) /* ContainersCapacity */
     , (50200,  16,         32) /* ItemUseable - Remote */
     , (50200,  25,        666) /* Level */
     , (50200,  74, 1073741824) /* MerchandiseItemTypes - None */
     , (50200,  75,          0) /* MerchandiseMinValue */
     , (50200,  76,     100000) /* MerchandiseMaxValue */
     , (50200,  93,    2098200) /* PhysicsState - ReportCollisions, IgnoreCollisions, Gravity, ReportCollisionsAsEnvironment */
     , (50200, 113,          1) /* Gender - Male */
     , (50200, 133,          4) /* ShowableOnRadar - ShowAlways */
     , (50200, 134,         16) /* PlayerKillerStatus - RubberGlue */
     , (50200, 188,          1) /* HeritageGroup - Aluvian */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (50200,   1, True ) /* Stuck */
     , (50200,  11, True ) /* IgnoreCollisions */
     , (50200,  12, True ) /* ReportCollisions */
     , (50200,  13, False) /* Ethereal */
     , (50200,  14, True ) /* GravityStatus */
     , (50200,  19, False) /* Attackable */
     , (50200,  39, True ) /* DealMagicalItems */
     , (50200,  41, True ) /* ReportCollisionsAsEnvironment */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (50200,  37,       1) /* BuyPrice */
     , (50200,  38,       1) /* SellPrice */
     , (50200,  54,       3) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (50200,   1, 'Mortimyer') /* Name */
     , (50200,   5, 'Professional Embalmer') /* Template */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (50200,   1,   33554433) /* Setup */
     , (50200,   2,  150994945) /* MotionTable */
     , (50200,   3,  536870913) /* SoundTable */
     , (50200,   6,   67108990) /* PaletteBase */
     , (50200,   8,  100667446) /* Icon */
     , (50200,   9,   83890511) /* EyesTexture */
     , (50200,  10,   83890562) /* NoseTexture */
     , (50200,  11,   83890637) /* MouthTexture */
     , (50200,  15,   67117076) /* HairPalette */
     , (50200,  16,   67109564) /* EyesPalette */
     , (50200,  17,   67109560) /* SkinPalette */
     , (50200,  57,      60002) /* Pk Trophy Alt Currency */;

INSERT INTO `weenie_properties_attribute` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`)
VALUES (50200,   1, 260, 0, 0) /* Strength */
     , (50200,   2, 290, 0, 0) /* Endurance */
     , (50200,   3, 200, 0, 0) /* Quickness */
     , (50200,   4, 290, 0, 0) /* Coordination */
     , (50200,   5, 290, 0, 0) /* Focus */
     , (50200,   6, 200, 0, 0) /* Self */;

INSERT INTO `weenie_properties_attribute_2nd` (`object_Id`, `type`, `init_Level`, `level_From_C_P`, `c_P_Spent`, `current_Level`)
VALUES (50200,   1,    10, 0, 0, 495) /* MaxHealth */
     , (50200,   3,    10, 0, 0, 790) /* MaxStamina */
     , (50200,   5,    10, 0, 0, 700) /* MaxMana */;

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (50200,  2 /* Vendor */,    0.8, NULL, NULL, NULL, NULL, 1 /* Open */, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  10 /* Tell */, 0, 1, NULL, "Bring me more.. more extremities of those you hate. Or love. Nobody is judging.", NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (50200,  2 /* Vendor */,    0.8, NULL, NULL, NULL, NULL, 2 /* Close */, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  10 /* Tell */, 0, 1, NULL, 'Oh this is perfect for my collection..', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (50200,  2 /* Vendor */,    0.8, NULL, NULL, NULL, NULL, 4 /* Buy */, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  10 /* Tell */, 0, 1, NULL, "Tasty... I mean... Good material for a great study later.", NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO `weenie_properties_create_list` (`object_Id`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`)
VALUES (50200, 2, 25703,  0, 93, 1, False) /* Create Dapper Suit (25703) for Wield */
     , (50200, 4, 37360, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37361, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37359, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37358, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37357, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37356, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37355, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37354, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37353, -1, 0, 0, False) /* Ink */
     , (50200, 4, 37365, -1, 0, 0, False) /* Quill Ben*/
     , (50200, 4, 37364, -1, 0, 0, False) /* Quill Intro*/
     , (50200, 4, 37363, -1, 0, 0, False) /* Quill Extract*/
     , (50200, 4, 37362, -1, 0, 0, False) /* Quill Inflict*/
     , (50200, 4, 43379, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 49455, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 45374, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 45373, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 45372, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 45371, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 45370, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 43387, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 43380, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 41747, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 41746, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 38760, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37373, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37372, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37371, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37370, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37369, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37352, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37351, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37350, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37349, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37348, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37347, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37346, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37345, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37344, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37343, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37342, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37341, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37340, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37336, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37333, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37337, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37332, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37331, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37330, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37329, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37328, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37327, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37326, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37325, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37324, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37323, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37321, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37319, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37318, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37317, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37316, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37315, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37314, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37313, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37312, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37311, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37310, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37309, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37307, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37305, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37304, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37303, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37302, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37301, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 37300, -1, 0, 0, False) /* Glyph*/
     , (50200, 4, 30094, -1, 0, 0, False) /* FP AQUA*/
     , (50200, 4, 30095, -1, 0, 0, False) /* FP BG*/
     , (50200, 4, 30096, -1, 0, 0, False) /* FP BO*/
     , (50200, 4, 30097, -1, 0, 0, False) /* FP EMERALD*/
     , (50200, 4, 30098, -1, 0, 0, False) /* FP FIRE OPAL*/
     , (50200, 4, 30099, -1, 0, 0, False) /* FP IMP TOPAZ*/
     , (50200, 4, 30100, -1, 0, 0, False) /* FP JET*/
     , (50200, 4, 30101, -1, 0, 0, False) /* FP PERIDOT*/
     , (50200, 4, 30102, -1, 0, 0, False) /* FP RED GARNET*/
     , (50200, 4, 30103, -1, 0, 0, False) /* FP SUNSTONE*/
     , (50200, 4, 30104, -1, 0, 0, False) /* FP WHITE SAPPHIRE*/
     , (50200, 4, 30105, -1, 0, 0, False) /* FP YELLOW TOPAZ*/
     , (50200, 4, 30106, -1, 0, 0, False) /* FP ZIRCON*/
     , (50200, 4, 30186, -1, 0, 0, False) /* Smithy Crystal Rare*/
     , (50200, 4, 30215, -1, 0, 0, False) /* Tinker's Crystal Rare*/
     , (50200, 4, 30225, -1, 0, 0, False) /* Imbuer's Crystal Rare*/
     , (50200, 4, 30246, -1, 0, 0, False) /* Artist's Crystal Rare*/
     , (50200, 4, 30222, -1, 0, 0, False) /* Loyalty Rare*/
     , (50200, 4, 30217, -1, 0, 0, False) /* Leadership Rare*/
     , (50200, 4, 6600, -1, 0, 0, False) /* PREPATCH AMULI COAT*/
     , (50200, 4, 6606, -1, 0, 0, False) /* PREPATCH AMULI LEGS*/
     , (50200, 4, 35180, -1, 0, 0, False) /* Hulking*/
     , (50200, 4, 43189, -1, 0, 0, False) /* Custom Gem of Knowledge 10m n 20m d */
     , (50200, 4, 27256, -1, 0, 0, False) /* Burning Coal */
     , (50200, 4, 5893, -1, 0, 0, False) /* PPHoary */
     , (50200, 4, 28842, -1, 0, 0, False) /* Penguin Essence */
     , (50200, 4, 29295, -1, 0, 0, False) /* Blank Aug  */;
