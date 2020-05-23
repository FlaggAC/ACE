DELETE FROM `weenie` WHERE `class_Id` = 6600;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (6600, 'coatamullianshadowgreater', 2, '2005-02-09 10:00:00') /* Clothing */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (6600,   1,          2) /* ItemType - Armor */
     , (6600,   3,         21) /* PaletteTemplate - Gold */
     , (6600,   4,      13312) /* ClothingPriority - OuterwearChest, OuterwearUpperArms, OuterwearLowerArms */
     , (6600,   5,       1600) /* EncumbranceVal */
     , (6600,   8,       1000) /* Mass */
     , (6600,   9,       6656) /* ValidLocations - ChestArmor, UpperArmArmor, LowerArmArmor */
     , (6600,  16,          1) /* ItemUseable - No */
     , (6600,  19,       300) /* Value */
     , (6600,  27,          8) /* ArmorType - Scalemail */
     , (6600,  28,        190) /* ArmorLevel */
     , (6600,  33,          1) /* Bonded - Bonded */
     , (6600,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (6600,  15,          300) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (6600,  22, True ) /* Inscribable */
     , (6600,  23, True ) /* DestroyOnSell */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (6600,  12,     0.3) /* Shade */
     , (6600,  13,       1) /* ArmorModVsSlash */
     , (6600,  14,     1.1) /* ArmorModVsPierce */
     , (6600,  15,       1) /* ArmorModVsBludgeon */
     , (6600,  16,     0.8) /* ArmorModVsCold */
     , (6600,  17,     0.8) /* ArmorModVsFire */
     , (6600,  18,     0.8) /* ArmorModVsAcid */
     , (6600,  19,     0.5) /* ArmorModVsElectric */
     , (6600, 110,       1) /* BulkMod */
     , (6600, 111,       1) /* SizeMod */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (6600,   1, 'Greater Amuli Shadow Coat') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (6600,   1,   33554854) /* Setup */
     , (6600,   3,  536870932) /* SoundTable */
     , (6600,   6,   67108990) /* PaletteBase */
     , (6600,   7,  268435873) /* ClothingBase */
     , (6600,   8,  100670435) /* Icon */
     , (6600,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 6606;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (6606, 'leggingsamullianshadowgreater', 2, '2005-02-09 10:00:00') /* Clothing */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (6606,   1,          2) /* ItemType - Armor */
     , (6606,   3,         18) /* PaletteTemplate - YellowBrown */
     , (6606,   4,       2816) /* ClothingPriority - OuterwearUpperLegs, OuterwearLowerLegs, OuterwearAbdomen */
     , (6606,   5,       2288) /* EncumbranceVal */
     , (6606,   8,       1275) /* Mass */
     , (6606,   9,      25600) /* ValidLocations - AbdomenArmor, UpperLegArmor, LowerLegArmor */
     , (6606,  16,          1) /* ItemUseable - No */
     , (6606,  19,       300) /* Value */
     , (6606,  27,          2) /* ArmorType - Leather */
     , (6606,  28,        190) /* ArmorLevel */
     , (6606,  33,          1) /* Bonded - Bonded */
     , (6606,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (6606,  15,          300) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (6606,  22, True ) /* Inscribable */
     , (6606,  23, True ) /* DestroyOnSell */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (6606,  12,     0.3) /* Shade */
     , (6606,  13,       1) /* ArmorModVsSlash */
     , (6606,  14,     0.8) /* ArmorModVsPierce */
     , (6606,  15,       1) /* ArmorModVsBludgeon */
     , (6606,  16,     0.8) /* ArmorModVsCold */
     , (6606,  17,     0.8) /* ArmorModVsFire */
     , (6606,  18,     0.8) /* ArmorModVsAcid */
     , (6606,  19,     0.6) /* ArmorModVsElectric */
     , (6606, 110,       1) /* BulkMod */
     , (6606, 111,       1) /* SizeMod */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (6606,   1, 'Greater Amuli Shadow Leggings') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (6606,   1,   33554856) /* Setup */
     , (6606,   3,  536870932) /* SoundTable */
     , (6606,   6,   67108990) /* PaletteBase */
     , (6606,   7,  268435872) /* ClothingBase */
     , (6606,   8,  100670443) /* Icon */
     , (6606,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 35180;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (35180, 'ace35180-hulkingbunnyslippers', 2, '2019-02-04 06:52:23') /* Clothing */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (35180,   1,          4) /* ItemType - Clothing */
     , (35180,   4,      65536) /* ClothingPriority - Feet */
     , (35180,   5,        500) /* EncumbranceVal */
     , (35180,   9,        256) /* ValidLocations - FootWear */
     , (35180,  16,          1) /* ItemUseable - No */
     , (35180,  19,          350) /* Value */
     , (35180,  28,         50) /* ArmorLevel */
     , (35180,  53,        101) /* PlacementPosition */
     , (35180,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (35180, 106,         30) /* ItemSpellcraft */
     , (35180, 107,        397) /* ItemCurMana */
     , (35180, 108,        500) /* ItemMaxMana */
     , (35180, 109,        225) /* ItemDifficulty */
     , (35180, 151,          1) /* HookType - Floor */
     , (35180,  15,          350) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (35180,  11, True ) /* IgnoreCollisions */
     , (35180,  13, True ) /* Ethereal */
     , (35180,  14, True ) /* GravityStatus */
     , (35180,  19, True ) /* Attackable */
     , (35180,  22, True ) /* Inscribable */
     , (35180,  69, False) /* IsSellable */
     , (35180, 100, True ) /* Dyable */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (35180,   5, -0.033330000936985) /* ManaRate */
     , (35180,  13,     0.5) /* ArmorModVsSlash */
     , (35180,  14,     0.5) /* ArmorModVsPierce */
     , (35180,  15,     0.5) /* ArmorModVsBludgeon */
     , (35180,  16, 1.29999995231628) /* ArmorModVsCold */
     , (35180,  17, 0.400000005960464) /* ArmorModVsFire */
     , (35180,  18, 0.400000005960464) /* ArmorModVsAcid */
     , (35180,  19, 0.400000005960464) /* ArmorModVsElectric */
     , (35180,  39,       2) /* DefaultScale */
     , (35180, 165,       1) /* ArmorModVsNether */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (35180,   1, 'Hulking Bunny Slippers') /* Name */
     , (35180,  16, 'A pair of hulking bunny slippers.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (35180,   1,   33557435) /* Setup */
     , (35180,   6,   67108990) /* PaletteBase */
     , (35180,   7,  268437202) /* ClothingBase */
     , (35180,   8,  100672378) /* Icon */
     , (35180,  22,  872415275) /* PhysicsEffectTable */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (35180,  2257,      2)  /* Jahannan's Blessing */
     , (35180,  2301,      2)  /* Saladur's Blessing */
     , (35180,  2529,      2)  /* Major Sprint */;
