DELETE FROM `weenie` WHERE `class_Id` = 261008;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261008, 'northeastruschkresidence', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261008,   1,      65536) /* ItemType - Portal */
     , (261008,  16,         32) /* ItemUseable - Remote */
     , (261008,  86,        150) /* MinLevel */
     , (261008,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (261008,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (261008, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (261008, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261008,   1, True ) /* Stuck */
     , (261008,  11, False) /* IgnoreCollisions */
     , (261008,  12, True ) /* ReportCollisions */
     , (261008,  13, True ) /* Ethereal */
     , (261008,  15, True ) /* LightsStatus */
     , (261008,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261008,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261008,   1, 'Northeast Ruschk Residence') /* Name */
     , (261008,  38, 'Northeast Ruschk Residence') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261008,   1,   33555925) /* Setup */
     , (261008,   2,  150994947) /* MotionTable */
     , (261008,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (261008, 2, 4129664, 133.025986, -87.033966,  -5.997000,  0.399041, 0, 0, 0.916933) /* NE Corner of Ruschk Residence*/
/* @teleloc 0x003F0380 [133.025986 -87.033966 -5.997000] 0.399041 0.000000 0.000000 0.916933*/;

