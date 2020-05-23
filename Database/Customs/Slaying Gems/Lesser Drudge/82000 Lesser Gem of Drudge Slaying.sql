DELETE FROM `weenie` WHERE `class_Id` = 82000;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82000, 'lesserdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82000,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82000,   3,         13) /* PaletteTemplate - Purple */
     , (82000,   5,          5) /* EncumbranceVal */
     , (82000,  11,          100) /* MaxStackSize */
     , (82000,  12,          1) /* StackSize */
     , (82000,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82000,  19,        1500) /* Value */
     , (82000,  53,        101) /* PlacementPosition */
     , (82000,  91,        1) /* MaxStructure */
     , (82000,  92,        1) /* Structure */
     , (82000,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82000,  94,         -1) /* TargetType - ALL */
     , (82000, 131,         78) /* MaterialType - Ceramic */
     , (82000, 151,          9) /* HookType - Floor, Yard */
     , (82000,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82000,  11, True ) /* IgnoreCollisions */
     , (82000,  13, True ) /* Ethereal */
     , (82000,  14, True ) /* GravityStatus */
     , (82000,  19, True ) /* Attackable */
     , (82000,  22, True ) /* Inscribable */
     , (82000,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82000,   1, 'Lesser Gem of Drudge Slaying') /* Name */
     , (82000,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82000,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82000,   1,   33554809) /* Setup */
     , (82000,   3,  536870932) /* SoundTable */
     , (82000,   6,   67111919) /* PaletteBase */
     , (82000,   7,  268435723) /* ClothingBase */
     , (82000,   8,  100672149) /* Icon */
     , (82000,  22,  872415275) /* PhysicsEffectTable */
     , (82000,  50,  100673313) /* IconOverlay */;
