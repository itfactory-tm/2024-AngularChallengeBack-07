﻿
using FritFest.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FritFest.API.DbContexts
{
    public class FestivalContext : DbContext
    {
        public FestivalContext(DbContextOptions<FestivalContext> options) : base(options)
        {
        }
        public DbSet<Artiest> Artiest { get; set; }
        public DbSet<Artikel> Artikel { get; set; }
        public DbSet<Dag> Dag { get; set; }
        public DbSet<Editie> Editie { get; set; }
        public DbSet<FoodTruck> FoodTruck { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Locatie> Locatie { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Podium> Podium { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<TijdStip> TijdStip{ get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TijdStip>().HasKey(ts => new {ts.ArtiestId, ts.PodiumId });
        }
    }
}
