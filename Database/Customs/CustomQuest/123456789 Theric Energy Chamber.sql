DELETE FROM `weenie` WHERE `class_Id` = 123456789;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (123456789, 'thericenergychamber', 7, '2019-02-04 06:52:23') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (123456789,   1,      65536) /* ItemType - Portal */
     , (123456789,  16,         32) /* ItemUseable - Remote */
     , (123456789,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (123456789,  86,        70) /* MinLevel */
     , (123456789,  93,       3084) /* PhysicsState - Ethereal, ReportCollisions, Gravity, LightingOn */
     , (123456789, 111,          1) /* PortalBitmask - Unrestricted */
     , (123456789, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (123456789,   1, True ) /* Stuck */
     , (123456789,  11, False) /* IgnoreCollisions */
     , (123456789,  12, True ) /* ReportCollisions */
     , (123456789,  13, True ) /* Ethereal */
     , (123456789,  15, True ) /* LightsStatus */
     , (123456789,  88, False ) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (123456789,  54, -0.100000001490116) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (123456789,   1, 'Theric Energy Chamber') /* Name */
     , (123456789,  16, 'The energy from this portal requires deactivation before entrance is allowed.') /* LongDesc */
     , (123456789,  37, 'ThericLeverPulled') /* QuestRestriction */
     , (123456789,  38, 'Theric Energy Chamber') /* AppraisalPortalDestination */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (123456789,   1,   33555925) /* Setup */
     , (123456789,   2,  150994947) /* MotionTable */
     , (123456789,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (123456789, 2, 4129676, 10.029513, -49.478134, - 0.005000, -0.999985, 0, 0, 0.005571) /* Destination */
/* @teleloc  0x01FE0142 [10.029513 -49.478134  0.005000] -0.999985 0.000000 0.000000 0.005571 */;
