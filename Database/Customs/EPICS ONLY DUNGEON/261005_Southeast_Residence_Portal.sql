DELETE FROM `weenie` WHERE `class_Id` = 261005;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261005, 'southeaseruschkresidence', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261005,   1,      65536) /* ItemType - Portal */
     , (261005,  16,         32) /* ItemUseable - Remote */
     , (261005,  86,        150) /* MinLevel */
     , (261005,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (261005,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (261005, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (261005, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261005,   1, True ) /* Stuck */
     , (261005,  11, False) /* IgnoreCollisions */
     , (261005,  12, True ) /* ReportCollisions */
     , (261005,  13, True ) /* Ethereal */
     , (261005,  15, True ) /* LightsStatus */
     , (261005,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261005,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261005,   1, 'Southeast Ruschk Residence') /* Name */
     , (261005,  38, 'Southeast Ruschk Residence') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261005,   1,   33555925) /* Setup */
     , (261005,   2,  150994947) /* MotionTable */
     , (261005,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (261005, 2, 4129667, 133.117599, -213.137177,  -5.995000,  0.902392, 0, 0, 0.430915) /* SE Corner of Ruschk Residence*/
/* @teleloc 0x003F0383 [133.117599 -213.137177 -5.995000] 0.902392 0.000000 0.000000 0.430915*/;

