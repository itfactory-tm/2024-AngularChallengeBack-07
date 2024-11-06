using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Podium
    {
        [Key]
        public Guid PodiumId { get; set; }
        public string Naam { get; set; }

        [ForeignKey(nameof(Locatie))]
        public Guid locatieId { get; set; }
        public Locatie Locatie { get; set; }

        public ICollection<TijdStip> TijdStippen { get; set; }
        public ICollection<Foto> Fotos { get; set; }
    }
}
