USE festivaldb;

-- Drop Tables if they exist
DROP TABLE IF EXISTS DagList;
DROP TABLE IF EXISTS Foto;
DROP TABLE IF EXISTS Artikel;
DROP TABLE IF EXISTS SponsorLijst;
DROP TABLE IF EXISTS TruckList;
DROP TABLE IF EXISTS Sponsor;
DROP TABLE IF EXISTS MenuItem;
DROP TABLE IF EXISTS FoodTruck;
DROP TABLE IF EXISTS Ticket;
DROP TABLE IF EXISTS Tickettype;
DROP TABLE IF EXISTS Tijdstip;
DROP TABLE IF EXISTS Dag;
DROP TABLE IF EXISTS Podium;
DROP TABLE IF EXISTS Locatie;
DROP TABLE IF EXISTS Artiest;
DROP TABLE IF EXISTS Genre;
DROP TABLE IF EXISTS Editie;

-- Step 1: Create the Database
CREATE DATABASE IF NOT EXISTS festivaldb;
USE festivaldb;

-- Create tables
-- Create Editie (Edition) table
CREATE TABLE Editie (
    editieId CHAR(36) PRIMARY KEY,            -- GUID type as CHAR(36)
    editieNaam VARCHAR(100) NOT NULL,         -- EditieNaam as a string (not nullable)
    adres VARCHAR(100) NOT NULL,              -- Adres as a string (not nullable)
    postcode VARCHAR(4) NOT NULL,             -- Postcode as a string (not nullable)
    gemeente VARCHAR(100) NOT NULL,           -- Gemeente as a string (not nullable)
    telNr VARCHAR(12) NOT NULL,               -- TelNr as a string (not nullable)
    email VARCHAR(50) NOT NULL,               -- Email as a string (not nullable)
    jaar INT NOT NULL                         -- Jaar as an integer (not nullable)
);


-- Create Genre table
CREATE TABLE Genre (
    genreId CHAR(36) PRIMARY KEY,          -- GUID type for the GenreId
    naam VARCHAR(255) NOT NULL             -- Naam (Name) as a string (not nullable)
);


-- Create Artiest (Artist) table (adjusted for GenreId relationship)
CREATE TABLE Artiest (
    artiestId CHAR(36) PRIMARY KEY,             -- GUID for ArtiestId
    naam VARCHAR(255) NOT NULL,                 -- Name of the artist
    email VARCHAR(255) NOT NULL,                -- Email of the artist
    beschrijving VARCHAR(255) NOT NULL,         -- Description of the artist
    spotifyApi VARCHAR(255),                    -- Spotify API link (nullable)
    genreId CHAR(36),                           -- Foreign key to Genre table
    FOREIGN KEY (genreId) REFERENCES Genre(genreId)   -- Foreign key constraint
);


-- Create Locatie table
CREATE TABLE Locatie (
    locatieId CHAR(36) PRIMARY KEY,         -- GUID as primary key
    naam VARCHAR(255) NOT NULL,             -- Location name (not nullable)
    coordinaten VARCHAR(255) NOT NULL      -- Coordinates (not nullable)
);

-- Create Podium table
CREATE TABLE Podium (
    podiumId CHAR(36) PRIMARY KEY,        -- GUID as the primary key for PodiumId
    naam VARCHAR(255) NOT NULL,           -- Name of the stage (not nullable)
    locationId CHAR(36),                  -- Foreign key to the Locatie table
    FOREIGN KEY (locationId) REFERENCES Locatie(locatieId) -- Foreign key constraint to Locatie
);


-- Create Dag (Day) table
CREATE TABLE Dag (
    dagId CHAR(36) PRIMARY KEY,          -- GUID type as CHAR(36)
    naam VARCHAR(255),                   -- Naam is nullable (optional field)
    startDatum DATETIME NOT NULL,        -- StartDatum is required (DATETIME type)
    eindDatum DATETIME NOT NULL          -- EindDatum is required (DATETIME type)
);


-- Create TijdStip table
CREATE TABLE TijdStip (
    tijdStipId CHAR(36) PRIMARY KEY,            -- Primary Key for TijdStip
    tijd DATETIME NOT NULL,                     -- The date and time of the event
    artiestId CHAR(36),                         -- Foreign Key to Artiest (Artist)
    podiumId CHAR(36),                          -- Foreign Key to Podium (Stage)
    FOREIGN KEY (artiestId) REFERENCES Artiest(artiestId),  -- Foreign Key constraint to Artiest
    FOREIGN KEY (podiumId) REFERENCES Podium(podiumId)      -- Foreign Key constraint to Podium
);


-- Create TicketType table
CREATE TABLE TicketType (
    ticketTypeId CHAR(36) PRIMARY KEY,       -- GUID as the primary key for TicketTypeId
    naam VARCHAR(255) NOT NULL               -- The name of the ticket type (e.g., VIP, General Admission)
);


-- Create Ticket table
CREATE TABLE Ticket (
    ticketId CHAR(36) PRIMARY KEY,                 -- GUID as the primary key for TicketId
    prijs DECIMAL(18,2) NOT NULL,                  -- Price of the ticket (decimal type)
    firstName VARCHAR(255) NOT NULL,               -- First name of the ticket holder
    lastName VARCHAR(255) NOT NULL,                -- Last name of the ticket holder
    telNr VARCHAR(50),                             -- Phone number of the ticket holder
    email VARCHAR(255),                            -- Email of the ticket holder
    editieId CHAR(36),                             -- Foreign key to Editie (Edition)
    ticketTypeId CHAR(36),                         -- Foreign key to TicketType
    dagId CHAR(36),                                -- Foreign key to Dag (Day)
    FOREIGN KEY (editieId) REFERENCES Editie(editieId),      -- Foreign key constraint to Editie
    FOREIGN KEY (ticketTypeId) REFERENCES TicketType(ticketTypeId),  -- Foreign key constraint to TicketType
    FOREIGN KEY (dagId) REFERENCES Dag(dagId)               -- Foreign key constraint to Dag
);


-- Create DagList (Day List) table
CREATE TABLE DagList (
    ticketId CHAR(36),
    dagId CHAR(36),
    PRIMARY KEY (ticketId, dagId),
    FOREIGN KEY (ticketId) REFERENCES Ticket(ticketId),
    FOREIGN KEY (dagId) REFERENCES Dag(dagId)
);

-- Create FoodTruck table
CREATE TABLE FoodTruck (
    foodTruckId CHAR(36) PRIMARY KEY,      -- GUID type as CHAR(36)
    naam VARCHAR(255) NOT NULL,            -- Naam as a string (not nullable)
    locatieId CHAR(36) NOT NULL,           -- LocatieId as a GUID (foreign key)
    FOREIGN KEY (locatieId) REFERENCES Locatie(locatieId)  -- Foreign key reference to Locatie table
);

-- Create TruckList table
CREATE TABLE TruckList (
    foodTruckId CHAR(36),              -- Foreign key to FoodTruck
    editieId CHAR(36),                 -- Foreign key to Editie
    PRIMARY KEY (foodTruckId, editieId),  -- Composite primary key
    FOREIGN KEY (foodTruckId) REFERENCES FoodTruck(foodTruckId),  -- Foreign key reference to FoodTruck
    FOREIGN KEY (editieId) REFERENCES Editie(editieId)             -- Foreign key reference to Editie
);

-- Create MenuItem table
CREATE TABLE MenuItem (
    menuItemId CHAR(36) PRIMARY KEY,        -- GUID as primary key for MenuItemId
    naam VARCHAR(255) NOT NULL,             -- Name of the menu item (not nullable)
    prijs DECIMAL(10, 2) NOT NULL,          -- Price of the menu item (not nullable)
    foodTruckId CHAR(36),                   -- Foreign key to the FoodTruck table
    FOREIGN KEY (foodTruckId) REFERENCES FoodTruck(foodTruckId) -- Foreign key constraint to FoodTruck
);


-- Create Sponsor table
CREATE TABLE Sponsor (
    sponsorId CHAR(36) PRIMARY KEY,           -- GUID as the primary key for SponsorId
    sponsorNaam VARCHAR(255) NOT NULL,        -- Name of the sponsor (not nullable)
    hoeveelheid INT,                          -- Amount contributed by the sponsor
    gesponsordeItem VARCHAR(255)            -- Sponsored item description
);

-- Create SponsorLijst (Sponsor List) table
CREATE TABLE SponsorLijst (
    sponsorId CHAR(36),                      -- Foreign key to Sponsor table
    editieId CHAR(36),                       -- Foreign key to Editie table
    PRIMARY KEY (sponsorId, editieId),       -- Composite primary key
    FOREIGN KEY (sponsorId) REFERENCES Sponsor(sponsorId),  -- Foreign key constraint to Sponsor
    FOREIGN KEY (editieId) REFERENCES Editie(editieId)       -- Foreign key constraint to Editie
);


-- Create Artikel (News Article) table
CREATE TABLE Artikel (
    artikelId CHAR(36) PRIMARY KEY,           -- GUID type as CHAR(36)
    titel VARCHAR(255) NOT NULL,              -- Titel is required
    beschrijving TEXT,                        -- Beschrijving is nullable (TEXT type)
    datum DATETIME NOT NULL,                  -- Datum is required and uses DATETIME
    editieId CHAR(36),                        -- EditieId is a GUID (CHAR(36))
    FOREIGN KEY (editieId) REFERENCES Editie(editieId)  -- Foreign key referencing the Editie table
);


-- Create Foto (Photo) table
CREATE TABLE Foto (
    fotoId CHAR(36) PRIMARY KEY,           -- GUID type as CHAR(36) for primary key
    bestand VARCHAR(255) NOT NULL,         -- Bestand (File) as a string (not nullable)
    beschrijving TEXT,                     -- Beschrijving (Description) as text (nullable)
    editieId CHAR(36) NOT NULL,            -- Foreign key to Editie (Edition)
    artikelId CHAR(36) NOT NULL,           -- Foreign key to Artikel (Article)
    podiumId CHAR(36) NOT NULL,            -- Foreign key to Podium (Stage)
    FOREIGN KEY (editieId) REFERENCES Editie(editieId),   -- Foreign key to Editie
    FOREIGN KEY (artikelId) REFERENCES Artikel(artikelId), -- Foreign key to Artikel
    FOREIGN KEY (podiumId) REFERENCES Podium(podiumId)     -- Foreign key to Podium
);

-- Insert a new Edition (Editie)
INSERT INTO Editie (editieId, editieNaam, adres, postcode, gemeente, telNr, email, jaar)
VALUES 
(UUID(), 'FritFest', '123 Festival Lane', '1000', 'Brussels', '020123456', 'info@fritfest.be', 2024);


-- Insert genres into the Genre table
INSERT INTO Genre (genreId, naam)
VALUES 
(UUID(), 'Rock'),
(UUID(), 'Jazz'),
(UUID(), 'Pop'),
(UUID(), 'Electronic'),
(UUID(), 'Hip-Hop');

-- Insert Locations
-- Insert locations into the Locatie table
INSERT INTO Locatie (locatieId, naam, coordinaten)
VALUES 
(UUID(), 'S1', '50.8503° N, 4.3517° E'),
(UUID(), 'S2', '50.8476° N, 4.3572° E'),
(UUID(), 'T1', '50.8457° N, 4.3500° E');

-- Insert Stages
-- Insert podiums into the Podium table
-- Insert podium (stage) into the Podium table
INSERT INTO Podium (podiumId, naam, locationId)
VALUES 
(UUID(), 'Main Stage', (SELECT locatieId FROM Locatie WHERE naam = 'S1')),
(UUID(), 'Side Stage', (SELECT locatieId FROM Locatie WHERE naam = 'S2')),
(UUID(), 'Electronic Arena', (SELECT locatieId FROM Locatie WHERE naam = 'S3'));

-- Insert a new Day (Dag)
INSERT INTO Dag (dagId, naam, startDatum, eindDatum)
VALUES 
(UUID(), 'Day 1', '2024-10-18 10:00:00', '2024-10-18 23:59:59');

-- Insert artists into the Artiest table
INSERT INTO Artiest (artiestId, naam, email, beschrijving, genreId)
VALUES 
(UUID(), 'The Rolling Stones', 'contact@rollingstones.com', 'Rock band from the UK', 
    (SELECT genreId FROM Genre WHERE naam = 'Rock')),
(UUID(), 'Miles Davis', 'milesd@jazz.com', 'Famous jazz trumpeter', 
    (SELECT genreId FROM Genre WHERE naam = 'Jazz')),
(UUID(), 'Taylor Swift', 'taylorswift@popstar.com', 'Pop music superstar', 
    (SELECT genreId FROM Genre WHERE naam = 'Pop'));


-- Insert Time Slots
-- Insert into TijdStip table
INSERT INTO TijdStip (tijdStipId, tijd, artiestId, podiumId)
VALUES 
(UUID(), '2024-11-06 18:30:00', 
 (SELECT artiestId FROM Artiest WHERE naam = 'Artist Name'), 
 (SELECT podiumId FROM Podium WHERE naam = 'Main Stage'));

-- Insert into TicketType table
INSERT INTO TicketType (ticketTypeId, naam) 
VALUES 
(UUID(), 'VIP'),
(UUID(), 'General Admission');

-- Insert into Ticket table
INSERT INTO Ticket (ticketId, prijs, firstName, lastName, telNr, email, editieId, ticketTypeId, dagId)
VALUES 
(UUID(), 99.99, 'John', 'Doe', '123-456-7890', 'john.doe@example.com', 
 (SELECT editieId FROM Editie WHERE editieNaam = 'FritFest'), 
 (SELECT ticketTypeId FROM TicketType WHERE naam = 'VIP'),
 (SELECT dagId FROM Dag WHERE naam = 'Day 1'));


-- Insert a new FoodTruck
INSERT INTO FoodTruck (foodTruckId, naam, locatieId)
VALUES 
(UUID(), 'Burger King', (SELECT locatieId FROM Locatie WHERE naam = 'T1')),
(UUID(), 'Pizza Hut', (SELECT locatieId FROM Locatie WHERE naam = 'T1'));

-- Insert menu items into the MenuItem table
INSERT INTO MenuItem (menuItemId, naam, prijs, foodTruckId)
VALUES 
(UUID(), 'Cheeseburger', 5.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Burger King')),
(UUID(), 'Pizza', 8.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Pizza Hut'));


-- Insert sponsors into the Sponsor table
INSERT INTO Sponsor (sponsorId, sponsorNaam, hoeveelheid, gesponsordeItem)
VALUES 
(UUID(), 'CocaCola', 10000, 'Drink Sponsorship'),
(UUID(), 'Heineken', 15000, 'Beer Sponsorship');


-- Insert sponsor-Editie associations into SponsorLijst table
INSERT INTO SponsorLijst (sponsorId, editieId)
VALUES 
((SELECT sponsorId FROM Sponsor WHERE sponsorNaam = 'CocaCola'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest')),
((SELECT sponsorId FROM Sponsor WHERE sponsorNaam = 'Heineken'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'));

-- Insert Trucks into TruckList (Food Trucks for Fritfest)
INSERT INTO TruckList (foodTruckId, editieId) VALUES
((SELECT foodTruckId FROM FoodTruck WHERE naam = 'Burger King'), (SELECT editieId FROM Editie WHERE editieNaam = 'FritFest')),
((SELECT foodTruckId FROM FoodTruck WHERE naam = 'Pizza Hut'), (SELECT editieId FROM Editie WHERE editieNaam = 'FritFest'));

-- Insert an Article
INSERT INTO Artikel (artikelId, titel, beschrijving, datum, editieId) 
VALUES 
(UUID(), 'Festival Opening Ceremony', 'Description of the opening ceremony...', '2024-10-17 10:00:00', 
 (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'));


-- Insert Photos related to Fritfest
-- Insert into Foto table
INSERT INTO Foto (fotoId, bestand, beschrijving, editieId, artikelId, podiumId)
VALUES
(UUID(), 'foto1.jpg', 'Photo of the main stage during the festival', 
    (SELECT editieId FROM Editie WHERE editieNaam = 'FritFest'), 
    (SELECT artikelId FROM Artikel WHERE titel = 'Festival Opening Ceremony'), 
    (SELECT podiumId FROM Podium WHERE naam = 'Main Stage'));


-- Verify Data with Select Queries
-- SELECT * FROM Genre;
-- SELECT * FROM Artiest;
-- SELECT * FROM Podium;
-- SELECT * FROM Tijdstip;
-- SELECT * FROM Dag;
-- SELECT * FROM Tickettype;
-- SELECT * FROM Ticket;
-- SELECT * FROM DagList;
