DELETE FROM `weenie` WHERE `class_Id` = 261001;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (261001, 'ace261001-linkablemonstergenerator2min', 1, '2019-08-21 18:15:51') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (261001,  66,          1) /* CheckpointStatus */
     , (261001,  81,          0) /* MaxGeneratedObjects */
     , (261001,  82,          0) /* InitGeneratedObjects */
     , (261001,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (261001,   1, True ) /* Stuck */
     , (261001,  11, True ) /* IgnoreCollisions */
     , (261001,  18, True ) /* Visibility */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (261001,  41,      120) /* RegenerationInterval */
     , (261001,  43,       1) /* GeneratorRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (261001,   1, 'Linkable Monster Generator ( 2 Min.)') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (261001,   1,   33555051) /* Setup */
     , (261001,   8,  100667494) /* Icon */;

INSERT INTO weenie_properties_generator (object_Id, probability, weenie_Class_Id, delay, init_Create, max_Create, when_Create, where_Create, stack_Size, palette_Id, shade, obj_Cell_Id, origin_X, origin_Y, origin_Z, angles_W, angles_X, angles_Y, angles_Z)
VALUES (261001, -1, 261010, 60, 1, 1, 1, 4, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Ruschk Resident (261010) (x1 up to max of 2) - Regenerate upon Destruction - Location to (re)Generate: scatter - specific is 4 not 2 */;
