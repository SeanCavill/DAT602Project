DROP TABLE IF EXISTS PlayerItem;
DROP TABLE IF EXISTS GamePlayer;
DROP TABLE IF EXISTS Message;
DROP TABLE IF EXISTS Player;
DROP TABLE IF EXISTS Game;
DROP TABLE IF EXISTS TileItem;
DROP TABLE IF EXISTS Tile;
DROP TABLE IF EXISTS Asset;



CREATE TABLE Player (
  Username      varchar(10) NOT NULL, 
  Password      varchar(20), 
  LockedStatus  bit(1), 
  Administrator bit(1), 
  OnlineStatus  bit(1), 
  LoginAttempts int(1), 
  PRIMARY KEY (Username));
  

		
CREATE TABLE Game (
  GameID     int(10) NOT NULL AUTO_INCREMENT, 
  GameName   varchar(20), 
  GameActive bit(1), 
  PRIMARY KEY (GameID));
  
  CREATE TABLE Message (
  MessageID int(10) NOT NULL AUTO_INCREMENT, 
  GameID    int(10) NOT NULL, 
  Username  varchar(10) NOT NULL, 
  Text      varchar(255),
  PRIMARY KEY (MessageID));
  
   
        
CREATE TABLE Tile (
  Location int(4) NOT NULL AUTO_INCREMENT, 
  PRIMARY KEY (Location));
  
  CREATE TABLE GamePlayer (
  GameID       int(10) NOT NULL, 
  Username     varchar(10) NOT NULL, 
  Color        varchar(10), 
  Score        int(5), 
  TileLocation int(4) NOT NULL, 
  PRIMARY KEY (GameID, Username));
 
  
CREATE TABLE TileItem (
  TileLocation  int(4) NOT NULL, 
  AssetItemName varchar(10) NOT NULL,
  PRIMARY KEY (TileLocation, 
  AssetItemName));
  
  
        
CREATE TABLE Asset (
  ItemName varchar(10) NOT NULL, 
  Effect   varchar(10), 
  PRIMARY KEY (ItemName));
  

		
  
CREATE TABLE PlayerItem (
  AssetItemName      varchar(10) NOT NULL, 
  GamePlayerGameID   int(10) NOT NULL, 
  GamePlayerUsername varchar(10) NOT NULL,
  PRIMARY KEY (AssetItemName, 
  GamePlayerGameID, 
  GamePlayerUsername));
  
ALTER TABLE GamePlayer ADD FOREIGN KEY (GameID) REFERENCES Game (GameID);
ALTER TABLE GamePlayer ADD FOREIGN KEY (Username) REFERENCES Player (Username);
ALTER TABLE GamePlayer ADD FOREIGN KEY (TileLocation) REFERENCES Tile (Location);
ALTER TABLE TileItem ADD  FOREIGN KEY (TileLocation) REFERENCES Tile (Location);
ALTER TABLE TileItem ADD FOREIGN KEY (AssetItemName) REFERENCES Asset (ItemName);
ALTER TABLE PlayerItem ADD FOREIGN KEY (AssetItemName) REFERENCES Asset (ItemName);
ALTER TABLE PlayerItem ADD FOREIGN KEY (GamePlayerGameID) REFERENCES GamePlayer (GameID);
ALTER TABLE PlayerItem ADD FOREIGN KEY (GamePlayerUsername) REFERENCES GamePlayer (Username);
ALTER TABLE Message ADD FOREIGN KEY (GameID) REFERENCES Game (GameID);
ALTER TABLE Message ADD FOREIGN KEY (Username) REFERENCES Player (Username);

INSERT INTO Tile(Location)
  Values(1),
		(2),
		(9);

INSERT INTO Game(GameName, GameActive)
Values('seans game', 1),
	  ('seans second game', 1),
      ('Todds game', 0);
  
INSERT INTO Asset(ItemName, Effect)
  Values('bomb', -10),
		('Add-score', 10);
        
INSERT INTO Player(username, Password, LockedStatus, Administrator, OnlineStatus, LoginAttempts)
  Values('Sean', 'Password1', 0, 1, 0, 0),
		('Todd', 'Password2', 0, 0, 0, 0),
        ('Sarah', 'Password3', 0, 1, 0, 0);


INSERT INTO TileItem(TileLocation, AssetItemName)
  Values(1, 'bomb'),
		(2, 'bomb'),
		(9, 'Add-score');
        
INSERT INTO GamePlayer(GameID, Username, Color, Score, TileLocation)
  Values(1, 'Sean', 'red', 5, 1),
		(2, 'Todd', 'yellow', 0, 9);

INSERT INTO Message(GameID, Username, Text)
  Values(1, 'Sean', 'hello'),
		(2, 'Todd', 'hello 2');
	
INSERT INTO PlayerItem(AssetItemName, GamePlayerGameID, GamePlayerUsername)
  Values('bomb', 1, 'Sean'),
		('bomb', 2, 'Sean'),
        ('bomb', 1, 'Todd'),
		('Add-score', 1, 'Sean');
        


SELECT* FROM asset;
SELECT* FROM game;
SELECT* FROM gameplayer;
SELECT* FROM message;
SELECT* FROM player;
SELECT* FROM playeritem;
SELECT* FROM tile;
SELECT* FROM tileitem;

        
        