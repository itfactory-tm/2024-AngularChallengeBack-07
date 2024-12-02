-- Step 1: Create the Database
CREATE DATABASE IF NOT EXISTS festivaldb;
USE festivaldb;


-- Drop Tables if they exist
DROP TABLE IF EXISTS DayList;
DROP TABLE IF EXISTS Photo;
DROP TABLE IF EXISTS Article;
DROP TABLE IF EXISTS SponsorList;
DROP TABLE IF EXISTS TruckList;
DROP TABLE IF EXISTS Sponsor;
DROP TABLE IF EXISTS MenuItem;
DROP TABLE IF EXISTS FoodTruck;
DROP TABLE IF EXISTS BoughtTicket;
DROP TABLE IF EXISTS Ticket;
DROP TABLE IF EXISTS Tickettype;
DROP TABLE IF EXISTS TimeSlot;
DROP TABLE IF EXISTS Day;
DROP TABLE IF EXISTS Stage;
DROP TABLE IF EXISTS Location;
Drop table if Exists ArtistList;
DROP TABLE IF EXISTS Artist;
DROP TABLE IF EXISTS Genre;
DROP TABLE IF EXISTS Edition;



-- Create tables
-- Create Editie (Edition) table
CREATE TABLE Edition (
    editionId CHAR(36) PRIMARY KEY,            -- GUID type as CHAR(36)
    editionName VARCHAR(100) NOT NULL,         -- EditieNaam as a string (not nullable)
    adres VARCHAR(100) NOT NULL,              -- Adres as a string (not nullable)
    zipCode VARCHAR(4) NOT NULL,             -- Postcode as a string (not nullable)
    municipality VARCHAR(100) NOT NULL,           -- Gemeente as a string (not nullable)
    phoneNr VARCHAR(12) NOT NULL,               -- TelNr as a string (not nullable)
    mail VARCHAR(50) NOT NULL,               -- Email as a string (not nullable)
    year INT NOT NULL                         -- Jaar as an integer (not nullable)
);

-- Create Genre table
CREATE TABLE Genre (
    genreId CHAR(36) PRIMARY KEY,          -- GUID type for the GenreId
    name VARCHAR(255) NOT NULL             -- Naam (Name) as a string (not nullable)
);

-- Create Artiest (Artist) table (adjusted for GenreId relationship)
CREATE TABLE Artist (
    artistId CHAR(36) PRIMARY KEY,             -- GUID for ArtiestId
    name VARCHAR(255) NOT NULL,                 -- Name of the artist
    mail VARCHAR(255) NOT NULL,                -- Email of the artist
    description VARCHAR(255) NOT NULL,         -- Description of the artist
    spotifyLink VARCHAR(255),                    -- Spotify API link (nullable)
    apiCode VARCHAR(255),
    genre VARCHAR(255), -- Foreign key to Genre table
    spotifyPhoto VARCHAR(255)
);

CREATE TABLE ArtistList(
                               artistId CHAR(36),                      -- Foreign key to Sponsor table
                               editionId CHAR(36),                       -- Foreign key to Editie table
                               PRIMARY KEY (artistId, editionId),       -- Composite primary key
                               FOREIGN KEY (artistId) REFERENCES Artist(artistId),  -- Foreign key constraint to Sponsor
                               FOREIGN KEY (editionId) REFERENCES Edition(editionId)
);

-- Create Locatie table
CREATE TABLE Location (
    locationId CHAR(36) PRIMARY KEY,         -- GUID as primary key
    name VARCHAR(255) NOT NULL,             -- Location name (not nullable)
    coordinates VARCHAR(255) NOT NULL      -- Coordinates (not nullable)
);

-- Create Podium table
CREATE TABLE Stage (
    stageId CHAR(36) PRIMARY KEY,        -- GUID as the primary key for PodiumId
    name VARCHAR(255) NOT NULL,           -- Name of the stage (not nullable)
    locationId CHAR(36),                  -- Foreign key to the Locatie table
    FOREIGN KEY (locationId) REFERENCES Location(locationId) -- Foreign key constraint to Locatie
);

-- Create Dag (Day) table
CREATE TABLE Day (
    dayId CHAR(36) PRIMARY KEY,          -- GUID type as CHAR(36)
    name VARCHAR(255),                   -- Naam is nullable (optional field)
    startDate DATETIME NOT NULL,        -- StartDatum is required (DATETIME type)
    endDate DATETIME NOT NULL          -- EindDatum is required (DATETIME type)
);

-- Create TijdStip table
CREATE TABLE TimeSlot (
    timeSlotId CHAR(36) PRIMARY KEY,            -- Primary Key for TijdStip
    time DATETIME NOT NULL,                     -- The date and time of the event
    artistId CHAR(36),                         -- Foreign Key to Artiest (Artist)
    stageId CHAR(36),                          -- Foreign Key to Podium (Stage)
    FOREIGN KEY (artistId) REFERENCES Artist(artistId),  -- Foreign Key constraint to Artiest
    FOREIGN KEY (stageId) REFERENCES Stage(stageId)      -- Foreign Key constraint to Podium
);

-- Create TicketType table
CREATE TABLE TicketType (
    ticketTypeId CHAR(36) PRIMARY KEY,       -- GUID as the primary key for TicketTypeId
    name VARCHAR(255) NOT NULL,               -- The name of the ticket type (e.g., VIP, General Admission)
    price double NOT NULL
);

-- Create Ticket table
CREATE TABLE Ticket (
    ticketId CHAR(36) PRIMARY KEY,                 -- GUID as the primary key for TicketId 
    editionId CHAR(36),                             -- Foreign key to Editie (Edition)
    ticketTypeId CHAR(36),                         -- Foreign key to TicketType
    dayId CHAR(36),                                -- Foreign key to Dag (Day)
    FOREIGN KEY (editionId) REFERENCES Edition(editionId),      -- Foreign key constraint to Editie
    FOREIGN KEY (ticketTypeId) REFERENCES TicketType(ticketTypeId),  -- Foreign key constraint to TicketType
    FOREIGN KEY (dayId) REFERENCES Day(dayId)               -- Foreign key constraint to Dag
);

CREATE TABLE BoughtTicket (
    boughtTicketId CHAR(36) PRIMARY KEY,                 -- GUID as the primary key for TicketId
    buyerName CHAR(50),
    buyerMail CHAR(50),
    holderName CHAR(50),
    holderMail CHAR(50),   
    editionId CHAR(36),
    ticketId CHAR(36),
    
    payed boolean,
    FOREIGN KEY (editionId) REFERENCES Edition(editionId),
    FOREIGN KEY (ticketId) REFERENCES Ticket(ticketId)
    
);

-- Create DagList (Day List) table
CREATE TABLE DayList (
    ticketId CHAR(36),
    dayId CHAR(36),
    PRIMARY KEY (ticketId, dayId),
    FOREIGN KEY (ticketId) REFERENCES Ticket(ticketId),
    FOREIGN KEY (dayId) REFERENCES Day(dayId)
);

-- Create FoodTruck table
CREATE TABLE FoodTruck (
    foodTruckId CHAR(36) PRIMARY KEY,      -- GUID type as CHAR(36)
    name VARCHAR(255) NOT NULL,            -- Naam as a string (not nullable)
    locationId CHAR(36) NOT NULL,           -- LocatieId as a GUID (foreign key)
    FOREIGN KEY (locationId) REFERENCES Location(locationId)  -- Foreign key reference to Locatie table
);

-- Create TruckList table
CREATE TABLE TruckList (
    foodTruckId CHAR(36),              -- Foreign key to FoodTruck
    editionId CHAR(36),                 -- Foreign key to Editie
    PRIMARY KEY (foodTruckId, editionId),  -- Composite primary key
    FOREIGN KEY (foodTruckId) REFERENCES FoodTruck(foodTruckId),  -- Foreign key reference to FoodTruck
    FOREIGN KEY (editionId) REFERENCES Edition(editionId)             -- Foreign key reference to Editie
);

-- Create MenuItem table
CREATE TABLE MenuItem (
    menuItemId CHAR(36) PRIMARY KEY,        -- GUID as primary key for MenuItemId
    name VARCHAR(255) NOT NULL,             -- Name of the menu item (not nullable)
    price DECIMAL(10, 2) NOT NULL,          -- Price of the menu item (not nullable)
    foodTruckId CHAR(36),                   -- Foreign key to the FoodTruck table
    FOREIGN KEY (foodTruckId) REFERENCES FoodTruck(foodTruckId) -- Foreign key constraint to FoodTruck
);

-- Create Sponsor table
CREATE TABLE Sponsor (
    sponsorId CHAR(36) PRIMARY KEY,           -- GUID as the primary key for SponsorId
    sponsorName VARCHAR(255) NOT NULL,        -- Name of the sponsor (not nullable)
    sponsorLogo VARCHAR(255),
    sponsorMail VARCHAR(255),
    amount INT,                          -- Amount contributed by the sponsor
    sponsoredItem VARCHAR(255)            -- Sponsored item description
);

-- Create SponsorLijst (Sponsor List) table
CREATE TABLE SponsorList (
    sponsorId CHAR(36),                      -- Foreign key to Sponsor table
    editionId CHAR(36),                       -- Foreign key to Editie table
    PRIMARY KEY (sponsorId, editionId),       -- Composite primary key
    FOREIGN KEY (sponsorId) REFERENCES Sponsor(sponsorId),  -- Foreign key constraint to Sponsor
    FOREIGN KEY (editionId) REFERENCES Edition(editionId)       -- Foreign key constraint to Editie
);

-- Create Artikel (News Article) table
CREATE TABLE Article (
    articleId CHAR(36) PRIMARY KEY,           -- GUID type as CHAR(36)
    title VARCHAR(255) NOT NULL,              -- Titel is required
    description TEXT,                        -- Beschrijving is nullable (TEXT type)
    date DATETIME NOT NULL,                  -- Datum is required and uses DATETIME
    editionId CHAR(36),                        -- EditieId is a GUID (CHAR(36))
    FOREIGN KEY (editionId) REFERENCES Edition(editionId)  -- Foreign key referencing the Editie table
);

-- Create Foto (Photo) table
CREATE TABLE Photo (
    photoId CHAR(36) PRIMARY KEY,           -- GUID type as CHAR(36) for primary key
    file VARCHAR(255) NOT NULL,         -- Bestand (file name) as a string
    description VARCHAR(255) NOT NULL,
    editionId CHAR(36),                     -- Foreign key to Editie table
    FOREIGN KEY (editionId) REFERENCES Edition(editionId),  -- Foreign key constraint to Editie
    articleId CHAR(36),
    foreign key (articleId) references Article(articleId),
    stageId CHAR(36),
    foreign key (stageId) references Stage(stageId)
);

-- Insert data into Editie (Edition)
INSERT INTO Edition (editionId, editionName, adres, zipCode, municipality, phoneNr, mail, year)
VALUES 
(UUID(), 'Fritfest', 'Main Street 123', '1000', 'Brussels', '02-1234567', 'info@fritfest.be', 2024);



-- Insert data into Genre
INSERT INTO Genre (genreId, name)
VALUES
(UUID(), 'Rock'),
(UUID(), 'Pop'),
(UUID(), 'Jazz'),
(UUID(), 'Electronic');

-- Insert data into Artiest (Artist)
INSERT INTO Artist (artistId, name, mail, description, spotifyLink,apiCode ,genre, spotifyPhoto)
VALUES 
(UUID(), 'The Rockers', 'rockers@music.com', 'A famous rock band', 'spotify.com/therockers','22Wzsyh7moQAwSODsMF6w2' ,'',''),
(UUID(), 'DJ Spin', 'djspin@beats.com', 'A well-known electronic DJ', 'spotify.com/djspin',' 22Wzsyh7moQAwSODsMF6w2','',''),
(UUID(), 'PopStar', 'popstar@music.com', 'A pop music sensation', 'spotify.com/popstar','' ,'',''),
(UUID(), 'Jazz Quartet', 'jazzquartet@jazz.com', 'A group of jazz musicians', 'spotify.com/jazzquartet','','','');

-- Insert data into Locatie (Location)
INSERT INTO Location (locationId, name, coordinates)
VALUES
(UUID(), 'Main Stage', '52.366, 4.904'),
(UUID(), 'Beach Arena', '51.922, 4.481'),
(UUID(), 'Jazz Lounge', '52.364, 4.903');

INSERT INTO Stage (stageId,name, locationId) 
VALUES 
(UUID(), 'Test1',(SELECT locationId FROM Location WHERE name = 'Main Stage')),
(UUID(),'Test2' ,(SELECT locationId FROM Location WHERE name = 'Beach Arena')),
(UUID(),'test3' ,(SELECT locationId FROM Location WHERE name = 'Jazz Lounge'));


-- Insert data into Dag (Day)
INSERT INTO Day (dayId, name, startDate, endDate)
VALUES
(UUID(), 'Friday', '2024-07-01 10:00:00', '2024-07-01 22:00:00'),
(UUID(), 'Saturday', '2024-07-02 10:00:00', '2024-07-02 22:00:00'),
(UUID(), 'Sunday', '2024-07-03 10:00:00', '2024-07-03 22:00:00');
-- Insert data into TijdStip (Time Slot)
INSERT INTO TimeSlot (timeSlotId, time, artistId, stageId)
VALUES
(UUID(), '2024-07-01 12:00:00', (SELECT artistId FROM Artist WHERE name = 'The Rockers'), (SELECT stageId FROM Stage WHERE name = 'Test1')),
(UUID(), '2024-07-01 14:00:00', (SELECT artistId FROM Artist WHERE name = 'DJ Spin'), (SELECT stageId FROM Stage WHERE name = 'Test2')),
(UUID(), '2024-07-02 16:00:00', (SELECT artistId FROM Artist WHERE name = 'PopStar'), (SELECT stageId FROM Stage WHERE name = 'Test1')),
(UUID(), '2024-07-02 18:00:00', (SELECT artistId FROM Artist WHERE name = 'Jazz Quartet'), (SELECT stageId FROM Stage WHERE name = 'Test3'));

-- Insert data into TicketType
INSERT INTO TicketType (ticketTypeId, name,price)
VALUES
(UUID(), 'General Admission',10.00),
(UUID(), 'Super',12.00),
(UUID(), 'VIP',15.00);

-- Insert data into Ticket
INSERT INTO Ticket (ticketId, editionId, ticketTypeId, dayId)
VALUES
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'General Admission'), (SELECT dayId FROM Day WHERE name = 'Friday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'Super'), (SELECT dayId FROM Day WHERE name = 'Friday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'VIP'), (SELECT dayId FROM Day WHERE name = 'Friday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'General Admission'), (SELECT dayId FROM Day WHERE name = 'Saturday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'Super'), (SELECT dayId FROM Day WHERE name = 'Saturday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'VIP'), (SELECT dayId FROM Day WHERE name = 'Saturday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'General Admission'), (SELECT dayId FROM Day WHERE name = 'Sunday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'Super'), (SELECT dayId FROM Day WHERE name = 'Sunday')),
(UUID(), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'), (SELECT ticketTypeId FROM TicketType WHERE name= 'VIP'), (SELECT dayId FROM Day WHERE name = 'Sunday'));

Insert Into BoughtTicket (boughtTicketId, buyerName, buyerMail, holderName, holderMail,editionId ,ticketId, payed)
Values
(uuid(), "Headmaster", "headmaster@chocoprins.cp", "Headmaster", "headmaster@chocoprins.cp", (select editionId from Edition where editionName = 'Fritfest'),(Select ticketId From Ticket Where ticketTypeId = (SELECT ticketTypeId FROM TicketType WHERE name = 'General Admission') And DayId = (SELECT dayId FROM Day WHERE name = 'Friday')),false),
(uuid(), "Headmaster", "headmaster@chocoprins.cp", "Arnould van Heacke", "arnouldvanheacke@chocoprins.cp",(select editionId from Edition where editionName = 'Fritfest'),(Select ticketId From Ticket Where ticketTypeId = (SELECT ticketTypeId FROM TicketType WHERE name = 'General Admission') And DayId = (SELECT dayId FROM Day WHERE name = 'Friday')),false),
(uuid(), "Headmaster", "headmaster@chocoprins.cp", "Chocoprins Joris", "chocoprinsJoris@chocoprins.cp", (select editionId from Edition where editionName = 'Fritfest'),(Select ticketId From Ticket Where ticketTypeId = (SELECT ticketTypeId FROM TicketType WHERE name = 'General Admission') And DayId = (SELECT dayId FROM Day WHERE name = 'Friday')),false);

-- Insert data into DagList (Day List)
-- INSERT INTO DayList (ticketId, dayId)
-- VALUES
-- ((SELECT ticketId FROM Ticket WHERE firstName = 'John' AND lastName = 'Doe'), (SELECT dayId FROM Day WHERE name = 'Day 1')),
-- ((SELECT ticketId FROM Ticket WHERE firstName = 'Jane' AND lastName = 'Smith'), (SELECT dayId FROM Day WHERE name = 'Day 2'));

-- Insert data into FoodTruck
INSERT INTO FoodTruck (foodTruckId, name, locationId)
VALUES
(UUID(), 'Burger Truck', (SELECT locationId FROM Location WHERE name = 'Main Stage')),
(UUID(), 'Pizza Truck', (SELECT locationId FROM Location WHERE name = 'Beach Arena')),
(UUID(), 'Ice Cream Truck', (SELECT locationId FROM Location WHERE name = 'Jazz Lounge'));

-- Insert data into TruckList (Food Truck List)
INSERT INTO TruckList (foodTruckId, editionId)
VALUES
((SELECT foodTruckId FROM FoodTruck WHERE name = 'Burger Truck'), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest')),
((SELECT foodTruckId FROM FoodTruck WHERE name ='Pizza Truck'), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest')),
((SELECT foodTruckId FROM FoodTruck WHERE name = 'Ice Cream Truck'), (SELECT editionId FROM Edition WHERE editionName ='Fritfest'));

-- Insert data into MenuItem (Menu Items)
INSERT INTO MenuItem (menuItemId, name, price, foodTruckId)
VALUES
(UUID(), 'Cheeseburger', 10.00, (SELECT foodTruckId FROM FoodTruck WHERE name = 'Burger Truck')),
(UUID(), 'Veggie Burger', 12.00, (SELECT foodTruckId FROM FoodTruck WHERE name = 'Burger Truck')),
(UUID(), 'Margherita Pizza', 8.00, (SELECT foodTruckId FROM FoodTruck WHERE name = 'Pizza Truck')),
(UUID(), 'Pepperoni Pizza', 10.00, (SELECT foodTruckId FROM FoodTruck WHERE name = 'Pizza Truck')),
(UUID(), 'Vanilla Ice Cream', 3.00, (SELECT foodTruckId FROM FoodTruck WHERE name = 'Ice Cream Truck')),
(UUID(), 'Chocolate Ice Cream', 3.50, (SELECT foodTruckId FROM FoodTruck WHERE name = 'Ice Cream Truck'));

-- Insert data into Sponsor
INSERT INTO Sponsor (sponsorId, sponsorName, sponsorMail,sponsorLogo,amount, sponsoredItem)
VALUES
(UUID(), 'TechCo', '','',50000, 'Stage Equipment'),
(UUID(), 'DrinkCorp','','', 20000, 'Refreshments'),
(UUID(), 'Foodies Ltd','','', 30000, 'Food Stalls');

-- Insert data into SponsorLijst (Sponsor List)
INSERT INTO SponsorList (sponsorId, editionId)
VALUES
((SELECT sponsorId FROM Sponsor WHERE sponsorName = 'TechCo'), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest')),
((SELECT sponsorId FROM Sponsor WHERE sponsorName = 'DrinkCorp'), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest')),
((SELECT sponsorId FROM Sponsor WHERE sponsorName = 'Foodies Ltd'), (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'));

INSERT INTO ArtistList(artistId, editionId) VALUES 
((SELECT artistId FROM Artist WHERE name = 'The Rockers'),(SELECT editionId FROM Edition WHERE editionName = 'Fritfest')),
((SELECT artistId FROM Artist WHERE name = 'DJ Spin'),(SELECT editionId FROM Edition WHERE editionName = 'Fritfest'));
-- Insert data into Artikel (Article)
INSERT INTO Article (articleId, title, description, date, editionId)
VALUES
(UUID(), 'Fritfest 2024 Highlights', 'The best moments from Fritfest 2024', '2024-06-30 09:00:00', (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'));

-- Insert data into Foto (Photo)
INSERT INTO Photo (photoId, file, description,  editionId,articleId,stageId)
VALUES
(UUID(), 'photo1.jpg', 'Main Stage Crowd',  (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'),(select articleId from Article WHERE title = 'Fritfest 2024 Highlights'),(select stageId from Stage where name = 'Test1')),
(UUID(), 'photo2.jpg', 'DJ Spin Performing',  (SELECT editionId FROM Edition WHERE editionName = 'Fritfest'),(select articleId from Article WHERE title = 'Fritfest 2024 Highlights'),(select stageId from Stage where name = 'Test2'));
