using FritFest.API.DbContexts;
using FritFest.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FritFest.API
{
    public class DbInitializer
    {
        public static void Initialize(FestivalContext context)
        {
            // Ensure database is created
            context.Database.Migrate();

            if (context.Editions.Any())
            {
                return;
            }

            // Create the necessary IDs to link entities

            if (!context.Editions.Any())
            {
                // Seed Editions
                context.Editions.Add(new Edition
                {
                    EditionId = Guid.NewGuid(),
                    EditionName = "Fritfest",
                    Adres = "Main Street 123",
                    ZipCode = "1000",
                    Municipality = "Brussels",
                    PhoneNr = "02-1234567",
                    Mail = "info@fritfest.be",
                    Year = 2024
                });
                context.SaveChanges();
            }

            

            if (!context.Artists.Any())
            {

                // Seed Artists
                context.Artists.AddRange(
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Lost Frequencies",
                        Mail = "info@lostfrequencies.be",
                        Description = "Known for his hit track Where Are You Now with David Kushner, Lost Frequencies has become a global EDM sensation and one of Belgium's most successful musical exports",
                        SpotifyLink = "https://open.spotify.com/artist/22bE4uQ6baNwSHPVcDxLCe?si=8b4cc544eb804081",  // Real Spotify Link
                        ApiCode = "",  // The Spotify Artist ID
                        SpotifyPhoto = "",  // You can upload and link a photo if you want
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "De Romeo's",
                        Mail = "info@deromeos.be",
                        Description = "De Romeo's is een Vlaamse muziekgroep, bestaande uit Chris Van Tongelen, Davy Gilles en Gunther Levi. De groep brengt een Nederlandstalige mix van eigen nummers, covers en hit-medleys.",
                        SpotifyLink = "https://open.spotify.com/artist/6M2wZ9GZgrQXHCFfjv46we?si=97177077e8c84502",  // Real Spotify Link
                        ApiCode = "6M2wZ9GZGrQXHCFf7R1J2E",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Regi",
                        Mail = "info@tttartists.be",
                        Description = "Reginald Penxten, beter bekend onder zijn artiestennaam Regi, is een Belgische dj en producer. Hij is sinds de jaren negentig zeer succesvol als producer en songwriter achter talloze dance-acts, waaronder Milk Inc. en Sylver. ",
                        SpotifyLink = "https://open.spotify.com/artist/6qqNVTkY8uBg9cP3Jd7DAH?si=5d9b96b0e85143a6",  // Real Spotify Link
                        ApiCode = "6qqNVTkY8uBg9cP3Jd7DAz",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Bart Peeters",
                        Mail = "info@bart.be",
                        Description = "Bart August Maria Peeters is een Vlaams zanger, drummer, gitarist, presentator en acteur. Peeters studeerde Germaanse filologie en theaterwetenschappen in Antwerpen.",
                        SpotifyLink = "https://open.spotify.com/artist/6eUKZXaKkcviH0Ku9w2n3V?si=43201c9587dc461e",  // Real Spotify Link
                        ApiCode = "7n2wHs1T2YJ3c1cRInuQu7",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Clouseau",
                        Mail = "management@clouseau.be",
                        Description = "Clouseau is een Belgische popgroep rond de broers Koen Wauters en Kris Wauters. De groep werd in 1984 opgericht door Bob Savenberg en scoorde door de jaren heen hits met onder andere Daar gaat ze, Louise, Passie, Anne, Vonken & vuur, Zie me graag en Nobelprijs.",
                        SpotifyLink = "https://open.spotify.com/artist/1Xyo4u8uXC1ZmMpatF05PJ?si=9cecf09c6a9a40c3",  // Real Spotify Link
                        ApiCode = "",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Camille",
                        Mail = "eva@thebookingcompany.be",
                        Description = "Camille Dhont, vooral bekend als kortweg Camille, is een Belgische zangeres en actrice uit Wevelgem. Ze brak door dankzij haar rol in de jeugdserie #LikeMe en scoorde hits met nummers als Vergeet de tijd, Vuurwerk en Geen tranen meer over. Als Miss Poes won ze in 2022 het tweede seizoen van The Masked Singer.",
                        SpotifyLink = "https://open.spotify.com/artist/1Xyo4u8uXC1ZmMpatF05PJ?si=9cecf09c6a9a40c3",  // Real Spotify Link
                        ApiCode = "",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Pommelien Thijs",
                        Mail = "eva@thebookingcompany.be",
                        Description = "Pommelien Thijs is een Belgische actrice en zangeres. Thijs werd bekend als actrice, met hoofdrollen in de Ketnet-serie #LikeMe en de VRT-Netflixserie Knokke Off.",
                        SpotifyLink = "https://open.spotify.com/artist/1Xyo4u8uXC1ZmMpatF05PJ?si=9cecf09c6a9a40c3",  // Real Spotify Link
                        ApiCode = "",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Metejoor",
                        Mail = "tim@nextar.be",
                        Description = "Metejoor, artiestennaam van Joris Van Rossem, is een Vlaamse zanger van het Vlaamse popgenre, bekend van de hits Het beste aan mij, 1 op een miljoen, Rendez-vous, Dit is wat mijn mama zei en Wat wil je van mij.",
                        SpotifyLink = "https://open.spotify.com/artist/1Xyo4u8uXC1ZmMpatF05PJ?si=9cecf09c6a9a40c3",  // Real Spotify Link
                        ApiCode = "",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Average Rob",
                        Mail = "cs@averagerob.com",
                        Description = "Robert Van Impe, beter bekend als Average Rob, is een Belgisch comedian, youtuber, acteur en TV persoonlijkheid. Hij werkte sinds 2016 voor het Belgische magazine Humo en presenteerde het radioprogramma BOITLYFE op jongerenzender StuBru samen met Omdat Het Kan.",
                        SpotifyLink = "https://open.spotify.com/artist/1Xyo4u8uXC1ZmMpatF05PJ?si=9cecf09c6a9a40c3",  // Real Spotify Link
                        ApiCode = "",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Artist
                    {
                        ArtistId = Guid.NewGuid(),
                        Name = "Laura Tesoro",
                        Mail = "info@tttartists.be",
                        Description = "Laura Tesoro is een Belgische zangeres, presentatrice en actrice. Ze werd vooral bekend door haar deelname aan de derde editie van The Voice van Vlaanderen in 2014, waar ze de tweede plaats behaalde. In 2016 vertegenwoordigde ze België tijdens het Eurovisiesongfestival 2016, waar ze een 10de plaats behaalde.",
                        SpotifyLink = "https://open.spotify.com/artist/1Xyo4u8uXC1ZmMpatF05PJ?si=9cecf09c6a9a40c3",  // Real Spotify Link
                        ApiCode = "",  // The Spotify Artist ID
                        SpotifyPhoto = "",
                        Genre = "",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    }
                );

                context.SaveChanges();

            }
            // Seed Locations

            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                    new Location
                        { LocationId = Guid.NewGuid(), Name = "Main Stage", Longitude = 52.366, Latitude = 4.904 },
                    new Location
                        { LocationId = Guid.NewGuid(), Name = "Beach Arena", Longitude = 51.922, Latitude = 4.481 },
                    new Location
                        { LocationId = Guid.NewGuid(), Name = "Jazz Lounge", Longitude = 52.364, Latitude = 4.903 }
                );
                context.SaveChanges();
            }

            if (!context.Stages.Any())
            {
                // Seed Stages
                context.Stages.AddRange(
                    new Stage
                    {
                        StageId = Guid.NewGuid(), Name = "Main Stage",
                        LocationId = context.Locations.First(l => l.Name == "Main Stage").LocationId
                    },
                    new Stage
                    {
                        StageId = Guid.NewGuid(), Name = "Dance Stage",
                        LocationId = context.Locations.First(l => l.Name == "Beach Arena").LocationId
                    },
                    new Stage
                    {
                        StageId = Guid.NewGuid(), Name = "Pop Stage",
                        LocationId = context.Locations.First(l => l.Name == "Jazz Lounge").LocationId
                    }
                );
                context.SaveChanges();
            }

            

            if (!context.TicketTypes.Any())
            {
                // Seed Tickets Types
                context.TicketTypes.AddRange(
                    new TicketType { TicketTypeId = Guid.NewGuid(), Name = "General Admission", Price = 29.99 },
                    new TicketType { TicketTypeId = Guid.NewGuid(), Name = "Super", Price = 50.00 },
                    new TicketType { TicketTypeId = Guid.NewGuid(), Name = "VIP", Price = 150.00 }
                );
                context.SaveChanges();
            }

            if (!context.FoodTrucks.Any())
            {

                // Seed Food Trucks
                context.FoodTrucks.AddRange(
                    new FoodTruck
                    {
                        FoodTruckId = Guid.NewGuid(), Name = "Burger Truck",
                        LocationId = context.Locations.First(l => l.Name == "Main Stage").LocationId,
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new FoodTruck
                    {
                        FoodTruckId = Guid.NewGuid(), Name = "Pizza Truck",
                        LocationId = context.Locations.First(l => l.Name == "Jazz Lounge").LocationId,
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new FoodTruck
                    {
                        FoodTruckId = Guid.NewGuid(), Name = "Fries Truck",
                        LocationId = context.Locations.First(l => l.Name == "Main Stage").LocationId,
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new FoodTruck
                    {
                        FoodTruckId = Guid.NewGuid(), Name = "Ice Cream Truck",
                        LocationId = context.Locations.First(l => l.Name == "Beach Arena").LocationId,
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    }
                );
                context.SaveChanges();
            }

            if (!context.MenuItems.Any())
            {
                // Seed Menu Items
                context.MenuItems.AddRange(
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "The Classic Smash", Price = 9.50M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Burger Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Spicy Blaze Burger ", Price = 10.50M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Burger Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "BBQ Bacon Stack", Price = 11.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Burger Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "The Veggie Supreme", Price = 9.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Burger Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Hawaiian Heatwave ", Price = 10.50M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Burger Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Classic Margherita", Price = 12.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Pizza Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Pepperoni Inferno", Price = 13.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Pizza Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Veggie Medley", Price = 12.50M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Pizza Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Classic Double Scoop Cone", Price = 5.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Ice Cream Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Tropical Sundae Bowl", Price = 6.50M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Ice Cream Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Chocolate Overload Milkshake", Price = 7.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Ice Cream Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Build-Your-Own Ice Cream Sandwich", Price = 6.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Ice Cream Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Classic Belgian Fries", Price = 4.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Fries Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Loaded Belgian Fries", Price = 6.50M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Fries Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Frikandel with Belgian Fries", Price = 10.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Fries Truck").FoodTruckId
                    },
                    new MenuItem
                    {
                        MenuItemId = Guid.NewGuid(), Name = "Belgian Cheese Sauce Fries", Price = 12.00M,
                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Fries Truck").FoodTruckId
                    }
                );
                context.SaveChanges();
            }

            if (!context.Sponsors.Any())
            {
                // Seed Sponsors
                context.Sponsors.AddRange(
                    new Sponsor
                    {
                        SponsorId = Guid.NewGuid(), SponsorName = "TechCo", Amount = 50000,
                        SponsoredItem = "Stages Equipment",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId, SponsorMail = ""
                    },
                    new Sponsor
                    {
                        SponsorId = Guid.NewGuid(), SponsorName = "DrinkCorp", Amount = 20000,
                        SponsoredItem = "Refreshments",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId, SponsorMail = ""
                    },
                    new Sponsor
                    {
                        SponsorId = Guid.NewGuid(), SponsorName = "Foodies Ltd", Amount = 30000,
                        SponsoredItem = "Food Stalls",
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId, SponsorMail = ""
                    }
                );
                context.SaveChanges();
            }

            if (!context.Articles.Any())
            {
                // Seed Articles
                context.Articles.AddRange(
                    new Article
                    {
                        ArticleId = Guid.NewGuid(), Title = "Fritfest 2024 Highlights",
                        Description = "The best moments from Fritfest 2024",
                        Date = DateTime.Parse("2024-06-30 09:00:00"),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    },
                    new Article
                    {
                        ArticleId = Guid.NewGuid(), Title = "Fritfest 2024 Lineup",
                        Description = "The artists performing at Fritfest 2024",
                        Date = DateTime.Parse("2024-06-30 09:00:00"),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
                    }
                );
                context.SaveChanges();
            }
            

            if (!context.Days.Any())
            {
                context.Days.AddRange(
                    new Day
                    {
                        DayId = Guid.NewGuid(), Name = "Friday"
                    },
                    new Day
                    {
                        DayId = Guid.NewGuid(), Name = "Saturday", 
                    },
                    new Day
                    {
                        DayId = Guid.NewGuid(), Name = "Sunday", 
                    }
                );
                context.SaveChanges();
            }

            if (!context.Tickets.Any())
            {
                // Seed  Tickets
                context.Tickets.AddRange(
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "General Admission").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Friday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "Super").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Friday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "VIP").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Friday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "General Admission").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Saturday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "Super").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Saturday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "VIP").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Saturday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "General Admission").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Sunday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "Super").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Sunday").DayId
                    },
                    new Ticket
                    {
                        TicketId = Guid.NewGuid(),
                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
                        TicketTypeId = context.TicketTypes.First(t => t.Name == "VIP").TicketTypeId,
                        DayId = context.Days.First(d => d.Name == "Sunday").DayId
                    }
                );
                context.SaveChanges();
            }



            // Seed Bought Tickets
            if (!context.BoughtTickets.Any())
            {
                context.BoughtTickets.AddRange(
                    new BoughtTicket
                    {
                        BoughtTicketId = Guid.NewGuid(), BuyerName = "Headmaster",
                        BuyerMail = "headmaster@chocoprins.cp", HolderName = "Headmaster",
                        HolderMail = "headmaster@chocoprins.cp",
                        TicketId = context.Tickets.First(t =>
                            t.TicketTypeId == context.TicketTypes.First(tt => tt.Name == "General Admission")
                                .TicketTypeId && t.DayId == context.Days.First(d => d.Name == "Friday").DayId).TicketId,
                        Payed = false
                    },
                    new BoughtTicket
                    {
                        BoughtTicketId = Guid.NewGuid(), BuyerName = "Headmaster",
                        BuyerMail = "headmaster@chocoprins.cp", HolderName = "Arnould van Heacke",
                        HolderMail = "arnouldvanheacke@chocoprins.cp",
                        TicketId = context.Tickets.First(t =>
                            t.TicketTypeId == context.TicketTypes.First(tt => tt.Name == "General Admission")
                                .TicketTypeId && t.DayId == context.Days.First(d => d.Name == "Friday").DayId).TicketId,
                        Payed = false
                    },
                    new BoughtTicket
                    {
                        BoughtTicketId = Guid.NewGuid(), BuyerName = "Headmaster",
                        BuyerMail = "headmaster@chocoprins.cp", HolderName = "Chocoprins Joris",
                        HolderMail = "chocoprinsJoris@chocoprins.cp",
                        TicketId = context.Tickets.First(t =>
                            t.TicketTypeId == context.TicketTypes.First(tt => tt.Name == "General Admission")
                                .TicketTypeId && t.DayId == context.Days.First(d => d.Name == "Friday").DayId).TicketId,
                        Payed = false
                    }
                );
                context.SaveChanges();  
            }

            if (!context.TimeSlots.Any())
            {
                context.TimeSlots.AddRange(
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(), 
                        StartTime = DateTime.Parse("2025-07-18 12:00:00"),
                        EndTime = DateTime.Parse("2025-07-18 13:30:00"),
                        DayId = context.Days.First(d => d.Name == "Friday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Main Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Lost Frequencies").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-18 14:00:00"),
                        EndTime = DateTime.Parse("2025-07-18 15:30:00"),
                        DayId = context.Days.First(d => d.Name == "Friday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Dance Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "De Romeo's").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-18 16:00:00"),
                        EndTime = DateTime.Parse("2025-07-18 17:30:00"),
                        DayId = context.Days.First(d => d.Name == "Friday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Pop Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Regi").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-19 12:00:00"),
                        EndTime = DateTime.Parse("2025-07-19 13:30:00"),
                        DayId = context.Days.First(d => d.Name == "Saturday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Main Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Bart Peeters").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-19 14:00:00"),
                        EndTime = DateTime.Parse("2025-07-19 15:30:00"),
                        DayId = context.Days.First(d => d.Name == "Saturday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Dance Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Clouseau").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-19 16:00:00"),
                        EndTime = DateTime.Parse("2025-07-19 17:30:00"),
                        DayId = context.Days.First(d => d.Name == "Saturday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Pop Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Camille").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-20 12:00:00"),
                        EndTime = DateTime.Parse("2025-07-20 13:30:00"),
                        DayId = context.Days.First(d => d.Name == "Sunday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Main Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Pommelien Thijs").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-20 14:00:00"),
                        EndTime = DateTime.Parse("2025-07-20 15:30:00"),
                        DayId = context.Days.First(d => d.Name == "Sunday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Dance Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Metejoor").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-20 16:00:00"),
                        EndTime = DateTime.Parse("2025-07-20 17:30:00"),
                        DayId = context.Days.First(d => d.Name == "Sunday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Pop Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Laura Tesoro").ArtistId
                    },
                    new TimeSlot
                    {
                        TimeSlotId = Guid.NewGuid(),
                        StartTime = DateTime.Parse("2025-07-20 19:00:00"),
                        EndTime = DateTime.Parse("2025-07-20 20:30:00"),
                        DayId = context.Days.First(d => d.Name == "Sunday").DayId,
                        StageId = context.Stages.First(s => s.Name == "Main Stage").StageId,
                        ArtistId = context.Artists.First(a => a.Name == "Average Rob").ArtistId
                    }

                );
                context.SaveChanges();
            }



            // Save all changes to the database
            context.SaveChanges();
        }
    }
}
