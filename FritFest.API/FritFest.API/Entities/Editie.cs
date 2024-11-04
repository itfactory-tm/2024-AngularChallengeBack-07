using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Editie
    {
        [Key]
        public Guid EditieId { get; set; }
        public string EditieNaam { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string TelNr { get; set; }
        public string Email { get; set; }
        public int Jaar { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Artiest> Artiesten { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public ICollection<Artikel> Artikelen { get; set; }
        public ICollection<Sponsor> Sponsors { get; set; }
        public ICollection<FoodTruck> Foodtrucks { get; set; }

    }
}
