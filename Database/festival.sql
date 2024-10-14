Drop Table if exists DagList;
Drop Table if exists Dag;
Drop Table if exists Ticket;
Drop Table if exists Tijdstip;
Drop Table if exists Tickettype;
Drop Table if exists Podium;
Drop Table if exists Artiest;
Drop Table if exists Genre;

-- Step 1: Create the Database
CREATE DATABASE IF NOT EXISTS FestivalDB;
USE FestivalDB;

-- Step 2: Create the Genre Table
CREATE TABLE Genre (
    genreId INT AUTO_INCREMENT PRIMARY KEY,
    naam VARCHAR(100) NOT NULL
);

-- Step 3: Create the Artiest (Artist) Table
CREATE TABLE Artiest (
    artiestId INT AUTO_INCREMENT PRIMARY KEY,
    naam VARCHAR(100) NOT NULL,
    email VARCHAR(100),
    genreId INT,
    FOREIGN KEY (genreId) REFERENCES Genre(genreId) ON DELETE SET NULL
);

-- Step 4: Create the Podium (Stage) Table
CREATE TABLE Podium (
    podiumId INT AUTO_INCREMENT PRIMARY KEY,
    naam VARCHAR(100) NOT NULL,
    coordinaten VARCHAR(100)
);

-- Step 5: Create the Tijdstip (Time Slot) Table
CREATE TABLE Tijdstip (
    tijdstipId INT AUTO_INCREMENT PRIMARY KEY,
    tijdstip DATETIME NOT NULL,
    artiestId INT,
    podiumId INT,
    FOREIGN KEY (artiestId) REFERENCES Artiest(artiestId) ON DELETE CASCADE,
    FOREIGN KEY (podiumId) REFERENCES Podium(podiumId) ON DELETE CASCADE
);

-- Step 6: Create the Dag (Day) Table
CREATE TABLE Dag (
    dagId INT AUTO_INCREMENT PRIMARY KEY,
    date DATE NOT NULL
);

-- Step 7: Create the Tickettype (Ticket Type) Table
CREATE TABLE Tickettype (
    typeId INT AUTO_INCREMENT PRIMARY KEY,
    naam VARCHAR(100) NOT NULL
);

-- Step 8: Create the Ticket Table
CREATE TABLE Ticket (
    ticketId INT AUTO_INCREMENT PRIMARY KEY,
    prijs DECIMAL(10,2) NOT NULL,
    typeId INT,
    firstName VarChar(50),
	lastName VarChar(50),
	telephoneNumber VarChar(50),
	email VarChar(50),
    FOREIGN KEY (typeId) REFERENCES Tickettype(typeId) ON DELETE SET NULL
);

-- Step 9: Create the DagList (Day List) Table
CREATE TABLE DagList (
    ticketListId INT AUTO_INCREMENT PRIMARY KEY,
    ticketId INT,
    dagId INT,
    FOREIGN KEY (ticketId) REFERENCES Ticket(ticketId) ON DELETE CASCADE,
    FOREIGN KEY (dagId) REFERENCES Dag(dagId) ON DELETE CASCADE
);

-- Insert Genres
INSERT INTO Genre (naam) VALUES 
('Rock'), 
('Jazz'), 
('Pop'), 
('Electronic'), 
('Hip-Hop');

-- Insert Artists
INSERT INTO Artiest (naam, email, genreId) VALUES 
('The Rolling Stones', 'contact@rollingstones.com', 1), 
('Miles Davis', 'milesd@jazz.com', 2), 
('Taylor Swift', 'taylorswift@popstar.com', 3), 
('Daft Punk', 'info@daftpunk.com', 4), 
('Kanye West', 'kanye@hiphop.com', 5);

-- Insert Stages
INSERT INTO Podium (naam, coordinaten) VALUES 
('Main Stage', '50.8503° N, 4.3517° E'), 
('Side Stage', '50.8476° N, 4.3572° E'), 
('Electronic Arena', '50.8457° N, 4.3500° E');

-- Insert Days
INSERT INTO Dag (date) VALUES 
('2024-10-18'), 
('2024-10-19'), 
('2024-10-20');

-- Insert Time Slots (Tijdstip)
INSERT INTO Tijdstip (tijdstip, artiestId, podiumId) VALUES 
('2024-10-18 14:00:00', 1, 1),  -- Rolling Stones on Main Stage
('2024-10-18 18:00:00', 2, 2),  -- Miles Davis on Side Stage
('2024-10-19 16:30:00', 3, 1),  -- Taylor Swift on Main Stage
('2024-10-19 21:00:00', 4, 3),  -- Daft Punk in Electronic Arena
('2024-10-20 20:00:00', 5, 1);  -- Kanye West on Main Stage

-- Insert Ticket Types
INSERT INTO Tickettype (naam) VALUES 
('Day Pass'), 
('Weekend Pass'), 
('VIP Pass');

-- Insert Tickets
INSERT INTO Ticket (prijs, typeId, firstName, lastName, telephoneNumber, email) VALUES
(50.00, 1, 'Arno', 'Van Haecke', '1234567890', 'arno.vh@example.com'), -- Day Pass
(90.00, 2, 'Steffy Sandra Monique', 'Meylaers', '9876543210', 'steffy.m@example.com'), -- Weekend Pass
(150.00, 3, 'Wesley Johan A Flopper', 'Meylaers', '5551234567', 'wesley.m@example.com'), -- VIP Pass
(50.00, 1, 'Chocoprins', 'Joris', '4445556666', 'choco.joris@example.com'), -- Day Pass
(90.00, 2, 'Emiel', 'De Pedofiel', '1112223333', 'emiel.p@example.com'); -- Weekend Pass


-- Insert Ticket-Day Associations (DagList)
-- Day Pass valid only for 18th October
INSERT INTO DagList (ticketId, dagId) VALUES 
(1, 1);

-- Weekend Pass valid for both 18th and 19th October
INSERT INTO DagList (ticketId, dagId) VALUES 
(2, 1), 
(2, 2);

-- VIP Pass valid for all 3 days
INSERT INTO DagList (ticketId, dagId) VALUES 
(3, 1), 
(3, 2), 
(3, 3);

-- Verify Data with Select Queries
SELECT * FROM Genre;
SELECT * FROM Artiest;
SELECT * FROM Podium;
SELECT * FROM Tijdstip;
SELECT * FROM Dag;
SELECT * FROM Tickettype;
SELECT * FROM Ticket;
SELECT * FROM DagList;