DELETE FROM `weenie` WHERE `class_Id` = 261007;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261007, 'northwestruschkresidence', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261007,   1,      65536) /* ItemType - Portal */
     , (261007,  16,         32) /* ItemUseable - Remote */
     , (261007,  86,        150) /* MinLevel */
     , (261007,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (261007,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (261007, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (261007, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261007,   1, True ) /* Stuck */
     , (261007,  11, False) /* IgnoreCollisions */
     , (261007,  12, True ) /* ReportCollisions */
     , (261007,  13, True ) /* Ethereal */
     , (261007,  15, True ) /* LightsStatus */
     , (261007,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261007,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261007,   1, 'Northwest Ruschk Residence') /* Name */
     , (261007,  38, 'Northwest Ruschk Residence') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261007,   1,   33555925) /* Setup */
     , (261007,   2,  150994947) /* MotionTable */
     , (261007,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (261007, 2, 4129500, 6.764382, -86.888161,  -5.995000,  0.418791, 0, 0, -0.908083) /* NW Corner of Ruschk Residence*/
/* @teleloc 0x003F02DC [6.764382 -86.888161 -5.995000] 0.418791 0.000000 0.000000 -0.908083*/;

