DELETE FROM `weenie` WHERE `class_Id` = 777777;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (777777, 'bottlexp', 62, '2019-07-28 05:55:30') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (777777,   1,       2048) /* ItemType - Gem */
     , (777777,   3,         83) /* PaletteTemplate - Amber */
     , (777777,   5,         10) /* EncumbranceVal */
     , (777777,  16,          8) /* ItemUseable - Contained */
     , (777777,  18,          1) /* UiEffects - Magical */
     , (777777,  19,       25000) /* Value */
     , (777777,  33,          0) /* Bonded - Normal */
     , (777777,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (777777, 114,          0) /* Attuned - Normal */
     , (777777, 369,         70) /* UseRequiresLevel */
     , (777777, 185,          3) /* TypeOfAlteration */
     , (777777, 186,          1) /* SkillToBeAltered */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (777777,  22, True ) /* Inscribable */
     , (777777,  63, False ) /* UnlimitedUse */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (777777,  76,     0.5) /* Translucency */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (777777,   1, 'Bottle of Xp') /* Name */
     , (777777,  14, 'Using this bottle will deplete the experience stored within.') /* Use */
     , (777777,  15, 'A bottle capable of holding the essence of experience. ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (777777,   1,   33554601) /* Setup */
     , (777777,   3,  536870932) /* SoundTable */
     , (777777,   6,   67111919) /* PaletteBase */
     , (777777,   8,  100690538) /* Icon */
     , (777777,  22,  872415275) /* PhysicsEffectTable */;