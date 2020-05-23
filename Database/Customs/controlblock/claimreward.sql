DELETE FROM `quest` WHERE `name` = 'claimreward';

INSERT INTO `quest` (`name`, `min_Delta`, `max_Solves`, `message`, `last_Modified`)
VALUES ('claimreward', 600, -1, 'claimed control block reward', '2019-02-04 06:51:50');
