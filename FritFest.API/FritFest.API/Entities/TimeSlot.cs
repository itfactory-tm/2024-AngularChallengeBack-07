using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class TimeSlot
    {
        [Key]
        public Guid TimeSlotId { get; set; } // Primary Key for TijdStip

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        [ForeignKey(nameof(Artist))]
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        [ForeignKey(nameof(Stage))]
        public Guid StageId { get; set; }
        public Stage Stage { get; set; }
    }
}
