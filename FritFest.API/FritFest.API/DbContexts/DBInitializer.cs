//using FritFest.API.DbContexts;
//using FritFest.API.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;

//namespace FritFest.API
//{
//    public class DbInitializer
//    {
//        public static void Initialize(FestivalContext context)
//        {
//            // Ensure database is created
//            context.Database.Migrate();

//            if (context.Editions.Any())
//            {
//                return;
//            }

//            // Create the necessary IDs to link entities

//            if (!context.Editions.Any())
//            {
//                // Seed Editions
//                context.Editions.Add(new Edition
//                {
//                    EditionId = Guid.NewGuid(),
//                    EditionName = "Fritfest",
//                    Adres = "Main Street 123",
//                    ZipCode = "1000",
//                    Municipality = "Brussels",
//                    PhoneNr = "02-1234567",
//                    Mail = "info@fritfest.be",
//                    Year = 2024
//                });
//                context.SaveChanges();
//            }

//            if (!context.Genres.Any())
//            {
//                // Seed Genres
//                context.Genres.AddRange(
//                    new Genre { GenreId = Guid.NewGuid(), Name = "Rock" },
//                    new Genre { GenreId = Guid.NewGuid(), Name = "Pop" },
//                    new Genre { GenreId = Guid.NewGuid(), Name = "Jazz" },
//                    new Genre { GenreId = Guid.NewGuid(), Name = "Electronic" }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Artists.Any())
//            {

//                // Seed Artists
//                context.Artists.AddRange(
//                    new Artist
//                    {
//                        ArtistId = Guid.NewGuid(), Name = "The Rockers", Mail = "rockers@music.com",
//                        Description = "A famous rock band", SpotifyLink = "spotify.com/therockers",
//                        ApiCode = "22Wzsyh7moQAwSODsMF6w2", SpotifyPhoto = "", Genre = "", EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                    },
//                    new Artist
//                    {
//                        ArtistId = Guid.NewGuid(), Name = "DJ Spin", Mail = "djspin@beats.com",
//                        Description = "A well-known electronic DJ", SpotifyLink = "spotify.com/djspin",
//                        ApiCode = "22Wzsyh7moQAwSODsMF6w2", SpotifyPhoto = "", Genre = "",EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    },
//                    new Artist
//                    {
//                        ArtistId = Guid.NewGuid(), Name = "PopStar", Mail = "popstar@music.com",
//                        Description = "A pop music sensation", SpotifyLink = "spotify.com/popstar", ApiCode = "",
//                        SpotifyPhoto = "", Genre = "",EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    },
//                    new Artist
//                    {
//                        ArtistId = Guid.NewGuid(), Name = "Jazz Quartet", Mail = "jazzquartet@jazz.com",
//                        Description = "A group of jazz musicians", SpotifyLink = "spotify.com/jazzquartet",
//                        SpotifyPhoto = "", ApiCode = "", Genre = "",EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    },
//                    new Artist
//                    {
//                        ArtistId = Guid.NewGuid(), Name = "SPINALL", Mail = "test@123.com", Description = "",
//                        SpotifyLink = "https://open.spotify.com/artist/2NtQA3PY9chI8l65ejZLTP?si=61d672e3af404c55",
//                        SpotifyPhoto = "", ApiCode = "2NtQA3PY9chI8l65ejZLTP", Genre = "",EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    }
//                );
//                context.SaveChanges();
//            }
//            // Seed Locations

//            if (!context.Locations.Any())
//            {
//                context.Locations.AddRange(
//                    new Location
//                        { LocationId = Guid.NewGuid(), Name = "Main Stage", Longitude = 52.366, Latitude = 4.904 },
//                    new Location
//                        { LocationId = Guid.NewGuid(), Name = "Beach Arena", Longitude = 51.922, Latitude = 4.481 },
//                    new Location
//                        { LocationId = Guid.NewGuid(), Name = "Jazz Lounge", Longitude = 52.364, Latitude = 4.903 }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Stages.Any())
//            {
//                // Seed Stages
//                context.Stages.AddRange(
//                    new Stage
//                    {
//                        StageId = Guid.NewGuid(), Name = "Test1",
//                        LocationId = context.Locations.First(l => l.Name == "Main Stage").LocationId
//                    },
//                    new Stage
//                    {
//                        StageId = Guid.NewGuid(), Name = "Test2",
//                        LocationId = context.Locations.First(l => l.Name == "Beach Arena").LocationId
//                    },
//                    new Stage
//                    {
//                        StageId = Guid.NewGuid(), Name = "Test3",
//                        LocationId = context.Locations.First(l => l.Name == "Jazz Lounge").LocationId
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Days.Any())
//            {
//                // Seed Days
//                context.Days.AddRange(
//                    new Day
//                    {
//                        DayId = Guid.NewGuid(), Name = "Friday", StartDate = DateTime.Parse("2024-07-01 10:00:00"),
//                        EndDate = DateTime.Parse("2024-07-01 22:00:00")
//                    },
//                    new Day
//                    {
//                        DayId = Guid.NewGuid(), Name = "Saturday", StartDate = DateTime.Parse("2024-07-02 10:00:00"),
//                        EndDate = DateTime.Parse("2024-07-02 22:00:00")
//                    },
//                    new Day
//                    {
//                        DayId = Guid.NewGuid(), Name = "Sunday", StartDate = DateTime.Parse("2024-07-03 10:00:00"),
//                        EndDate = DateTime.Parse("2024-07-03 22:00:00")
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.TicketTypes.Any())
//            {
//                // Seed Tickets Types
//                context.TicketTypes.AddRange(
//                    new TicketType { TicketTypeId = Guid.NewGuid(), Name = "General Admission", Price = 10.00 },
//                    new TicketType { TicketTypeId = Guid.NewGuid(), Name = "Super", Price = 12.00 },
//                    new TicketType { TicketTypeId = Guid.NewGuid(), Name = "VIP", Price = 15.00 }
//                );
//                context.SaveChanges();
//            }

//            if (!context.FoodTrucks.Any())
//            {

//                // Seed Food Trucks
//                context.FoodTrucks.AddRange(
//                    new FoodTruck
//                    {
//                        FoodTruckId = Guid.NewGuid(), Name = "Burger Truck",
//                        LocationId = context.Locations.First(l => l.Name == "Main Stage").LocationId,
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    },
//                    new FoodTruck
//                    {
//                        FoodTruckId = Guid.NewGuid(), Name = "Pizza Truck",
//                        LocationId = context.Locations.First(l => l.Name == "Jazz Lounge").LocationId,
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    },
//                    new FoodTruck
//                    {
//                        FoodTruckId = Guid.NewGuid(), Name = "Ice Cream Truck",
//                        LocationId = context.Locations.First(l => l.Name == "Beach Arena").LocationId,
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.MenuItems.Any())
//            {
//                // Seed Menu Items
//                context.MenuItems.AddRange(
//                    new MenuItem
//                    {
//                        MenuItemId = Guid.NewGuid(), Name = "Cheeseburger", Price = 10.00M,
//                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Burger Truck").FoodTruckId
//                    },
//                    new MenuItem
//                    {
//                        MenuItemId = Guid.NewGuid(), Name = "Veggie Burger", Price = 12.00M,
//                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Burger Truck").FoodTruckId
//                    },
//                    new MenuItem
//                    {
//                        MenuItemId = Guid.NewGuid(), Name = "Margherita Pizza", Price = 8.00M,
//                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Pizza Truck").FoodTruckId
//                    },
//                    new MenuItem
//                    {
//                        MenuItemId = Guid.NewGuid(), Name = "Pepperoni Pizza", Price = 10.00M,
//                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Pizza Truck").FoodTruckId
//                    },
//                    new MenuItem
//                    {
//                        MenuItemId = Guid.NewGuid(), Name = "Vanilla Ice Cream", Price = 3.00M,
//                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Ice Cream Truck").FoodTruckId
//                    },
//                    new MenuItem
//                    {
//                        MenuItemId = Guid.NewGuid(), Name = "Chocolate Ice Cream", Price = 3.50M,
//                        FoodTruckId = context.FoodTrucks.First(ft => ft.Name == "Ice Cream Truck").FoodTruckId
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Sponsors.Any())
//            {
//                // Seed Sponsors
//                context.Sponsors.AddRange(
//                    new Sponsor
//                    {
//                        SponsorId = Guid.NewGuid(), SponsorName = "TechCo", Amount = 50000,
//                        SponsoredItem = "Stages Equipment", SponsorLogo = "",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId, SponsorMail = ""
//                    },
//                    new Sponsor
//                    {
//                        SponsorId = Guid.NewGuid(), SponsorName = "DrinkCorp", Amount = 20000,
//                        SponsoredItem = "Refreshments", SponsorLogo = "",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId, SponsorMail = ""
//                    },
//                    new Sponsor
//                    {
//                        SponsorId = Guid.NewGuid(), SponsorName = "Foodies Ltd", Amount = 30000,
//                        SponsoredItem = "Food Stalls", SponsorLogo = "",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId, SponsorMail = ""
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Articles.Any())
//            {
//                // Seed Articles
//                context.Articles.AddRange(
//                    new Article
//                    {
//                        ArticleId = Guid.NewGuid(), Title = "Fritfest 2024 Highlights",
//                        Description = "The best moments from Fritfest 2024",
//                        Date = DateTime.Parse("2024-06-30 09:00:00"),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    },
//                    new Article
//                    {
//                        ArticleId = Guid.NewGuid(), Title = "Fritfest 2024 Lineup",
//                        Description = "The artists performing at Fritfest 2024",
//                        Date = DateTime.Parse("2024-06-30 09:00:00"),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Photos.Any())
//            {
//                // Seed Photos
//                context.Photos.AddRange(
//                    new Photo
//                    {
//                        PhotoId = Guid.NewGuid(), File = "photo1.jpg", Description = "Main Stage Crowd",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        ArticleId = context.Articles.First(a => a.Title == "Fritfest 2024 Highlights").ArticleId,
//                        StageId = context.Stages.First(s => s.Name == "Test1").StageId
//                    },
//                    new Photo
//                    {
//                        PhotoId = Guid.NewGuid(), File = "photo2.jpg", Description = "DJ Spin Performing",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        ArticleId = context.Articles.First(a => a.Title == "Fritfest 2024 Lineup")
//                            .ArticleId,
//                        StageId = context.Stages.First(s => s.Name == "Test2").StageId
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Days.Any())
//            {
//                context.Days.AddRange(
//                    new Day
//                    {
//                        DayId = Guid.NewGuid(), Name = "Friday", StartDate = DateTime.Parse("2024-07-01 10:00:00"),
//                        EndDate = DateTime.Parse("2024-07-01 22:00:00")
//                    },
//                    new Day
//                    {
//                        DayId = Guid.NewGuid(), Name = "Saturday", StartDate = DateTime.Parse("2024-07-02 10:00:00"),
//                        EndDate = DateTime.Parse("2024-07-02 22:00:00")
//                    },
//                    new Day
//                    {
//                        DayId = Guid.NewGuid(), Name = "Sunday", StartDate = DateTime.Parse("2024-07-03 10:00:00"),
//                        EndDate = DateTime.Parse("2024-07-03 22:00:00")
//                    }
//                );
//                context.SaveChanges();
//            }

//            if (!context.Tickets.Any())
//            {
//                // Seed  Tickets
//                context.Tickets.AddRange(
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "General Admission").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Friday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "Super").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Friday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "VIP").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Friday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "General Admission").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Saturday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "Super").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Saturday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "VIP").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Saturday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "General Admission").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Sunday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "Super").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Sunday").DayId
//                    },
//                    new Ticket
//                    {
//                        TicketId = Guid.NewGuid(),
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketTypeId = context.TicketTypes.First(t => t.Name == "VIP").TicketTypeId,
//                        DayId = context.Days.First(d => d.Name == "Sunday").DayId
//                    }
//                );
//                context.SaveChanges();
//            }



//            // Seed Bought Tickets
//            if (!context.BoughtTickets.Any())
//            {
//                context.BoughtTickets.AddRange(
//                    new BoughtTicket
//                    {
//                        BoughtTicketId = Guid.NewGuid(), BuyerName = "Headmaster",
//                        BuyerMail = "headmaster@chocoprins.cp", HolderName = "Headmaster",
//                        HolderMail = "headmaster@chocoprins.cp",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketId = context.Tickets.First(t =>
//                            t.TicketTypeId == context.TicketTypes.First(tt => tt.Name == "General Admission")
//                                .TicketTypeId && t.DayId == context.Days.First(d => d.Name == "Friday").DayId).TicketId,
//                        Payed = false
//                    },
//                    new BoughtTicket
//                    {
//                        BoughtTicketId = Guid.NewGuid(), BuyerName = "Headmaster",
//                        BuyerMail = "headmaster@chocoprins.cp", HolderName = "Arnould van Heacke",
//                        HolderMail = "arnouldvanheacke@chocoprins.cp",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketId = context.Tickets.First(t =>
//                            t.TicketTypeId == context.TicketTypes.First(tt => tt.Name == "General Admission")
//                                .TicketTypeId && t.DayId == context.Days.First(d => d.Name == "Friday").DayId).TicketId,
//                        Payed = false
//                    },
//                    new BoughtTicket
//                    {
//                        BoughtTicketId = Guid.NewGuid(), BuyerName = "Headmaster",
//                        BuyerMail = "headmaster@chocoprins.cp", HolderName = "Chocoprins Joris",
//                        HolderMail = "chocoprinsJoris@chocoprins.cp",
//                        EditionId = context.Editions.First(e => e.EditionName == "Fritfest").EditionId,
//                        TicketId = context.Tickets.First(t =>
//                            t.TicketTypeId == context.TicketTypes.First(tt => tt.Name == "General Admission")
//                                .TicketTypeId && t.DayId == context.Days.First(d => d.Name == "Friday").DayId).TicketId,
//                        Payed = false
//                    }
//                );
//                context.SaveChanges();  
//            }

//            if (!context.TimeSlots.Any())
//            {
//                context.TimeSlots.AddRange(
//                    new TimeSlot
//                    {
//                        TimeSlotId = Guid.NewGuid(), 
//                        StartTime = DateTime.Parse("2024-07-01 10:00:00"),
//                        EndTime = DateTime.Parse("2024-07-01 10:30:00"),
//                        StageId = context.Stages.First(s => s.Name == "Test1").StageId,
//                        ArtistId = context.Artists.First(a => a.Name == "The Rockers").ArtistId
//                    },
//                    new TimeSlot
//                    {
//                        TimeSlotId = Guid.NewGuid(),
//                        StartTime= DateTime.Parse("2024-07-01 10:00:00"),
//                        EndTime = DateTime.Parse("2024-07-01 10:30:00"),
//                        StageId = context.Stages.First(s => s.Name == "Test1").StageId,
//                        ArtistId = context.Artists.First(a => a.Name == "The Rockers").ArtistId
//                    }
//                );
//                context.SaveChanges();
//            }



//            // Save all changes to the database
//            context.SaveChanges();
//        }
//    }
//}
