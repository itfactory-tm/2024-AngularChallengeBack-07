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
DROP TABLE IF EXISTS GekochteTicket;
DROP TABLE IF EXISTS Ticket;
DROP TABLE IF EXISTS Tickettype;
DROP TABLE IF EXISTS Tijdstip;
DROP TABLE IF EXISTS Dag;
DROP TABLE IF EXISTS Podium;
DROP TABLE IF EXISTS Locatie;
Drop table if Exists ArtiestenLijst;
DROP TABLE IF EXISTS Artiest;
DROP TABLE IF EXISTS Genre;
DROP TABLE IF EXISTS Editie;
DROP TABLE IF EXISTS ArtiestenLijst;


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
    spotifyLink VARCHAR(255),                    -- Spotify API link (nullable)
    apiCode VARCHAR(255),
    genreId CHAR(36),                           -- Foreign key to Genre table
    FOREIGN KEY (genreId) REFERENCES Genre(genreId)   -- Foreign key constraint
);

CREATE TABLE ArtiestenLijst(
                               artiestId CHAR(36),                      -- Foreign key to Sponsor table
                               editieId CHAR(36),                       -- Foreign key to Editie table
                               PRIMARY KEY (artiestId, editieId),       -- Composite primary key
                               FOREIGN KEY (artiestId) REFERENCES Artiest(artiestId),  -- Foreign key constraint to Sponsor
                               FOREIGN KEY (editieId) REFERENCES Editie(editieId)
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
    locatieId CHAR(36),                  -- Foreign key to the Locatie table
    FOREIGN KEY (locatieId) REFERENCES Locatie(locatieId) -- Foreign key constraint to Locatie
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
    naam VARCHAR(255) NOT NULL,               -- The name of the ticket type (e.g., VIP, General Admission)
    prijs double NOT NULL
);

-- Create Ticket table
CREATE TABLE Ticket (
    ticketId CHAR(36) PRIMARY KEY,                 -- GUID as the primary key for TicketId 
    editieId CHAR(36),                             -- Foreign key to Editie (Edition)
    ticketTypeId CHAR(36),                         -- Foreign key to TicketType
    dagId CHAR(36),                                -- Foreign key to Dag (Day)
    FOREIGN KEY (editieId) REFERENCES Editie(editieId),      -- Foreign key constraint to Editie
    FOREIGN KEY (ticketTypeId) REFERENCES TicketType(ticketTypeId),  -- Foreign key constraint to TicketType
    FOREIGN KEY (dagId) REFERENCES Dag(dagId)               -- Foreign key constraint to Dag
);

CREATE TABLE GekochteTicket (
    gekochteTicketId CHAR(36) PRIMARY KEY,                 -- GUID as the primary key for TicketId
    naamVanKoper CHAR(50),
    emailVanKoper CHAR(50),
    naamVanHouder CHAR(50),
    emailVanHouder CHAR(50),   
    ticketId CHAR(36),
    betaald boolean,
    FOREIGN KEY (ticketId) REFERENCES Ticket(ticketId)
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
    bestand VARCHAR(255) NOT NULL,         -- Bestand (file name) as a string
    beschrijving VARCHAR(255) NOT NULL,
    editieId CHAR(36),                     -- Foreign key to Editie table
    FOREIGN KEY (editieId) REFERENCES Editie(editieId),  -- Foreign key constraint to Editie
    artikelId CHAR(36),
    foreign key (artikelId) references Artikel(artikelId),
    podiumId CHAR(36),
    foreign key (podiumId) references Podium(podiumId)
);

-- Insert data into Editie (Edition)
INSERT INTO Editie (editieId, editieNaam, adres, postcode, gemeente, telNr, email, jaar)
VALUES 
(UUID(), 'Fritfest', 'Main Street 123', '1000', 'Brussels', '02-1234567', 'info@fritfest.be', 2024);
INSERT INTO UserType(typeId, naam)
VALUES 
    (UUID(), 'admin'),
    (UUID(), 'user');

-- Insert data into UserType


-- Insert data into Genre
INSERT INTO Genre (genreId, naam)
VALUES
(UUID(), 'Rock'),
(UUID(), 'Pop'),
(UUID(), 'Jazz'),
(UUID(), 'Electronic');

-- Insert data into Artiest (Artist)
INSERT INTO Artiest (artiestId, naam, email, beschrijving, spotifyLink,apiCode ,genreId)
VALUES 
(UUID(), 'The Rockers', 'rockers@music.com', 'A famous rock band', 'spotify.com/therockers','22Wzsyh7moQAwSODsMF6w2' ,(SELECT genreId FROM Genre WHERE naam = 'Rock')),
(UUID(), 'DJ Spin', 'djspin@beats.com', 'A well-known electronic DJ', 'spotify.com/djspin',' 22Wzsyh7moQAwSODsMF6w2',(SELECT genreId FROM Genre WHERE naam = 'Electronic')),
(UUID(), 'PopStar', 'popstar@music.com', 'A pop music sensation', 'spotify.com/popstar','' ,(SELECT genreId FROM Genre WHERE naam = 'Pop')),
(UUID(), 'Jazz Quartet', 'jazzquartet@jazz.com', 'A group of jazz musicians', 'spotify.com/jazzquartet',' ',(SELECT genreId FROM Genre WHERE naam = 'Jazz'));

-- Insert data into Locatie (Location)
INSERT INTO Locatie (locatieId, naam, coordinaten)
VALUES
(UUID(), 'Main Stage', '52.366, 4.904'),
(UUID(), 'Beach Arena', '51.922, 4.481'),
(UUID(), 'Jazz Lounge', '52.364, 4.903');

INSERT INTO Podium (podiumId,naam, locatieId) 
VALUES 
(UUID(), 'Test1',(SELECT locatieId FROM Locatie WHERE naam = 'Main Stage')),
(UUID(),'Test2' ,(SELECT locatieId FROM Locatie WHERE naam = 'Beach Arena')),
(UUID(),'test3' ,(SELECT locatieId FROM Locatie WHERE naam = 'Jazz Lounge'));


-- Insert data into Dag (Day)
INSERT INTO Dag (dagId, naam, startDatum, eindDatum)
VALUES
(UUID(), 'Day 1', '2024-07-01 10:00:00', '2024-07-01 22:00:00'),
(UUID(), 'Day 2', '2024-07-02 10:00:00', '2024-07-02 22:00:00');

-- Insert data into TijdStip (Time Slot)
INSERT INTO TijdStip (tijdStipId, tijd, artiestId, podiumId)
VALUES
(UUID(), '2024-07-01 12:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'The Rockers'), (SELECT podiumId FROM Podium WHERE naam = 'Test1')),
(UUID(), '2024-07-01 14:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'DJ Spin'), (SELECT podiumId FROM Podium WHERE naam = 'Test2')),
(UUID(), '2024-07-02 16:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'PopStar'), (SELECT podiumId FROM Podium WHERE naam = 'Test1')),
(UUID(), '2024-07-02 18:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'Jazz Quartet'), (SELECT podiumId FROM Podium WHERE naam = 'Test3'));

-- Insert data into TicketType
INSERT INTO TicketType (ticketTypeId, naam,prijs)
VALUES
(UUID(), 'General Admission',10.00),
(UUID(), 'VIP',12.00);

-- Insert data into Ticket
INSERT INTO Ticket (ticketId, editieId, ticketTypeId, dagId)
VALUES
(UUID(), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE naam = 'General Admission'), (SELECT dagId FROM Dag WHERE naam = 'Day 1')),
(UUID(), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE naam = 'VIP'), (SELECT dagId FROM Dag WHERE naam = 'Day 2'));

Insert Into GekochteTicket(gekochteTicketId, naamVanKoper, emailVanKoper, naamVanHouder, emailVanHouder, ticketId, betaald)
Values
(uuid(), "Headmaster", "headmaster@chocoprins.cp", "Headmaster", "headmaster@chocoprins.cp", (Select ticketId From Ticket Where ticketTypeId = (SELECT ticketTypeId FROM TicketType WHERE naam = 'General Admission')), true),
(uuid(), "Headmaster", "headmaster@chocoprins.cp", "Arnould van Heacke", "arnouldvanheacke@chocoprins.cp", (Select ticketId From Ticket Where ticketTypeId = (SELECT ticketTypeId FROM TicketType WHERE naam = 'General Admission')), true),
(uuid(), "Headmaster", "headmaster@chocoprins.cp", "Chocoprins Joris", "chocoprinsJoris@chocoprins.cp", (Select ticketId From Ticket Where ticketTypeId = (SELECT ticketTypeId FROM TicketType WHERE naam = 'General Admission')), true);

-- Insert data into DagList (Day List)
INSERT INTO DagList (ticketId, dagId)
VALUES
((SELECT ticketId FROM Ticket WHERE firstName = 'John' AND lastName = 'Doe'), (SELECT dagId FROM Dag WHERE naam = 'Day 1')),
((SELECT ticketId FROM Ticket WHERE firstName = 'Jane' AND lastName = 'Smith'), (SELECT dagId FROM Dag WHERE naam = 'Day 2'));

-- Insert data into FoodTruck
INSERT INTO FoodTruck (foodTruckId, naam, locatieId)
VALUES
(UUID(), 'Burger Truck', (SELECT locatieId FROM Locatie WHERE naam = 'Main Stage')),
(UUID(), 'Pizza Truck', (SELECT locatieId FROM Locatie WHERE naam = 'Beach Arena')),
(UUID(), 'Ice Cream Truck', (SELECT locatieId FROM Locatie WHERE naam = 'Jazz Lounge'));

-- Insert data into TruckList (Food Truck List)
INSERT INTO TruckList (foodTruckId, editieId)
VALUES
((SELECT foodTruckId FROM FoodTruck WHERE naam = 'Burger Truck'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest')),
((SELECT foodTruckId FROM FoodTruck WHERE naam = 'Pizza Truck'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest')),
((SELECT foodTruckId FROM FoodTruck WHERE naam = 'Ice Cream Truck'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'));

-- Insert data into MenuItem (Menu Items)
INSERT INTO MenuItem (menuItemId, naam, prijs, foodTruckId)
VALUES
(UUID(), 'Cheeseburger', 10.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Burger Truck')),
(UUID(), 'Veggie Burger', 12.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Burger Truck')),
(UUID(), 'Margherita Pizza', 8.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Pizza Truck')),
(UUID(), 'Pepperoni Pizza', 10.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Pizza Truck')),
(UUID(), 'Vanilla Ice Cream', 3.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Ice Cream Truck')),
(UUID(), 'Chocolate Ice Cream', 3.50, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Ice Cream Truck'));

-- Insert data into Sponsor
INSERT INTO Sponsor (sponsorId, sponsorNaam, hoeveelheid, gesponsordeItem)
VALUES
(UUID(), 'TechCo', 50000, 'Stage Equipment'),
(UUID(), 'DrinkCorp', 20000, 'Refreshments'),
(UUID(), 'Foodies Ltd', 30000, 'Food Stalls');

-- Insert data into SponsorLijst (Sponsor List)
INSERT INTO SponsorLijst (sponsorId, editieId)
VALUES
((SELECT sponsorId FROM Sponsor WHERE sponsorNaam = 'TechCo'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest')),
((SELECT sponsorId FROM Sponsor WHERE sponsorNaam = 'DrinkCorp'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest')),
((SELECT sponsorId FROM Sponsor WHERE sponsorNaam = 'Foodies Ltd'), (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'));

INSERT INTO ArtiestenLijst(artiestId, editieId) VALUES 
((SELECT artiestId FROM Artiest WHERE naam = 'The Rockers'),(SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest')),
((SELECT artiestId FROM Artiest WHERE naam = 'DJ Spin'),(SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'));
-- Insert data into Artikel (Article)
INSERT INTO Artikel (artikelId, titel, beschrijving, datum, editieId)
VALUES
(UUID(), 'Fritfest 2024 Highlights', 'The best moments from Fritfest 2024', '2024-06-30 09:00:00', (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'));

-- Insert data into Foto (Photo)
INSERT INTO Foto (fotoId, bestand, beschrijving,  editieId,artikelId,podiumId)
VALUES
(UUID(), 'photo1.jpg', 'Main Stage Crowd',  (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'),(select artikelId from Artikel WHERE titel = 'Fritfest 2024 Highlights'),(select podiumId from Podium where naam = 'Test1')),
(UUID(), 'photo2.jpg', 'DJ Spin Performing',  (SELECT editieId FROM Editie WHERE editieNaam = 'Fritfest'),(select artikelId from Artikel WHERE titel = 'Fritfest 2024 Highlights'),(select podiumId from Podium where naam = 'Test2'));
