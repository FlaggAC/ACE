DELETE FROM `weenie` WHERE `class_Id` = 152001;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (152001, 'guardianspawner', 1, '2019-08-21 18:15:51') /* Generic */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (152001,  66,          1) /* CheckpointStatus */
     , (152001,  81,          0) /* MaxGeneratedObjects */
     , (152001,  82,          0) /* InitGeneratedObjects */
     , (152001,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (152001,   1, True ) /* Stuck */
     , (152001,  11, True ) /* IgnoreCollisions */
     , (152001,  18, True ) /* Visibility */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (152001,  41,      300) /* RegenerationInterval */
     , (152001,  43,       1) /* GeneratorRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (152001,   1, 'Linkable Monster Generator ( 2 Min.)') /* Name */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (152001,   1,   33555051) /* Setup */
     , (152001,   8,  100667494) /* Icon */;

INSERT INTO weenie_properties_generator (object_Id, probability, weenie_Class_Id, delay, init_Create, max_Create, when_Create, where_Create, stack_Size, palette_Id, shade, obj_Cell_Id, origin_X, origin_Y, origin_Z, angles_W, angles_X, angles_Y, angles_Z)
VALUES (152001, -1, 9876543, 60, 1, 1, 1, 4, -1, 0, 0, 0x003F0387, 10.002115, 0.946441, 0.005000, 0.011613, 0, 0, -0.999933) /* Generate Edge Beach Golem (150001) (x1 up to max of 2) - Regenerate upon Destruction - Location to (re)Generate: scatter - specific is 4 not 2 */;
