using System;
using System.Collections.Generic;
using FritFest.API.Entities;

namespace FritFest.API.Dtos
{
    public class FoodTruckDto
    {
        public Guid FoodTruckId { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } // Maps from Locatie.Naam if available
        public Guid EditionId { get; set; }
        public string EditionName { get; set; }
        /*public int MenuItemCount { get; set; } */ // Number of related menu items
    }
}
