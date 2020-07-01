DELETE FROM `weenie` WHERE `class_Id` = 1910064;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (1910064, '1910064-gemaugmentationtinkeringspecsalv', 67, '2019-02-04 06:52:23') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (1910064,   1,        128) /* ItemType - Misc */
     , (1910064,   5,         50) /* EncumbranceVal */
     , (1910064,  16,          8) /* ItemUseable - Contained */
     , (1910064,  19,          0) /* Value */
     , (1910064,  33,          1) /* Bonded - Bonded */
     , (1910064,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (1910064, 114,          1) /* Attuned - Attuned */
     , (1910064, 215,          7) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (1910064,   3, 1) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (1910064,  11, True ) /* IgnoreCollisions */
     , (1910064,  13, True ) /* Ethereal */
     , (1910064,  14, True ) /* GravityStatus */
     , (1910064,  19, True ) /* Attackable */
     , (1910064,  22, True ) /* Inscribable */
     , (1910064,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (1910064,   1, 'Ciandra''s Essence') /* Name */
     , (1910064,  16, 'Using this gem will specialize your skill in Salvaging and raise your skill points accordingly.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (1910064,   1,   33554809) /* Setup */
     , (1910064,   3,  536870932) /* SoundTable */
     , (1910064,   8,  100686474) /* Icon */
     , (1910064,  22,  872415275) /* PhysicsEffectTable */;
