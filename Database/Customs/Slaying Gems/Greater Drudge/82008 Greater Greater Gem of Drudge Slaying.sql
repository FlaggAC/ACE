DELETE FROM `weenie` WHERE `class_Id` = 82008;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82008, 'greatergreaterdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82008,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82008,   3,         13) /* PaletteTemplate - Purple */
     , (82008,   5,          5) /* EncumbranceVal */
     , (82008,  11,          100) /* MaxStackSize */
     , (82008,  12,          1) /* StackSize */
     , (82008,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82008,  19,        1500) /* Value */
     , (82008,  53,        101) /* PlacementPosition */
     , (82008,  91,        1) /* MaxStructure */
     , (82008,  92,        1) /* Structure */
     , (82008,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82008,  94,         -1) /* TargetType - ALL */
     , (82008, 131,         86) /* MaterialType - Ceramic */
     , (82008, 151,          9) /* HookType - Floor, Yard */
     , (82008,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82008,  11, True ) /* IgnoreCollisions */
     , (82008,  13, True ) /* Ethereal */
     , (82008,  14, True ) /* GravityStatus */
     , (82008,  19, True ) /* Attackable */
     , (82008,  22, True ) /* Inscribable */
     , (82008,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82008,   1, 'Greater Greater Gem of Drudge Slaying') /* Name */
     , (82008,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82008,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82008,   1,   33554809) /* Setup */
     , (82008,   3,  536870932) /* SoundTable */
     , (82008,   6,   67111919) /* PaletteBase */
     , (82008,   7,  268435723) /* ClothingBase */
     , (82008,   8,  100672149) /* Icon */
     , (82008,  22,  872415275) /* PhysicsEffectTable */
     , (82008,  50,  100673295) /* IconOverlay */;
