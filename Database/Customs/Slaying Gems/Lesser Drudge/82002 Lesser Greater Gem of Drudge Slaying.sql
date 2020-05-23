DELETE FROM `weenie` WHERE `class_Id` = 82002;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82002, 'lessergreaterdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82002,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82002,   3,         13) /* PaletteTemplate - Purple */
     , (82002,   5,          5) /* EncumbranceVal */
     , (82002,  11,          100) /* MaxStackSize */
     , (82002,  12,          1) /* StackSize */
     , (82002,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82002,  19,        1500) /* Value */
     , (82002,  53,        101) /* PlacementPosition */
     , (82002,  91,        1) /* MaxStructure */
     , (82002,  92,        1) /* Structure */
     , (82002,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82002,  94,         -1) /* TargetType - ALL */
     , (82002, 131,         80) /* MaterialType - Ceramic */
     , (82002, 151,          9) /* HookType - Floor, Yard */
     , (82002,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82002,  11, True ) /* IgnoreCollisions */
     , (82002,  13, True ) /* Ethereal */
     , (82002,  14, True ) /* GravityStatus */
     , (82002,  19, True ) /* Attackable */
     , (82002,  22, True ) /* Inscribable */
     , (82002,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82002,   1, 'Lesser Greater Gem of Drudge Slaying') /* Name */
     , (82002,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82002,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82002,   1,   33554809) /* Setup */
     , (82002,   3,  536870932) /* SoundTable */
     , (82002,   6,   67111919) /* PaletteBase */
     , (82002,   7,  268435723) /* ClothingBase */
     , (82002,   8,  100672149) /* Icon */
     , (82002,  22,  872415275) /* PhysicsEffectTable */
     , (82002,  50,  100673313) /* IconOverlay */;
