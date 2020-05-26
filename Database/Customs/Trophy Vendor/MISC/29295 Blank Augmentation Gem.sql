DELETE FROM `weenie` WHERE `class_Id` = 29295;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (29295, 'gemaugmentationblank', 1, '2019-02-04 06:52:23') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (29295,   1,        128) /* ItemType - Misc */
     , (29295,   5,         10) /* EncumbranceVal */
     , (29295,   8,         10) /* Mass */
     , (29295,  16,          1) /* ItemUseable - No */
     , (29295,  19,        50) /* Value */
     , (29295,  33,          1) /* Bonded - Bonded */
     , (29295,  53,        101) /* PlacementPosition */
     , (29295,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (29295, 114,          1) /* Attuned - Attuned */
     , (29295,  15,        75) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (29295,  11, True ) /* IgnoreCollisions */
     , (29295,  13, True ) /* Ethereal */
     , (29295,  14, True ) /* GravityStatus */
     , (29295,  19, True ) /* Attackable */
     , (29295,  22, True ) /* Inscribable */
     , (29295,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (29295,   1, 'Blank Augmentation Gem') /* Name */
     , (29295,  16, 'An uninscribed Augmentation Gem. Hand this item to an Augmentation Trainer in exchange for an inscribed Augmentation Gem.') /* LongDesc */
     , (29295,  33, 'BlankAug') /* Quest */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (29295,   1,   33554809) /* Setup */
     , (29295,   3,  536870932) /* SoundTable */
     , (29295,   8,  100686475) /* Icon */
     , (29295,  22,  872415275) /* PhysicsEffectTable */;