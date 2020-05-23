DELETE FROM `weenie` WHERE `class_Id` = 123456;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (123456, 'mazetrap', 24, '2005-02-09 10:00:00') /* PressurePlate */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (123456,   1,        128) /* ItemType - Misc */
     , (123456,   5,        500) /* EncumbranceVal */
     , (123456,   8,        250) /* Mass */
     , (123456,   9,          0) /* ValidLocations - None */
     , (123456,  16,          1) /* ItemUseable - No */
     , (123456,  19,       1000) /* Value */
     , (123456,  83,       2048) /* ActivationResponse - Emote */
     , (123456,  93,       1036) /* PhysicsState - Ethereal, ReportCollisions, Gravity */
     , (123456, 106,        1000) /* ItemSpellcraft */
     , (123456, 119,          1) /* Active */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (123456,   1, True ) /* Stuck */
     , (123456,  11, False) /* IgnoreCollisions */
     , (123456,  12, True ) /* ReportCollisions */
     , (123456,  13, True ) /* Ethereal */
     , (123456,  18, True ) /* Visibility */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (123456,  11,       2) /* ResetInterval */
     , (123456,  39,     0.5) /* DefaultScale */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (123456,   1, 'Maze Trap') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (123456,   1,   33555536) /* Setup */
     , (123456,   2,  150994977) /* MotionTable */
     , (123456,   8,  100668114) /* Icon */
     , (123456,  23,        149) /* UseSound - TriggerActivated */;

INSERT INTO `weenie_properties_i_i_d` (`object_Id`, `type`, `value`)
VALUES (123456,  16,          0) /* ActivationTarget */;

INSERT INTO `weenie_properties_skill` (`object_Id`, `type`, `level_From_P_P`, `s_a_c`, `p_p`, `init_Level`, `resistance_At_Last_Check`, `last_Used_Time`)
VALUES (123456, 31, 0, 3, 0, 1000, 0, 1528.82098340946) /* CreatureEnchantment Specialized */
     , (123456, 33, 0, 3, 0, 1000, 0, 1528.82098340946) /* LifeMagic           Specialized */
     , (123456, 34, 0, 3, 0, 1000, 0, 1528.82098340946) /* WarMagic            Specialized */;

INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)
VALUES (123456,  8 /* Activation */,      1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

SET @parent_id = LAST_INSERT_ID();

INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (@parent_id,  0,  99 /* TeleTarget */, 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1484390676, 81.597847, -230.347534, 0.005000, 0.712439, 0, 0, -0.5927655)
, (@parent_id,  1,  19 /* CastSpellInstant */, 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4949 /* Harm */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
 , (@parent_id,  2,  19 /* CastSpellInstant */, 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4949 /* Harm 6 */, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
   , (@parent_id,  3,  18 /* direct broadcast */, 0, 1, NULL, 'You have stepped on a trap! The Theric Energy that powers the maze siphons your Experitus Energy and protects the chamber from your entry.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
	, (@parent_id,  4,  31 /* EraseQuest */, 0, 1, NULL, 'ThericLeverPulled', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
