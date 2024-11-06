using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Foto
    {
        [Key]
        public Guid FotoId { get; set; }
        public string Bestand { get; set; }
        public string Beschrijving { get; set; }

        [ForeignKey(nameof(Editie))]
        public Guid EditieId { get; set; }
        public Editie Editie { get; set; }

        [ForeignKey(nameof(Artikel))]
        public Guid ArtikelId { get; set; }
        public Artikel Artikel { get; set; }

        [ForeignKey(nameof(Podium))]
        public Guid PodiumId { get; set; }
        public Podium Podium { get; set; }
    }
}
