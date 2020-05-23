DELETE FROM `weenie` WHERE `class_Id` = 251000;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (251000, 'ace251000-alacritarretreat', 7, '2019-08-22 15:46:49') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (251000,   1,      65536) /* ItemType - Portal */
     , (251000,  16,         32) /* ItemUseable - Remote */
     , (251000,  86,          1) /* MinLevel */
     , (251000,  87,        100) /* MaxLevel */
     , (251000,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (251000,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (251000, 111,         48) /* PortalBitmask - NoSummon, NoRecall */
     , (251000, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (251000,   1, True ) /* Stuck */
     , (251000,  11, False) /* IgnoreCollisions */
     , (251000,  12, True ) /* ReportCollisions */
     , (251000,  13, True ) /* Ethereal */
     , (251000,  15, True ) /* LightsStatus */
     , (251000,  88, True ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (251000,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (251000,   1, 'Alacritar Retreat') /* Name */
     , (251000,  38, 'Alacritar Retreat') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (251000,   1,   33555925) /* Setup */
     , (251000,   2,  150994947) /* MotionTable */
     , (251000,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (251000, 2, 4129099, 309.763000, -80.791206, -47.994999,  0.010102, 0, 0, -0.999949) /* Destination */
/* @teleloc 0x003F014B [309.763000 -80.791206 -47.994999] 0.010102 0.000000 0.000000 -0.999949*/;

