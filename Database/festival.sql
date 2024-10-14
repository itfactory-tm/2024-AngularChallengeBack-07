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
    genreId Char(36) Not Null PRIMARY KEY,
    naam VARCHAR(100) NOT NULL
);

-- Step 3: Create the Artiest (Artist) Table
CREATE TABLE Artiest (
    artiestId Char(36) Not Null PRIMARY KEY,
    naam VARCHAR(100) NOT NULL,
    email VARCHAR(100),
    genreId Char(36),
    FOREIGN KEY (genreId) REFERENCES Genre(genreId) ON DELETE SET NULL
);

-- Step 4: Create the Podium (Stage) Table
CREATE TABLE Podium (
    podiumId Char(36) Not Null PRIMARY KEY,
    naam VARCHAR(100) NOT NULL,
    coordinaten VARCHAR(100)
);

-- Step 5: Create the Tijdstip (Time Slot) Table
CREATE TABLE Tijdstip (
    tijdstipId Char(36) Not Null PRIMARY KEY,
    tijdstip DATETIME NOT NULL,
    artiestId Char(36),
    podiumId Char(36),
    FOREIGN KEY (artiestId) REFERENCES Artiest(artiestId) ON DELETE CASCADE,
    FOREIGN KEY (podiumId) REFERENCES Podium(podiumId) ON DELETE CASCADE
);

-- Step 6: Create the Dag (Day) Table
CREATE TABLE Dag (
    dagId Char(36) Not Null PRIMARY KEY,
    date DATE NOT NULL
);

-- Step 7: Create the Tickettype (Ticket Type) Table
CREATE TABLE Tickettype (
    typeId Char(36) Not Null PRIMARY KEY,
    naam VARCHAR(100) NOT NULL
);

-- Step 8: Create the Ticket Table
CREATE TABLE Ticket (
    ticketId Char(36) Not Null PRIMARY KEY,
    prijs DECIMAL(10,2) NOT NULL,
    typeId Char(36),
    firstName VarChar(50),
	lastName VarChar(50),
	telephoneNumber VarChar(50),
	email VarChar(50),
    FOREIGN KEY (typeId) REFERENCES Tickettype(typeId) ON DELETE SET NULL
);

-- Step 9: Create the DagList (Day List) Table
CREATE TABLE DagList (
    ticketListId Char(36) Not Null PRIMARY KEY,
    ticketId Char(36),
    dagId Char(36),
    FOREIGN KEY (ticketId) REFERENCES Ticket(ticketId) ON DELETE CASCADE,
    FOREIGN KEY (dagId) REFERENCES Dag(dagId) ON DELETE CASCADE
);

-- Insert Genres
INSERT INTO Genre (naam) VALUES 
(UUID(), 'Rock'), 
(UUID(), 'Jazz'), 
(UUID(), 'Pop'), 
(UUID(), 'Electronic'), 
(UUID(), 'Hip-Hop');

-- Insert Artists
INSERT INTO Artiest (naam, email, genreId) VALUES 
(UUID(), 'The Rolling Stones', 'contact@rollingstones.com', 1), 
(UUID(), 'Miles Davis', 'milesd@jazz.com', 2), 
(UUID(), 'Taylor Swift', 'taylorswift@popstar.com', 3), 
(UUID(), 'Daft Punk', 'info@daftpunk.com', 4), 
(UUID(), 'Kanye West', 'kanye@hiphop.com', 5);

-- Insert Stages
INSERT INTO Podium (naam, coordinaten) VALUES 
(UUID(), 'Main Stage', '50.8503° N, 4.3517° E'), 
(UUID(), 'Side Stage', '50.8476° N, 4.3572° E'), 
(UUID(), 'Electronic Arena', '50.8457° N, 4.3500° E');

-- Insert Days
INSERT INTO Dag (date) VALUES 
(UUID(), '2024-10-18'), 
(UUID(), '2024-10-19'), 
(UUID(), '2024-10-20');

-- Insert Time Slots (Tijdstip)
INSERT INTO Tijdstip (tijdstip, artiestId, podiumId) VALUES 
(UUID(), '2024-10-18 14:00:00', 1, 1),  -- Rolling Stones on Main Stage
(UUID(), '2024-10-18 18:00:00', 2, 2),  -- Miles Davis on Side Stage
(UUID(), '2024-10-19 16:30:00', 3, 1),  -- Taylor Swift on Main Stage
(UUID(), '2024-10-19 21:00:00', 4, 3),  -- Daft Punk in Electronic Arena
(UUID(), '2024-10-20 20:00:00', 5, 1);  -- Kanye West on Main Stage

-- Insert Ticket Types
INSERT INTO Tickettype (naam) VALUES 
(UUID(), 'Day Pass'), 
(UUID(), 'Weekend Pass'), 
(UUID(), 'VIP Pass');

-- Insert Tickets
INSERT INTO Ticket (prijs, typeId, firstName, lastName, telephoneNumber, email) VALUES
(UUID(), 50.00, 1, 'Arno', 'Van haecke', '1234567890', 'arno.vh@example.com'), -- Day Pass
(UUID(), 90.00, 2, 'Steffy Sandra Monique', 'Meylaers', '9876543210', 'steffy.m@example.com'), -- Weekend Pass
(UUID(), 150.00, 3, 'Wesley Johan A Flopper', 'Meylaers', '5551234567', 'wesley.m@example.com'), -- VIP Pass
(UUID(), 50.00, 1, 'Chocoprins', 'Joris', '4445556666', 'choco.joris@example.com'), -- Day Pass
(UUID(), 90.00, 2, 'Emiel', 'De Pedofiel', '1112223333', 'emiel.p@example.com'); -- Weekend Pass


-- Insert Ticket-Day Associations (DagList)
-- Day Pass valid only for 18th October
INSERT INTO DagList (ticketId, dagId) VALUES 
(UUID(), 1, 1);

-- Weekend Pass valid for both 18th and 19th October
INSERT INTO DagList (ticketId, dagId) VALUES 
(UUID(), 2, 1), 
(UUID(), 2, 2);

-- VIP Pass valid for all 3 days
INSERT INTO DagList (ticketId, dagId) VALUES 
(UUID(), 3, 1), 
(UUID(), 3, 2), 
(UUID(), 3, 3);-- Insert Genres
INSERT INTO Genre (genreId, naam) VALUES 
('1e5a0336-5052-11ee-b86e-0242ac120002', 'Rock'), 
('1e5a0378-5052-11ee-b86e-0242ac120002', 'Jazz'), 
('1e5a03a4-5052-11ee-b86e-0242ac120002', 'Pop'), 
('1e5a03cc-5052-11ee-b86e-0242ac120002', 'Electronic'), 
('1e5a03e0-5052-11ee-b86e-0242ac120002', 'Hip-Hop');

-- Insert Artists
INSERT INTO Artiest (artiestId, naam, email, genreId) VALUES 
('1e5a0418-5052-11ee-b86e-0242ac120002', 'The Rolling Stones', 'contact@rollingstones.com', '1e5a0336-5052-11ee-b86e-0242ac120002'), 
('1e5a0434-5052-11ee-b86e-0242ac120002', 'Miles Davis', 'milesd@jazz.com', '1e5a0378-5052-11ee-b86e-0242ac120002'), 
('1e5a0450-5052-11ee-b86e-0242ac120002', 'Taylor Swift', 'taylorswift@popstar.com', '1e5a03a4-5052-11ee-b86e-0242ac120002'), 
('1e5a0466-5052-11ee-b86e-0242ac120002', 'Daft Punk', 'info@daftpunk.com', '1e5a03cc-5052-11ee-b86e-0242ac120002'), 
('1e5a047c-5052-11ee-b86e-0242ac120002', 'Kanye West', 'kanye@hiphop.com', '1e5a03e0-5052-11ee-b86e-0242ac120002');

-- Insert Stages
INSERT INTO Podium (podiumId, naam, coordinaten) VALUES 
('1e5a0490-5052-11ee-b86e-0242ac120002', 'Main Stage', '50.8503° N, 4.3517° E'), 
('1e5a04a6-5052-11ee-b86e-0242ac120002', 'Side Stage', '50.8476° N, 4.3572° E'), 
('1e5a04b8-5052-11ee-b86e-0242ac120002', 'Electronic Arena', '50.8457° N, 4.3500° E');

-- Insert Days
INSERT INTO Dag (dagId, date) VALUES 
('1e5a04cc-5052-11ee-b86e-0242ac120002', '2024-10-18'), 
('1e5a04e0-5052-11ee-b86e-0242ac120002', '2024-10-19'), 
('1e5a04f4-5052-11ee-b86e-0242ac120002', '2024-10-20');

-- Insert Time Slots (Tijdstip)
INSERT INTO Tijdstip (tijdstipId, tijdstip, artiestId, podiumId) VALUES 
('1e5a0506-5052-11ee-b86e-0242ac120002', '2024-10-18 14:00:00', '1e5a0418-5052-11ee-b86e-0242ac120002', '1e5a0490-5052-11ee-b86e-0242ac120002'),  -- Rolling Stones on Main Stage
('1e5a0528-5052-11ee-b86e-0242ac120002', '2024-10-18 18:00:00', '1e5a0434-5052-11ee-b86e-0242ac120002', '1e5a04a6-5052-11ee-b86e-0242ac120002'),  -- Miles Davis on Side Stage
('1e5a0540-5052-11ee-b86e-0242ac120002', '2024-10-19 16:30:00', '1e5a0450-5052-11ee-b86e-0242ac120002', '1e5a0490-5052-11ee-b86e-0242ac120002'),  -- Taylor Swift on Main Stage
('1e5a0552-5052-11ee-b86e-0242ac120002', '2024-10-19 21:00:00', '1e5a0466-5052-11ee-b86e-0242ac120002', '1e5a04b8-5052-11ee-b86e-0242ac120002'),  -- Daft Punk in Electronic Arena
('1e5a0568-5052-11ee-b86e-0242ac120002', '2024-10-20 20:00:00', '1e5a047c-5052-11ee-b86e-0242ac120002', '1e5a0490-5052-11ee-b86e-0242ac120002');  -- Kanye West on Main Stage

-- Insert Ticket Types
INSERT INTO Tickettype (typeId, naam) VALUES 
('1e5a0580-5052-11ee-b86e-0242ac120002', 'Day Pass'), 
('1e5a0592-5052-11ee-b86e-0242ac120002', 'Weekend Pass'), 
('1e5a05a8-5052-11ee-b86e-0242ac120002', 'VIP Pass');

-- Insert Tickets
INSERT INTO Ticket (ticketId, prijs, typeId, firstName, lastName, telephoneNumber, email) VALUES
('1e5a05bc-5052-11ee-b86e-0242ac120002', 50.00, '1e5a0580-5052-11ee-b86e-0242ac120002', 'Arno', 'Van haecke', '1234567890', 'arno.vh@example.com'), -- Day Pass
('1e5a05ce-5052-11ee-b86e-0242ac120002', 90.00, '1e5a0592-5052-11ee-b86e-0242ac120002', 'Steffy Sandra Monique', 'Meylaers', '9876543210', 'steffy.m@example.com'), -- Weekend Pass
('1e5a05e0-5052-11ee-b86e-0242ac120002', 150.00, '1e5a05a8-5052-11ee-b86e-0242ac120002', 'Wesley Johan A Flopper', 'Meylaers', '5551234567', 'wesley.m@example.com'), -- VIP Pass
('1e5a05f4-5052-11ee-b86e-0242ac120002', 50.00, '1e5a0580-5052-11ee-b86e-0242ac120002', 'Chocoprins', 'Joris', '4445556666', 'choco.joris@example.com'), -- Day Pass
('1e5a0606-5052-11ee-b86e-0242ac120002', 90.00, '1e5a0592-5052-11ee-b86e-0242ac120002', 'Emiel', 'De Pedofiel', '1112223333', 'emiel.p@example.com'); -- Weekend Pass

-- Insert Ticket-Day Associations (DagList)
-- Day Pass valid only for 18th October
INSERT INTO DagList (ticketId, dagId) VALUES 
('1e5a05bc-5052-11ee-b86e-0242ac120002', '1e5a04cc-5052-11ee-b86e-0242ac120002');  -- Ticket 1 for Day 1

-- Weekend Pass valid for both 18th and 19th October
INSERT INTO DagList (ticketId, dagId) VALUES 
('1e5a05ce-5052-11ee-b86e-0242ac120002', '1e5a04cc-5052-11ee-b86e-0242ac120002'), 
('1e5a05ce-5052-11ee-b86e-0242ac120002', '1e5a04e0-5052-11ee-b86e-0242ac120002');  -- Ticket 2 for Day 1 and Day 2

-- VIP Pass valid for all 3 days
INSERT INTO DagList (ticketId, dagId) VALUES 
('1e5a05e0-5052-11ee-b86e-0242ac120002', '1e5a04cc-5052-11ee-b86e-0242ac120002'), 
('1e5a05e0-5052-11ee-b86e-0242ac120002', '1e5a04e0-5052-11ee-b86e-0242ac120002'), 
('1e5a05e0-5052-11ee-b86e-0242ac120002', '1e5a04f4-5052-11ee-b86e-0242ac120002');  -- Ticket 3 for Day 1, Day 2, and Day 3


-- Verify Data with Select Queries
SELECT * FROM Genre;
SELECT * FROM Artiest;
SELECT * FROM Podium;
SELECT * FROM Tijdstip;
SELECT * FROM Dag;
SELECT * FROM Tickettype;
SELECT * FROM Ticket;
SELECT * FROM DagList;
