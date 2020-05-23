DELETE FROM `weenie` WHERE `class_Id` = 261006;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261006, 'southwestruschkresidence', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261006,   1,      65536) /* ItemType - Portal */
     , (261006,  16,         32) /* ItemUseable - Remote */
     , (261006,  86,        150) /* MinLevel */
     , (261006,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (261006,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (261006, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (261006, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261006,   1, True ) /* Stuck */
     , (261006,  11, False) /* IgnoreCollisions */
     , (261006,  12, True ) /* ReportCollisions */
     , (261006,  13, True ) /* Ethereal */
     , (261006,  15, True ) /* LightsStatus */
     , (261006,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261006,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261006,   1, 'Southwest Ruschk Residence') /* Name */
     , (261006,  38, 'Southwest Ruschk Residence') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261006,   1,   33555925) /* Setup */
     , (261006,   2,  150994947) /* MotionTable */
     , (261006,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (261006, 2, 4129503, 7.068302, -213.607162,  -5.995000,  -0.947665, 0, 0, 0.319265) /* SW Corner of Ruschk Residence*/
/* @teleloc 0x003F02DF [7.068302 -213.607162 -5.995000] -0.947665 0.000000 0.000000 0.319265*/;

