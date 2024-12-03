
using FritFest.API.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FritFest.API.DbContexts
{
    public class FestivalContext : DbContext
    {
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<BoughtTicket> BoughtTickets { get; set; }
        public DbSet<DayList> DayLists { get; set; }
        public DbSet<FoodTruck> FoodTrucks { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        // Constructor to pass options to the base class (DbContext)
        public FestivalContext(DbContextOptions<FestivalContext> options) : base(options) { }

        // Configuring relationships and table names
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Edition>().ToTable("Edition");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<FoodTruck>().ToTable("FoodTruck");
            modelBuilder.Entity<Sponsor>().ToTable("Sponsor");
            modelBuilder.Entity<Artist>().ToTable("Artist");
            modelBuilder.Entity<BoughtTicket>().ToTable("BoughtTicket");
            modelBuilder.Entity<Day>().ToTable("Day");
            modelBuilder.Entity<DayList>().ToTable("DayList");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<MenuItem>().ToTable("MenuItem");
            modelBuilder.Entity<Photo>().ToTable("Photo");
            modelBuilder.Entity<Stage>().ToTable("Stage");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<TicketType>().ToTable("TicketType");
            modelBuilder.Entity<TimeSlot>().ToTable("TimeSlot");
            
            //var fritFestId = Guid.NewGuid();

            //modelBuilder.Entity<Editions>().HasData(
               
            //    new Editions
            //    {
            //        EditionId = fritFestId,
            //        EditionName = "Fritfest",
            //        Adres = "Main Street 123",
            //        ZipCode = "1000",
            //        Municipality = "Brussels",
            //        PhoneNr = "02-1234567",
            //        Mail = "info@fritfest.be",
            //        Year = 2024
            //    });

            //// Seed Genres
            //modelBuilder.Entity<Genres>().HasData(
            //    new Genres { GenreId = Guid.NewGuid(), Name = "Rock" },
            //    new Genres { GenreId = Guid.NewGuid(), Name = "Pop" },
            //    new Genres { GenreId = Guid.NewGuid(), Name = "Jazz" },
            //    new Genres { GenreId = Guid.NewGuid(), Name = "Electronic" }
            //);

            //// Seed Artists
            //modelBuilder.Entity<Artists>().HasData(
            //    new Artists { ArtistId = Guid.NewGuid(), Name = "The Rockers", Mail = "rockers@music.com", Description = "A famous rock band", SpotifyLink = "spotify.com/therockers", ApiCode = "22Wzsyh7moQAwSODsMF6w2",SpotifyPhoto="",Genres = ""},
            //    new Artists { ArtistId = Guid.NewGuid(), Name = "DJ Spin", Mail = "djspin@beats.com", Description = "A well-known electronic DJ", SpotifyLink = "spotify.com/djspin", ApiCode = "22Wzsyh7moQAwSODsMF6w2",SpotifyPhoto="", Genres = ""},
            //    new Artists { ArtistId = Guid.NewGuid(), Name = "PopStar", Mail = "popstar@music.com", Description = "A pop music sensation", SpotifyLink = "spotify.com/popstar",ApiCode="", SpotifyPhoto="",Genres = ""},
            //    new Artists { ArtistId = Guid.NewGuid(), Name = "Jazz Quartet", Mail = "jazzquartet@jazz.com", Description = "A group of jazz musicians", SpotifyLink = "spotify.com/jazzquartet", SpotifyPhoto="",ApiCode="",Genres = "" },
            //    new Artists { ArtistId = Guid.NewGuid(), Name = "SPINALL",Mail="test@123.com",Description="", SpotifyLink = "https://open.spotify.com/artist/2NtQA3PY9chI8l65ejZLTP?si=61d672e3af404c55",SpotifyPhoto="", ApiCode = "2NtQA3PY9chI8l65ejZLTP",Genres = ""}
            //);
            //var mainStageId = Guid.NewGuid();
            //var beachArenaId = Guid.NewGuid();
            //var jazzLoungeId = Guid.NewGuid();
            //// Seed Locations
            //modelBuilder.Entity<Locations>().HasData(
            //    new Locations { LocationId = mainStageId, Name = "Main Stages", Longitude = 52.366, Latitude = 4.904 },
            //    new Locations { LocationId = beachArenaId, Name = "Beach Arena", Longitude = 51.922, Latitude = 4.481 },
            //    new Locations { LocationId = jazzLoungeId, Name = "Jazz Lounge", Longitude = 52.364, Latitude = 4.903 }
            //);
          

            //modelBuilder.Entity<Stages>().HasData(
            //    new Stages { StageId = Guid.NewGuid(), Name = "Test1", LocationId =  mainStageId },
            //    new Stages {StageId = Guid.NewGuid(), Name = "Test2", LocationId = beachArenaId },
            //    new Stages {StageId = Guid.NewGuid(), Name = "Test3", LocationId = jazzLoungeId}
            //);

            //// Seed Days
            //modelBuilder.Entity<Days>().HasData(
            //    new Days { DayId = Guid.NewGuid(), Name = "Friday", StartDate = DateTime.Parse("2024-07-01 10:00:00"), EndDate = DateTime.Parse("2024-07-01 22:00:00") },
            //    new Days { DayId = Guid.NewGuid(), Name = "Saturday", StartDate = DateTime.Parse("2024-07-02 10:00:00"), EndDate = DateTime.Parse("2024-07-02 22:00:00") },
            //    new Days { DayId = Guid.NewGuid(), Name = "Sunday", StartDate = DateTime.Parse("2024-07-03 10:00:00"), EndDate = DateTime.Parse("2024-07-03 22:00:00") }
            //);

            //// Seed Tickets Types
            //modelBuilder.Entity<TicketTypes>().HasData(
            //    new TicketTypes { TicketTypeId = Guid.NewGuid(), Name = "General Admission", Price = 10.00 },
            //    new TicketTypes { TicketTypeId = Guid.NewGuid(), Name = "Super", Price = 12.00 },
            //    new TicketTypes { TicketTypeId = Guid.NewGuid(), Name = "VIP", Price = 15.00 }
            //);
            //var burgerTruckId = Guid.NewGuid();
            //var pizzaTruckId = Guid.NewGuid();
            //var iceCreamTruckId = Guid.NewGuid();
            //var editionId = fritFestId;
            //// Seed Food Trucks
            //modelBuilder.Entity<FoodTrucks>().HasData(
            //    new FoodTrucks { FoodTruckId = burgerTruckId, Name = "Burger Truck", LocationId = mainStageId, EditionId = editionId},
            //    new FoodTrucks { FoodTruckId = pizzaTruckId, Name = "Pizza Truck", LocationId = beachArenaId, EditionId  =   editionId },
            //    new FoodTrucks { FoodTruckId = iceCreamTruckId, Name = "Ice Cream Truck", LocationId = jazzLoungeId, EditionId = editionId }
            //);

            //// Seed Menu Items
            //modelBuilder.Entity<MenuItems>().HasData(
            //    new MenuItems { MenuItemId = Guid.NewGuid(), Name = "Cheeseburger", Price = 10.00M, FoodTruckId = burgerTruckId },
            //    new MenuItems { MenuItemId = Guid.NewGuid(), Name = "Veggie Burger", Price = 12.00M, FoodTruckId = burgerTruckId },
            //    new MenuItems { MenuItemId = Guid.NewGuid(), Name = "Margherita Pizza", Price = 8.00M, FoodTruckId = pizzaTruckId },
            //    new MenuItems { MenuItemId = Guid.NewGuid(), Name = "Pepperoni Pizza", Price = 10.00M, FoodTruckId = pizzaTruckId },
            //    new MenuItems { MenuItemId = Guid.NewGuid(), Name = "Vanilla Ice Cream", Price = 3.00M, FoodTruckId = iceCreamTruckId },
            //    new MenuItems { MenuItemId = Guid.NewGuid(), Name = "Chocolate Ice Cream", Price = 3.50M, FoodTruckId = iceCreamTruckId }
            //);

            //// Seed Sponsors
            //modelBuilder.Entity<Sponsors>().HasData(
            //    new Sponsors { SponsorId = Guid.NewGuid(), SponsorName = "TechCo", Amount = 50000, SponsoredItem = "Stages Equipment" ,SponsorLogo="",EditionId=fritFestId,  SponsorMail=""},
            //    new Sponsors { SponsorId = Guid.NewGuid(), SponsorName = "DrinkCorp", Amount = 20000, SponsoredItem = "Refreshments", SponsorLogo = "", EditionId = fritFestId, SponsorMail = "" },
            //    new Sponsors { SponsorId = Guid.NewGuid(), SponsorName = "Foodies Ltd", Amount = 30000, SponsoredItem = "Food Stalls", SponsorLogo = "", EditionId = fritFestId, SponsorMail = "" }
            //);

            //var articleId = Guid.NewGuid();
            //// Seed Articles
            //modelBuilder.Entity<Articles>().HasData(
            //    new Articles { ArticleId = articleId, Title = "Fritfest 2024 Highlights", Description = "The best moments from Fritfest 2024", Date = DateTime.Parse("2024-06-30 09:00:00"), EditionId = fritFestId }
            //);

            //// Seed Photos
            //modelBuilder.Entity<Photos>().HasData(
            //    new Photos { PhotoId = Guid.NewGuid(), File = "photo1.jpg", Description = "Main Stages Crowd", EditionId = fritFestId , ArticleId = articleId, StageId = mainStageId }
            //);

            


            // // Disable cascading delete on the EditionId foreign key
            modelBuilder.Entity<Photo>()
                .HasOne(p => p.Edition)   // Navigation property
                .WithMany(e => e.Photos)                // Editions does not have a navigation property for Photos
                .HasForeignKey(p => p.EditionId)
                .OnDelete(DeleteBehavior.NoAction);  // Disable cascading delete for Editions

            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.FoodTruck)
                .WithMany(f => f.MenuItems)
                .HasForeignKey(mi => mi.FoodTruckId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Artist>()
                .HasOne(a => a.Edition)
                .WithMany(e => e.Artists)
                .HasForeignKey(a => a.EditionId)
                .OnDelete(DeleteBehavior.NoAction);
           




            modelBuilder.Entity<DayList>()
                .HasKey(dl => new { dl.TicketId, dl.DayId });
            
            modelBuilder.Entity<DayList>()
                .HasOne(dl => dl.Day)
                .WithMany()
                .HasForeignKey(dl => dl.DayId)
                .OnDelete(DeleteBehavior.NoAction);  // Restrict deletion if related DayLists records exist

            modelBuilder.Entity<Stage>()
    .HasMany(s => s.TimeSlots)
    .WithOne(ts => ts.Stage)
    .HasForeignKey(ts => ts.StageId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BoughtTicket>()
                .HasOne(b => b.Edition)
                .WithMany(e => e.Tickets)
                .HasForeignKey(b => b.EditionId)
                .OnDelete(DeleteBehavior.NoAction); 



            modelBuilder.Entity<DayList>()
                .HasOne(dl => dl.Ticket)
                .WithMany()
                .HasForeignKey(dl => dl.TicketId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasColumnType("DECIMAL(18, 2)");

            

            
        }

    }
}

