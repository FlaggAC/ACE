DELETE FROM `weenie` WHERE `class_Id` = 261012;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261012, 'ruschkresidentsurface', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261012,   1,      65536) /* ItemType - Portal */
     , (261012,  16,         32) /* ItemUseable - Remote */
     , (261012,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (261012,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (261012, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (261012, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261012,   1, True ) /* Stuck */
     , (261012,  11, False) /* IgnoreCollisions */
     , (261012,  12, True ) /* ReportCollisions */
     , (261012,  13, True ) /* Ethereal */
     , (261012,  15, True ) /* LightsStatus */
     , (261012,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261012,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261012,   1, 'Surface Portal to Holtburg') /* Name */
     , (261012,  38, 'Surface Portal to Holtburg') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261012,   1,   33555925) /* Setup */
     , (261012,   2,  150994947) /* MotionTable */
     , (261012,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (261012, 2, 2847146009, 84, 7.1,  94,  0.996917, 0, 0, -0.0784591) /* Destination Holtburg LS*/
/* @teleloc 0x0174013A [60.041981 -111.043434 0.005000] 0.999877 0.000000 0.000000 0.015699*/;

