DELETE FROM `quest` WHERE `name` = 'StipendTimer_0812';

INSERT INTO `quest` (`name`, `min_Delta`, `max_Solves`, `message`)
VALUES ('StipendTimer_0812', 259200, -1, 'Amount of stipends received.');


DELETE FROM `quest` WHERE `name` = 'StipendsCollectedInAMonth';

INSERT INTO `quest` (`name`, `min_Delta`, `max_Solves`, `message`) 
VALUES ('StipendsCollectedInAMonth', 0, 100, 'Amount of stipends player has received within a 27 day period.');


DELETE FROM `quest` WHERE `name` = 'StipendTimer_Monthly';
INSERT INTO `quest` (`name`, `min_Delta`, `max_Solves`, `message`) 
VALUES ('StipendTimer_Monthly', '10', '-1', 'Monthly timer for receiving up to 4 stipends.')
