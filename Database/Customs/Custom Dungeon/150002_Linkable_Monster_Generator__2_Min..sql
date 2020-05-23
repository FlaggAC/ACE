DELETE FROM `weenie` WHERE `class_Id` = 150002;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (150002, 'ace150002-linkablemonstergenerator2min', 1, '2019-08-21 18:15:51') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (150002,  66,          1) /* CheckpointStatus */
     , (150002,  81,          0) /* MaxGeneratedObjects */
     , (150002,  82,          0) /* InitGeneratedObjects */
     , (150002,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (150002,   1, True ) /* Stuck */
     , (150002,  11, True ) /* IgnoreCollisions */
     , (150002,  18, True ) /* Visibility */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (150002,  41,      120) /* RegenerationInterval */
     , (150002,  43,       1) /* GeneratorRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (150002,   1, 'Linkable Monster Generator ( 2 Min.)') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (150002,   1,   33555051) /* Setup */
     , (150002,   8,  100667494) /* Icon */;

INSERT INTO weenie_properties_generator (object_Id, probability, weenie_Class_Id, delay, init_Create, max_Create, when_Create, where_Create, stack_Size, palette_Id, shade, obj_Cell_Id, origin_X, origin_Y, origin_Z, angles_W, angles_X, angles_Y, angles_Z)
VALUES (150002, -1, 151001, 60, 1, 1, 1, 4, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Edge Beach Golem (150001) (x1 up to max of 2) - Regenerate upon Destruction - Location to (re)Generate: scatter - specific is 4 not 2 */;
