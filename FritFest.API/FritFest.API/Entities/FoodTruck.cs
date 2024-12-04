using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class FoodTruck
    {
        [Key]
        public Guid FoodTruckId { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Location))]
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        
        public Edition Edition { get; set; }

        [ForeignKey(nameof(Edition))]
        public Guid EditionId { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
