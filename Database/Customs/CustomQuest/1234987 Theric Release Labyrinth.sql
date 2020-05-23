DELETE FROM `weenie` WHERE `class_Id` = 1234987;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (1234987, 'thericreleaselabyrinth', 7, '2019-02-04 06:52:23') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (1234987,   1,      65536) /* ItemType - Portal */
     , (1234987,  16,         32) /* ItemUseable - Remote */
     , (1234987,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (1234987,  86,        70) /* MinLevel */
     , (1234987,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (1234987, 111,          1) /* PortalBitmask - Unrestricted */
     , (1234987, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (1234987,   1, True ) /* Stuck */
     , (1234987,  11, False) /* IgnoreCollisions */
     , (1234987,  12, True ) /* ReportCollisions */
     , (1234987,  13, True ) /* Ethereal */
     , (1234987,  15, True ) /* LightsStatus */
     , (1234987,  88, False ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (1234987,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (1234987,   1, 'Theric Release Labyrinth') /* Name */
     , (1234987,  16, 'A maze powered by Theric Energy.') /* LongDesc */
     , (1234987,  37, 'ThericLabyrinthFlag') /* QuestRestriction */
     , (1234987,  38, 'Theric Release Labyrinth') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (1234987,   1,   33555925) /* Setup */
     , (1234987,   2,  150994947) /* MotionTable */
     , (1234987,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (1234987, 2, 1484390676, 75.503204, -230.092712, 0.005000, 0.701023, 0, 0, -0.713138) /* Destination */
/* @teleloc  0x587A0114 [75.503204 -230.092712 0.005000] 0.701023 0.000000 0.000000 -0.713138 */;
