DELETE FROM `weenie` WHERE `class_Id` = 5893;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (5893, 'robehoarymattekar', 2, '2019-02-04 06:52:23') /* Clothing */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (5893,   1,          2) /* ItemType - Armor */
     , (5893,   3,         61) /* PaletteTemplate - White */
     , (5893,   4,      81664) /* ClothingPriority - OuterwearUpperLegs, OuterwearLowerLegs, OuterwearChest, OuterwearAbdomen, OuterwearUpperArms, OuterwearLowerArms, Feet */
     , (5893,   5,       1300) /* EncumbranceVal */
     , (5893,   8,        340) /* Mass */
     , (5893,   9,      32512) /* ValidLocations - Armor */
     , (5893,  16,          1) /* ItemUseable - No */
     , (5893,  19,        500) /* Value */
     , (5893,  27,          1) /* ArmorType - Cloth */
     , (5893,  28,        150) /* ArmorLevel */
     , (5893,  53,        101) /* PlacementPosition */
     , (5893,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (5893, 150,        103) /* HookPlacement - Hook */
     , (5893, 151,          2) /* HookType - Wall */
     , (5893,  15,        500) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (5893,  11, True ) /* IgnoreCollisions */
     , (5893,  13, True ) /* Ethereal */
     , (5893,  14, True ) /* GravityStatus */
     , (5893,  19, True ) /* Attackable */
     , (5893,  22, True ) /* Inscribable */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (5893,  12,       1) /* Shade */
     , (5893,  13, 1.20000004768372) /* ArmorModVsSlash */
     , (5893,  14, 0.899999976158142) /* ArmorModVsPierce */
     , (5893,  15, 0.899999976158142) /* ArmorModVsBludgeon */
     , (5893,  16,       2) /* ArmorModVsCold */
     , (5893,  17, 0.699999988079071) /* ArmorModVsFire */
     , (5893,  18,       1) /* ArmorModVsAcid */
     , (5893,  19,       2) /* ArmorModVsElectric */
     , (5893, 110,       1) /* BulkMod */
     , (5893, 111,       1) /* SizeMod */
     , (5893, 165,       1) /* ArmorModVsNether */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (5893,   1, 'Hoary Mattekar Robe') /* Name */
     , (5893,  15, 'Rare, lightweight, but warm robe crafted from the hide of the elusive Hoary Mattekar, rumored to appear only under certain conditions.') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (5893,   1,   33554854) /* Setup */
     , (5893,   3,  536870932) /* SoundTable */
     , (5893,   6,   67108990) /* PaletteBase */
     , (5893,   7,  268436244) /* ClothingBase */
     , (5893,   8,  100672057) /* Icon */
     , (5893,  22,  872415275) /* PhysicsEffectTable */
     , (5893,  36,  234881046) /* MutateFilter */;
DELETE FROM `weenie` WHERE `class_Id` = 27256;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (27256, 'gemburningcoal', 38, '2005-02-09 10:00:00') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (27256,   1,         32) /* ItemType - Food */
     , (27256,   5,         75) /* EncumbranceVal */
     , (27256,   8,         75) /* Mass */
     , (27256,  11,         10) /* MaxStackSize */
     , (27256,  12,          1) /* StackSize */
     , (27256,  13,         75) /* StackUnitEncumbrance */
     , (27256,  14,         75) /* StackUnitMass */
     , (27256,  15,        7) /* StackUnitValue */
     , (27256,  16,          8) /* ItemUseable - Contained */
     , (27256,  18,          1) /* UiEffects - Magical */
     , (27256,  19,        7) /* Value */
     , (27256,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (27256,  94,         16) /* TargetType - Creature */
     , (27256, 106,        150) /* ItemSpellcraft */
     , (27256, 107,         50) /* ItemCurMana */
     , (27256, 108,         50) /* ItemMaxMana */
     , (27256, 109,        200) /* ItemDifficulty */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (27256,  23, True ) /* DestroyOnSell */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (27256,   1, 'Burning Coal') /* Name */
     , (27256,  14, 'Use this item to stoke the fire within.') /* Use */
     , (27256,  16, 'A smoldering coal. The center of this rock seems to glow with intense heat, yet the surface is cool to the touch.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (27256,   1,   33558517) /* Setup */
     , (27256,   3,  536870932) /* SoundTable */
     , (27256,   8,  100676392) /* Icon */
     , (27256,  22,  872415275) /* PhysicsEffectTable */
     , (27256,  28,       3204) /* Spell - Blazing Heart */;
DELETE FROM `weenie` WHERE `class_Id` = 28842;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (28842, 'potioneggpenguincave', 38, '2019-02-04 06:52:23') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (28842,   1,        128) /* ItemType - Misc */
     , (28842,   3,          2) /* PaletteTemplate - Blue */
     , (28842,   5,        100) /* EncumbranceVal */
     , (28842,  11,          1) /* MaxStackSize */
     , (28842,  12,          1) /* StackSize */
     , (28842,  13,         35) /* StackUnitEncumbrance */
     , (28842,  15,        15) /* StackUnitValue */
     , (28842,  16,          8) /* ItemUseable - Contained */
     , (28842,  19,       15) /* Value */
     , (28842,  53,        101) /* PlacementPosition */
     , (28842,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (28842, 106,        111) /* ItemSpellcraft */
     , (28842, 107,        100) /* ItemCurMana */
     , (28842, 108,        100) /* ItemMaxMana */
     , (28842, 109,          0) /* ItemDifficulty */
     , (28842, 150,         11) /* HookPlacement */
     , (28842, 151,         11) /* HookType - Floor, Wall, Yard */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (28842,  11, True ) /* IgnoreCollisions */
     , (28842,  13, True ) /* Ethereal */
     , (28842,  14, True ) /* GravityStatus */
     , (28842,  19, True ) /* Attackable */
     , (28842,  22, False) /* Inscribable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (28842,   1, 'Essence of Cave Penguin') /* Name */
     , (28842,  14, 'Use this item to drink it.') /* Use */
     , (28842,  16, 'This is a drink prepared by Chef Martam. It consists of a raw cave penguin egg and various secret ingredients.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (28842,   1,   33554603) /* Setup */
     , (28842,   3,  536870932) /* SoundTable */
     , (28842,   8,  100686396) /* Icon */
     , (28842,  22,  872415275) /* PhysicsEffectTable */
     , (28842,  28,       3571) /* Spell - Health Boost */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (28842,  3571,      2)  /* Health Boost */;
DELETE FROM `weenie` WHERE `class_Id` = 29295;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (29295, 'gemaugmentationblank', 1, '2019-02-04 06:52:23') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (29295,   1,        128) /* ItemType - Misc */
     , (29295,   5,         10) /* EncumbranceVal */
     , (29295,   8,         10) /* Mass */
     , (29295,  16,          1) /* ItemUseable - No */
     , (29295,  19,        75) /* Value */
     , (29295,  33,          1) /* Bonded - Bonded */
     , (29295,  53,        101) /* PlacementPosition */
     , (29295,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (29295, 114,          1) /* Attuned - Attuned */
     , (29295,  15,        75) /* StackUnitValue */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (29295,  11, True ) /* IgnoreCollisions */
     , (29295,  13, True ) /* Ethereal */
     , (29295,  14, True ) /* GravityStatus */
     , (29295,  19, True ) /* Attackable */
     , (29295,  22, True ) /* Inscribable */
     , (29295,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (29295,   1, 'Blank Augmentation Gem') /* Name */
     , (29295,  16, 'An uninscribed Augmentation Gem. Hand this item to an Augmentation Trainer in exchange for an inscribed Augmentation Gem.') /* LongDesc */
     , (29295,  33, 'BlankAug') /* Quest */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (29295,   1,   33554809) /* Setup */
     , (29295,   3,  536870932) /* SoundTable */
     , (29295,   8,  100686475) /* Icon */
     , (29295,  22,  872415275) /* PhysicsEffectTable */;DELETE FROM `weenie` WHERE `class_Id` = 31425;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (31425, 'ace31425-rageofgraelgem', 38, '2019-03-26 20:02:53') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (31425,   1,       2048) /* ItemType - Gem */
     , (31425,   3,         14) /* PaletteTemplate - Red */
     , (31425,   5,         10) /* EncumbranceVal */
     , (31425,  11,        100) /* MaxStackSize */
     , (31425,  12,          1) /* StackSize */
     , (31425,  13,         10) /* StackUnitEncumbrance */
     , (31425,  15,          6) /* StackUnitValue */
     , (31425,  16,          8) /* ItemUseable - Contained */
     , (31425,  18,          1) /* UiEffects - Magical */
     , (31425,  19,          6) /* Value */
     , (31425,  53,        101) /* PlacementPosition - Resting */
     , (31425,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (31425,  94,         16) /* TargetType - Creature */
     , (31425, 106,        210) /* ItemSpellcraft */
     , (31425, 107,        100) /* ItemCurMana */
     , (31425, 108,        200) /* ItemMaxMana */
     , (31425, 109,          0) /* ItemDifficulty */
     , (31425, 110,          0) /* ItemAllegianceRankLimit */
     , (31425, 151,          2) /* HookType - Wall */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (31425,  11, True ) /* IgnoreCollisions */
     , (31425,  13, True ) /* Ethereal */
     , (31425,  14, True ) /* GravityStatus */
     , (31425,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (31425,   1, 'Rage of Grael Gem') /* Name */
     , (31425,  14, 'Use this gem to infuse your wielded weapon with the rage of Grael, increasing its damage value by 3 points.  The effects of this spell stack with Blood Drinker.') /* Use */
     , (31425,  15, 'A gem that seems to pulse with the distilled rage of the ancient gladiator, Grael.') /* ShortDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (31425,   1,   33554809) /* Setup */
     , (31425,   3,  536870932) /* SoundTable */
     , (31425,   6,   67111919) /* PaletteBase */
     , (31425,   7,  268435723) /* ClothingBase */
     , (31425,   8,  100687889) /* Icon */
     , (31425,  22,  872415275) /* PhysicsEffectTable */
     , (31425,  28,       3828) /* Spell - Rage of Grael */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (31425,  3828,      2)  /* Rage of Grael */;
