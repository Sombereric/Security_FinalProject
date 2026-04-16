CREATE DATABASE mistGamingServices
GO

USE mistGamingServices
GO

--Tables
CREATE TABLE Users(
	UserID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UserName VARCHAR(50) NOT NULL,
    UserEmail VARCHAR(255) NOT NULL UNIQUE,
    UserPasswordHash VARCHAR(255) NOT NULL --hashed first not stored in plain text
);

CREATE TABLE Publishers(
	PublisherID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	PublisherName VARCHAR(100) NOT NULL,
	PublisherRating DECIMAL(3,1),
	CONSTRAINT CHK_PublisherRating CHECK (PublisherRating BETWEEN 1 AND 10)
);

CREATE TABLE GamesInApp(
	GameID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	GameName VARCHAR(100) NOT NULL,
	GamePrice DECIMAL(10,2) NOT NULL,
	PublisherID INT NOT NULL,
	GameRating DECIMAL(3,1),
	GameGenre VARCHAR(50),
	CONSTRAINT FK_PublishID FOREIGN KEY (PublisherID)
        REFERENCES Publishers(PublisherID),
	CONSTRAINT CHK_GameRating CHECK (GameRating BETWEEN 1 AND 10)
);

CREATE TABLE UserOwnedGames(
	GameID INT NOT NULL,
	UserID INT NOT NULL,
	DateAdded DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_GameID FOREIGN KEY (GameID)
        REFERENCES GamesInApp(GameID),
    CONSTRAINT FK_UserOwnedGames_UserID FOREIGN KEY (UserID)
        REFERENCES Users(UserID),
	PRIMARY KEY (GameID, UserID)
);

--this would be handled by payment providers like banks or credit unions.
--for this project we will use these but in real systems you wouldn't have this
CREATE TABLE PaymentMethods(
	PaymentMethodID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	UserID INT NOT NULL,
	CardHolderName VARCHAR(100) NOT NULL,
	CardNumber VARCHAR(19) NOT NULL,
	CardSecurityNumber VARCHAR(3) NOT NULL,
	CardExpireDate DATE NOT NULL,
	CONSTRAINT FK_PaymentMethods_UserID FOREIGN KEY (UserID)
        REFERENCES Users(UserID)
);

CREATE TABLE Logs(
	LogsID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UserID INT,
	LogDate DATE NOT NULL,
	LogType VARCHAR(50),
	LogInformation VARCHAR(200),
	CONSTRAINT FK_Logs_UserID FOREIGN KEY (UserID)
        REFERENCES Users(UserID)
);

--note please replaced plain text passwords with hashed passwords
INSERT INTO Users (UserName, UserEmail, UserPasswordHash) VALUES
('alice', 'alice@example.com', 'hashed_password1'),
('bob', 'bob@example.com', 'hashed_password2'),
('charlie', 'charlie@example.com', 'hashed_password3');

INSERT INTO Publishers (PublisherName, PublisherRating) VALUES
('Valve', 9.5),
('Ubisoft', 7.8),
('EA', 6.5),
('Indie Dev Studio', 8.9);

INSERT INTO GamesInApp (GameName, GamePrice, PublisherID, GameRating, GameGenre) VALUES
('Half-Life 3', 59.99, 1, 9.8, 'FPS'),
('Portal 3', 39.99, 1, 9.6, 'Puzzle'),
('Assassin''s Creed X', 69.99, 2, 8.2, 'Action'),
('FIFA 26', 59.99, 3, 7.0, 'Sports'),
('Indie Adventure', 19.99, 4, 8.7, 'Adventure');

INSERT INTO UserOwnedGames (GameID, UserID) VALUES
(1, 1), -- Alice owns Half-Life 3
(2, 1), -- Alice owns Portal 3
(3, 2), -- Bob owns Assassin's Creed
(5, 3); -- Charlie owns Indie Adventure

INSERT INTO PaymentMethods (UserID, CardHolderName, CardNumber, CardSecurityNumber, CardExpireDate) VALUES
(1, 'Alice Smith', '1234567812345678', '123', '2027-05-01'),
(2, 'Bob Johnson', '8765432187654321', '456', '2026-08-01'),
(3, 'Charlie Brown', '1111222233334444', '789', '2028-01-01');