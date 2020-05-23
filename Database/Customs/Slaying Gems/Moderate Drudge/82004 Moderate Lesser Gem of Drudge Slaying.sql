DELETE FROM `weenie` WHERE `class_Id` = 82004;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82004, 'moderatelesserdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82004,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82004,   3,         13) /* PaletteTemplate - Purple */
     , (82004,   5,          5) /* EncumbranceVal */
     , (82004,  11,          100) /* MaxStackSize */
     , (82004,  12,          1) /* StackSize */
     , (82004,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82004,  19,        1500) /* Value */
     , (82004,  53,        101) /* PlacementPosition */
     , (82004,  91,        1) /* MaxStructure */
     , (82004,  92,        1) /* Structure */
     , (82004,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82004,  94,         -1) /* TargetType - ALL */
     , (82004, 131,         82) /* MaterialType - Ceramic */
     , (82004, 151,          9) /* HookType - Floor, Yard */
     , (82004,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82004,  11, True ) /* IgnoreCollisions */
     , (82004,  13, True ) /* Ethereal */
     , (82004,  14, True ) /* GravityStatus */
     , (82004,  19, True ) /* Attackable */
     , (82004,  22, True ) /* Inscribable */
     , (82004,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82004,   1, 'Moderate Lesser Gem of Drudge Slaying') /* Name */
     , (82004,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82004,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82004,   1,   33554809) /* Setup */
     , (82004,   3,  536870932) /* SoundTable */
     , (82004,   6,   67111919) /* PaletteBase */
     , (82004,   7,  268435723) /* ClothingBase */
     , (82004,   8,  100672149) /* Icon */
     , (82004,  22,  872415275) /* PhysicsEffectTable */
     , (82004,  50,  100673305) /* IconOverlay */;
