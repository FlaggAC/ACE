DELETE FROM `weenie` WHERE `class_Id` = 1910062;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (1910062, '1910062-gemaugmentationtinkeringspecitem', 67, '2019-02-04 06:52:23') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (1910062,   1,        128) /* ItemType - Misc */
     , (1910062,   5,         50) /* EncumbranceVal */
     , (1910062,  16,          8) /* ItemUseable - Contained */
     , (1910062,  19,          0) /* Value */
     , (1910062,  33,          1) /* Bonded - Bonded */
     , (1910062,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (1910062, 114,          1) /* Attuned - Attuned */
     , (1910062, 215,          8) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (1910062,   3, 1) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (1910062,  11, True ) /* IgnoreCollisions */
     , (1910062,  13, True ) /* Ethereal */
     , (1910062,  14, True ) /* GravityStatus */
     , (1910062,  19, True ) /* Attackable */
     , (1910062,  22, True ) /* Inscribable */
     , (1910062,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (1910062,   1, 'Yoshi''s Essence') /* Name */
     , (1910062,  16, 'Using this gem will specialize your skill in Item Tinkering and raise your skill points accordingly.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (1910062,   1,   33554809) /* Setup */
     , (1910062,   3,  536870932) /* SoundTable */
     , (1910062,   8,  100686474) /* Icon */
     , (1910062,  22,  872415275) /* PhysicsEffectTable */;
