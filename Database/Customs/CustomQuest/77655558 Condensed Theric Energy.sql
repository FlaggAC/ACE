DELETE FROM `weenie` WHERE `class_Id` = 77655558;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (77655558, 'condensedthericenergy', 51, '2019-07-16 14:01:15') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (77655558,   1,        128) /* ItemType - Misc */
     , (77655558,   5,          5) /* EncumbranceVal */
     , (77655558,  11,          1) /* MaxStackSize */
     , (77655558,  12,          1) /* StackSize */
     , (77655558,  16,          1) /* ItemUseable - No */
     , (77655558,  19,          0) /* Value */
     , (77655558,  53,        101) /* PlacementPosition - Resting */
     , (77655558,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (77655558,  15,         0) /* StackUnitValue */
     , (77655558,  114,         1) /* Attuned */
     , (77655558,  33,          1) /* Bonded */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (77655558,  11, True ) /* IgnoreCollisions */
     , (77655558,  13, True ) /* Ethereal */
     , (77655558,  14, True ) /* GravityStatus */
     , (77655558,  19, True ) /* Attackable */
     , (77655558,  69, False ) /* Sellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (77655558,   1, 'Condensed Theric Energy') /* Name */
     , (77655558,  16, 'The Theric Chamber Guardian left this behind, I should take it to the researcher.') /* LongDesc */
     , (77655558,  33, 'ThericEnergyPickup') /* Quest */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (77655558,   1,   33557522) /* Setup */
     , (77655558,   3,  536871022) /* SoundTable */
     , (77655558,   8,  100672521) /* Icon */
     , (77655558,  22,  872415275) /* PhysicsEffectTable */;
