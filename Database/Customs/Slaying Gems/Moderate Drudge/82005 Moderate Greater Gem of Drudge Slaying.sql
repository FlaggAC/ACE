DELETE FROM `weenie` WHERE `class_Id` = 82005;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82005, 'moderategreaterdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82005,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82005,   3,         13) /* PaletteTemplate - Purple */
     , (82005,   5,          5) /* EncumbranceVal */
     , (82005,  11,          100) /* MaxStackSize */
     , (82005,  12,          1) /* StackSize */
     , (82005,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82005,  19,        1500) /* Value */
     , (82005,  53,        101) /* PlacementPosition */
     , (82005,  91,        1) /* MaxStructure */
     , (82005,  92,        1) /* Structure */
     , (82005,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82005,  94,         -1) /* TargetType - ALL */
     , (82005, 131,         83) /* MaterialType - Ceramic */
     , (82005, 151,          9) /* HookType - Floor, Yard */
     , (82005,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82005,  11, True ) /* IgnoreCollisions */
     , (82005,  13, True ) /* Ethereal */
     , (82005,  14, True ) /* GravityStatus */
     , (82005,  19, True ) /* Attackable */
     , (82005,  22, True ) /* Inscribable */
     , (82005,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82005,   1, 'Moderate Greater Gem of Drudge Slaying') /* Name */
     , (82005,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82005,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82005,   1,   33554809) /* Setup */
     , (82005,   3,  536870932) /* SoundTable */
     , (82005,   6,   67111919) /* PaletteBase */
     , (82005,   7,  268435723) /* ClothingBase */
     , (82005,   8,  100672149) /* Icon */
     , (82005,  22,  872415275) /* PhysicsEffectTable */
     , (82005,  50,  100673305) /* IconOverlay */;
