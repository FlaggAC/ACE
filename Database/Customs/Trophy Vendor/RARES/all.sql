DELETE FROM `weenie` WHERE `class_Id` = 30186;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (30186, 'gemrarevolatilearmortinkering', 38, '2019-08-11 06:52:23') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (30186,   1,       2048) /* ItemType - Gem */
     , (30186,   3,         39) /* PaletteTemplate - Black */
     , (30186,   5,          5) /* EncumbranceVal */
     , (30186,   8,          5) /* Mass */
     , (30186,  11,        100) /* MaxStackSize */
     , (30186,  12,          1) /* StackSize */
     , (30186,  13,          5) /* StackUnitEncumbrance */
     , (30186,  14,          5) /* StackUnitMass */
     , (30186,  15,          10) /* StackUnitValue */
     , (30186,  16,          8) /* ItemUseable - Contained */
     , (30186,  17,          9) /* RareId */
     , (30186,  18,          1) /* UiEffects - Magical */
     , (30186,  19,          10) /* Value */
     , (30186,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (30186,  33,         -1) /* Bonded - Slippery */
     , (30186,  53,        101) /* PlacementPosition */
     , (30186,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (30186,  94,         16) /* TargetType - Creature */
     , (30186, 106,        325) /* ItemSpellcraft */
     , (30186, 108,      10000) /* ItemMaxMana */
     , (30186, 109,          0) /* ItemDifficulty */
     , (30186, 150,        103) /* HookPlacement - Hook */
     , (30186, 151,         11) /* HookType - Floor, Wall, Yard */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (30186,  11, True ) /* IgnoreCollisions */
     , (30186,  13, True ) /* Ethereal */
     , (30186,  14, True ) /* GravityStatus */
     , (30186,  19, True ) /* Attackable */
     , (30186,  22, True ) /* Inscribable */
     , (30186, 108, True ) /* RareUsesTimer */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (30186,   1, 'Smithy''s Crystal') /* Name */
     , (30186,  16, 'Using this gem will increase your Armor Tinkering skill by 250 for 15 minutes.') /* LongDesc */
     , (30186,  20, 'Smithy''s Crystals') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (30186,   1,   33554809) /* Setup */
     , (30186,   3,  536870932) /* SoundTable */
     , (30186,   6,   67111919) /* PaletteBase */
     , (30186,   7,  268435723) /* ClothingBase */
     , (30186,   8,  100686697) /* Icon */
     , (30186,  22,  872415275) /* PhysicsEffectTable */
     , (30186,  28,       3683) /* Spell - Prodigal Armor Expertise */
     , (30186,  50,  100686630) /* IconOverlay */
     , (30186,  52,  100686604) /* IconUnderlay */;
DELETE FROM `weenie` WHERE `class_Id` = 30215;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (30215, 'gemrarevolatileitemtinkering', 38, '2019-08-11 06:52:23') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (30215,   1,       2048) /* ItemType - Gem */
     , (30215,   3,         39) /* PaletteTemplate - Black */
     , (30215,   5,          5) /* EncumbranceVal */
     , (30215,   8,          5) /* Mass */
     , (30215,  11,        100) /* MaxStackSize */
     , (30215,  12,          1) /* StackSize */
     , (30215,  13,          5) /* StackUnitEncumbrance */
     , (30215,  14,          5) /* StackUnitMass */
     , (30215,  15,          10) /* StackUnitValue */
     , (30215,  16,          8) /* ItemUseable - Contained */
     , (30215,  17,         22) /* RareId */
     , (30215,  18,          1) /* UiEffects - Magical */
     , (30215,  19,          10) /* Value */
     , (30215,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (30215,  33,         -1) /* Bonded - Slippery */
     , (30215,  53,        101) /* PlacementPosition */
     , (30215,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (30215,  94,         16) /* TargetType - Creature */
     , (30215, 106,        325) /* ItemSpellcraft */
     , (30215, 108,      10000) /* ItemMaxMana */
     , (30215, 109,          0) /* ItemDifficulty */
     , (30215, 150,        103) /* HookPlacement - Hook */
     , (30215, 151,         11) /* HookType - Floor, Wall, Yard */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (30215,  11, True ) /* IgnoreCollisions */
     , (30215,  13, True ) /* Ethereal */
     , (30215,  14, True ) /* GravityStatus */
     , (30215,  19, True ) /* Attackable */
     , (30215,  22, True ) /* Inscribable */
     , (30215, 108, True ) /* RareUsesTimer */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (30215,   1, 'Tinker''s Crystal') /* Name */
     , (30215,  16, 'Using this gem will increase your Item Tinkering skill by 250 for 15 minutes.') /* LongDesc */
     , (30215,  20, 'Tinker''s Crystals') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (30215,   1,   33554809) /* Setup */
     , (30215,   3,  536870932) /* SoundTable */
     , (30215,   6,   67111919) /* PaletteBase */
     , (30215,   7,  268435723) /* ClothingBase */
     , (30215,   8,  100686697) /* Icon */
     , (30215,  22,  872415275) /* PhysicsEffectTable */
     , (30215,  28,       3714) /* Spell - Prodigal Item Expertise */
     , (30215,  50,  100686661) /* IconOverlay */
     , (30215,  52,  100686604) /* IconUnderlay */;
DELETE FROM `weenie` WHERE `class_Id` = 30217;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (30217, 'gemrarevolatileleadership', 38, '2019-08-11 06:52:23') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (30217,   1,       2048) /* ItemType - Gem */
     , (30217,   3,         39) /* PaletteTemplate - Black */
     , (30217,   5,          5) /* EncumbranceVal */
     , (30217,   8,          5) /* Mass */
     , (30217,  11,        100) /* MaxStackSize */
     , (30217,  12,          1) /* StackSize */
     , (30217,  13,          5) /* StackUnitEncumbrance */
     , (30217,  14,          5) /* StackUnitMass */
     , (30217,  15,          10) /* StackUnitValue */
     , (30217,  16,          8) /* ItemUseable - Contained */
     , (30217,  17,         24) /* RareId */
     , (30217,  18,          1) /* UiEffects - Magical */
     , (30217,  19,          10) /* Value */
     , (30217,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (30217,  33,         -1) /* Bonded - Slippery */
     , (30217,  53,        101) /* PlacementPosition */
     , (30217,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (30217,  94,         16) /* TargetType - Creature */
     , (30217, 106,        325) /* ItemSpellcraft */
     , (30217, 108,      10000) /* ItemMaxMana */
     , (30217, 109,          0) /* ItemDifficulty */
     , (30217, 150,        103) /* HookPlacement - Hook */
     , (30217, 151,         11) /* HookType - Floor, Wall, Yard */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (30217,  11, True ) /* IgnoreCollisions */
     , (30217,  13, True ) /* Ethereal */
     , (30217,  14, True ) /* GravityStatus */
     , (30217,  19, True ) /* Attackable */
     , (30217,  22, True ) /* Inscribable */
     , (30217, 108, True ) /* RareUsesTimer */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (30217,   1, 'Monarch''s Crystal') /* Name */
     , (30217,  16, 'Using this gem will increase your Leadership skill by 250 for 15 minutes.') /* LongDesc */
     , (30217,  20, 'Monarch''s Crystals') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (30217,   1,   33554809) /* Setup */
     , (30217,   3,  536870932) /* SoundTable */
     , (30217,   6,   67111919) /* PaletteBase */
     , (30217,   7,  268435723) /* ClothingBase */
     , (30217,   8,  100686697) /* Icon */
     , (30217,  22,  872415275) /* PhysicsEffectTable */
     , (30217,  28,       3716) /* Spell - Prodigal Leadership Mastery */
     , (30217,  50,  100686663) /* IconOverlay */
     , (30217,  52,  100686604) /* IconUnderlay */;
DELETE FROM `weenie` WHERE `class_Id` = 30222;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (30222, 'gemrarevolatileloyalty', 38, '2019-08-11 06:52:23') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (30222,   1,       2048) /* ItemType - Gem */
     , (30222,   3,         39) /* PaletteTemplate - Black */
     , (30222,   5,          5) /* EncumbranceVal */
     , (30222,   8,          5) /* Mass */
     , (30222,  11,        100) /* MaxStackSize */
     , (30222,  12,          1) /* StackSize */
     , (30222,  13,          5) /* StackUnitEncumbrance */
     , (30222,  14,          5) /* StackUnitMass */
     , (30222,  15,          10) /* StackUnitValue */
     , (30222,  16,          8) /* ItemUseable - Contained */
     , (30222,  17,         27) /* RareId */
     , (30222,  18,          1) /* UiEffects - Magical */
     , (30222,  19,          10) /* Value */
     , (30222,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (30222,  33,         -1) /* Bonded - Slippery */
     , (30222,  53,        101) /* PlacementPosition */
     , (30222,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (30222,  94,         16) /* TargetType - Creature */
     , (30222, 106,        325) /* ItemSpellcraft */
     , (30222, 108,      10000) /* ItemMaxMana */
     , (30222, 109,          0) /* ItemDifficulty */
     , (30222, 150,        103) /* HookPlacement - Hook */
     , (30222, 151,         11) /* HookType - Floor, Wall, Yard */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (30222,  11, True ) /* IgnoreCollisions */
     , (30222,  13, True ) /* Ethereal */
     , (30222,  14, True ) /* GravityStatus */
     , (30222,  19, True ) /* Attackable */
     , (30222,  22, True ) /* Inscribable */
     , (30222, 108, True ) /* RareUsesTimer */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (30222,   1, 'Adherent''s Crystal') /* Name */
     , (30222,  16, 'Using this gem will increase your Loyalty skill by 250 for 15 minutes.') /* LongDesc */
     , (30222,  20, 'Adherent''s Crystals') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (30222,   1,   33554809) /* Setup */
     , (30222,   3,  536870932) /* SoundTable */
     , (30222,   6,   67111919) /* PaletteBase */
     , (30222,   7,  268435723) /* ClothingBase */
     , (30222,   8,  100686697) /* Icon */
     , (30222,  22,  872415275) /* PhysicsEffectTable */
     , (30222,  28,       3701) /* Spell - Prodigal Fealty */
     , (30222,  50,  100686669) /* IconOverlay */
     , (30222,  52,  100686604) /* IconUnderlay */;
DELETE FROM `weenie` WHERE `class_Id` = 30225;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (30225, 'gemrarevolatilemagicitemtinkering', 38, '2019-08-11 06:52:23') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (30225,   1,       2048) /* ItemType - Gem */
     , (30225,   3,         39) /* PaletteTemplate - Black */
     , (30225,   5,          5) /* EncumbranceVal */
     , (30225,   8,          5) /* Mass */
     , (30225,  11,        100) /* MaxStackSize */
     , (30225,  12,          1) /* StackSize */
     , (30225,  13,          5) /* StackUnitEncumbrance */
     , (30225,  14,          5) /* StackUnitMass */
     , (30225,  15,          10) /* StackUnitValue */
     , (30225,  16,          8) /* ItemUseable - Contained */
     , (30225,  17,         30) /* RareId */
     , (30225,  18,          1) /* UiEffects - Magical */
     , (30225,  19,          10) /* Value */
     , (30225,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (30225,  33,         -1) /* Bonded - Slippery */
     , (30225,  53,        101) /* PlacementPosition */
     , (30225,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (30225,  94,         16) /* TargetType - Creature */
     , (30225, 106,        325) /* ItemSpellcraft */
     , (30225, 108,      10000) /* ItemMaxMana */
     , (30225, 109,          0) /* ItemDifficulty */
     , (30225, 150,        103) /* HookPlacement - Hook */
     , (30225, 151,         11) /* HookType - Floor, Wall, Yard */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (30225,  11, True ) /* IgnoreCollisions */
     , (30225,  13, True ) /* Ethereal */
     , (30225,  14, True ) /* GravityStatus */
     , (30225,  19, True ) /* Attackable */
     , (30225,  22, True ) /* Inscribable */
     , (30225, 108, True ) /* RareUsesTimer */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (30225,   1, 'Imbuer''s Crystal') /* Name */
     , (30225,  16, 'Using this gem will increase your Magic Item Tinkering skill by 250 for 15 minutes.') /* LongDesc */
     , (30225,  20, 'Imbuer''s Crystals') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (30225,   1,   33554809) /* Setup */
     , (30225,   3,  536870932) /* SoundTable */
     , (30225,   6,   67111919) /* PaletteBase */
     , (30225,   7,  268435723) /* ClothingBase */
     , (30225,   8,  100686697) /* Icon */
     , (30225,  22,  872415275) /* PhysicsEffectTable */
     , (30225,  28,       3722) /* Spell - Prodigal Magic Item Expertise */
     , (30225,  50,  100686672) /* IconOverlay */
     , (30225,  52,  100686604) /* IconUnderlay */;
DELETE FROM `weenie` WHERE `class_Id` = 30246;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (30246, 'gemrarevolatileweapontinkering', 38, '2019-08-11 06:52:23') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (30246,   1,       2048) /* ItemType - Gem */
     , (30246,   3,         39) /* PaletteTemplate - Black */
     , (30246,   5,          5) /* EncumbranceVal */
     , (30246,   8,          5) /* Mass */
     , (30246,  11,        100) /* MaxStackSize */
     , (30246,  12,          1) /* StackSize */
     , (30246,  13,          5) /* StackUnitEncumbrance */
     , (30246,  14,          5) /* StackUnitMass */
     , (30246,  15,          10) /* StackUnitValue */
     , (30246,  16,          8) /* ItemUseable - Contained */
     , (30246,  17,         41) /* RareId */
     , (30246,  18,          1) /* UiEffects - Magical */
     , (30246,  19,          10) /* Value */
     , (30246,  26,          1) /* AccountRequirements - AsheronsCall_Subscription */
     , (30246,  33,         -1) /* Bonded - Slippery */
     , (30246,  53,        101) /* PlacementPosition */
     , (30246,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (30246,  94,         16) /* TargetType - Creature */
     , (30246, 106,        325) /* ItemSpellcraft */
     , (30246, 108,      10000) /* ItemMaxMana */
     , (30246, 109,          0) /* ItemDifficulty */
     , (30246, 150,        103) /* HookPlacement - Hook */
     , (30246, 151,         11) /* HookType - Floor, Wall, Yard */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (30246,  11, True ) /* IgnoreCollisions */
     , (30246,  13, True ) /* Ethereal */
     , (30246,  14, True ) /* GravityStatus */
     , (30246,  19, True ) /* Attackable */
     , (30246,  22, True ) /* Inscribable */
     , (30246, 108, True ) /* RareUsesTimer */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (30246,   1, 'Artist''s Crystal') /* Name */
     , (30246,  16, 'Using this gem will increase your Weapon Tinkering skill by 250 for 15 minutes.') /* LongDesc */
     , (30246,  20, 'Artist''s Crystals') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (30246,   1,   33554809) /* Setup */
     , (30246,   3,  536870932) /* SoundTable */
     , (30246,   6,   67111919) /* PaletteBase */
     , (30246,   7,  268435723) /* ClothingBase */
     , (30246,   8,  100686697) /* Icon */
     , (30246,  22,  872415275) /* PhysicsEffectTable */
     , (30246,  28,       3744) /* Spell - Prodigal Weapon Expertise */
     , (30246,  50,  100686694) /* IconOverlay */
     , (30246,  52,  100686604) /* IconUnderlay */;
