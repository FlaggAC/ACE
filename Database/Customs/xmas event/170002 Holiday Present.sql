DELETE FROM `weenie` WHERE `class_Id` = 170002;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (170002, 'customholidaypresentcaster', 35, '2019-02-04 06:52:23') /* Caster */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (170002,   1,      32768) /* ItemType - Caster */
     , (170002,   3,         14) /* PaletteTemplate - Red */
     , (170002,   5,        200) /* EncumbranceVal */
     , (170002,   8,         50) /* Mass */
     , (170002,   9,   16777216) /* ValidLocations - Held */
     , (170002,  16,    6291464) /* ItemUseable - SourceContainedTargetRemoteNeverWalk */
     , (170002,  18,          1) /* UiEffects - Magical */
     , (170002,  19,       5000) /* Value */
     , (170002,  46,        512) /* DefaultCombatStyle - Magic */
     , (170002,  52,          1) /* ParentLocation */
     , (170002,  53,          1) /* PlacementPosition */
     , (170002,  93,       3092) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity, LightingOn */
     , (170002,  94,         16) /* TargetType - Creature */
     , (170002, 106,        400) /* ItemSpellcraft */
     , (170002, 107,        600) /* ItemCurMana */
     , (170002, 108,        600) /* ItemMaxMana */
     , (170002, 109,        50) /* ItemDifficulty */
     , (170002, 150,        103) /* HookPlacement - Hook */
     , (170002, 151,          2) /* HookType - Wall */
     , (170002, 158,          1) /* WieldRequirements - Level */
     , (170002, 159,          1) /* WieldSkillType - Axe */
     /*, (170002, 160,          1)  WieldDifficulty */
     , (170002, 353,          0) /* WeaponType - Undef */
     , (170002,  33,          1) /* Bonded - Bonded */
     , (170002, 114,          1) /* Attuned - Attuned */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (170002,  11, True ) /* IgnoreCollisions */
     , (170002,  13, True ) /* Ethereal */
     , (170002,  14, True ) /* GravityStatus */
     , (170002,  15, True ) /* LightsStatus */
     , (170002,  19, True ) /* Attackable */
     , (170002,  22, True ) /* Inscribable */
     , (170002,  23, True ) /* DestroyOnSell */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (170002,   5, -0.025000000372529) /* ManaRate */
     , (170002,  29,       1.2) /* WeaponDefense */
     , (170002,  39,       0.2) /* DefaultScale */
     , (170002,  77,       1) /* PhysicsScriptIntensity */
     , (170002, 144,       0.25) /* ManaConversionMod */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (170002,   1, 'Holiday Present') /* Name */
     , (170002,  16, 'Merry Xmas! PotatoAC 2019.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (170002,   1,   33560155) /* Setup */
     , (170002,   3,  536870932) /* SoundTable */
     , (170002,   6,   67111919) /* PaletteBase */
     , (170002,   7,  268436199) /* ClothingBase */
     , (170002,   8,  100673909) /* Icon */
     , (170002,  19,         88) /* ActivationAnimation */
     , (170002,  22,  872415275) /* PhysicsEffectTable */
     , (170002,  27, 1073742049) /* UseUserAnimation - UseMagicWand */;

INSERT INTO `weenie_properties_spell_book` (`object_Id`, `spell`, `probability`)
VALUES (170002,   2013,      2)  /* Wizards */;
     
