using FritFest.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Data
{
    public class FestivalContext: DbContext
    {
        public FestivalContext() { }
        public FestivalContext(DbContextOptions<FestivalContext> options) : base(options) { }

        public DbSet<Artiest> Artiesten { get; set; }
        public DbSet<Dag> Dagen { get; set; }
        public DbSet<DagList> DagLijsten { get; set; }
        public DbSet<Editie> Edities { get; set; }
        public DbSet<FoodTruck> FoodTrucks { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Locatie> Locaties { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<NieuwsArtikel> Artikels { get; set; }
        public DbSet<Podium> Podiums { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<SponsorLijst> sponsorLijsten { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TijdStip> TijdStippen { get; set; }
        public DbSet<TruckList> TruckLijsten { get; set; }

    }
}
