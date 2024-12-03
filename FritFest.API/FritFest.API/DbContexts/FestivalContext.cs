
using FritFest.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FritFest.API.DbContexts
{
    public class FestivalContext : DbContext
    {
        public DbSet<Edition> Edition { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<TimeSlot> TimeSlot { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<BoughtTicket> BoughtTicket { get; set; }
        public DbSet<DayList> DayList { get; set; }
        public DbSet<FoodTruck> FoodTruck { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Photo> Photo { get; set; }

        // Constructor to pass options to the base class (DbContext)
        public FestivalContext(DbContextOptions<FestivalContext> options) : base(options) { }

        // Configuring relationships and table names
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Edition>().HasKey(e => e.EditionId);
            modelBuilder.Entity<Genre>().HasKey(g => g.GenreId);
            modelBuilder.Entity<Artist>().HasKey(a => a.ArtistId);
            modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
            modelBuilder.Entity<Stage>().HasKey(s => s.StageId);
            modelBuilder.Entity<Day>().HasKey(d => d.DayId);
            modelBuilder.Entity<TimeSlot>().HasKey(t => t.TimeSlotId);
            modelBuilder.Entity<TicketType>().HasKey(t => t.TicketTypeId);
            modelBuilder.Entity<Ticket>().HasKey(t => t.TicketId);
            modelBuilder.Entity<BoughtTicket>().HasKey(b => b.BoughtTicketId);
            modelBuilder.Entity<DayList>().HasKey(d => new { d.TicketId, d.DayId });
            modelBuilder.Entity<FoodTruck>().HasKey(f => f.FoodTruckId);
            modelBuilder.Entity<MenuItem>().HasKey(m => m.MenuItemId);
            modelBuilder.Entity<Sponsor>().HasKey(s => s.SponsorId);
            modelBuilder.Entity<Article>().HasKey(a => a.ArticleId);
            modelBuilder.Entity<Photo>().HasKey(p => p.PhotoId);

            // Configure GUID generation
            modelBuilder.Entity<Edition>().Property(e => e.EditionId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Genre>().Property(g => g.GenreId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Artist>().Property(a => a.ArtistId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Location>().Property(l => l.LocationId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Stage>().Property(s => s.StageId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Day>().Property(d => d.DayId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<TimeSlot>().Property(t => t.TimeSlotId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<TicketType>().Property(t => t.TicketTypeId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Ticket>().Property(t => t.TicketId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<BoughtTicket>().Property(b => b.BoughtTicketId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<FoodTruck>().Property(f => f.FoodTruckId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<MenuItem>().Property(m => m.MenuItemId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Sponsor>().Property(s => s.SponsorId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Article>().Property(a => a.ArticleId).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Photo>().Property(p => p.PhotoId).HasDefaultValueSql("gen_random_uuid()");
        }
    }
}
