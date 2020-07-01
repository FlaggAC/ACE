DELETE FROM `weenie` WHERE `class_Id` = 1910063;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (1910063, '1910063-gemaugmentationtinkeringspecmagic', 67, '2019-02-04 06:52:23') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (1910063,   1,        128) /* ItemType - Misc */
     , (1910063,   5,         50) /* EncumbranceVal */
     , (1910063,  16,          8) /* ItemUseable - Contained */
     , (1910063,  19,          0) /* Value */
     , (1910063,  33,          1) /* Bonded - Bonded */
     , (1910063,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (1910063, 114,          1) /* Attuned - Attuned */
     , (1910063, 215,         10) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (1910063,   3, 1) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (1910063,  11, True ) /* IgnoreCollisions */
     , (1910063,  13, True ) /* Ethereal */
     , (1910063,  14, True ) /* GravityStatus */
     , (1910063,  19, True ) /* Attackable */
     , (1910063,  22, True ) /* Inscribable */
     , (1910063,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (1910063,   1, 'Celdiseth''s Essence') /* Name */
     , (1910063,  16, 'Using this gem will specialize your skill in Magic Item Tinkering and raise your skill points accordingly.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (1910063,   1,   33554809) /* Setup */
     , (1910063,   3,  536870932) /* SoundTable */
     , (1910063,   8,  100686474) /* Icon */
     , (1910063,  22,  872415275) /* PhysicsEffectTable */;
