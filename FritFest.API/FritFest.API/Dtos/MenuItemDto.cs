using System;

namespace FritFest.API.Dtos
{
    public class MenuItemDto
    {
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid FoodTruckId { get; set; }
        public string FoodTruckName { get; set; } // Maps FoodTrucks.Naam
    }
}
