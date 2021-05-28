DROP DATABASE IF EXISTS DAT602SeanCavill;
CREATE DATABASE DAT602SeanCavill;
USE DAT602SeanCavill;

DROP PROCEDURE IF EXISTS CreateDataBase;
DELIMITER ;;
CREATE PROCEDURE CreateDataBase()
	BEGIN

	CREATE TABLE tblplayer (
	  `userName`      varchar(20) PRIMARY KEY, 
	  `email`			varchar(50) UNIQUE NOT NULL,
	  `password`      varchar(20),
	  `loginAttempts` int(1) NOT NULL DEFAULT 0,
	  `isLocked`  bit(1) NOT NULL DEFAULT FALSE, 
	  `isAdministrator` bit(1) NOT NULL DEFAULT FALSE, 
	  `isOnline`  bit(1) NOT NULL DEFAULT FALSE 
	  );

	CREATE TABLE tblMap (
	  `name` VARCHAR(6) PRIMARY KEY,
	  `xLength` INTEGER NOT NULL,
	  `yLength` INTEGER NOT NULL,
	  `xHome`  INTEGER NOT NULL, 
	  `yHome` INTEGER NOT NULL
	  );

			
	CREATE TABLE tblGame (
	  `name` varchar(20) PRIMARY KEY, 
	  `mapName` varchar(6) NOT NULL, 
	  `gameActive` bit NOT NULL DEFAULT TRUE,
	  FOREIGN KEY (`mapName`) REFERENCES tblMap (`name`) ON UPDATE CASCADE
	  );
	  
	  
	CREATE TABLE tblTile (
	  `gameName` varchar(20) NOT NULL,
	  `xValue` integer NOT NULL,
	  `yValue` integer NOT NULL,
	  `isVisible` bit NOT NULL DEFAULT false,
	  `number` integer NOT NULL DEFAULT 0,
	  FOREIGN KEY (`gameName`) REFERENCES tblGame (`name`) ON UPDATE CASCADE ON DELETE CASCADE,
	  PRIMARY KEY (`gameName`, `xValue`, `yValue`)
	  );
	  
	  CREATE TABLE tblGamePlayer ( 
	  `userName` varchar(10) NOT NULL, 
	  `gameName` varchar(20) NOT NULL,    
	  `tileXValue` integer NOT NULL,
	  `tileYValue` integer NOT NULL,
	  `colour` varchar(7), 
	  `score` integer DEFAULT 0, 
	  `isActive` bit DEFAULT FALSE,
	  FOREIGN KEY (`userName`) REFERENCES tblPlayer (`userName`) ON UPDATE CASCADE ON DELETE CASCADE,
	  FOREIGN KEY (`gameName`) REFERENCES tblGame (`name`) ON UPDATE CASCADE ON DELETE CASCADE,
	  FOREIGN KEY (`gameName`, `tileXValue`, `tileYValue`) REFERENCES tblTile (`gameName`, `xValue`, `yValue`) ON UPDATE CASCADE ON DELETE CASCADE,
	  PRIMARY KEY (`userName`, `gameName`)
	  );
	 
	 CREATE TABLE tblChat (
	  `messageID` integer AUTO_INCREMENT PRIMARY KEY, 
	  `userName`  varchar(20) NOT NULL,
	  `text` varchar(50) NOT NULL,
	  FOREIGN KEY (`userName`) REFERENCES tblPlayer (`userName`) ON UPDATE CASCADE ON DELETE CASCADE
	  );
	  
	CREATE TABLE tblAsset (
	  `name` varchar(10) PRIMARY KEY, 
	  `effect`   varchar(20) NOT NULL
	  );
	  
	  
	CREATE TABLE tblTileItem (
	  `itemName` varchar(10) NOT NULL DEFAULT 'Gold', 
	  `gameName` varchar(20) NOT NULL,
	  `xValue` integer NOT NULL,
	  `yValue` integer NOT NULL,
	  FOREIGN KEY (`itemName`) REFERENCES tblAsset (`name`) ON UPDATE CASCADE ON DELETE CASCADE, 
	  FOREIGN KEY (`gameName`,`xValue`,`yValue`) REFERENCES tblTile (`gameName`, `xValue`, `yValue`) ON UPDATE CASCADE ON DELETE CASCADE,
	  PRIMARY KEY (`itemName`, `gameName`, `xValue`, `yValue`)
	  );
	  
	CREATE TABLE tblTileBomb (
		`gameName` varchar(20) NOT NULL,
		`xValue` integer NOT NULL,
		`yValue` integer NOT NULL,
		`damage` integer NOT NULL DEFAULT 50,
		FOREIGN KEY (`gameName`,`xValue`,`yValue`) REFERENCES tblTile (`gameName`, `xValue`, `yValue`) ON UPDATE CASCADE ON DELETE CASCADE,
		PRIMARY KEY (`gameName`, `xValue`, `yValue`)
		);
	  
	  
	CREATE TABLE tblPlayerItem (
	  `userName` varchar(20) NOT NULL,
	  `gameName` varchar(20) NOT NULL,
	  `itemName` varchar(10) NOT NULL,
	  `quantity` integer DEFAULT 1,
	  FOREIGN KEY (`userName`, `gameName`) REFERENCES tblGamePlayer (`userName`, `gameName`) ON UPDATE CASCADE ON DELETE CASCADE,
	  FOREIGN KEY (`itemName`) REFERENCES tblAsset (`name`) ON UPDATE CASCADE ON DELETE CASCADE,
	  PRIMARY KEY (`userName`, `gameName`, `itemName`)
	  );

END;;
DELIMITER ;
  
Call CreateDataBase();
/* INSERT STATEMENTS */
/*
INSERT INTO tblPlayer(`userName`, `email`, `password`, `loginAttempts`, `isLocked`, `isAdministrator`, `isOnline`)
  Values('Sean', 'email1@gmail.com','Password1', 0, false, true, true),
		('Todd', 'email2@gmail.com', 'Password2', 1, false, false, false),
        ('Sarah', 'email3@gmail.com', 'Password3', 5, true, false, false);
*/
INSERT INTO tblMap(`Name`, `xLength`, `yLength`, `xHome`, `yHome`)
Values('Small', 21, 21, 11, 11),
	  ('Medium', 41, 41, 21, 21),
      ('Large', 61, 61, 31, 31);        
/*
INSERT INTO tblGame(`name`, `mapName`, `gameActive`)
Values('seans game', 'Small', true),
	  ('seans second game', 'Medium', false),
      ('Todds game', 'Large', false);

 INSERT INTO tblTile(`GameName`, `xValue`, `yValue` , `isVisible`, `number`)
  Values('seans game', '1', '1', false, 1),
		('seans game', '2', '1', true, 2),
		('Todds game', '1', '1', true, 0); 
        
will create game after procedure is declared for test data as entering all the tiles manually would be a lot
 
 INSERT INTO tblTileBomb(`GameName`, `xValue`, `yValue` , `damage`)
  Values('seans game', '1', '1', 20),
		('seans game', '2', '1', 30),
		('Todds game', '1', '1', 5);
        
*/
INSERT INTO tblAsset(`name`, `effect`)
  Values('Binoculars', 'Clear Tiles'),
		('Gold', 'Add Score'),
        ('Club', 'Steal Score');
        
/*
INSERT INTO tblTileItem(`itemName`, `gameName`, `xValue`, `yValue`)
  Values('Binoculars', 'seans game', '1', '1'),
		('Gold', 'seans game', '2', '1'),
		('Club', 'Todds game', '1', '1');
        
INSERT INTO tblGamePlayer(`userName`, `gameName`, `tileXValue`, `tileYValue`, `score`, `isActive`, `colour`)
  Values('Sean', 'seans game', 1, 1, 100, true, '#85ADF8'),
		('Todd', 'seans game', 2, 1, 200, true, '#A4FDF7'),
        ('Sarah', 'Todds Game', 1, 1, 0, false, '#B6AFA2');

INSERT INTO tblChat(`userName`, `text`)
  Values('Sean', 'hello'),
		('Todd', 'hello 2');
	
INSERT INTO tblPlayerItem(`userName`, `gameName`, `itemName`)
  Values('Sean', 'seans game', 'Binoculars'),
		('Todd','seans game', 'Gold'),
        ('Sarah','Todds Game', 'Club');
*/
/* UPDATE STATEMENTS */
/*
UPDATE tblPlayer
	SET `loginAttempts` = 0
	WHERE `userName` = 'Sean';

UPDATE tblGamePlayer
	SET Score = 10
	WHERE UserName = 'Sean';

UPDATE tblPlayerItem
	SET ItemName = 'Club'
    WHERE ItemName = 'Gold';
    
UPDATE tblAsset
	SET name = 'Spoon'
    WHERE name = 'Club';
    
UPDATE tblTileItem
	SET xValue = 1
    WHERE xValue = 2;
    
UPDATE tbltile
	SET Number = 9
    Where Number = 1;
    
UPDATE tblTileBomb
	SET Damage = 10
    WHERE Damage = 5;
    
UPDATE tblGame
	SET GameActive = true
    WHERE name = 'Todds Game';
    
UPDATE tblMap
	SET xLength = 5
    WHERE name = 'Small';
*/
/*SELECT STATEMENTS*/
/*
SELECT* FROM tblAsset;
SELECT* FROM tblChat;
SELECT* FROM tblGame;
SELECT* FROM tblGamePlayer;
SELECT* FROM tblPlayer;
SELECT* FROM tblPlayerItem;
SELECT* FROM tblTile;
SELECT* FROM tblTileitem;
SELECT* FROM tblTileBomb;
SELECT* FROM tblMap;
*/
/*DELETE STATEMENTS

DELETE FROM tblAsset;
DELETE FROM tblChat;
DELETE FROM tblGame;
DELETE FROM tblGamePlayer;
DELETE FROM tblPlayer;
DELETE FROM tblPlayerItem;
DELETE FROM tblTile;
DELETE FROM tblTileitem;
DELETE FROM tblTileBomb;
DELETE FROM tblMap;

*/

/* PROCEDURES */

/* Find User */

DROP PROCEDURE IF EXISTS FindPlayer;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `FindPlayer`(pUserName varchar(20))
BEGIN
START TRANSACTION;
	IF EXISTS (SELECT * FROM tblPlayer WHERE UserName = pUserName) THEN
		BEGIN
			SELECT concat('User ', pUserName, ' Exists') AS MESSAGE;
		END;
	ELSE
		SELECT concat('User ', pUserName, ' Does not exist') AS MESSAGE;
	END IF;
COMMIT;
END;;
DELIMITER ;

/* Register a Player */


DELIMITER ;

DROP PROCEDURE IF EXISTS RegisterPlayer;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `RegisterPlayer`(pUserName VARCHAR(20), pEmail VARCHAR(50), pPassword VARCHAR(25))
BEGIN
START TRANSACTION;
  IF EXISTS (SELECT * FROM tblPlayer WHERE UserName = pUserName) THEN
  BEGIN
     SELECT 'UserName is taken' AS MESSAGE;
  END;
  ELSEIF EXISTS (SELECT * FROM tblPlayer WHERE Email = pEmail) THEN
	BEGIN
		SELECT 'Email Already used by another account' AS MESSAGE;
	END;
	ELSE 
     INSERT INTO tblPlayer(`UserName`,`Password`,`Email`)
     VALUE (pUserName, pEmail, pPassword);
     SELECT 'Registered User Successfully' AS MESSAGE;
  END IF;
COMMIT;
END ;;
DELIMITER ;

/* Login to an existing player */

DROP PROCEDURE IF EXISTS LoginPlayer;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `loginPlayer`(pUserName VARCHAR(20), pPassword VARCHAR(25))
BEGIN
DECLARE lcUserName varchar(20);
DECLARE lcPassword varchar(25);
DECLARE lcIsLocked bit;
DECLARE lcLoginAttempts integer;
SELECT `UserName`, `Password`, `IsLocked`, `loginAttempts`
FROM tblPlayer
WHERE `UserName` = pUserName
INTO lcUserName, lcPassword, lcIsLocked, lcLoginAttempts;
IF (lcUserName IS NULL)  THEN
	BEGIN
		SELECT concat(pUserName, " Doesn't exist in the database! Register or check spelling.") AS MESSAGE;
	END;
	ELSEIF ((lcLoginAttempts > 4) OR (lcIsLocked = true)) THEN
	BEGIN
		SELECT concat("Your account is locked :(! Plase Email This Admin:",`Email`, " To get it unlocked.") AS MESSAGE
        FROM tblPlayer
        WHERE `isAdministrator` = true
        ORDER BY RAND()
        LIMIT 1;		
	END;
    ELSEIF (lcPassword <> pPassword) THEN
    BEGIN
		UPDATE tblPlayer
        SET `loginAttempts` = `loginAttempts` + 1
        WHERE `userName` = lcUserName;
        SELECT concat("You have ", 5-`loginAttempts`, " Login Attempts Left") AS MESSAGE
        FROM tblPlayer
        WHERE `userName` = lcUserName;
	END;
	ELSEIF ((lcUserName = pUserName) AND (lcPassword = pPassword) AND (lcIsLocked = false) AND (lcLoginAttempts < 5)) THEN
    BEGIN
		UPDATE tblplayer
        SET `isOnline` = true, `loginAttempts` = 0
        WHERE`userName` = pUserName;
        SELECT concat("Logged in as ", lcUserName) AS MESSAGE;
	END ;
    END IF;
    COMMIT;
END;;
DELIMITER ;
    
    
/* Log Out Procedure */

DROP PROCEDURE IF EXISTS LogoutPlayer;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `LogoutPlayer`(pUserName varchar(20))
BEGIN
	START TRANSACTION;
        BEGIN
            UPDATE tblPlayer
            SET `isOnline` = false
            WHERE `userName` = pUserName;
            SELECT concat(pUserName, " Is now offline") AS MESSAGE;
        END;
	COMMIT;
END;;
DELIMITER ;
    
/* Admin Access */

DROP PROCEDURE IF EXISTS AdminAcess;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AdminAcess`(pUserName varchar(20))
BEGIN
START TRANSACTION;
	IF EXISTS (SELECT `userName` FROM tblPlayer WHERE `userName` = pUserName AND `isAdministrator` = true) THEN
		BEGIN
			SELECT concat(pUserName, " You have Admin Access") AS MESSAGE;
		END;
	ELSE
	BEGIN
		SELECT concat(pUserName, " You don't have admin Access") AS MESSAGE;
	END;
    END IF;
    COMMIT;
    
END;;

DELIMITER ;

/* Admin list All games */

DROP PROCEDURE IF EXISTS GetAllGames;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllGames`()
BEGIN

SELECT `name` FROM tblGame AS MESSAGE
ORDER BY `name` DESC;

END ;;
DELIMITER ;

/* Admin list all players */
DROP PROCEDURE IF EXISTS GetAllPlayers;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllPlayers`()
BEGIN

SELECT `userName` FROM tblPlayer  AS MESSAGE
ORDER BY `userName` DESC;
 
 END ;;
DELIMITER ;

/* Admin Edit User */

DROP PROCEDURE IF EXISTS EditPlayer;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `EditPlayer`(pUserName varchar(20), pNewUserName varchar(20), pNewEmail varchar(50), pNewPassword varchar(25), pNewAdminStatus bit, pNewLockedStatus bit)
BEGIN
START TRANSACTION;
	
IF EXISTS(SELECT `userName` FROM tblPlayer WHERE `userName` = pUserName) THEN
	BEGIN
	IF EXISTS(SELECT `Email` FROM tblPlayer WHERE `Email` = pNewEmail) THEN
	BEGIN
		SELECT concat('Email ', pNewEmail, " Already used by another account") AS MESSAGE;
	END;
	ELSE
		UPDATE tblPlayer
		SET `userName` = pNewUserName, `email` = pNewEmail, `password` = pNewPassword, `isAdministrator` = pNewAdminStatus, `isLocked` = pNewLockedStatus
		WHERE `userName` = pUserName;
		SELECT concat(pUserName, " Has been updated") AS MESSAGE;
	END IF;
	END;
ELSE
	SELECT concat("Could not find user ", pUserName) AS MESSAGE;

END IF;
COMMIT;
    
END;;

DELIMITER ;
            
    
/* Admin Delete PLayer */
DROP PROCEDURE IF EXISTS DeletePlayer;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeletePlayer`(pUserName varchar(20))
BEGIN
IF EXISTS(SELECT `userName` FROM tblPlayer WHERE `userName` = pUserName) THEN
	BEGIN
		DELETE FROM tblPlayer
		WHERE `userName` = pUserName;
        
		SELECT concat(pUserName, " Is now Deleted") AS MESSAGE;
	END;
    
ELSE
	SELECT concat("Could not find user ", pUserName) AS MESSAGE;
    
END IF;
END;;
DELIMITER ;

/* Admin Delete Game */

DROP PROCEDURE IF EXISTS DeleteGame;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteGame`(pGameName varchar(20))
BEGIN

DELETE FROM tblGame
WHERE `name` = pGameName;
SELECT concat(pGameName, ' Deleted') AS MESSAGE;

END ;;
DELIMITER ;


/*Create Game*/
        
DROP PROCEDURE IF EXISTS CreateGame;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateGame`(pGameName varchar(20), pMapName varchar(25))
BEGIN

DECLARE lcXLength varchar(20);
DECLARE lcYLength varchar(25);
DECLARE lcXCount integer DEFAULT 1;
DECLARE lcYCount integer DEFAULT 1;
DECLARE lcCount integer DEFAULT 1;
DECLARE lcRowNums integer;
DECLARE lcAssetAmount integer DEFAULT 100;

IF NOT EXISTS(SELECT `name` FROM tblGame WHERE `name` = pGameName) THEN
	IF EXISTS(SELECT `name` FROM tblMap WHERE `name` = pMapName) THEN

			/* Creates Game */
			INSERT INTO tblGame(`name`,`MapName`)
			VALUE (pGameName, pMapName);
			
			/* Creates Game Tiles */
			SELECT `xLength`, `yLength` FROM tblMap
			WHERE `name` = pMapName
			INTO lcXLength, lcYLength;
			WHILE  lcYCount <= lcYLength DO
				WHILE lcXCount <= lcXLength DO
				   INSERT INTO tblTile(`GameName`, `xValue`, `yValue`)
				   VALUES(pGameName, lcYCount, lcXCount);
				   SET lcXCount = lcXCount + 1;			
				END WHILE;
				SET lcXCount = 1;
				SET lcYCount = lcYCount + 1;	
			END WHILE;
			
			SET lcAssetAmount = lcXLength*lcYLength/10;
			
			/* Randomizes bombs on game tiles */
			
			INSERT INTO tblTileBomb (`gameName`, `xValue`, `yValue`)
			SELECT `gameName`, `xValue`, `yValue` FROM tbltile 
			WHERE `gameName` = pGameName
			ORDER BY RAND ( )
			LIMIT lcAssetAmount ON DUPLICATE KEY UPDATE `damage` = (`damage`+50); 
			
			/* Randomizes Items */
			
			INSERT INTO tblTileItem (`gameName`, `xValue`, `yValue`, `ItemName`)
			SELECT tblTile.gameName, tblTile.xValue, tblTile.yValue, tblAsset.name FROM tbltile CROSS JOIN tblAsset
			WHERE `gameName` = pGameName
			ORDER BY RAND ( )
			LIMIT lcAssetAmount;
			
			/* Creates Numbers */
			
			/*Gets amount of bomb tiles in a game */
			
			SELECT COUNT(*) FROM tblTileBomb
			WHERE `gameName` = pGameName INTO lcRowNums;
			
			/* While Loop updates numbers on adjacent tiles */
			
			WHILE lcCount <= lcRowNums DO
			/* goes through each bomb location */
			SELECT `xValue`, `yValue` FROM tblTileBomb 
			WHERE `gameName` = pGameName
			ORDER BY `xValue`, `yValue` DESC LIMIT lcCount,1 INTO lcXCount, lcYCount;
			
			/*Updates Tiles that are in an adjacent pattern */
			UPDATE tblTile
			SET `number` = `number`+1
			WHERE ((`xvalue` = lcXCount OR `xvalue`= lcXCount+1 OR `xvalue`= lcXCount-1) AND (`yvalue` = lcYCount OR `yvalue` = lcYCount+1 OR `yvalue`= lcYCount-1) AND `gameName` = pGameName);
		  /* 
			Long Version
			WHERE (`xvalue` = lcXCount AND `yvalue` = lcYCount+1)
			OR (`xvalue` = lcXCount AND `yvalue` = lcYCount+-1)
			OR (`xvalue` = lcXCount+1 AND `yvalue` = lcYCount+1)
			OR (`xvalue` = lcXCount+1 AND `yvalue` = lcYCount)
			OR (`xvalue` = lcXCount+1 AND `yvalue` = lcYCount-1)
			OR (`xvalue` = lcXCount-1 AND `yvalue` = lcYCount+1)
			OR (`xvalue` = lcXCount-1 AND `yvalue` = lcYCount)
			OR (`xvalue` = lcXCount-1 AND `yvalue` = lcYCount-1)
			AND `gameName` = pGameName; */

			
			SET lcCount = lcCount + 1;
			END WHILE;

			SELECT `xHome`, `yHome` FROM tblMap 
			WHERE `name` = pMapName INTO lcXCount, lcYCount;
			
			/* Making Home Tile Visible */
			UPDATE tblTile
			SET `isVisible` = 1
			WHERE (`xValue` = lcXCount) AND (`yValue` = lcYCount);
			
			SELECT 'Created Game' AS MESSAGE;
			
			
		ELSE
				SELECT 'Invalid Map' AS MESSAGE;
	END IF;
ELSE
		SELECT 'Game with name already exists' AS MESSAGE;
END IF;
COMMIT;
END;;
DELIMITER ;			


  
/*
Create Game Player
 */
 
DROP PROCEDURE IF EXISTS JoinGame;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `JoinGame`(pPlayerName varchar(20), pGameName varchar(20))
BEGIN

DECLARE lcXHome integer;
DECLARE lcYHome integer;
DECLARE lcColour varchar(7);

IF EXISTS(SELECT `userName`, `gameName` FROM tblGamePlayer WHERE `userName` = pPlayerName AND pGameName = `gameName`) THEN

	/* Checks if the square they're on is occupied by an active player (Players at home, or just joining on an occupied square will not be active until they move)*/
	IF NOT EXISTS(SELECT `GameName`, `TileXValue`, `TileYValue`, `isActive`, COUNT(*) FROM tblGamePlayer
			  WHERE ((`isActive` = true) AND (`gameName` = pGameName))
              GROUP by `GameName`, `TilexValue`, `TileYValue`, `isActive`
              HAVING COUNT(*) >= 1) THEN
					UPDATE tblGamePlayer
					SET `isActive` = true
					WHERE `username` = pPlayerName and `gameName` = pGameName;
	END IF;

	SELECT concat(pPlayerName, ' Rejoined the Game') AS MESSAGE;
ELSE

    /* Start point */
    SELECT `xHome`, `yHome` FROM tblMap
    WHERE `name` = (SELECT `mapName` FROM tblGame WHERE `name` = pGameName)
    INTO lcXHome, lcYHome;
    
    /* Random Color */
    SET lcColour = concat('#',SUBSTRING((lpad(hex(round(rand() * 10000000)),6,0)),-6));
    
    /* Creates GamePlayer */
    INSERT INTO tblGamePlayer(`userName`, `gameName`, `TileXValue`, `TileYValue`, `colour`)
    SELECT tblPlayer.userName, tblGame.name, lcXHome, lcYHome, lcColour
	FROM tblPlayer, tblGame
    WHERE tblPlayer.userName = pPlayerName AND tblGame.Name = pGameName;
	
    SELECT concat(pPlayerName, ' Joined The Game') AS MESSAGE;

END IF;
COMMIT;
END;;

DELIMITER ;



/* move in game */

DROP PROCEDURE IF EXISTS MovePlayer;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `MovePlayer`(pPlayerName varchar(20), pGameName varchar(20), pXValue integer, pYValue integer)
BEGIN
/*Checks if tile is adjacent */
DECLARE lcCurrentTileX integer;
DECLARE lcCurrentTileY integer;

SELECT `tileXValue`, `tileYValue` FROM tblGamePlayer WHERE `gameName` = pGameName AND `userName` = pPlayerName INTO lcCurrentTileX, lcCurrentTileY;

/* Checks if tile is adjacent(x moves no more than +1 or -1, y moves no more than +1 -1 on their axis) */
IF ((ABS(lcCurrentTileX-pXValue) <= 1) AND (ABS(lcCurrentTileY-pYValue) <= 1)) THEN
	/* Checks if active players on a tile is > 0 before moving */
	IF NOT EXISTS(SELECT `GameName`, `TileXValue`, `TileYValue`, `isActive`, COUNT(*) FROM tblGamePlayer
				  WHERE ((`isActive` = true) AND (`gameName` = pGameName)) AND `tileXValue` = pXValue AND `tileYValue` = pYValue
				  GROUP by `GameName`, `TilexValue`, `TileYValue`, `isActive`
				  HAVING COUNT(*) >= 1) THEN
						/* updates the user to being active in the game */
						UPDATE tblGamePlayer
						SET `isActive` = true,`TilexValue` = pXValue, `TileyValue` = pYValue
						WHERE `username` = pPlayerName and `gameName` = pGameName;
						
						/* Updates tile to be visible to players */
						UPDATE tblTile
						SET `isVisible` = true
						WHERE `gameName` = pGameName AND `xValue` = PXValue AND `yValue` = pYValue;

				  /*Checks if player landed on bomb square */
				  IF EXISTS(SELECT `gameName`, `xValue`, `yValue` FROM tblTileBomb 
							  WHERE `gameName` = pGameName AND `xValue` = PXValue AND `yValue` = pYValue) THEN
							  
									/* Reduces player score */
								  
									UPDATE tblGamePlayer
									SET `score` = `score`-(SELECT 'damage' FROM tblTileBomb WHERE `gameName` = pGameName AND `xValue` = PXValue AND `yValue` = pYValue);
								  
									/* Deletes Bomb From Tile As it has been activated */
									
									DELETE FROM tblTileBomb WHERE `gameName` = pGameName AND `xValue` = PXValue AND `yValue` = pYValue;
									
									/* Updates tile number to represent 1 less bomb adjacent to tile */
									UPDATE tblTile	
									SET `number` = `number`-1
                                    WHERE ((`xvalue` = pXValue OR `xvalue`= pXValue+1 OR `xvalue`= pXValue-1) AND (`yvalue` = pYValue OR `yvalue` = pYValue+1 OR `yvalue`= pYValue-1) AND `gameName` = pGameName);
									/*
                                    Old Way
                                    WHERE (`xvalue` = pXValue AND `yvalue` = pYValue+1)
									OR (`xvalue` = pXValue AND `yvalue` = pYValue+-1)
									OR (`xvalue` = pXValue+1 AND `yvalue` = pYValue+1)
									OR (`xvalue` = pXValue+1 AND `yvalue` = pYValue)
									OR (`xvalue` = pXValue+1 AND `yvalue` = pYValue-1)
									OR (`xvalue` = pXValue-1 AND `yvalue` = pYValue+1)
									OR (`xvalue` = pXValue-1 AND `yvalue` = pYValue)
									OR (`xvalue` = pXValue-1 AND `yvalue` = pYValue-1)
                                    AND (AND `gameName` = pGameName);
                                    */
				 
				  END IF;

	 SELECT concat(pPlayerName, 'Moved successfully') AS MESSAGE;
     
	/*If moveing to same tile player is active on picks up items on tile */
	ELSEIF EXISTS(SELECT `GameName`, `TileXValue`, `TileYValue`, `isActive` FROM tblGamePlayer
				  WHERE ((`isActive` = true) AND (`gameName` = pGameName)) AND `tileXValue` = pXValue AND `tileYValue` = pYValue) THEN
                  
		INSERT INTO tblPlayerItem(`userName`, `gameName`, `itemName`)
		SELECT pPlayerName, `gameName`, `itemName` FROM tblTileItem
		WHERE `xValue` = pXValue and `yValue` = pYValue and `gameName` = pGameName
		ON DUPLICATE KEY UPDATE `quantity` = `quantity`+1;

		/* Removes items from tile when picked up */
		DELETE FROM tblTileItem
		WHERE `xValue` = pXValue AND `yValue` = pYValue AND `gameName` = pGameName;
		
		SELECT 'Attempted to pick up item' AS MESSAGE;				 
				  
	ELSE
		/* Returns tile is occupied and doesn't update gameplayers tile */
			SELECT 'Tile occupied' AS MESSAGE;
	END IF;

ELSE
	SELECT 'Tile Not Adjacent' AS MESSAGE;
END IF;
COMMIT;
END;;
DELIMITER ;

/* Leave Game */

DROP PROCEDURE IF EXISTS LeaveGame;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `LeaveGame`(pPlayerName VARCHAR(20), pGameName varchar(20))
BEGIN

UPDATE tblGamePlayer
SET `isActive` = false
WHERE `gameName` = pGameName AND `userName` = pPlayerName;

SELECT concat(pPlayerName, ' Has left the game') AS MESSAGE;

END ;;
DELIMITER ;

/* Use Item */

DROP PROCEDURE IF EXISTS ConsumeItem;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ConsumeItem`(pPlayerName VARCHAR(20), pGameName VARCHAR(50), pItemName VARCHAR(10))
BEGIN

DECLARE lcxValue integer;
DECLARE lcyValue integer;

SELECT `TilexValue`, `TileYValue` FROM tblGamePlayer WHERE `gameName` = pGameName INTO lcXValue, lcYValue;

IF((SELECT `isActive` FROM tblGamePlayer WHERE `gameName` = pGameName AND `userName` = pPlayerName) = true) THEN
	 IF EXISTS(SELECT * FROM tblPlayerItem WHERE `userName` = pPlayerName AND `gameName` = pGameName AND `itemName` = pItemName AND `quantity` >= 1) THEN
		  CASE
		  WHEN pItemName = 'Binoculars' THEN
				/* Uncovers Tiles */
				UPDATE tblTile	
				SET `isVisible` = true
				WHERE ((`xvalue` = lcXValue OR `xvalue`= lcXValue+1 OR `xvalue`= lcXValue-1) AND (`yvalue` = lcYValue OR `yvalue` = lcYValue+1 OR `yvalue`= lcYValue-1)
                AND `gameName` = pGameName);
                
		  WHEN pItemName = 'Gold' THEN
				UPDATE tblGamePlayer
				SET `Score` = `Score`+10
				WHERE `gameName` = pGameName AND `userName` = pPlayerName;
		  WHEN pItemName = 'Club' THEN
				UPDATE tblGamePlayer
				SET `Score` = `Score`-5
				WHERE `gameName` = pGameName AND isActive = true;
				
				UPDATE tblGamePlayer
				SET `Score` = `Score`+10
				WHERE `gameName` = pGameName AND `userName` = pPlayerName;
			

		  END CASE;
		  /* removes 1 quantity of item from inventory */
		  UPDATE tblPlayerItem
		  SET `quantity` = `quantity`-1
		  WHERE `userName` = pPlayerName AND `gameName` = pGameName AND `itemName` = pItemName;
          
          SELECT concat('used ', pItemName) AS MESSAGE;

	ELSE
		SELECT 'Item not in inventory' AS MESSAGE;
    END IF;
ELSE
    SELECT 'Move to a valid tile before using items' AS MESSAGE;
END IF;
COMMIT;
END ;;
DELIMITER ;

/* Player Send Message */


DROP PROCEDURE IF EXISTS SendMessage;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SendMessage`(pUserName varchar(20), pText varchar(255))
BEGIN
INSERT INTO tblChat(`userName`, `text`)
VALUES(puserName, pText);

SELECT concat('sent ', pText, ' as message from', pUserName) AS MESSAGE;

END ;;
DELIMITER ;


/* Get Chat messages */

DROP PROCEDURE IF EXISTS GetChat;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetChat`()
BEGIN

SELECT `userName`, `text` FROM tblChat  AS MESSAGE
ORDER BY `messageID` DESC
LIMIT 10;

END ;;
DELIMITER ;



/* list Online players */
DROP PROCEDURE IF EXISTS GetOnlinePlayers;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetOnlinePlayers`()
BEGIN

SELECT `userName` FROM tblPlayer  AS MESSAGE
WHERE `isOnline` = true
ORDER BY `userName` DESC;

END ;;
DELIMITER ;

/* list Active games */

DROP PROCEDURE IF EXISTS GetActiveGames;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetActiveGames`()
BEGIN

SELECT `name` FROM tblGame AS MESSAGE
WHERE `gameActive` = true
ORDER BY `name` DESC;

END ;;
DELIMITER ;

/* list Game Players */

DROP PROCEDURE IF EXISTS GetGamePlayers;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetGamePlayers`(pGameName varchar(20))
BEGIN

SELECT `userName`, `score`, `colour` FROM tblGamePlayer AS MESSAGE
WHERE `isActive` = true AND `gameName` = pGameName
ORDER BY `userName` DESC;

END ;;
DELIMITER ;

/* TEST DATA */

INSERT INTO tblPlayer(`userName`, `email`, `password`, `loginAttempts`, `isLocked`, `isAdministrator`, `isOnline`)
  Values('Sean', 'email1@gmail.com','Password1', 0, false, true, true),
		('Todd', 'email2@gmail.com', 'Password2', 1, false, false, false),
        ('NewUser', 'email5@gmail.com', 'Passwordd', 1, false, false, true),
        ('Sarah', 'email3@gmail.com', 'Password3', 5, true, false, true);
        

Call CreateGame("New", "Medium");
Call CreateGame("New2", "Small");
Call CreateGame("New3", "Large");

UPDATE tblGame
SET `gameActive` = false
WHERE `name` ='New3';


Call JoinGame('Sean', 'New');
Call JoinGame('Todd', 'New');
Call JoinGame('Sarah', 'New');
Call JoinGame('Sean', 'New2');
Call JoinGame('NewUser', 'New2');
        
UPDATE tblGamePlayer
SET `isActive` = true
WHERE `gameName` ='New2';


INSERT INTO tblChat(`userName`, `text`)
  Values('Sean', 'this wont show'),
		('Todd', 'hello 2'),
        ('Sean', 'hello1'),
        ('Sean', 'hello2'),
        ('Sean', 'hello3'),
        ('Sean', 'hello4'),
        ('NewUser', 'hello5'),
        ('Sarah', 'hello6'),
        ('Sean', 'hello7'),
        ('Sean', 'hello8'),
        ('Sean', 'hello9'),
        ('Sean', 'Most Recent Message');
	
INSERT INTO tblPlayerItem(`userName`, `gameName`, `itemName`, `quantity`)
  Values('Sean', 'New', 'Binoculars', 10),
		('Todd','New', 'Gold', 1),
        ('Sarah','New', 'Club', 1),
        ('Sean', 'New2', 'Binoculars', 5);


	




