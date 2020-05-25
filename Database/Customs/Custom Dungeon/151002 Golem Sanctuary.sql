DELETE FROM `weenie` WHERE `class_Id` = 151002;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (151002, 'ace151002-golemsanctuary', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)  /* MinLevel */
VALUES (151002,   1,      65536) /* ItemType - Portal */
     , (151002,  16,         32) /* ItemUseable - Remote */
     , (151002,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (151002,  86,        200) /* MinLevel */
     , (151002,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (151002, 111,         56) /* PortalBitmask - NoSummon, NoRecall, No NPK */
     , (151002, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (151002,   1, True ) /* Stuck */
     , (151002,  11, False) /* IgnoreCollisions */
     , (151002,  12, True ) /* ReportCollisions */
     , (151002,  13, True ) /* Ethereal */
     , (151002,  15, True ) /* LightsStatus */
     , (151002,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (151002,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (151002,   1, 'Golem Sanctuary') /* Name */
     , (151002,  38, 'Golem Sanctuary') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (151002,   1,   33555925) /* Setup */
     , (151002,   2,  150994947) /* MotionTable */
     , (151002,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (151002, 2, 24379706, 60.041981,  -111.043434,  0.005000,  0.999877, 0, 0, 0.015699) /* Destination */
/* @teleloc 0x0174013A [60.041981 -111.043434 0.005000] 0.999877 0.000000 0.000000 0.015699*/;

