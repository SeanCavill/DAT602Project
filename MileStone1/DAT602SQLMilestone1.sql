DROP DATABASE IF EXISTS DAT602SeanCavill;
CREATE DATABASE DAT602SeanCavill;
USE DAT602SeanCavill;

/* CREATE STATEMENTS */


DROP PROCEDURE IF EXISTS CreateDataBase;
DELIMITER ;;
CREATE PROCEDURE CreateDataBase()
	BEGIN
		CREATE TABLE tblPlayer (
		  `userName`      varchar(20) PRIMARY KEY, 
		  `email`			varchar(50) UNIQUE NOT NULL,
		  `password`      varchar(20),
		  `loginAttempts` int NOT NULL DEFAULT 0,
		  `isLocked`  bit NOT NULL DEFAULT FALSE, 
		  `isAdministrator` bit NOT NULL DEFAULT FALSE, 
		  `isOnline`  bit NOT NULL DEFAULT FALSE 
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
		  `gameActive` bit NOT NULL DEFAULT FALSE,
		  FOREIGN KEY (`mapName`) REFERENCES tblMap (`name`) ON UPDATE CASCADE
		  );
		  
		  
		CREATE TABLE tblTile (
		  `gameName` varchar(20) NOT NULL,
		  `xValue` integer NOT NULL,
		  `yValue` integer NOT NULL,
		  `isVisible` bit NOT NULL DEFAULT false,
		  `number` integer,
		  FOREIGN KEY (`gameName`) REFERENCES tblGame (`name`) ON UPDATE CASCADE ON DELETE CASCADE,
		  PRIMARY KEY (`gameName`, `xValue`, `yValue`)
		  );
		  
		  CREATE TABLE tblGamePlayer ( 
		  `userName` varchar(10) NOT NULL, 
		  `gameName` varchar(20) NOT NULL,    
		  `tileXValue` integer NOT NULL,
		  `tileYValue` integer NOT NULL,
		  `colour` varchar(10), 
		  `score` integer, 
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
		  `name` varchar(20) PRIMARY KEY, 
		  `effect`   varchar(20) NOT NULL
		  );
		  
		  
		CREATE TABLE tblTileItem (
		  `itemName` varchar(10) NOT NULL, 
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
			`damage` integer NOT NULL,
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

CALL CreateDataBase();

/* INSERT STATEMENTS */

INSERT INTO tblPlayer(`userName`, `email`, `password`, `loginAttempts`, `isLocked`, `isAdministrator`, `isOnline`)
  Values('Sean', 'email1@gmail.com','Password1', 0, false, true, true),
		('Todd', 'email2@gmail.com', 'Password2', 1, false, false, false),
        ('Sarah', 'email3@gmail.com', 'Password3', 5, true, false, false);
        
INSERT INTO tblMap(`Name`, `xLength`, `yLength`, `xHome`, `yHome`)
Values('Small', 21, 21, 11, 11),
	  ('Medium', 41, 41, 21, 21),
      ('Large', 61, 61, 31, 31);        

INSERT INTO tblGame(`name`, `mapName`, `gameActive`)
Values('seans game', 'Small', true),
	  ('seans second game', 'Medium', false),
      ('Todds game', 'Large', false);

INSERT INTO tblTile(`GameName`, `xValue`, `yValue` , `isVisible`, `number`)
  Values('seans game', '1', '1', false, 1),
		('seans game', '2', '1', true, 2),
		('Todds game', '1', '1', true, null);
 
 INSERT INTO tblTileBomb(`GameName`, `xValue`, `yValue` , `damage`)
  Values('seans game', '1', '1', 20),
		('seans game', '2', '1', 30),
		('Todds game', '1', '1', 5);
        

INSERT INTO tblAsset(`name`, `effect`)
  Values('Binoculars', 'Clear Tiles'),
		('Gold', 'Add Score'),
        ('Club', 'Steal Score');
        

INSERT INTO tblTileItem(`itemName`, `gameName`, `xValue`, `yValue`)
  Values('Binoculars', 'seans game', '1', '1'),
		('Gold', 'seans game', '2', '1'),
		('Club', 'Todds game', '1', '1');
        
INSERT INTO tblGamePlayer(`userName`, `gameName`, `tileXValue`, `tileYValue`, `score`, `isActive`, `colour`)
  Values('Sean', 'seans game', 1, 1, 100, true, 'red'),
		('Todd', 'seans game', 2, 1, 200, true, 'yellow'),
        ('Sarah', 'Todds Game', 1, 1, 0, false, 'blue');

INSERT INTO tblChat(`userName`, `text`)
  Values('Sean', 'hello'),
		('Todd', 'hello 2');
	
INSERT INTO tblPlayerItem(`userName`, `gameName`, `itemName`)
  Values('Sean', 'seans game', 'Binoculars'),
		('Todd','seans game', 'Gold'),
        ('Sarah','Todds Game', 'Club');
       
/* UPDATE STATEMENTS */

UPDATE tblPlayer
	SET userName = 'Shaun'
	WHERE userName = 'Sean';

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

/*SELECT STATEMENTS*/

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

/*DELETE STATEMENTS*/

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












        
        