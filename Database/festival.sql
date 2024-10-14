DROP TABLE IF EXISTS DagList;
DROP TABLE IF EXISTS Foto;
DROP TABLE IF EXISTS Nieuws;
DROP TABLE IF EXISTS SponsorLijst;
DROP TABLE IF EXISTS Sponsors;
DROP TABLE IF EXISTS Menu;
DROP TABLE IF EXISTS FoodTrucks;
DROP TABLE IF EXISTS Locaties;
DROP TABLE IF EXISTS Ticket;
DROP TABLE IF EXISTS Tickettype;
DROP TABLE IF EXISTS Tijdstip;
DROP TABLE IF EXISTS Dag;
DROP TABLE IF EXISTS Podium;
DROP TABLE IF EXISTS Artiest;
DROP TABLE IF EXISTS Genre;
DROP TABLE IF EXISTS Editie;

-- Step 1: Create the Database
CREATE DATABASE IF NOT EXISTS FestivalDB;
USE FestivalDB;

-- Create tables
-- Create Genre table
CREATE TABLE Genre (
    genreId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL
);

-- Create Artiest (Artist) table
CREATE TABLE Artiest (
    artiestId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    genreId CHAR(36),
    FOREIGN KEY (genreId) REFERENCES Genre(genreId)
);

-- Create Locatie (Location) table
CREATE TABLE Locatie (
    locatieId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL,
    coordinaten VARCHAR(255) NOT NULL
);

-- Create Podium (Stage) table
CREATE TABLE Podium (
    podiumId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL,
    locatieId CHAR(36),
    FOREIGN KEY (locatieId) REFERENCES Locatie(locatieId)
);

-- Create Dag (Day) table
CREATE TABLE Dag (
    dagId CHAR(36) PRIMARY KEY,
    date DATE NOT NULL
);

-- Create Tijdstip (Time Slot) table
CREATE TABLE Tijdstip (
    tijdstipId CHAR(36) PRIMARY KEY,
    tijdstip TIMESTAMP NOT NULL,
    artiestId CHAR(36),
    podiumId CHAR(36),
    FOREIGN KEY (artiestId) REFERENCES Artiest(artiestId),
    FOREIGN KEY (podiumId) REFERENCES Podium(podiumId)
);

-- Create Tickettype (Ticket Type) table
CREATE TABLE Tickettype (
    typeId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL
);

-- Create Ticket table
CREATE TABLE Ticket (
    ticketId CHAR(36) PRIMARY KEY,
    prijs DECIMAL(10, 2) NOT NULL,
    typeId CHAR(36),
    firstName VARCHAR(255) NOT NULL,
    lastName VARCHAR(255) NOT NULL,
    telephoneNumber VARCHAR(15) NOT NULL,
    email VARCHAR(255) NOT NULL,
    FOREIGN KEY (typeId) REFERENCES Tickettype(typeId)
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
    foodTruckId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL,
    locatieId CHAR(36),
    FOREIGN KEY (locatieId) REFERENCES Locatie(locatieId)
);

-- Create MenuItem table
CREATE TABLE MenuItem (
    menuItemId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL,
    prijs DECIMAL(10, 2) NOT NULL,
    foodTruckId CHAR(36),
    FOREIGN KEY (foodTruckId) REFERENCES FoodTruck(foodTruckId)
);

-- Create Sponsor table
CREATE TABLE Sponsor (
    sponsorId CHAR(36) PRIMARY KEY,
    naam VARCHAR(255) NOT NULL,
    logo VARCHAR(255) NOT NULL
);

-- Create NieuwsArtikel (News Article) table
CREATE TABLE NieuwsArtikel (
    artikelId CHAR(36) PRIMARY KEY,
    titel VARCHAR(255) NOT NULL,
    inhoud TEXT NOT NULL,
    datum TIMESTAMP NOT NULL
);

-- Create Foto table
CREATE TABLE Foto (
    fotoId CHAR(36) PRIMARY KEY,
    bestand VARCHAR(255) NOT NULL,
    beschrijving TEXT
);

-- Insert Genres
INSERT INTO Genre (genreId, naam) VALUES 
(UUID(), 'Rock'), 
(UUID(), 'Jazz'), 
(UUID(), 'Pop'), 
(UUID(), 'Electronic'), 
(UUID(), 'Hip-Hop');

-- Insert Locations
INSERT INTO Locatie (locatieId, naam, coordinaten) VALUES 
(UUID(), 'City Park', '50.8503° N, 4.3517° E'), 
(UUID(), 'Downtown Arena', '50.8476° N, 4.3572° E'), 
(UUID(), 'Riverside Amphitheater', '50.8457° N, 4.3500° E');

-- Insert Stages
INSERT INTO Podium (podiumId, naam, locatieId) VALUES 
(UUID(), 'Main Stage', (SELECT locatieId FROM Locatie WHERE naam = 'City Park')), 
(UUID(), 'Side Stage', (SELECT locatieId FROM Locatie WHERE naam = 'Downtown Arena')), 
(UUID(), 'Electronic Arena', (SELECT locatieId FROM Locatie WHERE naam = 'Riverside Amphitheater'));

-- Insert Days
INSERT INTO Dag (dagId, date) VALUES 
(UUID(), '2024-10-18'), 
(UUID(), '2024-10-19'), 
(UUID(), '2024-10-20');

-- Insert Artists
INSERT INTO Artiest (artiestId, naam, email, genreId) VALUES 
(UUID(), 'The Rolling Stones', 'contact@rollingstones.com', (SELECT genreId FROM Genre WHERE naam = 'Rock')), 
(UUID(), 'Miles Davis', 'milesd@jazz.com', (SELECT genreId FROM Genre WHERE naam = 'Jazz')), 
(UUID(), 'Taylor Swift', 'taylorswift@popstar.com', (SELECT genreId FROM Genre WHERE naam = 'Pop')), 
(UUID(), 'Daft Punk', 'info@daftpunk.com', (SELECT genreId FROM Genre WHERE naam = 'Electronic')), 
(UUID(), 'Kanye West', 'kanye@hiphop.com', (SELECT genreId FROM Genre WHERE naam = 'Hip-Hop'));

-- Insert Time Slots
INSERT INTO Tijdstip (tijdstipId, tijdstip, artiestId, podiumId) VALUES 
(UUID(), '2024-10-18 14:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'The Rolling Stones'), (SELECT podiumId FROM Podium WHERE naam = 'Main Stage')),  
(UUID(), '2024-10-18 18:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'Miles Davis'), (SELECT podiumId FROM Podium WHERE naam = 'Side Stage')),  
(UUID(), '2024-10-19 16:30:00', (SELECT artiestId FROM Artiest WHERE naam = 'Taylor Swift'), (SELECT podiumId FROM Podium WHERE naam = 'Main Stage')),  
(UUID(), '2024-10-19 21:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'Daft Punk'), (SELECT podiumId FROM Podium WHERE naam = 'Electronic Arena')),  
(UUID(), '2024-10-20 20:00:00', (SELECT artiestId FROM Artiest WHERE naam = 'Kanye West'), (SELECT podiumId FROM Podium WHERE naam = 'Main Stage'));

-- Insert Ticket Types
INSERT INTO Tickettype (typeId, naam) VALUES 
(UUID(), 'Day Pass'), 
(UUID(), 'Weekend Pass'), 
(UUID(), 'VIP Pass');

-- Insert Tickets
INSERT INTO Ticket (ticketId, prijs, typeId, firstName, lastName, telephoneNumber, email) VALUES
(UUID(), 50.00, (SELECT typeId FROM Tickettype WHERE naam = 'Day Pass'), 'Arno', 'Van Haecke', '1234567890', 'arno.vh@example.com'), 
(UUID(), 90.00, (SELECT typeId FROM Tickettype WHERE naam = 'Weekend Pass'), 'Steffy Sandra Monique', 'Meylaers', '9876543210', 'steffy.m@example.com'), 
(UUID(), 150.00, (SELECT typeId FROM Tickettype WHERE naam = 'VIP Pass'), 'Wesley Johan A Flopper', 'Meylaers', '5551234567', 'wesley.m@example.com'), 
(UUID(), 50.00, (SELECT typeId FROM Tickettype WHERE naam = 'Day Pass'), 'Chocoprins', 'Joris', '4445556666', 'choco.joris@example.com'), 
(UUID(), 90.00, (SELECT typeId FROM Tickettype WHERE naam = 'Weekend Pass'), 'Emiel', 'De Pedofiel', '1112223333', 'emiel.p@example.com');

-- Insert Ticket-Day Associations
-- Day Pass valid only for 18th October
INSERT INTO DagList (ticketId, dagId) VALUES 
((SELECT ticketId FROM Ticket WHERE firstName = 'Arno' AND lastName = 'Van Haecke'), (SELECT dagId FROM Dag WHERE date = '2024-10-18'));

-- Weekend Pass valid for both 18th and 19th October
INSERT INTO DagList (ticketId, dagId) VALUES 
((SELECT ticketId FROM Ticket WHERE firstName = 'Steffy Sandra Monique' AND lastName = 'Meylaers'), (SELECT dagId FROM Dag WHERE date = '2024-10-18')), 
((SELECT ticketId FROM Ticket WHERE firstName = 'Steffy Sandra Monique' AND lastName = 'Meylaers'), (SELECT dagId FROM Dag WHERE date = '2024-10-19'));

-- VIP Pass valid for all 3 days
INSERT INTO DagList (ticketId, dagId) VALUES 
((SELECT ticketId FROM Ticket WHERE firstName = 'Wesley Johan A Flopper' AND lastName = 'Meylaers'), (SELECT dagId FROM Dag WHERE date = '2024-10-18')), 
((SELECT ticketId FROM Ticket WHERE firstName = 'Wesley Johan A Flopper' AND lastName = 'Meylaers'), (SELECT dagId FROM Dag WHERE date = '2024-10-19')), 
((SELECT ticketId FROM Ticket WHERE firstName = 'Wesley Johan A Flopper' AND lastName = 'Meylaers'), (SELECT dagId FROM Dag WHERE date = '2024-10-20'));

-- Insert Food Trucks
INSERT INTO FoodTruck (foodTruckId, naam, locatieId) VALUES 
(UUID(), 'Tasty Tacos', (SELECT locatieId FROM Locatie WHERE naam = 'City Park')), 
(UUID(), 'Burgers Galore', (SELECT locatieId FROM Locatie WHERE naam = 'Downtown Arena')), 
(UUID(), 'Sushi Station', (SELECT locatieId FROM Locatie WHERE naam = 'Riverside Amphitheater'));

-- Insert Menu Items
INSERT INTO MenuItem (menuItemId, naam, prijs, foodTruckId) VALUES 
(UUID(), 'Chicken Taco', 5.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Tasty Tacos')), 
(UUID(), 'Beef Taco', 6.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Tasty Tacos')), 
(UUID(), 'Cheeseburger', 7.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Burgers Galore')), 
(UUID(), 'Veggie Burger', 7.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Burgers Galore')), 
(UUID(), 'California Roll', 8.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Sushi Station')), 
(UUID(), 'Spicy Tuna Roll', 9.00, (SELECT foodTruckId FROM FoodTruck WHERE naam = 'Sushi Station'));



-- Verify Data with Select Queries
SELECT * FROM Genre;
SELECT * FROM Artiest;
SELECT * FROM Podium;
SELECT * FROM Tijdstip;
SELECT * FROM Dag;
SELECT * FROM Tickettype;
SELECT * FROM Ticket;
SELECT * FROM DagList;
