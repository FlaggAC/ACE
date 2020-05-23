DELETE FROM `weenie` WHERE `class_Id` = 777779;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (777779, 'controlblockdungeon', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (777779,   1,      65536) /* ItemType - Portal */
     , (777779,  16,         32) /* ItemUseable - Remote */
     , (777779,  86,        100) /* MinLevel */
     , (777779,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (777779,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (777779, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (777779, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (777779,   1, True ) /* Stuck */
     , (777779,  11, False) /* IgnoreCollisions */
     , (777779,  12, True ) /* ReportCollisions */
     , (777779,  13, True ) /* Ethereal */
     , (777779,  15, True ) /* LightsStatus */
     , (777779,  28, True ) /* PK Status? */
     , (777779,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (777779,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (777779,   1, 'Control Block Arena') /* Name */
     , (777779,  38, 'Control Block Arena') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (777779,   1,   33555925) /* Setup */
     , (777779,   2,  150994947) /* MotionTable */
     , (777779,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (777779, 2, 24379706, 60.041981,  -111.043434,  0.005000,  0.999877, 0, 0, 0.015699) /* Destination */
/* @teleloc 0x0174013A [60.041981 -111.043434 0.005000] 0.999877 0.000000 0.000000 0.015699*/;
