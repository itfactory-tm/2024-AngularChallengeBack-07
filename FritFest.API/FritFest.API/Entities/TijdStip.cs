using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class TijdStip
    {
        [Key]
        public Guid TijdStipId { get; set; } // Primary Key for TijdStip

        public DateTime Tijd { get; set; }

        
        [ForeignKey(nameof(Artiest))]
        public Guid ArtiestId { get; set; }
        public Artiest Artiest { get; set; }
        
        [ForeignKey(nameof(Podium))]
        public Guid PodiumId { get; set; }
        public Podium Podium { get; set; }
    }
}
