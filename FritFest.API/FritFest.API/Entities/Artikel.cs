using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Artikel
    {
        [Key]
        public Guid ArtikelId { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public DateTime Datum { get; set; }
        [ForeignKey(nameof(Editie))]
        public Guid EditieId { get; set; }
        public Editie Editie { get; set; }
    }
}
