DELETE FROM `weenie` WHERE `class_Id` = 82001;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (82001, 'lesserlesserdrudgeslaying', 51, '2019-08-12 23:02:29') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (82001,   1, 1073741824) /* ItemType - TinkeringMaterial */
     , (82001,   3,         13) /* PaletteTemplate - Purple */
     , (82001,   5,          5) /* EncumbranceVal */
     , (82001,  11,          100) /* MaxStackSize */
     , (82001,  12,          1) /* StackSize */
     , (82001,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (82001,  19,        1500) /* Value */
     , (82001,  53,        101) /* PlacementPosition */
     , (82001,  91,        1) /* MaxStructure */
     , (82001,  92,        1) /* Structure */
     , (82001,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (82001,  94,         -1) /* TargetType - ALL */
     , (82001, 131,         79) /* MaterialType - Ceramic */
     , (82001, 151,          9) /* HookType - Floor, Yard */
     , (82001,  15,         1500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (82001,  11, True ) /* IgnoreCollisions */
     , (82001,  13, True ) /* Ethereal */
     , (82001,  14, True ) /* GravityStatus */
     , (82001,  19, True ) /* Attackable */
     , (82001,  22, True ) /* Inscribable */
     , (82001,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (82001,   1, 'Lesser Lesser Gem of Drudge Slaying') /* Name */
     , (82001,  14, 'Apply this gem to a treasure-generated weapon to grant the target with the ''Drudge Slayer'' property.') /* Use */
     , (82001,  15, 'A manifestation of your knowledge about Drudges ') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (82001,   1,   33554809) /* Setup */
     , (82001,   3,  536870932) /* SoundTable */
     , (82001,   6,   67111919) /* PaletteBase */
     , (82001,   7,  268435723) /* ClothingBase */
     , (82001,   8,  100672149) /* Icon */
     , (82001,  22,  872415275) /* PhysicsEffectTable */
     , (82001,  50,  100673313) /* IconOverlay */;
