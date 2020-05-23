DELETE FROM `weenie` WHERE `class_Id` = 82003;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82003, 'moderatedrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82003,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82003,   3,         13) /* PaletteTemplate - Purple */
     , (82003,   5,          5) /* EncumbranceVal */
     , (82003,  11,          100) /* MaxStackSize */
     , (82003,  12,          1) /* StackSize */
     , (82003,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82003,  19,        1500) /* Value */
     , (82003,  53,        101) /* PlacementPosition */
     , (82003,  91,        1) /* MaxStructure */
     , (82003,  92,        1) /* Structure */
     , (82003,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82003,  94,         -1) /* TargetType - ALL */
     , (82003, 131,         81) /* MaterialType - Ceramic */
     , (82003, 151,          9) /* HookType - Floor, Yard */
     , (82003,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82003,  11, True ) /* IgnoreCollisions */
     , (82003,  13, True ) /* Ethereal */
     , (82003,  14, True ) /* GravityStatus */
     , (82003,  19, True ) /* Attackable */
     , (82003,  22, True ) /* Inscribable */
     , (82003,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82003,   1, 'Moderate Gem of Drudge Slaying') /* Name */
     , (82003,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82003,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82003,   1,   33554809) /* Setup */
     , (82003,   3,  536870932) /* SoundTable */
     , (82003,   6,   67111919) /* PaletteBase */
     , (82003,   7,  268435723) /* ClothingBase */
     , (82003,   8,  100672149) /* Icon */
     , (82003,  22,  872415275) /* PhysicsEffectTable */
     , (82003,  50,  100673305) /* IconOverlay */;
