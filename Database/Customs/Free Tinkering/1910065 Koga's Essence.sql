DELETE FROM `weenie` WHERE `class_Id` = 1910065;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (1910065, '1910065-gemaugmentationtinkeringspecweap', 67, '2020-06-14 00:25:17') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (1910065,   1,        128) /* ItemType - Misc */
     , (1910065,   5,         50) /* EncumbranceVal */
     , (1910065,  16,          8) /* ItemUseable - Contained */
     , (1910065,  19,          0) /* Value */
     , (1910065,  33,          1) /* Bonded - Bonded */
     , (1910065,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (1910065, 114,          1) /* Attuned - Attuned */
     , (1910065, 215,         11) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (1910065,   3, 1) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (1910065,  11, True ) /* IgnoreCollisions */
     , (1910065,  13, True ) /* Ethereal */
     , (1910065,  14, True ) /* GravityStatus */
     , (1910065,  19, True ) /* Attackable */
     , (1910065,  22, True ) /* Inscribable */
     , (1910065,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (1910065,   1, 'Koga''s Essence') /* Name */
     , (1910065,  16, 'Using this gem will specialize your skill in Weapon Tinkering and raise your skill points accordingly.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (1910065,   1,   33554809) /* Setup */
     , (1910065,   3,  536870932) /* SoundTable */
     , (1910065,   8,  100686474) /* Icon */
     , (1910065,  22,  872415275) /* PhysicsEffectTable */;
