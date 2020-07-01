DELETE FROM `weenie` WHERE `class_Id` = 1910061;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (1910061, '1910061-gemaugmentationtinkeringspecarmor', 67, '2019-02-04 06:52:23') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (1910061,   1,        128) /* ItemType - Misc */
     , (1910061,   5,         50) /* EncumbranceVal */
     , (1910061,  16,          8) /* ItemUseable - Contained */
     , (1910061,  19,          0) /* Value */
     , (1910061,  33,          1) /* Bonded - Bonded */
     , (1910061,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (1910061, 114,          1) /* Attuned - Attuned */
     , (1910061, 215,          9) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (1910061,   3, 1) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (1910061,  11, True ) /* IgnoreCollisions */
     , (1910061,  13, True ) /* Ethereal */
     , (1910061,  14, True ) /* GravityStatus */
     , (1910061,  19, True ) /* Attackable */
     , (1910061,  22, True ) /* Inscribable */
     , (1910061,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (1910061,   1, 'Jibril''s Essence') /* Name */
     , (1910061,  16, 'Using this gem will specialize your skill in Armor Tinkering and raise your skill points accordingly.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (1910061,   1,   33554809) /* Setup */
     , (1910061,   3,  536870932) /* SoundTable */
     , (1910061,   8,  100686474) /* Icon */
     , (1910061,  22,  872415275) /* PhysicsEffectTable */;
