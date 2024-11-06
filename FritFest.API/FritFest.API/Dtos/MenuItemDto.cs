using System;

namespace FritFest.API.Dtos
{
    public class MenuItemDto
    {
        public Guid MenuItemId { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public Guid FoodTruckId { get; set; }
        public string FoodTruckNaam { get; set; } // Maps FoodTruck.Naam
    }
}
