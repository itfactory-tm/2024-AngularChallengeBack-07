using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class MenuItem
    {
        [Key]
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(FoodTruck))]
        public Guid FoodTruckId { get; set; }
        public FoodTruck FoodTruck { get; set; }

    }
}
