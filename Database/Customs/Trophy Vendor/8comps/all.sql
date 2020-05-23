DELETE FROM `weenie` WHERE `class_Id` = 37300;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37300, 'ace37300-glyphofendurance', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37300,   1,        128) /* ItemType - Misc */
     , (37300,   5,         25) /* EncumbranceVal */
     , (37300,  11,       1000) /* MaxStackSize */
     , (37300,  12,          1) /* StackSize */
     , (37300,  13,         25) /* StackUnitEncumbrance */
     , (37300,  15,          2) /* StackUnitValue */
     , (37300,  16,          1) /* ItemUseable - No */
     , (37300,  19,          2) /* Value */
     , (37300,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37300,  13, True ) /* Ethereal */
     , (37300,  14, True ) /* GravityStatus */
     , (37300,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37300,   1, 'Glyph of Endurance') /* Name */
     , (37300,  20, 'Glyphs of Endurance') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37300,   1,   33554809) /* Setup */
     , (37300,   3,  536870932) /* SoundTable */
     , (37300,   6,   67111919) /* PaletteBase */
     , (37300,   8,  100690191) /* Icon */
     , (37300,  22,  872415275) /* PhysicsEffectTable */
     , (37300,  50,  100686648) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37301;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37301, 'ace37301-glyphofflame', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37301,   1,        128) /* ItemType - Misc */
     , (37301,   5,         25) /* EncumbranceVal */
     , (37301,  11,       1000) /* MaxStackSize */
     , (37301,  12,          1) /* StackSize */
     , (37301,  13,         25) /* StackUnitEncumbrance */
     , (37301,  15,      2) /* StackUnitValue */
     , (37301,  16,          1) /* ItemUseable - No */
     , (37301,  19,      2) /* Value */
     , (37301,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37301,  11, True ) /* IgnoreCollisions */
     , (37301,  13, True ) /* Ethereal */
     , (37301,  14, True ) /* GravityStatus */
     , (37301,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37301,   1, 'Glyph of Flame') /* Name */
     , (37301,  20, 'Glyphs of Flame') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37301,   1,   33554809) /* Setup */
     , (37301,   3,  536870932) /* SoundTable */
     , (37301,   6,   67111919) /* PaletteBase */
     , (37301,   8,  100690191) /* Icon */
     , (37301,  22,  872415275) /* PhysicsEffectTable */
     , (37301,  50,  100686650) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37302;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37302, 'ace37302-glyphoffletching', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37302,   1,        128) /* ItemType - Misc */
     , (37302,   5,         25) /* EncumbranceVal */
     , (37302,  11,       1000) /* MaxStackSize */
     , (37302,  12,          1) /* StackSize */
     , (37302,  13,         25) /* StackUnitEncumbrance */
     , (37302,  15,      2) /* StackUnitValue */
     , (37302,  16,          1) /* ItemUseable - No */
     , (37302,  19,      2) /* Value */
     , (37302,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37302,  11, True ) /* IgnoreCollisions */
     , (37302,  13, True ) /* Ethereal */
     , (37302,  14, True ) /* GravityStatus */
     , (37302,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37302,   1, 'Glyph of Fletching') /* Name */
     , (37302,  20, 'Glyphs of Fletching') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37302,   1,   33554809) /* Setup */
     , (37302,   3,  536870932) /* SoundTable */
     , (37302,   6,   67111919) /* PaletteBase */
     , (37302,   8,  100690191) /* Icon */
     , (37302,  22,  872415275) /* PhysicsEffectTable */
     , (37302,  50,  100686651) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37303;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37303, 'ace37303-glyphoffocus', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37303,   1,        128) /* ItemType - Misc */
     , (37303,   5,         25) /* EncumbranceVal */
     , (37303,  11,       1000) /* MaxStackSize */
     , (37303,  12,          1) /* StackSize */
     , (37303,  13,         25) /* StackUnitEncumbrance */
     , (37303,  15,      2) /* StackUnitValue */
     , (37303,  16,          1) /* ItemUseable - No */
     , (37303,  19,      2) /* Value */
     , (37303,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37303,  11, True ) /* IgnoreCollisions */
     , (37303,  13, True ) /* Ethereal */
     , (37303,  14, True ) /* GravityStatus */
     , (37303,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37303,   1, 'Glyph of Focus') /* Name */
     , (37303,  20, 'Glyphs of Focus') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37303,   1,   33554809) /* Setup */
     , (37303,   3,  536870932) /* SoundTable */
     , (37303,   6,   67111919) /* PaletteBase */
     , (37303,   8,  100690191) /* Icon */
     , (37303,  22,  872415275) /* PhysicsEffectTable */
     , (37303,  50,  100686652) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37304;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37304, 'ace37304-glyphofhealing', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37304,   1,        128) /* ItemType - Misc */
     , (37304,   5,         25) /* EncumbranceVal */
     , (37304,  11,       1000) /* MaxStackSize */
     , (37304,  12,          1) /* StackSize */
     , (37304,  13,         25) /* StackUnitEncumbrance */
     , (37304,  15,      2) /* StackUnitValue */
     , (37304,  16,          1) /* ItemUseable - No */
     , (37304,  19,      2) /* Value */
     , (37304,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37304,  11, True ) /* IgnoreCollisions */
     , (37304,  13, True ) /* Ethereal */
     , (37304,  14, True ) /* GravityStatus */
     , (37304,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37304,   1, 'Glyph of Healing') /* Name */
     , (37304,  20, 'Glyphs of Healing') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37304,   1,   33554809) /* Setup */
     , (37304,   3,  536870932) /* SoundTable */
     , (37304,   6,   67111919) /* PaletteBase */
     , (37304,   8,  100690191) /* Icon */
     , (37304,  22,  872415275) /* PhysicsEffectTable */
     , (37304,  50,  100686655) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37305;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37305, 'ace37305-glyphofhealth', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37305,   1,        128) /* ItemType - Misc */
     , (37305,   5,         25) /* EncumbranceVal */
     , (37305,  11,       1000) /* MaxStackSize */
     , (37305,  12,          1) /* StackSize */
     , (37305,  13,         25) /* StackUnitEncumbrance */
     , (37305,  15,      2) /* StackUnitValue */
     , (37305,  16,          1) /* ItemUseable - No */
     , (37305,  19,      2) /* Value */
     , (37305,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37305,  11, True ) /* IgnoreCollisions */
     , (37305,  13, True ) /* Ethereal */
     , (37305,  14, True ) /* GravityStatus */
     , (37305,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37305,   1, 'Glyph of Health') /* Name */
     , (37305,  20, 'Glyphs of Health') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37305,   1,   33554809) /* Setup */
     , (37305,   3,  536870932) /* SoundTable */
     , (37305,   6,   67111919) /* PaletteBase */
     , (37305,   8,  100690191) /* Icon */
     , (37305,  22,  872415275) /* PhysicsEffectTable */
     , (37305,  50,  100690194) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37307;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37307, 'ace37307-glyphofregeneration', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37307,   1,        128) /* ItemType - Misc */
     , (37307,   5,         25) /* EncumbranceVal */
     , (37307,  11,       1000) /* MaxStackSize */
     , (37307,  12,          1) /* StackSize */
     , (37307,  13,         25) /* StackUnitEncumbrance */
     , (37307,  15,      2) /* StackUnitValue */
     , (37307,  16,          1) /* ItemUseable - No */
     , (37307,  19,      2) /* Value */
     , (37307,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37307,  11, True ) /* IgnoreCollisions */
     , (37307,  13, True ) /* Ethereal */
     , (37307,  14, True ) /* GravityStatus */
     , (37307,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37307,   1, 'Glyph of Regeneration') /* Name */
     , (37307,  20, 'Glyphs of Regeneration') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37307,   1,   33554809) /* Setup */
     , (37307,   3,  536870932) /* SoundTable */
     , (37307,   6,   67111919) /* PaletteBase */
     , (37307,   8,  100690191) /* Icon */
     , (37307,  22,  872415275) /* PhysicsEffectTable */
     , (37307,  50,  100686656) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37309;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37309, 'ace37309-glyphofitemenchantment', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37309,   1,        128) /* ItemType - Misc */
     , (37309,   5,         25) /* EncumbranceVal */
     , (37309,  11,       1000) /* MaxStackSize */
     , (37309,  12,          1) /* StackSize */
     , (37309,  13,         25) /* StackUnitEncumbrance */
     , (37309,  15,      2) /* StackUnitValue */
     , (37309,  16,          1) /* ItemUseable - No */
     , (37309,  19,      2) /* Value */
     , (37309,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37309,  11, True ) /* IgnoreCollisions */
     , (37309,  13, True ) /* Ethereal */
     , (37309,  14, True ) /* GravityStatus */
     , (37309,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37309,   1, 'Glyph of Item Enchantment') /* Name */
     , (37309,  20, 'Glyphs of Item Enchantment') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37309,   1,   33554809) /* Setup */
     , (37309,   3,  536870932) /* SoundTable */
     , (37309,   6,   67111919) /* PaletteBase */
     , (37309,   8,  100690191) /* Icon */
     , (37309,  22,  872415275) /* PhysicsEffectTable */
     , (37309,  50,  100686660) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37310;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37310, 'ace37310-glyphofitemtinkering', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37310,   1,        128) /* ItemType - Misc */
     , (37310,   5,         25) /* EncumbranceVal */
     , (37310,  11,       1000) /* MaxStackSize */
     , (37310,  12,          1) /* StackSize */
     , (37310,  13,         25) /* StackUnitEncumbrance */
     , (37310,  15,      2) /* StackUnitValue */
     , (37310,  16,          1) /* ItemUseable - No */
     , (37310,  19,      2) /* Value */
     , (37310,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37310,  11, True ) /* IgnoreCollisions */
     , (37310,  13, True ) /* Ethereal */
     , (37310,  14, True ) /* GravityStatus */
     , (37310,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37310,   1, 'Glyph of Item Tinkering') /* Name */
     , (37310,  20, 'Glyphs of Item Tinkering') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37310,   1,   33554809) /* Setup */
     , (37310,   3,  536870932) /* SoundTable */
     , (37310,   6,   67111919) /* PaletteBase */
     , (37310,   8,  100690191) /* Icon */
     , (37310,  22,  872415275) /* PhysicsEffectTable */
     , (37310,  50,  100686661) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37311;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37311, 'ace37311-glyphofjump', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37311,   1,        128) /* ItemType - Misc */
     , (37311,   5,         25) /* EncumbranceVal */
     , (37311,  11,       1000) /* MaxStackSize */
     , (37311,  12,          1) /* StackSize */
     , (37311,  13,         25) /* StackUnitEncumbrance */
     , (37311,  15,      2) /* StackUnitValue */
     , (37311,  16,          1) /* ItemUseable - No */
     , (37311,  19,      2) /* Value */
     , (37311,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37311,  11, True ) /* IgnoreCollisions */
     , (37311,  13, True ) /* Ethereal */
     , (37311,  14, True ) /* GravityStatus */
     , (37311,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37311,   1, 'Glyph of Jump') /* Name */
     , (37311,  20, 'Glyphs of Jump') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37311,   1,   33554809) /* Setup */
     , (37311,   3,  536870932) /* SoundTable */
     , (37311,   6,   67111919) /* PaletteBase */
     , (37311,   8,  100690191) /* Icon */
     , (37311,  22,  872415275) /* PhysicsEffectTable */
     , (37311,  50,  100686662) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37312;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37312, 'ace37312-glyphofleadership', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37312,   1,        128) /* ItemType - Misc */
     , (37312,   5,         25) /* EncumbranceVal */
     , (37312,  11,       1000) /* MaxStackSize */
     , (37312,  12,          1) /* StackSize */
     , (37312,  13,         25) /* StackUnitEncumbrance */
     , (37312,  15,      2) /* StackUnitValue */
     , (37312,  16,          1) /* ItemUseable - No */
     , (37312,  19,      2) /* Value */
     , (37312,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37312,  11, True ) /* IgnoreCollisions */
     , (37312,  13, True ) /* Ethereal */
     , (37312,  14, True ) /* GravityStatus */
     , (37312,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37312,   1, 'Glyph of Leadership') /* Name */
     , (37312,  20, 'Glyphs of Leadership') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37312,   1,   33554809) /* Setup */
     , (37312,   3,  536870932) /* SoundTable */
     , (37312,   6,   67111919) /* PaletteBase */
     , (37312,   8,  100690191) /* Icon */
     , (37312,  22,  872415275) /* PhysicsEffectTable */
     , (37312,  50,  100686663) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37313;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37313, 'ace37313-glyphoflifemagic', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37313,   1,        128) /* ItemType - Misc */
     , (37313,   5,         25) /* EncumbranceVal */
     , (37313,  11,       1000) /* MaxStackSize */
     , (37313,  12,          1) /* StackSize */
     , (37313,  13,         25) /* StackUnitEncumbrance */
     , (37313,  15,      2) /* StackUnitValue */
     , (37313,  16,          1) /* ItemUseable - No */
     , (37313,  19,      2) /* Value */
     , (37313,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37313,  11, True ) /* IgnoreCollisions */
     , (37313,  13, True ) /* Ethereal */
     , (37313,  14, True ) /* GravityStatus */
     , (37313,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37313,   1, 'Glyph of Life Magic') /* Name */
     , (37313,  20, 'Glyphs of Life Magic') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37313,   1,   33554809) /* Setup */
     , (37313,   3,  536870932) /* SoundTable */
     , (37313,   6,   67111919) /* PaletteBase */
     , (37313,   8,  100690191) /* Icon */
     , (37313,  22,  872415275) /* PhysicsEffectTable */
     , (37313,  50,  100686664) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37314;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37314, 'ace37314-glyphoflightning', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37314,   1,        128) /* ItemType - Misc */
     , (37314,   5,         25) /* EncumbranceVal */
     , (37314,  11,       1000) /* MaxStackSize */
     , (37314,  12,          1) /* StackSize */
     , (37314,  13,         25) /* StackUnitEncumbrance */
     , (37314,  15,      2) /* StackUnitValue */
     , (37314,  16,          1) /* ItemUseable - No */
     , (37314,  19,      2) /* Value */
     , (37314,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37314,  11, True ) /* IgnoreCollisions */
     , (37314,  13, True ) /* Ethereal */
     , (37314,  14, True ) /* GravityStatus */
     , (37314,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37314,   1, 'Glyph of Lightning') /* Name */
     , (37314,  20, 'Glyphs of Lightning') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37314,   1,   33554809) /* Setup */
     , (37314,   3,  536870932) /* SoundTable */
     , (37314,   6,   67111919) /* PaletteBase */
     , (37314,   8,  100690191) /* Icon */
     , (37314,  22,  872415275) /* PhysicsEffectTable */
     , (37314,  50,  100686666) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37315;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37315, 'ace37315-glyphoflockpick', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37315,   1,        128) /* ItemType - Misc */
     , (37315,   5,         25) /* EncumbranceVal */
     , (37315,  11,       1000) /* MaxStackSize */
     , (37315,  12,          1) /* StackSize */
     , (37315,  13,         25) /* StackUnitEncumbrance */
     , (37315,  15,      2) /* StackUnitValue */
     , (37315,  16,          1) /* ItemUseable - No */
     , (37315,  19,      2) /* Value */
     , (37315,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37315,  11, True ) /* IgnoreCollisions */
     , (37315,  13, True ) /* Ethereal */
     , (37315,  14, True ) /* GravityStatus */
     , (37315,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37315,   1, 'Glyph of Lockpick') /* Name */
     , (37315,  20, 'Glyphs of Lockpick') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37315,   1,   33554809) /* Setup */
     , (37315,   3,  536870932) /* SoundTable */
     , (37315,   6,   67111919) /* PaletteBase */
     , (37315,   8,  100690191) /* Icon */
     , (37315,  22,  872415275) /* PhysicsEffectTable */
     , (37315,  50,  100686668) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37316;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37316, 'ace37316-glyphofloyalty', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37316,   1,        128) /* ItemType - Misc */
     , (37316,   5,         25) /* EncumbranceVal */
     , (37316,  11,       1000) /* MaxStackSize */
     , (37316,  12,          1) /* StackSize */
     , (37316,  13,         25) /* StackUnitEncumbrance */
     , (37316,  15,      2) /* StackUnitValue */
     , (37316,  16,          1) /* ItemUseable - No */
     , (37316,  19,      2) /* Value */
     , (37316,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37316,  11, True ) /* IgnoreCollisions */
     , (37316,  13, True ) /* Ethereal */
     , (37316,  14, True ) /* GravityStatus */
     , (37316,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37316,   1, 'Glyph of Loyalty') /* Name */
     , (37316,  20, 'Glyphs of Loyalty') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37316,   1,   33554809) /* Setup */
     , (37316,   3,  536870932) /* SoundTable */
     , (37316,   6,   67111919) /* PaletteBase */
     , (37316,   8,  100690191) /* Icon */
     , (37316,  22,  872415275) /* PhysicsEffectTable */
     , (37316,  50,  100686669) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37317;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37317, 'ace37317-glyphofmagicdefense', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37317,   1,        128) /* ItemType - Misc */
     , (37317,   5,         25) /* EncumbranceVal */
     , (37317,  11,       1000) /* MaxStackSize */
     , (37317,  12,          1) /* StackSize */
     , (37317,  13,         25) /* StackUnitEncumbrance */
     , (37317,  15,      2) /* StackUnitValue */
     , (37317,  16,          1) /* ItemUseable - No */
     , (37317,  19,      2) /* Value */
     , (37317,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37317,  11, True ) /* IgnoreCollisions */
     , (37317,  13, True ) /* Ethereal */
     , (37317,  14, True ) /* GravityStatus */
     , (37317,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37317,   1, 'Glyph of Magic Defense') /* Name */
     , (37317,  20, 'Glyphs of Magic Defense') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37317,   1,   33554809) /* Setup */
     , (37317,   3,  536870932) /* SoundTable */
     , (37317,   6,   67111919) /* PaletteBase */
     , (37317,   8,  100690191) /* Icon */
     , (37317,  22,  872415275) /* PhysicsEffectTable */
     , (37317,  50,  100686671) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37318;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37318, 'ace37318-glyphofmana', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37318,   1,        128) /* ItemType - Misc */
     , (37318,   5,         25) /* EncumbranceVal */
     , (37318,  11,       1000) /* MaxStackSize */
     , (37318,  12,          1) /* StackSize */
     , (37318,  13,         25) /* StackUnitEncumbrance */
     , (37318,  15,      2) /* StackUnitValue */
     , (37318,  16,          1) /* ItemUseable - No */
     , (37318,  19,      2) /* Value */
     , (37318,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37318,  11, True ) /* IgnoreCollisions */
     , (37318,  13, True ) /* Ethereal */
     , (37318,  14, True ) /* GravityStatus */
     , (37318,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37318,   1, 'Glyph of Mana') /* Name */
     , (37318,  20, 'Glyphs of Mana') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37318,   1,   33554809) /* Setup */
     , (37318,   3,  536870932) /* SoundTable */
     , (37318,   6,   67111919) /* PaletteBase */
     , (37318,   8,  100690191) /* Icon */
     , (37318,  22,  872415275) /* PhysicsEffectTable */
     , (37318,  50,  100690195) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37319;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37319, 'ace37319-glyphofmanaconversion', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37319,   1,        128) /* ItemType - Misc */
     , (37319,   5,         25) /* EncumbranceVal */
     , (37319,  11,       1000) /* MaxStackSize */
     , (37319,  12,          1) /* StackSize */
     , (37319,  13,         25) /* StackUnitEncumbrance */
     , (37319,  15,      2) /* StackUnitValue */
     , (37319,  16,          1) /* ItemUseable - No */
     , (37319,  19,      2) /* Value */
     , (37319,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37319,  11, True ) /* IgnoreCollisions */
     , (37319,  13, True ) /* Ethereal */
     , (37319,  14, True ) /* GravityStatus */
     , (37319,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37319,   1, 'Glyph of Mana Conversion') /* Name */
     , (37319,  20, 'Glyphs of Mana Conversion') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37319,   1,   33554809) /* Setup */
     , (37319,   3,  536870932) /* SoundTable */
     , (37319,   6,   67111919) /* PaletteBase */
     , (37319,   8,  100690191) /* Icon */
     , (37319,  22,  872415275) /* PhysicsEffectTable */
     , (37319,  50,  100686673) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37321;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37321, 'ace37321-glyphofmanaregeneration', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37321,   1,        128) /* ItemType - Misc */
     , (37321,   5,         25) /* EncumbranceVal */
     , (37321,  11,       1000) /* MaxStackSize */
     , (37321,  12,          1) /* StackSize */
     , (37321,  13,         25) /* StackUnitEncumbrance */
     , (37321,  15,      2) /* StackUnitValue */
     , (37321,  16,          1) /* ItemUseable - No */
     , (37321,  19,      2) /* Value */
     , (37321,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37321,  11, True ) /* IgnoreCollisions */
     , (37321,  13, True ) /* Ethereal */
     , (37321,  14, True ) /* GravityStatus */
     , (37321,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37321,   1, 'Glyph of Mana Regeneration') /* Name */
     , (37321,  20, 'Glyphs of Mana Regeneration') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37321,   1,   33554809) /* Setup */
     , (37321,   3,  536870932) /* SoundTable */
     , (37321,   6,   67111919) /* PaletteBase */
     , (37321,   8,  100690191) /* Icon */
     , (37321,  22,  872415275) /* PhysicsEffectTable */
     , (37321,  50,  100686674) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37323;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37323, 'ace37323-glyphofmeleedefense', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37323,   1,        128) /* ItemType - Misc */
     , (37323,   5,         25) /* EncumbranceVal */
     , (37323,  11,       1000) /* MaxStackSize */
     , (37323,  12,          1) /* StackSize */
     , (37323,  13,         25) /* StackUnitEncumbrance */
     , (37323,  15,      10) /* StackUnitValue */
     , (37323,  16,          1) /* ItemUseable - No */
     , (37323,  19,      10) /* Value */
     , (37323,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37323,  11, True ) /* IgnoreCollisions */
     , (37323,  13, True ) /* Ethereal */
     , (37323,  14, True ) /* GravityStatus */
     , (37323,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37323,   1, 'Glyph of Melee Defense') /* Name */
     , (37323,  20, 'Glyphs of Melee Defense') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37323,   1,   33554809) /* Setup */
     , (37323,   3,  536870932) /* SoundTable */
     , (37323,   6,   67111919) /* PaletteBase */
     , (37323,   8,  100690191) /* Icon */
     , (37323,  22,  872415275) /* PhysicsEffectTable */
     , (37323,  50,  100686675) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37324;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37324, 'ace37324-glyphofmissiledefense', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37324,   1,        128) /* ItemType - Misc */
     , (37324,   5,         25) /* EncumbranceVal */
     , (37324,  11,       1000) /* MaxStackSize */
     , (37324,  12,          1) /* StackSize */
     , (37324,  13,         25) /* StackUnitEncumbrance */
     , (37324,  15,      10) /* StackUnitValue */
     , (37324,  16,          1) /* ItemUseable - No */
     , (37324,  19,      10) /* Value */
     , (37324,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37324,  11, True ) /* IgnoreCollisions */
     , (37324,  13, True ) /* Ethereal */
     , (37324,  14, True ) /* GravityStatus */
     , (37324,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37324,   1, 'Glyph of Missile Defense') /* Name */
     , (37324,  20, 'Glyphs of Missile Defense') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37324,   1,   33554809) /* Setup */
     , (37324,   3,  536870932) /* SoundTable */
     , (37324,   6,   67111919) /* PaletteBase */
     , (37324,   8,  100690191) /* Icon */
     , (37324,  22,  872415275) /* PhysicsEffectTable */
     , (37324,  50,  100686676) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37325;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37325, 'ace37325-glyphofmonsterappraisal', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37325,   1,        128) /* ItemType - Misc */
     , (37325,   5,         25) /* EncumbranceVal */
     , (37325,  11,       1000) /* MaxStackSize */
     , (37325,  12,          1) /* StackSize */
     , (37325,  13,         25) /* StackUnitEncumbrance */
     , (37325,  15,      2) /* StackUnitValue */
     , (37325,  16,          1) /* ItemUseable - No */
     , (37325,  19,      2) /* Value */
     , (37325,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37325,  11, True ) /* IgnoreCollisions */
     , (37325,  13, True ) /* Ethereal */
     , (37325,  14, True ) /* GravityStatus */
     , (37325,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37325,   1, 'Glyph of Monster Appraisal') /* Name */
     , (37325,  20, 'Glyphs of Monster Appraisal') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37325,   1,   33554809) /* Setup */
     , (37325,   3,  536870932) /* SoundTable */
     , (37325,   6,   67111919) /* PaletteBase */
     , (37325,   8,  100690191) /* Icon */
     , (37325,  22,  872415275) /* PhysicsEffectTable */
     , (37325,  50,  100686631) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37326;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37326, 'ace37326-glyphofpersonappraisal', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37326,   1,        128) /* ItemType - Misc */
     , (37326,   5,         25) /* EncumbranceVal */
     , (37326,  11,       1000) /* MaxStackSize */
     , (37326,  12,          1) /* StackSize */
     , (37326,  13,         25) /* StackUnitEncumbrance */
     , (37326,  15,      2) /* StackUnitValue */
     , (37326,  16,          1) /* ItemUseable - No */
     , (37326,  19,      2) /* Value */
     , (37326,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37326,  11, True ) /* IgnoreCollisions */
     , (37326,  13, True ) /* Ethereal */
     , (37326,  14, True ) /* GravityStatus */
     , (37326,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37326,   1, 'Glyph of Person Appraisal') /* Name */
     , (37326,  20, 'Glyphs of Person Appraisal') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37326,   1,   33554809) /* Setup */
     , (37326,   3,  536870932) /* SoundTable */
     , (37326,   6,   67111919) /* PaletteBase */
     , (37326,   8,  100690191) /* Icon */
     , (37326,  22,  872415275) /* PhysicsEffectTable */
     , (37326,  50,  100686632) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37327;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37327, 'ace37327-glyphofpiercing', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37327,   1,        128) /* ItemType - Misc */
     , (37327,   5,         25) /* EncumbranceVal */
     , (37327,  11,       1000) /* MaxStackSize */
     , (37327,  12,          1) /* StackSize */
     , (37327,  13,         25) /* StackUnitEncumbrance */
     , (37327,  15,      2) /* StackUnitValue */
     , (37327,  16,          1) /* ItemUseable - No */
     , (37327,  19,      2) /* Value */
     , (37327,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37327,  11, True ) /* IgnoreCollisions */
     , (37327,  13, True ) /* Ethereal */
     , (37327,  14, True ) /* GravityStatus */
     , (37327,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37327,   1, 'Glyph of Piercing') /* Name */
     , (37327,  20, 'Glyphs of Piercing') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37327,   1,   33554809) /* Setup */
     , (37327,   3,  536870932) /* SoundTable */
     , (37327,   6,   67111919) /* PaletteBase */
     , (37327,   8,  100690191) /* Icon */
     , (37327,  22,  872415275) /* PhysicsEffectTable */
     , (37327,  50,  100686677) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37328;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37328, 'ace37328-glyphofquickness', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37328,   1,        128) /* ItemType - Misc */
     , (37328,   5,         25) /* EncumbranceVal */
     , (37328,  11,       1000) /* MaxStackSize */
     , (37328,  12,          1) /* StackSize */
     , (37328,  13,         25) /* StackUnitEncumbrance */
     , (37328,  15,      2) /* StackUnitValue */
     , (37328,  16,          1) /* ItemUseable - No */
     , (37328,  19,      2) /* Value */
     , (37328,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37328,  11, True ) /* IgnoreCollisions */
     , (37328,  13, True ) /* Ethereal */
     , (37328,  14, True ) /* GravityStatus */
     , (37328,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37328,   1, 'Glyph of Quickness') /* Name */
     , (37328,  20, 'Glyphs of Quickness') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37328,   1,   33554809) /* Setup */
     , (37328,   3,  536870932) /* SoundTable */
     , (37328,   6,   67111919) /* PaletteBase */
     , (37328,   8,  100690191) /* Icon */
     , (37328,  22,  872415275) /* PhysicsEffectTable */
     , (37328,  50,  100686680) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37329;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37329, 'ace37329-glyphofrun', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37329,   1,        128) /* ItemType - Misc */
     , (37329,   5,         25) /* EncumbranceVal */
     , (37329,  11,       1000) /* MaxStackSize */
     , (37329,  12,          1) /* StackSize */
     , (37329,  13,         25) /* StackUnitEncumbrance */
     , (37329,  15,      2) /* StackUnitValue */
     , (37329,  16,          1) /* ItemUseable - No */
     , (37329,  19,      2) /* Value */
     , (37329,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37329,  11, True ) /* IgnoreCollisions */
     , (37329,  13, True ) /* Ethereal */
     , (37329,  14, True ) /* GravityStatus */
     , (37329,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37329,   1, 'Glyph of Run') /* Name */
     , (37329,  20, 'Glyphs of Run') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37329,   1,   33554809) /* Setup */
     , (37329,   3,  536870932) /* SoundTable */
     , (37329,   6,   67111919) /* PaletteBase */
     , (37329,   8,  100690191) /* Icon */
     , (37329,  22,  872415275) /* PhysicsEffectTable */
     , (37329,  50,  100686681) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37330;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37330, 'ace37330-glyphofsalvaging', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37330,   1,        128) /* ItemType - Misc */
     , (37330,   5,         25) /* EncumbranceVal */
     , (37330,  11,       1000) /* MaxStackSize */
     , (37330,  12,          1) /* StackSize */
     , (37330,  13,         25) /* StackUnitEncumbrance */
     , (37330,  15,      2) /* StackUnitValue */
     , (37330,  16,          1) /* ItemUseable - No */
     , (37330,  19,      2) /* Value */
     , (37330,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37330,  11, True ) /* IgnoreCollisions */
     , (37330,  13, True ) /* Ethereal */
     , (37330,  14, True ) /* GravityStatus */
     , (37330,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37330,   1, 'Glyph of Salvaging') /* Name */
     , (37330,  20, 'Glyphs of Salvaging') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37330,   1,   33554809) /* Setup */
     , (37330,   3,  536870932) /* SoundTable */
     , (37330,   6,   67111919) /* PaletteBase */
     , (37330,   8,  100690191) /* Icon */
     , (37330,  22,  872415275) /* PhysicsEffectTable */
     , (37330,  50,  100690192) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37331;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37331, 'ace37331-glyphofself', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37331,   1,        128) /* ItemType - Misc */
     , (37331,   5,         25) /* EncumbranceVal */
     , (37331,  11,       1000) /* MaxStackSize */
     , (37331,  12,          1) /* StackSize */
     , (37331,  13,         25) /* StackUnitEncumbrance */
     , (37331,  15,      2) /* StackUnitValue */
     , (37331,  16,          1) /* ItemUseable - No */
     , (37331,  19,      2) /* Value */
     , (37331,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37331,  11, True ) /* IgnoreCollisions */
     , (37331,  13, True ) /* Ethereal */
     , (37331,  14, True ) /* GravityStatus */
     , (37331,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37331,   1, 'Glyph of Self') /* Name */
     , (37331,  20, 'Glyphs of Self') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37331,   1,   33554809) /* Setup */
     , (37331,   3,  536870932) /* SoundTable */
     , (37331,   6,   67111919) /* PaletteBase */
     , (37331,   8,  100690191) /* Icon */
     , (37331,  22,  872415275) /* PhysicsEffectTable */
     , (37331,  50,  100686682) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37332;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37332, 'ace37332-glyphofslashing', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37332,   1,        128) /* ItemType - Misc */
     , (37332,   5,         25) /* EncumbranceVal */
     , (37332,  11,       1000) /* MaxStackSize */
     , (37332,  12,          1) /* StackSize */
     , (37332,  13,         25) /* StackUnitEncumbrance */
     , (37332,  15,      2) /* StackUnitValue */
     , (37332,  16,          1) /* ItemUseable - No */
     , (37332,  19,      2) /* Value */
     , (37332,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37332,  11, True ) /* IgnoreCollisions */
     , (37332,  13, True ) /* Ethereal */
     , (37332,  14, True ) /* GravityStatus */
     , (37332,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37332,   1, 'Glyph of Slashing') /* Name */
     , (37332,  20, 'Glyphs of Slashing') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37332,   1,   33554809) /* Setup */
     , (37332,   3,  536870932) /* SoundTable */
     , (37332,   6,   67111919) /* PaletteBase */
     , (37332,   8,  100690191) /* Icon */
     , (37332,  22,  872415275) /* PhysicsEffectTable */
     , (37332,  50,  100686634) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37333;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37333, 'ace37333-glyphofstamina', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37333,   1,        128) /* ItemType - Misc */
     , (37333,   5,         25) /* EncumbranceVal */
     , (37333,  11,       1000) /* MaxStackSize */
     , (37333,  12,          1) /* StackSize */
     , (37333,  13,         25) /* StackUnitEncumbrance */
     , (37333,  15,      2) /* StackUnitValue */
     , (37333,  16,          1) /* ItemUseable - No */
     , (37333,  19,      2) /* Value */
     , (37333,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37333,  11, True ) /* IgnoreCollisions */
     , (37333,  13, True ) /* Ethereal */
     , (37333,  14, True ) /* GravityStatus */
     , (37333,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37333,   1, 'Glyph of Stamina') /* Name */
     , (37333,  20, 'Glyphs of Stamina') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37333,   1,   33554809) /* Setup */
     , (37333,   3,  536870932) /* SoundTable */
     , (37333,   6,   67111919) /* PaletteBase */
     , (37333,   8,  100690191) /* Icon */
     , (37333,  22,  872415275) /* PhysicsEffectTable */
     , (37333,  50,  100690193) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37336;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37336, 'ace37336-glyphofstaminaregeneration', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37336,   1,        128) /* ItemType - Misc */
     , (37336,   5,         25) /* EncumbranceVal */
     , (37336,  11,       1000) /* MaxStackSize */
     , (37336,  12,          1) /* StackSize */
     , (37336,  13,         25) /* StackUnitEncumbrance */
     , (37336,  15,      2) /* StackUnitValue */
     , (37336,  16,          1) /* ItemUseable - No */
     , (37336,  19,      2) /* Value */
     , (37336,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37336,  11, True ) /* IgnoreCollisions */
     , (37336,  13, True ) /* Ethereal */
     , (37336,  14, True ) /* GravityStatus */
     , (37336,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37336,   1, 'Glyph of Stamina Regeneration') /* Name */
     , (37336,  20, 'Glyphs of Stamina Regeneration') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37336,   1,   33554809) /* Setup */
     , (37336,   3,  536870932) /* SoundTable */
     , (37336,   6,   67111919) /* PaletteBase */
     , (37336,   8,  100690191) /* Icon */
     , (37336,  22,  872415275) /* PhysicsEffectTable */
     , (37336,  50,  100686687) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37337;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37337, 'ace37337-glyphofstrength', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37337,   1,        128) /* ItemType - Misc */
     , (37337,   5,         25) /* EncumbranceVal */
     , (37337,  11,       1000) /* MaxStackSize */
     , (37337,  12,          1) /* StackSize */
     , (37337,  13,         25) /* StackUnitEncumbrance */
     , (37337,  15,      2) /* StackUnitValue */
     , (37337,  16,          1) /* ItemUseable - No */
     , (37337,  19,      2) /* Value */
     , (37337,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37337,  11, True ) /* IgnoreCollisions */
     , (37337,  13, True ) /* Ethereal */
     , (37337,  14, True ) /* GravityStatus */
     , (37337,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37337,   1, 'Glyph of Strength') /* Name */
     , (37337,  20, 'Glyphs of Strength') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37337,   1,   33554809) /* Setup */
     , (37337,   3,  536870932) /* SoundTable */
     , (37337,   6,   67111919) /* PaletteBase */
     , (37337,   8,  100690191) /* Icon */
     , (37337,  22,  872415275) /* PhysicsEffectTable */
     , (37337,  50,  100686688) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37338;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37338, 'ace37338-glyphofmissileweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37338,   1,        128) /* ItemType - Misc */
     , (37338,   5,         25) /* EncumbranceVal */
     , (37338,  11,       1000) /* MaxStackSize */
     , (37338,  12,          1) /* StackSize */
     , (37338,  13,         25) /* StackUnitEncumbrance */
     , (37338,  15,      2) /* StackUnitValue */
     , (37338,  16,          1) /* ItemUseable - No */
     , (37338,  19,      2) /* Value */
     , (37338,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37338,  11, True ) /* IgnoreCollisions */
     , (37338,  13, True ) /* Ethereal */
     , (37338,  14, True ) /* GravityStatus */
     , (37338,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37338,   1, 'Glyph of Missile Weapons') /* Name */
     , (37338,  20, 'Glyphs of Missile Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37338,   1,   33554809) /* Setup */
     , (37338,   3,  536870932) /* SoundTable */
     , (37338,   6,   67111919) /* PaletteBase */
     , (37338,   8,  100690191) /* Icon */
     , (37338,  22,  872415275) /* PhysicsEffectTable */
     , (37338,  50,  100686638) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37339;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37339, 'ace37339-glyphoflightweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37339,   1,        128) /* ItemType - Misc */
     , (37339,   5,         25) /* EncumbranceVal */
     , (37339,  11,       1000) /* MaxStackSize */
     , (37339,  12,          1) /* StackSize */
     , (37339,  13,         25) /* StackUnitEncumbrance */
     , (37339,  15,      2) /* StackUnitValue */
     , (37339,  16,          1) /* ItemUseable - No */
     , (37339,  19,      2) /* Value */
     , (37339,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37339,  11, True ) /* IgnoreCollisions */
     , (37339,  13, True ) /* Ethereal */
     , (37339,  14, True ) /* GravityStatus */
     , (37339,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37339,   1, 'Glyph of Light Weapons') /* Name */
     , (37339,  20, 'Glyphs of Light Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37339,   1,   33554809) /* Setup */
     , (37339,   3,  536870932) /* SoundTable */
     , (37339,   6,   67111919) /* PaletteBase */
     , (37339,   8,  100690191) /* Icon */
     , (37339,  22,  872415275) /* PhysicsEffectTable */
     , (37339,  50,  100692242) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37340;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37340, 'ace37340-glyphofwarmagic', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37340,   1,        128) /* ItemType - Misc */
     , (37340,   5,         25) /* EncumbranceVal */
     , (37340,  11,       1000) /* MaxStackSize */
     , (37340,  12,          1) /* StackSize */
     , (37340,  13,         25) /* StackUnitEncumbrance */
     , (37340,  15,      2) /* StackUnitValue */
     , (37340,  16,          1) /* ItemUseable - No */
     , (37340,  19,      2) /* Value */
     , (37340,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37340,  11, True ) /* IgnoreCollisions */
     , (37340,  13, True ) /* Ethereal */
     , (37340,  14, True ) /* GravityStatus */
     , (37340,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37340,   1, 'Glyph of War Magic') /* Name */
     , (37340,  20, 'Glyphs of War Magic') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37340,   1,   33554809) /* Setup */
     , (37340,   3,  536870932) /* SoundTable */
     , (37340,   6,   67111919) /* PaletteBase */
     , (37340,   8,  100690191) /* Icon */
     , (37340,  22,  872415275) /* PhysicsEffectTable */
     , (37340,  50,  100686693) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37341;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37341, 'ace37341-glyphofweapontinkering', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37341,   1,        128) /* ItemType - Misc */
     , (37341,   5,         25) /* EncumbranceVal */
     , (37341,  11,       1000) /* MaxStackSize */
     , (37341,  12,          1) /* StackSize */
     , (37341,  13,         25) /* StackUnitEncumbrance */
     , (37341,  15,      2) /* StackUnitValue */
     , (37341,  16,          1) /* ItemUseable - No */
     , (37341,  19,      2) /* Value */
     , (37341,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37341,  11, True ) /* IgnoreCollisions */
     , (37341,  13, True ) /* Ethereal */
     , (37341,  14, True ) /* GravityStatus */
     , (37341,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37341,   1, 'Glyph of Weapon Tinkering') /* Name */
     , (37341,  20, 'Glyphs of Weapon Tinkering') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37341,   1,   33554809) /* Setup */
     , (37341,   3,  536870932) /* SoundTable */
     , (37341,   6,   67111919) /* PaletteBase */
     , (37341,   8,  100690191) /* Icon */
     , (37341,  22,  872415275) /* PhysicsEffectTable */
     , (37341,  50,  100686694) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37342;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37342, 'ace37342-glyphofcorrosion', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37342,   1,        128) /* ItemType - Misc */
     , (37342,   5,         25) /* EncumbranceVal */
     , (37342,  11,       1000) /* MaxStackSize */
     , (37342,  12,          1) /* StackSize */
     , (37342,  13,         25) /* StackUnitEncumbrance */
     , (37342,  15,      2) /* StackUnitValue */
     , (37342,  16,          1) /* ItemUseable - No */
     , (37342,  19,      2) /* Value */
     , (37342,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37342,  11, True ) /* IgnoreCollisions */
     , (37342,  13, True ) /* Ethereal */
     , (37342,  14, True ) /* GravityStatus */
     , (37342,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37342,   1, 'Glyph of Corrosion') /* Name */
     , (37342,  20, 'Glyphs of Corrosion') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37342,   1,   33554809) /* Setup */
     , (37342,   3,  536870932) /* SoundTable */
     , (37342,   6,   67111919) /* PaletteBase */
     , (37342,   8,  100690191) /* Icon */
     , (37342,  22,  872415275) /* PhysicsEffectTable */
     , (37342,  50,  100686623) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37343;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37343, 'ace37343-glyphofalchemy', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37343,   1,        128) /* ItemType - Misc */
     , (37343,   5,         25) /* EncumbranceVal */
     , (37343,  11,       1000) /* MaxStackSize */
     , (37343,  12,          1) /* StackSize */
     , (37343,  13,         25) /* StackUnitEncumbrance */
     , (37343,  15,      2) /* StackUnitValue */
     , (37343,  16,          1) /* ItemUseable - No */
     , (37343,  19,      2) /* Value */
     , (37343,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37343,  11, True ) /* IgnoreCollisions */
     , (37343,  13, True ) /* Ethereal */
     , (37343,  14, True ) /* GravityStatus */
     , (37343,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37343,   1, 'Glyph of Alchemy') /* Name */
     , (37343,  20, 'Glyphs of Alchemy') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37343,   1,   33554809) /* Setup */
     , (37343,   3,  536870932) /* SoundTable */
     , (37343,   6,   67111919) /* PaletteBase */
     , (37343,   8,  100690191) /* Icon */
     , (37343,  22,  872415275) /* PhysicsEffectTable */
     , (37343,  50,  100686627) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37344;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37344, 'ace37344-glyphofarcanelore', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37344,   1,        128) /* ItemType - Misc */
     , (37344,   5,         25) /* EncumbranceVal */
     , (37344,  11,       1000) /* MaxStackSize */
     , (37344,  12,          1) /* StackSize */
     , (37344,  13,         25) /* StackUnitEncumbrance */
     , (37344,  15,      2) /* StackUnitValue */
     , (37344,  16,          1) /* ItemUseable - No */
     , (37344,  19,      2) /* Value */
     , (37344,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37344,  11, True ) /* IgnoreCollisions */
     , (37344,  13, True ) /* Ethereal */
     , (37344,  14, True ) /* GravityStatus */
     , (37344,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37344,   1, 'Glyph of Arcane Lore') /* Name */
     , (37344,  20, 'Glyphs of Arcane Lore') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37344,   1,   33554809) /* Setup */
     , (37344,   3,  536870932) /* SoundTable */
     , (37344,   6,   67111919) /* PaletteBase */
     , (37344,   8,  100690191) /* Icon */
     , (37344,  22,  872415275) /* PhysicsEffectTable */
     , (37344,  50,  100686628) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37345;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37345, 'ace37345-glyphofarmor', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37345,   1,        128) /* ItemType - Misc */
     , (37345,   5,         25) /* EncumbranceVal */
     , (37345,  11,       1000) /* MaxStackSize */
     , (37345,  12,          1) /* StackSize */
     , (37345,  13,         25) /* StackUnitEncumbrance */
     , (37345,  15,      2) /* StackUnitValue */
     , (37345,  16,          1) /* ItemUseable - No */
     , (37345,  19,      2) /* Value */
     , (37345,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37345,  11, True ) /* IgnoreCollisions */
     , (37345,  13, True ) /* Ethereal */
     , (37345,  14, True ) /* GravityStatus */
     , (37345,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37345,   1, 'Glyph of Armor') /* Name */
     , (37345,  20, 'Glyphs of Armor') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37345,   1,   33554809) /* Setup */
     , (37345,   3,  536870932) /* SoundTable */
     , (37345,   6,   67111919) /* PaletteBase */
     , (37345,   8,  100690191) /* Icon */
     , (37345,  22,  872415275) /* PhysicsEffectTable */
     , (37345,  50,  100686629) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37346;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37346, 'ace37346-glyphofarmortinkering', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37346,   1,        128) /* ItemType - Misc */
     , (37346,   5,         25) /* EncumbranceVal */
     , (37346,  11,       1000) /* MaxStackSize */
     , (37346,  12,          1) /* StackSize */
     , (37346,  13,         25) /* StackUnitEncumbrance */
     , (37346,  15,      2) /* StackUnitValue */
     , (37346,  16,          1) /* ItemUseable - No */
     , (37346,  19,      2) /* Value */
     , (37346,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37346,  11, True ) /* IgnoreCollisions */
     , (37346,  13, True ) /* Ethereal */
     , (37346,  14, True ) /* GravityStatus */
     , (37346,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37346,   1, 'Glyph of Armor Tinkering') /* Name */
     , (37346,  20, 'Glyphs of Armor Tinkering') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37346,   1,   33554809) /* Setup */
     , (37346,   3,  536870932) /* SoundTable */
     , (37346,   6,   67111919) /* PaletteBase */
     , (37346,   8,  100690191) /* Icon */
     , (37346,  22,  872415275) /* PhysicsEffectTable */
     , (37346,  50,  100686630) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37347;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37347, 'ace37347-glyphofbludgeoning', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37347,   1,        128) /* ItemType - Misc */
     , (37347,   5,         25) /* EncumbranceVal */
     , (37347,  11,       1000) /* MaxStackSize */
     , (37347,  12,          1) /* StackSize */
     , (37347,  13,         25) /* StackUnitEncumbrance */
     , (37347,  15,      2) /* StackUnitValue */
     , (37347,  16,          1) /* ItemUseable - No */
     , (37347,  19,      2) /* Value */
     , (37347,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37347,  11, True ) /* IgnoreCollisions */
     , (37347,  13, True ) /* Ethereal */
     , (37347,  14, True ) /* GravityStatus */
     , (37347,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37347,   1, 'Glyph of Bludgeoning') /* Name */
     , (37347,  20, 'Glyphs of Bludgeoning') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37347,   1,   33554809) /* Setup */
     , (37347,   3,  536870932) /* SoundTable */
     , (37347,   6,   67111919) /* PaletteBase */
     , (37347,   8,  100690191) /* Icon */
     , (37347,  22,  872415275) /* PhysicsEffectTable */
     , (37347,  50,  100686636) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37348;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37348, 'ace37348-glyphoffrost', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37348,   1,        128) /* ItemType - Misc */
     , (37348,   5,         25) /* EncumbranceVal */
     , (37348,  11,       1000) /* MaxStackSize */
     , (37348,  12,          1) /* StackSize */
     , (37348,  13,         25) /* StackUnitEncumbrance */
     , (37348,  15,      2) /* StackUnitValue */
     , (37348,  16,          1) /* ItemUseable - No */
     , (37348,  19,      2) /* Value */
     , (37348,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37348,  11, True ) /* IgnoreCollisions */
     , (37348,  13, True ) /* Ethereal */
     , (37348,  14, True ) /* GravityStatus */
     , (37348,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37348,   1, 'Glyph of Frost') /* Name */
     , (37348,  20, 'Glyphs of Frost') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37348,   1,   33554809) /* Setup */
     , (37348,   3,  536870932) /* SoundTable */
     , (37348,   6,   67111919) /* PaletteBase */
     , (37348,   8,  100690191) /* Icon */
     , (37348,  22,  872415275) /* PhysicsEffectTable */
     , (37348,  50,  100686653) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37349;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37349, 'ace37349-glyphofcooking', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37349,   1,        128) /* ItemType - Misc */
     , (37349,   5,         25) /* EncumbranceVal */
     , (37349,  11,       1000) /* MaxStackSize */
     , (37349,  12,          1) /* StackSize */
     , (37349,  13,         25) /* StackUnitEncumbrance */
     , (37349,  15,      2) /* StackUnitValue */
     , (37349,  16,          1) /* ItemUseable - No */
     , (37349,  19,      2) /* Value */
     , (37349,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37349,  11, True ) /* IgnoreCollisions */
     , (37349,  13, True ) /* Ethereal */
     , (37349,  14, True ) /* GravityStatus */
     , (37349,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37349,   1, 'Glyph of Cooking') /* Name */
     , (37349,  20, 'Glyphs of Cooking') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37349,   1,   33554809) /* Setup */
     , (37349,   3,  536870932) /* SoundTable */
     , (37349,   6,   67111919) /* PaletteBase */
     , (37349,   8,  100690191) /* Icon */
     , (37349,  22,  872415275) /* PhysicsEffectTable */
     , (37349,  50,  100686639) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37350;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37350, 'ace37350-glyphofcoordination', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37350,   1,        128) /* ItemType - Misc */
     , (37350,   5,         25) /* EncumbranceVal */
     , (37350,  11,       1000) /* MaxStackSize */
     , (37350,  12,          1) /* StackSize */
     , (37350,  13,         25) /* StackUnitEncumbrance */
     , (37350,  15,      2) /* StackUnitValue */
     , (37350,  16,          1) /* ItemUseable - No */
     , (37350,  19,      2) /* Value */
     , (37350,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37350,  11, True ) /* IgnoreCollisions */
     , (37350,  13, True ) /* Ethereal */
     , (37350,  14, True ) /* GravityStatus */
     , (37350,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37350,   1, 'Glyph of Coordination') /* Name */
     , (37350,  20, 'Glyphs of Coordination') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37350,   1,   33554809) /* Setup */
     , (37350,   3,  536870932) /* SoundTable */
     , (37350,   6,   67111919) /* PaletteBase */
     , (37350,   8,  100690191) /* Icon */
     , (37350,  22,  872415275) /* PhysicsEffectTable */
     , (37350,  50,  100686641) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37351;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37351, 'ace37351-glyphofcreatureenchantment', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37351,   1,        128) /* ItemType - Misc */
     , (37351,   5,         25) /* EncumbranceVal */
     , (37351,  11,       1000) /* MaxStackSize */
     , (37351,  12,          1) /* StackSize */
     , (37351,  13,         25) /* StackUnitEncumbrance */
     , (37351,  15,      2) /* StackUnitValue */
     , (37351,  16,          1) /* ItemUseable - No */
     , (37351,  19,      2) /* Value */
     , (37351,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37351,  11, True ) /* IgnoreCollisions */
     , (37351,  13, True ) /* Ethereal */
     , (37351,  14, True ) /* GravityStatus */
     , (37351,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37351,   1, 'Glyph of Creature Enchantment') /* Name */
     , (37351,  20, 'Glyphs of Creature Enchantment') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37351,   1,   33554809) /* Setup */
     , (37351,   3,  536870932) /* SoundTable */
     , (37351,   6,   67111919) /* PaletteBase */
     , (37351,   8,  100690191) /* Icon */
     , (37351,  22,  872415275) /* PhysicsEffectTable */
     , (37351,  50,  100686642) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37352;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37352, 'ace37352-glyphofdeception', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37352,   1,        128) /* ItemType - Misc */
     , (37352,   5,         25) /* EncumbranceVal */
     , (37352,  11,       1000) /* MaxStackSize */
     , (37352,  12,          1) /* StackSize */
     , (37352,  13,         25) /* StackUnitEncumbrance */
     , (37352,  15,      2) /* StackUnitValue */
     , (37352,  16,          1) /* ItemUseable - No */
     , (37352,  19,      2) /* Value */
     , (37352,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37352,  11, True ) /* IgnoreCollisions */
     , (37352,  13, True ) /* Ethereal */
     , (37352,  14, True ) /* GravityStatus */
     , (37352,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37352,   1, 'Glyph of Deception') /* Name */
     , (37352,  20, 'Glyphs of Deception') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37352,   1,   33554809) /* Setup */
     , (37352,   3,  536870932) /* SoundTable */
     , (37352,   6,   67111919) /* PaletteBase */
     , (37352,   8,  100690191) /* Icon */
     , (37352,  22,  872415275) /* PhysicsEffectTable */
     , (37352,  50,  100686645) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37353;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37353, 'ace37353-inkofformation', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37353,   1,        128) /* ItemType - Misc */
     , (37353,   5,         30) /* EncumbranceVal */
     , (37353,  11,       1000) /* MaxStackSize */
     , (37353,  12,          1) /* StackSize */
     , (37353,  13,         30) /* StackUnitEncumbrance */
     , (37353,  15,      3) /* StackUnitValue */
     , (37353,  16,          1) /* ItemUseable - No */
     , (37353,  19,      3) /* Value */
     , (37353,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37353,  11, True ) /* IgnoreCollisions */
     , (37353,  13, True ) /* Ethereal */
     , (37353,  14, True ) /* GravityStatus */
     , (37353,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37353,   1, 'Ink of Formation') /* Name */
     , (37353,  20, 'Inks of Formation') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37353,   1,   33554602) /* Setup */
     , (37353,   3,  536870932) /* SoundTable */
     , (37353,   8,  100690183) /* Icon */
     , (37353,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37354;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37354, 'ace37354-inkofnullification', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37354,   1,        128) /* ItemType - Misc */
     , (37354,   5,         30) /* EncumbranceVal */
     , (37354,  11,       1000) /* MaxStackSize */
     , (37354,  12,          1) /* StackSize */
     , (37354,  13,         30) /* StackUnitEncumbrance */
     , (37354,  15,      3) /* StackUnitValue */
     , (37354,  16,          1) /* ItemUseable - No */
     , (37354,  19,      3) /* Value */
     , (37354,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37354,  11, True ) /* IgnoreCollisions */
     , (37354,  13, True ) /* Ethereal */
     , (37354,  14, True ) /* GravityStatus */
     , (37354,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37354,   1, 'Ink of Nullification') /* Name */
     , (37354,  20, 'Inks of Nullification') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37354,   1,   33554602) /* Setup */
     , (37354,   3,  536870932) /* SoundTable */
     , (37354,   8,  100690182) /* Icon */
     , (37354,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37355;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37355, 'ace37355-inkofobjectification', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37355,   1,        128) /* ItemType - Misc */
     , (37355,   5,         30) /* EncumbranceVal */
     , (37355,  11,       1000) /* MaxStackSize */
     , (37355,  12,          1) /* StackSize */
     , (37355,  13,         30) /* StackUnitEncumbrance */
     , (37355,  15,      3) /* StackUnitValue */
     , (37355,  16,          1) /* ItemUseable - No */
     , (37355,  19,      3) /* Value */
     , (37355,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37355,  11, True ) /* IgnoreCollisions */
     , (37355,  13, True ) /* Ethereal */
     , (37355,  14, True ) /* GravityStatus */
     , (37355,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37355,   1, 'Ink of Objectification') /* Name */
     , (37355,  20, 'Inks of Objectification') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37355,   1,   33554602) /* Setup */
     , (37355,   3,  536870932) /* SoundTable */
     , (37355,   8,  100690188) /* Icon */
     , (37355,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37356;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37356, 'ace37356-parabolicink', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37356,   1,        128) /* ItemType - Misc */
     , (37356,   5,         30) /* EncumbranceVal */
     , (37356,  11,       1000) /* MaxStackSize */
     , (37356,  12,          1) /* StackSize */
     , (37356,  13,         30) /* StackUnitEncumbrance */
     , (37356,  15,      3) /* StackUnitValue */
     , (37356,  16,          1) /* ItemUseable - No */
     , (37356,  19,      3) /* Value */
     , (37356,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37356,  11, True ) /* IgnoreCollisions */
     , (37356,  13, True ) /* Ethereal */
     , (37356,  14, True ) /* GravityStatus */
     , (37356,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37356,   1, 'Parabolic Ink') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37356,   1,   33554602) /* Setup */
     , (37356,   3,  536870932) /* SoundTable */
     , (37356,   8,  100690184) /* Icon */
     , (37356,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37357;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37357, 'ace37357-inkofpartition', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37357,   1,        128) /* ItemType - Misc */
     , (37357,   5,         30) /* EncumbranceVal */
     , (37357,  11,       1000) /* MaxStackSize */
     , (37357,  12,          1) /* StackSize */
     , (37357,  13,         30) /* StackUnitEncumbrance */
     , (37357,  15,      3) /* StackUnitValue */
     , (37357,  16,          1) /* ItemUseable - No */
     , (37357,  19,      3) /* Value */
     , (37357,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37357,  11, True ) /* IgnoreCollisions */
     , (37357,  13, True ) /* Ethereal */
     , (37357,  14, True ) /* GravityStatus */
     , (37357,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37357,   1, 'Ink of Partition') /* Name */
     , (37357,  20, 'Inks of Partition') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37357,   1,   33554602) /* Setup */
     , (37357,   3,  536870932) /* SoundTable */
     , (37357,   8,  100690189) /* Icon */
     , (37357,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37358;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37358, 'ace37358-inkofseparation', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37358,   1,        128) /* ItemType - Misc */
     , (37358,   5,         30) /* EncumbranceVal */
     , (37358,  11,       1000) /* MaxStackSize */
     , (37358,  12,          1) /* StackSize */
     , (37358,  13,         30) /* StackUnitEncumbrance */
     , (37358,  15,      3) /* StackUnitValue */
     , (37358,  16,          1) /* ItemUseable - No */
     , (37358,  19,      3) /* Value */
     , (37358,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37358,  11, True ) /* IgnoreCollisions */
     , (37358,  13, True ) /* Ethereal */
     , (37358,  14, True ) /* GravityStatus */
     , (37358,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37358,   1, 'Ink of Separation') /* Name */
     , (37358,  20, 'Inks of Separation') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37358,   1,   33554602) /* Setup */
     , (37358,   3,  536870932) /* SoundTable */
     , (37358,   8,  100690190) /* Icon */
     , (37358,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37359;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37359, 'ace37359-alacritousink', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37359,   1,        128) /* ItemType - Misc */
     , (37359,   5,         30) /* EncumbranceVal */
     , (37359,  11,       1000) /* MaxStackSize */
     , (37359,  12,          1) /* StackSize */
     , (37359,  13,         30) /* StackUnitEncumbrance */
     , (37359,  15,      3) /* StackUnitValue */
     , (37359,  16,          1) /* ItemUseable - No */
     , (37359,  19,      3) /* Value */
     , (37359,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37359,  11, True ) /* IgnoreCollisions */
     , (37359,  13, True ) /* Ethereal */
     , (37359,  14, True ) /* GravityStatus */
     , (37359,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37359,   1, 'Alacritous Ink') /* Name */
     , (37359,  20, 'Alacritous Inks') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37359,   1,   33554602) /* Setup */
     , (37359,   3,  536870932) /* SoundTable */
     , (37359,   8,  100690185) /* Icon */
     , (37359,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37360;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37360, 'ace37360-inkofconveyance', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37360,   1,        128) /* ItemType - Misc */
     , (37360,   5,         30) /* EncumbranceVal */
     , (37360,  11,       1000) /* MaxStackSize */
     , (37360,  12,          1) /* StackSize */
     , (37360,  13,         30) /* StackUnitEncumbrance */
     , (37360,  15,      3) /* StackUnitValue */
     , (37360,  16,          1) /* ItemUseable - No */
     , (37360,  19,      3) /* Value */
     , (37360,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37360,  11, True ) /* IgnoreCollisions */
     , (37360,  13, True ) /* Ethereal */
     , (37360,  14, True ) /* GravityStatus */
     , (37360,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37360,   1, 'Ink of Conveyance') /* Name */
     , (37360,  20, 'Inks of Conveyance') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37360,   1,   33554602) /* Setup */
     , (37360,   3,  536870932) /* SoundTable */
     , (37360,   8,  100690186) /* Icon */
     , (37360,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37361;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37361, 'ace37361-inkofdirection', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37361,   1,        128) /* ItemType - Misc */
     , (37361,   5,         30) /* EncumbranceVal */
     , (37361,  11,       1000) /* MaxStackSize */
     , (37361,  12,          1) /* StackSize */
     , (37361,  13,         30) /* StackUnitEncumbrance */
     , (37361,  15,      3) /* StackUnitValue */
     , (37361,  16,          1) /* ItemUseable - No */
     , (37361,  19,      3) /* Value */
     , (37361,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37361,  11, True ) /* IgnoreCollisions */
     , (37361,  13, True ) /* Ethereal */
     , (37361,  14, True ) /* GravityStatus */
     , (37361,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37361,   1, 'Ink of Direction') /* Name */
     , (37361,  20, 'Inks of Direction') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37361,   1,   33554602) /* Setup */
     , (37361,   3,  536870932) /* SoundTable */
     , (37361,   8,  100690187) /* Icon */
     , (37361,  22,  872415275) /* PhysicsEffectTable */;
DELETE FROM `weenie` WHERE `class_Id` = 37362;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37362, 'ace37362-quillofextraction', 44, '2019-04-25 00:00:00') /* CraftTool */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37362,   1,   67108864) /* ItemType - CraftAlchemyIntermediate */
     , (37362,   5,          4) /* EncumbranceVal */
     , (37362,  11,       1000) /* MaxStackSize */
     , (37362,  12,          1) /* StackSize */
     , (37362,  13,          4) /* StackUnitEncumbrance */
     , (37362,  15,      5) /* StackUnitValue */
     , (37362,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (37362,  19,      5) /* Value */
     , (37362,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (37362,  94,    4201088) /* TargetType - LockableMagicTarget, Gem, SpellComponents, CraftCookingBase */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37362,  11, True ) /* IgnoreCollisions */
     , (37362,  13, True ) /* Ethereal */
     , (37362,  14, True ) /* GravityStatus */
     , (37362,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37362,   1, 'Quill of Extraction') /* Name */
     , (37362,  20, 'Quills of Extraction') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37362,   1,   33559616) /* Setup */
     , (37362,   8,  100690199) /* Icon */;
DELETE FROM `weenie` WHERE `class_Id` = 37363;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37363, 'ace37363-quillofinfliction', 44, '2019-04-25 00:00:00') /* CraftTool */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37363,   1,   67108864) /* ItemType - CraftAlchemyIntermediate */
     , (37363,   5,          4) /* EncumbranceVal */
     , (37363,  11,       1000) /* MaxStackSize */
     , (37363,  12,          1) /* StackSize */
     , (37363,  13,          4) /* StackUnitEncumbrance */
     , (37363,  15,      5) /* StackUnitValue */
     , (37363,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (37363,  19,      5) /* Value */
     , (37363,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (37363,  94,    4201088) /* TargetType - LockableMagicTarget, Gem, SpellComponents, CraftCookingBase */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37363,  11, True ) /* IgnoreCollisions */
     , (37363,  13, True ) /* Ethereal */
     , (37363,  14, True ) /* GravityStatus */
     , (37363,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37363,   1, 'Quill of Infliction') /* Name */
     , (37363,  20, 'Quills of Infliction') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37363,   1,   33559616) /* Setup */
     , (37363,   8,  100690196) /* Icon */;
DELETE FROM `weenie` WHERE `class_Id` = 37364;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37364, 'ace37364-quillofintrospection', 44, '2019-04-25 00:00:00') /* CraftTool */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37364,   1,   67108864) /* ItemType - CraftAlchemyIntermediate */
     , (37364,   5,          4) /* EncumbranceVal */
     , (37364,  11,       1000) /* MaxStackSize */
     , (37364,  12,          1) /* StackSize */
     , (37364,  13,          4) /* StackUnitEncumbrance */
     , (37364,  15,      5) /* StackUnitValue */
     , (37364,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (37364,  19,      5) /* Value */
     , (37364,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (37364,  94,    4201088) /* TargetType - LockableMagicTarget, Gem, SpellComponents, CraftCookingBase */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37364,  11, True ) /* IgnoreCollisions */
     , (37364,  13, True ) /* Ethereal */
     , (37364,  14, True ) /* GravityStatus */
     , (37364,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37364,   1, 'Quill of Introspection') /* Name */
     , (37364,  20, 'Quills of Introspection') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37364,   1,   33559616) /* Setup */
     , (37364,   8,  100690197) /* Icon */;
DELETE FROM `weenie` WHERE `class_Id` = 37365;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37365, 'ace37365-quillofbenevolence', 44, '2019-04-25 00:00:00') /* CraftTool */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37365,   1,   67108864) /* ItemType - CraftAlchemyIntermediate */
     , (37365,   5,          4) /* EncumbranceVal */
     , (37365,  11,       1000) /* MaxStackSize */
     , (37365,  12,          1) /* StackSize */
     , (37365,  13,          4) /* StackUnitEncumbrance */
     , (37365,  15,      5) /* StackUnitValue */
     , (37365,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (37365,  19,      5) /* Value */
     , (37365,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (37365,  94,       4224) /* TargetType - Misc, SpellComponents */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37365,  11, True ) /* IgnoreCollisions */
     , (37365,  13, True ) /* Ethereal */
     , (37365,  14, True ) /* GravityStatus */
     , (37365,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37365,   1, 'Quill of Benevolence') /* Name */
     , (37365,  20, 'Quills of Benevolence') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37365,   1,   33559616) /* Setup */
     , (37365,   8,  100690198) /* Icon */;
DELETE FROM `weenie` WHERE `class_Id` = 37366;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37366, 'ace37366-glyphoflightweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37366,   1,        128) /* ItemType - Misc */
     , (37366,   5,         25) /* EncumbranceVal */
     , (37366,  11,       1000) /* MaxStackSize */
     , (37366,  12,          1) /* StackSize */
     , (37366,  13,         25) /* StackUnitEncumbrance */
     , (37366,  15,      2) /* StackUnitValue */
     , (37366,  16,          1) /* ItemUseable - No */
     , (37366,  19,      2) /* Value */
     , (37366,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37366,  11, True ) /* IgnoreCollisions */
     , (37366,  13, True ) /* Ethereal */
     , (37366,  14, True ) /* GravityStatus */
     , (37366,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37366,   1, 'Glyph of Light Weapons') /* Name */
     , (37366,  20, 'Glyphs of Light Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37366,   1,   33554809) /* Setup */
     , (37366,   3,  536870932) /* SoundTable */
     , (37366,   6,   67111919) /* PaletteBase */
     , (37366,   8,  100690191) /* Icon */
     , (37366,  22,  872415275) /* PhysicsEffectTable */
     , (37366,  50,  100692242) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37367;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37367, 'ace37367-glyphoflightweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37367,   1,        128) /* ItemType - Misc */
     , (37367,   5,         25) /* EncumbranceVal */
     , (37367,  11,       1000) /* MaxStackSize */
     , (37367,  12,          1) /* StackSize */
     , (37367,  13,         25) /* StackUnitEncumbrance */
     , (37367,  15,      2) /* StackUnitValue */
     , (37367,  16,          1) /* ItemUseable - No */
     , (37367,  19,      2) /* Value */
     , (37367,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37367,  11, True ) /* IgnoreCollisions */
     , (37367,  13, True ) /* Ethereal */
     , (37367,  14, True ) /* GravityStatus */
     , (37367,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37367,   1, 'Glyph of Light Weapons') /* Name */
     , (37367,  20, 'Glyphs of Light Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37367,   1,   33554809) /* Setup */
     , (37367,   3,  536870932) /* SoundTable */
     , (37367,   6,   67111919) /* PaletteBase */
     , (37367,   8,  100690191) /* Icon */
     , (37367,  22,  872415275) /* PhysicsEffectTable */
     , (37367,  50,  100692242) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37368;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37368, 'ace37368-glyphoflightweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37368,   1,        128) /* ItemType - Misc */
     , (37368,   5,         25) /* EncumbranceVal */
     , (37368,  11,       1000) /* MaxStackSize */
     , (37368,  12,          1) /* StackSize */
     , (37368,  13,         25) /* StackUnitEncumbrance */
     , (37368,  15,      2) /* StackUnitValue */
     , (37368,  16,          1) /* ItemUseable - No */
     , (37368,  19,      2) /* Value */
     , (37368,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37368,  11, True ) /* IgnoreCollisions */
     , (37368,  13, True ) /* Ethereal */
     , (37368,  14, True ) /* GravityStatus */
     , (37368,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37368,   1, 'Glyph of Light Weapons') /* Name */
     , (37368,  20, 'Glyphs of Light Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37368,   1,   33554809) /* Setup */
     , (37368,   3,  536870932) /* SoundTable */
     , (37368,   6,   67111919) /* PaletteBase */
     , (37368,   8,  100690191) /* Icon */
     , (37368,  22,  872415275) /* PhysicsEffectTable */
     , (37368,  50,  100692242) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37369;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37369, 'ace37369-glyphofheavyweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37369,   1,        128) /* ItemType - Misc */
     , (37369,   5,         25) /* EncumbranceVal */
     , (37369,  11,       1000) /* MaxStackSize */
     , (37369,  12,          1) /* StackSize */
     , (37369,  13,         25) /* StackUnitEncumbrance */
     , (37369,  15,      2) /* StackUnitValue */
     , (37369,  16,          1) /* ItemUseable - No */
     , (37369,  19,      2) /* Value */
     , (37369,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37369,  11, True ) /* IgnoreCollisions */
     , (37369,  13, True ) /* Ethereal */
     , (37369,  14, True ) /* GravityStatus */
     , (37369,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37369,   1, 'Glyph of Heavy Weapons') /* Name */
     , (37369,  20, 'Glyphs of Heavy Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37369,   1,   33554809) /* Setup */
     , (37369,   3,  536870932) /* SoundTable */
     , (37369,   6,   67111919) /* PaletteBase */
     , (37369,   8,  100690191) /* Icon */
     , (37369,  22,  872415275) /* PhysicsEffectTable */
     , (37369,  50,  100692248) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37370;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37370, 'ace37370-glyphoflightweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37370,   1,        128) /* ItemType - Misc */
     , (37370,   5,         25) /* EncumbranceVal */
     , (37370,  11,       1000) /* MaxStackSize */
     , (37370,  12,          1) /* StackSize */
     , (37370,  13,         25) /* StackUnitEncumbrance */
     , (37370,  15,      2) /* StackUnitValue */
     , (37370,  16,          1) /* ItemUseable - No */
     , (37370,  19,      2) /* Value */
     , (37370,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37370,  11, True ) /* IgnoreCollisions */
     , (37370,  13, True ) /* Ethereal */
     , (37370,  14, True ) /* GravityStatus */
     , (37370,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37370,   1, 'Glyph of Light Weapons') /* Name */
     , (37370,  20, 'Glyphs of Light Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37370,   1,   33554809) /* Setup */
     , (37370,   3,  536870932) /* SoundTable */
     , (37370,   6,   67111919) /* PaletteBase */
     , (37370,   8,  100690191) /* Icon */
     , (37370,  22,  872415275) /* PhysicsEffectTable */
     , (37370,  50,  100692242) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37371;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37371, 'ace37371-glyphofmissileweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37371,   1,        128) /* ItemType - Misc */
     , (37371,   5,         25) /* EncumbranceVal */
     , (37371,  11,       1000) /* MaxStackSize */
     , (37371,  12,          1) /* StackSize */
     , (37371,  13,         25) /* StackUnitEncumbrance */
     , (37371,  15,      2) /* StackUnitValue */
     , (37371,  16,          1) /* ItemUseable - No */
     , (37371,  19,      2) /* Value */
     , (37371,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37371,  11, True ) /* IgnoreCollisions */
     , (37371,  13, True ) /* Ethereal */
     , (37371,  14, True ) /* GravityStatus */
     , (37371,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37371,   1, 'Glyph of Missile Weapons') /* Name */
     , (37371,  20, 'Glyphs of Missile Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37371,   1,   33554809) /* Setup */
     , (37371,   3,  536870932) /* SoundTable */
     , (37371,   6,   67111919) /* PaletteBase */
     , (37371,   8,  100690191) /* Icon */
     , (37371,  22,  872415275) /* PhysicsEffectTable */
     , (37371,  50,  100686638) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37372;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37372, 'ace37372-glyphofmissileweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37372,   1,        128) /* ItemType - Misc */
     , (37372,   5,         25) /* EncumbranceVal */
     , (37372,  11,       1000) /* MaxStackSize */
     , (37372,  12,          1) /* StackSize */
     , (37372,  13,         25) /* StackUnitEncumbrance */
     , (37372,  15,      2) /* StackUnitValue */
     , (37372,  16,          1) /* ItemUseable - No */
     , (37372,  19,      2) /* Value */
     , (37372,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37372,  11, True ) /* IgnoreCollisions */
     , (37372,  13, True ) /* Ethereal */
     , (37372,  14, True ) /* GravityStatus */
     , (37372,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37372,   1, 'Glyph of Missile Weapons') /* Name */
     , (37372,  20, 'Glyphs of Missile Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37372,   1,   33554809) /* Setup */
     , (37372,   3,  536870932) /* SoundTable */
     , (37372,   6,   67111919) /* PaletteBase */
     , (37372,   8,  100690191) /* Icon */
     , (37372,  22,  872415275) /* PhysicsEffectTable */
     , (37372,  50,  100686638) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 37373;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (37373, 'ace37373-glyphoffinesseweapons', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (37373,   1,        128) /* ItemType - Misc */
     , (37373,   5,         25) /* EncumbranceVal */
     , (37373,  11,       1000) /* MaxStackSize */
     , (37373,  12,          1) /* StackSize */
     , (37373,  13,         25) /* StackUnitEncumbrance */
     , (37373,  15,      2) /* StackUnitValue */
     , (37373,  16,          1) /* ItemUseable - No */
     , (37373,  19,      2) /* Value */
     , (37373,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (37373,  11, True ) /* IgnoreCollisions */
     , (37373,  13, True ) /* Ethereal */
     , (37373,  14, True ) /* GravityStatus */
     , (37373,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (37373,   1, 'Glyph of Finesse Weapons') /* Name */
     , (37373,  20, 'Glyphs of Finesse Weapons') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (37373,   1,   33554809) /* Setup */
     , (37373,   3,  536870932) /* SoundTable */
     , (37373,   6,   67111919) /* PaletteBase */
     , (37373,   8,  100690191) /* Icon */
     , (37373,  22,  872415275) /* PhysicsEffectTable */
     , (37373,  50,  100692243) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 38760;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (38760, 'ace38760-glyphofmagicitemtinkering', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (38760,   1,        128) /* ItemType - Misc */
     , (38760,   5,         25) /* EncumbranceVal */
     , (38760,  11,       1000) /* MaxStackSize */
     , (38760,  12,          1) /* StackSize */
     , (38760,  13,         25) /* StackUnitEncumbrance */
     , (38760,  15,      2) /* StackUnitValue */
     , (38760,  16,          1) /* ItemUseable - No */
     , (38760,  19,      2) /* Value */
     , (38760,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (38760,  11, True ) /* IgnoreCollisions */
     , (38760,  13, True ) /* Ethereal */
     , (38760,  14, True ) /* GravityStatus */
     , (38760,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (38760,   1, 'Glyph of Magic Item Tinkering') /* Name */
     , (38760,  20, 'Glyphs of Magic Item Tinkering') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (38760,   1,   33554809) /* Setup */
     , (38760,   3,  536870932) /* SoundTable */
     , (38760,   6,   67111919) /* PaletteBase */
     , (38760,   8,  100690191) /* Icon */
     , (38760,  22,  872415275) /* PhysicsEffectTable */
     , (38760,  50,  100686672) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 41746;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (41746, 'ace41746-glyphofitemtinkering', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (41746,   1,        128) /* ItemType - Misc */
     , (41746,   5,         25) /* EncumbranceVal */
     , (41746,  11,       1000) /* MaxStackSize */
     , (41746,  12,          1) /* StackSize */
     , (41746,  13,         25) /* StackUnitEncumbrance */
     , (41746,  15,      2) /* StackUnitValue */
     , (41746,  16,          1) /* ItemUseable - No */
     , (41746,  19,      2) /* Value */
     , (41746,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (41746,  11, True ) /* IgnoreCollisions */
     , (41746,  13, True ) /* Ethereal */
     , (41746,  14, True ) /* GravityStatus */
     , (41746,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (41746,   1, 'Glyph of Item Tinkering') /* Name */
     , (41746,  20, 'Glyphs of Item Tinkering') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (41746,   1,   33554809) /* Setup */
     , (41746,   3,  536870932) /* SoundTable */
     , (41746,   6,   67111919) /* PaletteBase */
     , (41746,   8,  100690191) /* Icon */
     , (41746,  22,  872415275) /* PhysicsEffectTable */
     , (41746,  50,  100690692) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 41747;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (41747, 'ace41747-glyphoftwohandedcombat', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (41747,   1,        128) /* ItemType - Misc */
     , (41747,   5,         25) /* EncumbranceVal */
     , (41747,  11,       1000) /* MaxStackSize */
     , (41747,  12,          1) /* StackSize */
     , (41747,  13,         25) /* StackUnitEncumbrance */
     , (41747,  15,      2) /* StackUnitValue */
     , (41747,  16,          1) /* ItemUseable - No */
     , (41747,  19,      2) /* Value */
     , (41747,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (41747,  11, True ) /* IgnoreCollisions */
     , (41747,  13, True ) /* Ethereal */
     , (41747,  14, True ) /* GravityStatus */
     , (41747,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (41747,   1, 'Glyph of Two Handed Combat') /* Name */
     , (41747,  20, 'Glyphs of Two Handed Combat') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (41747,   1,   33554809) /* Setup */
     , (41747,   3,  536870932) /* SoundTable */
     , (41747,   6,   67111919) /* PaletteBase */
     , (41747,   8,  100690191) /* Icon */
     , (41747,  22,  872415275) /* PhysicsEffectTable */
     , (41747,  50,  100690691) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 43379;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (43379, 'ace43379-glyphofdamage', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (43379,   1,        128) /* ItemType - Misc */
     , (43379,   5,         25) /* EncumbranceVal */
     , (43379,  11,       1000) /* MaxStackSize */
     , (43379,  12,          1) /* StackSize */
     , (43379,  13,         25) /* StackUnitEncumbrance */
     , (43379,  15,      2) /* StackUnitValue */
     , (43379,  16,          1) /* ItemUseable - No */
     , (43379,  19,      2) /* Value */
     , (43379,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (43379,  11, True ) /* IgnoreCollisions */
     , (43379,  13, True ) /* Ethereal */
     , (43379,  14, True ) /* GravityStatus */
     , (43379,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (43379,   1, 'Glyph of Damage') /* Name */
     , (43379,  20, 'Glyphs of Damage') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (43379,   1,   33554809) /* Setup */
     , (43379,   3,  536870932) /* SoundTable */
     , (43379,   6,   67111919) /* PaletteBase */
     , (43379,   8,  100690191) /* Icon */
     , (43379,  22,  872415275) /* PhysicsEffectTable */
     , (43379,  50,  100691576) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 43380;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (43380, 'ace43380-glyphofvoidmagic', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (43380,   1,        128) /* ItemType - Misc */
     , (43380,   5,         25) /* EncumbranceVal */
     , (43380,  11,       1000) /* MaxStackSize */
     , (43380,  12,          1) /* StackSize */
     , (43380,  13,         25) /* StackUnitEncumbrance */
     , (43380,  15,      2) /* StackUnitValue */
     , (43380,  16,          1) /* ItemUseable - No */
     , (43380,  19,      2) /* Value */
     , (43380,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (43380,  11, True ) /* IgnoreCollisions */
     , (43380,  13, True ) /* Ethereal */
     , (43380,  14, True ) /* GravityStatus */
     , (43380,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (43380,   1, 'Glyph of Void Magic') /* Name */
     , (43380,  20, 'Glyphs of Void Magic') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (43380,   1,   33554809) /* Setup */
     , (43380,   3,  536870932) /* SoundTable */
     , (43380,   6,   67111919) /* PaletteBase */
     , (43380,   8,  100690191) /* Icon */
     , (43380,  22,  872415275) /* PhysicsEffectTable */
     , (43380,  50,  100691567) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 43387;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (43387, 'ace43387-glyphofnether', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (43387,   1,        128) /* ItemType - Misc */
     , (43387,   5,         25) /* EncumbranceVal */
     , (43387,  11,       1000) /* MaxStackSize */
     , (43387,  12,          1) /* StackSize */
     , (43387,  13,         25) /* StackUnitEncumbrance */
     , (43387,  15,      2) /* StackUnitValue */
     , (43387,  16,          1) /* ItemUseable - No */
     , (43387,  19,      2) /* Value */
     , (43387,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (43387,  11, True ) /* IgnoreCollisions */
     , (43387,  13, True ) /* Ethereal */
     , (43387,  14, True ) /* GravityStatus */
     , (43387,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (43387,   1, 'Glyph of Nether') /* Name */
     , (43387,  20, 'Glyphs of Nether') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (43387,   1,   33554809) /* Setup */
     , (43387,   3,  536870932) /* SoundTable */
     , (43387,   6,   67111919) /* PaletteBase */
     , (43387,   8,  100690191) /* Icon */
     , (43387,  22,  872415275) /* PhysicsEffectTable */
     , (43387,  50,  100691577) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 45370;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (45370, 'ace45370-glyphofdirtyfighting', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (45370,   1,        128) /* ItemType - Misc */
     , (45370,   5,         25) /* EncumbranceVal */
     , (45370,  11,       1000) /* MaxStackSize */
     , (45370,  12,          1) /* StackSize */
     , (45370,  13,         25) /* StackUnitEncumbrance */
     , (45370,  15,      2) /* StackUnitValue */
     , (45370,  16,          1) /* ItemUseable - No */
     , (45370,  19,      2) /* Value */
     , (45370,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (45370,  11, True ) /* IgnoreCollisions */
     , (45370,  13, True ) /* Ethereal */
     , (45370,  14, True ) /* GravityStatus */
     , (45370,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (45370,   1, 'Glyph of Dirty Fighting') /* Name */
     , (45370,  20, 'Glyphs of Dirty Fighting') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (45370,   1,   33554809) /* Setup */
     , (45370,   3,  536870932) /* SoundTable */
     , (45370,   6,   67111919) /* PaletteBase */
     , (45370,   8,  100690191) /* Icon */
     , (45370,  22,  872415275) /* PhysicsEffectTable */
     , (45370,  50,  100692244) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 45371;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (45371, 'ace45371-glyphofdualwield', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (45371,   1,        128) /* ItemType - Misc */
     , (45371,   5,         25) /* EncumbranceVal */
     , (45371,  11,       1000) /* MaxStackSize */
     , (45371,  12,          1) /* StackSize */
     , (45371,  13,         25) /* StackUnitEncumbrance */
     , (45371,  15,      2) /* StackUnitValue */
     , (45371,  16,          1) /* ItemUseable - No */
     , (45371,  19,      2) /* Value */
     , (45371,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (45371,  11, True ) /* IgnoreCollisions */
     , (45371,  13, True ) /* Ethereal */
     , (45371,  14, True ) /* GravityStatus */
     , (45371,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (45371,   1, 'Glyph of Dual Wield') /* Name */
     , (45371,  20, 'Glyphs of Dual Wield') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (45371,   1,   33554809) /* Setup */
     , (45371,   3,  536870932) /* SoundTable */
     , (45371,   6,   67111919) /* PaletteBase */
     , (45371,   8,  100690191) /* Icon */
     , (45371,  22,  872415275) /* PhysicsEffectTable */
     , (45371,  50,  100692245) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 45372;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (45372, 'ace45372-glyphofrecklessness', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (45372,   1,        128) /* ItemType - Misc */
     , (45372,   5,         25) /* EncumbranceVal */
     , (45372,  11,       1000) /* MaxStackSize */
     , (45372,  12,          1) /* StackSize */
     , (45372,  13,         25) /* StackUnitEncumbrance */
     , (45372,  15,      2) /* StackUnitValue */
     , (45372,  16,          1) /* ItemUseable - No */
     , (45372,  19,      2) /* Value */
     , (45372,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (45372,  11, True ) /* IgnoreCollisions */
     , (45372,  13, True ) /* Ethereal */
     , (45372,  14, True ) /* GravityStatus */
     , (45372,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (45372,   1, 'Glyph of Recklessness') /* Name */
     , (45372,  20, 'Glyphs of Recklessness') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (45372,   1,   33554809) /* Setup */
     , (45372,   3,  536870932) /* SoundTable */
     , (45372,   6,   67111919) /* PaletteBase */
     , (45372,   8,  100690191) /* Icon */
     , (45372,  22,  872415275) /* PhysicsEffectTable */
     , (45372,  50,  100686633) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 45373;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (45373, 'ace45373-glyphofshield', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (45373,   1,        128) /* ItemType - Misc */
     , (45373,   5,         25) /* EncumbranceVal */
     , (45373,  11,       1000) /* MaxStackSize */
     , (45373,  12,          1) /* StackSize */
     , (45373,  13,         25) /* StackUnitEncumbrance */
     , (45373,  15,      2) /* StackUnitValue */
     , (45373,  16,          1) /* ItemUseable - No */
     , (45373,  19,      2) /* Value */
     , (45373,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (45373,  11, True ) /* IgnoreCollisions */
     , (45373,  13, True ) /* Ethereal */
     , (45373,  14, True ) /* GravityStatus */
     , (45373,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (45373,   1, 'Glyph of Shield') /* Name */
     , (45373,  20, 'Glyphs of Shield') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (45373,   1,   33554809) /* Setup */
     , (45373,   3,  536870932) /* SoundTable */
     , (45373,   6,   67111919) /* PaletteBase */
     , (45373,   8,  100690191) /* Icon */
     , (45373,  22,  872415275) /* PhysicsEffectTable */
     , (45373,  50,  100692246) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 45374;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (45374, 'ace45374-glyphofsneakattack', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (45374,   1,        128) /* ItemType - Misc */
     , (45374,   5,         25) /* EncumbranceVal */
     , (45374,  11,       1000) /* MaxStackSize */
     , (45374,  12,          1) /* StackSize */
     , (45374,  13,         25) /* StackUnitEncumbrance */
     , (45374,  15,      2) /* StackUnitValue */
     , (45374,  16,          1) /* ItemUseable - No */
     , (45374,  19,      2) /* Value */
     , (45374,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (45374,  11, True ) /* IgnoreCollisions */
     , (45374,  13, True ) /* Ethereal */
     , (45374,  14, True ) /* GravityStatus */
     , (45374,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (45374,   1, 'Glyph of Sneak Attack') /* Name */
     , (45374,  20, 'Glyphs of Sneak Attack') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (45374,   1,   33554809) /* Setup */
     , (45374,   3,  536870932) /* SoundTable */
     , (45374,   6,   67111919) /* PaletteBase */
     , (45374,   8,  100690191) /* Icon */
     , (45374,  22,  872415275) /* PhysicsEffectTable */
     , (45374,  50,  100692247) /* IconOverlay */;
DELETE FROM `weenie` WHERE `class_Id` = 49455;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (49455, 'ace49455-glyphofsummoning', 51, '2019-04-30 00:00:00') /* Stackable */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (49455,   1,        128) /* ItemType - Misc */
     , (49455,   5,         25) /* EncumbranceVal */
     , (49455,  11,       1000) /* MaxStackSize */
     , (49455,  12,          1) /* StackSize */
     , (49455,  13,         25) /* StackUnitEncumbrance */
     , (49455,  15,      2) /* StackUnitValue */
     , (49455,  16,          1) /* ItemUseable - No */
     , (49455,  19,      2) /* Value */
     , (49455,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (49455,  11, True ) /* IgnoreCollisions */
     , (49455,  13, True ) /* Ethereal */
     , (49455,  14, True ) /* GravityStatus */
     , (49455,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (49455,   1, 'Glyph of Summoning') /* Name */
     , (49455,  20, 'Glyphs of Summoning') /* PluralName */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (49455,   1,   33554809) /* Setup */
     , (49455,   3,  536870932) /* SoundTable */
     , (49455,   6,   67111919) /* PaletteBase */
     , (49455,   8,  100690191) /* Icon */
     , (49455,  22,  872415275) /* PhysicsEffectTable */
     , (49455,  50,  100693009) /* IconOverlay */;
