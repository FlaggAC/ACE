DELETE FROM `weenie` WHERE `class_Id` = 82006;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82006, 'greaterdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82006,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82006,   3,         13) /* PaletteTemplate - Purple */
     , (82006,   5,          5) /* EncumbranceVal */
     , (82006,  11,          100) /* MaxStackSize */
     , (82006,  12,          1) /* StackSize */
     , (82006,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82006,  19,        1500) /* Value */
     , (82006,  53,        101) /* PlacementPosition */
     , (82006,  91,        1) /* MaxStructure */
     , (82006,  92,        1) /* Structure */
     , (82006,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82006,  94,         -1) /* TargetType - ALL */
     , (82006, 131,         84) /* MaterialType - Ceramic */
     , (82006, 151,          9) /* HookType - Floor, Yard */
     , (82006,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82006,  11, True ) /* IgnoreCollisions */
     , (82006,  13, True ) /* Ethereal */
     , (82006,  14, True ) /* GravityStatus */
     , (82006,  19, True ) /* Attackable */
     , (82006,  22, True ) /* Inscribable */
     , (82006,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82006,   1, 'Greater Gem of Drudge Slaying') /* Name */
     , (82006,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82006,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82006,   1,   33554809) /* Setup */
     , (82006,   3,  536870932) /* SoundTable */
     , (82006,   6,   67111919) /* PaletteBase */
     , (82006,   7,  268435723) /* ClothingBase */
     , (82006,   8,  100672149) /* Icon */
     , (82006,  22,  872415275) /* PhysicsEffectTable */
     , (82006,  50,  100673295) /* IconOverlay */;
