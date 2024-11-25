using System;
using System.Collections.Generic;
using FritFest.API.Entities;

namespace FritFest.API.Dtos
{
    public class FoodTruckDto
    {
        public Guid FoodTruckId { get; set; }
        public string Naam { get; set; }
        public Guid LocatieId { get; set; }
        public string LocatieNaam { get; set; } // Maps from Locatie.Naam if available
        public List<string> Edities { get; set; } // Number of related editions
        public int MenuItemCount { get; set; }  // Number of related menu items
    }
}
