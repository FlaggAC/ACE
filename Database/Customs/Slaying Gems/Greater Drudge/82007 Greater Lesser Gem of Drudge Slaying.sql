DELETE FROM `weenie` WHERE `class_Id` = 82007;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82007, 'greaterlesserdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82007,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82007,   3,         13) /* PaletteTemplate - Purple */
     , (82007,   5,          5) /* EncumbranceVal */
     , (82007,  11,          100) /* MaxStackSize */
     , (82007,  12,          1) /* StackSize */
     , (82007,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82007,  19,        1500) /* Value */
     , (82007,  53,        101) /* PlacementPosition */
     , (82007,  91,        1) /* MaxStructure */
     , (82007,  92,        1) /* Structure */
     , (82007,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82007,  94,         -1) /* TargetType - ALL */
     , (82007, 131,         85) /* MaterialType - Ceramic */
     , (82007, 151,          9) /* HookType - Floor, Yard */
     , (82007,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82007,  11, True ) /* IgnoreCollisions */
     , (82007,  13, True ) /* Ethereal */
     , (82007,  14, True ) /* GravityStatus */
     , (82007,  19, True ) /* Attackable */
     , (82007,  22, True ) /* Inscribable */
     , (82007,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82007,   1, 'Greater Lesser Gem of Drudge Slaying') /* Name */
     , (82007,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82007,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82007,   1,   33554809) /* Setup */
     , (82007,   3,  536870932) /* SoundTable */
     , (82007,   6,   67111919) /* PaletteBase */
     , (82007,   7,  268435723) /* ClothingBase */
     , (82007,   8,  100672149) /* Icon */
     , (82007,  22,  872415275) /* PhysicsEffectTable */
     , (82007,  50,  100673295) /* IconOverlay */;
