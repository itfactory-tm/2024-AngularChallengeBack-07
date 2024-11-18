using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class LocatieDto
    {
        public Guid LocatieId { get; set; }
        public string Naam { get; set; }
        public string Coordinaten { get; set; }

        // Lists to hold names of related entities for easier readability
        public List<string> FoodTruckNamen { get; set; }
        public List<string> PodiumNamen { get; set; }
    }
}
