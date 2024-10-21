using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Sponsor
    {
        [Key]
        public Guid SponsorId { get; set; }
        public string SponsorNaam { get; set; }
        public int Hoeveelheid { get; set; }
        public string GesponsordeItem { get; set; }

        public ICollection<Editie> Editie { get; set; }
    }
}
