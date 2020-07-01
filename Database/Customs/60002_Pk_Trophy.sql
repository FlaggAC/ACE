DELETE FROM `weenie` WHERE `class_Id` = 60002;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (60002, 'pktrophy', 51, '2019-07-16 14:01:15') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (60002,   1,        128) /* ItemType - Misc */
     , (60002,   5,          5) /* EncumbranceVal */
     , (60002,  11,          100) /* MaxStackSize */
     , (60002,  12,          1) /* StackSize */
     , (60002,  16,          1) /* ItemUseable - No */
     , (60002,  19,          25) /* Value */
     , (60002,  53,        101) /* PlacementPosition - Resting */
     , (60002,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (60002,  15,         25) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (60002,  11, True ) /* IgnoreCollisions */
     , (60002,  13, True ) /* Ethereal */
     , (60002,  14, True ) /* GravityStatus */
     , (60002,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (60002,   1, 'Pk Trophy') /* Name */
     , (60002,  16, 'Bloody piece off a players corpse.') /* LongDesc */
	  , (60002,  20, 'Pk Trophies') /* Plural Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (60002,   1,   33554802) /* Setup */
     , (60002,   3,  536870932) /* SoundTable */
     , (60002,   8,  100686362) /* Icon */
     , (60002,  22,  872415275) /* PhysicsEffectTble */;
