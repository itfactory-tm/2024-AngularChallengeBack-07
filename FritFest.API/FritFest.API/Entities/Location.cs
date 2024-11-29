using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Location
    {
        [Key]
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }

        public ICollection<FoodTruck> FoodTrucks { get; set; }
        public ICollection<Stage> Stages { get; set; }
    }
}
