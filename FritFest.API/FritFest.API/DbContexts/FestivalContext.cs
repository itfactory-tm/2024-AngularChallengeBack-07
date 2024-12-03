
using FritFest.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FritFest.API.DbContexts
{
    public class FestivalContext : DbContext
    {
        public FestivalContext(DbContextOptions<FestivalContext> options) : base(options)
        {
        }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<Edition> Edition { get; set; }
        public DbSet<FoodTruck> FoodTruck { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<BoughtTicket> BoughtTicket { get; set; }
        public DbSet<TimeSlot> TimeSlot{ get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TimeSlot>().HasKey(ts => new {ts.ArtistId, ts.StageId });
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



           

            //Association between Artiest and Editie
            //modelBuilder.Entity<Artist>()
            //    .HasMany(a => a.Edition)
            //    .WithMany(e => e.Artists)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ArtistList",
            //        join => join.HasOne<Edition>().WithMany().HasForeignKey("editionId"),
            //        join => join.HasOne<Artist>().WithMany().HasForeignKey("artistId")
            //    );

            //Association between Sponsor and Editie
            //modelBuilder.Entity<Sponsor>()
            //    .HasMany(a => a.Edition)
            //    .WithMany(e => e.Sponsors)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "SponsorList",
            //        join => join.HasOne<Edition>().WithMany().HasForeignKey("editionId"),
            //        join => join.HasOne<Sponsor>().WithMany().HasForeignKey("sponsorId")
            //    );

            //Association between Foodtruck and Editie
            //modelBuilder.Entity<FoodTruck>()
            //    .HasMany(a => a.Editions)
            //    .WithMany(e => e.Foodtrucks)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "TruckList",
            //        join => join.HasOne<Edition>().WithMany().HasForeignKey("editionId"),
            //        join => join.HasOne<FoodTruck>().WithMany().HasForeignKey("foodTruckId")
            //    );

        }
    }
}
