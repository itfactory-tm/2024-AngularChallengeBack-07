using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Stage
    {
        [Key]
        public Guid StageId { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Location))]
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<TimeSlot> TimeSlots { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
