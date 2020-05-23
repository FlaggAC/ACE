DELETE FROM `weenie` WHERE `class_Id` = 261000;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261000, 'ace261000-ruschkresidence', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261000,   1,      65536) /* ItemType - Portal */
     , (261000,  16,         32) /* ItemUseable - Remote */
     , (261000,  86,        150) /* MinLevel */
     , (261000,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (261000,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (261000, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (261000, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261000,   1, True ) /* Stuck */
     , (261000,  11, False) /* IgnoreCollisions */
     , (261000,  12, True ) /* ReportCollisions */
     , (261000,  13, True ) /* Ethereal */
     , (261000,  15, True ) /* LightsStatus */
     , (261000,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261000,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261000,   1, 'Ruschk Residence Main Entrance') /* Name */
     , (261000,  38, 'Ruschk Residence Main Entrance') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261000,   1,   33555925) /* Setup */
     , (261000,   2,  150994947) /* MotionTable */
     , (261000,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (261000, 2, 4129472, 70.067940, -131.685242, -11.995001,  0.001013, 0, 0, -0.999999) /* Destination */
/* @teleloc  0x003F02C0 [70.067940 -131.685242 -11.995001] 0.001013 0.000000 0.000000 -0.999999*/;

