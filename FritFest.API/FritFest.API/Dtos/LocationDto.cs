using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class LocationDto
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }

        // Lists to hold names of related entities for easier readability
        public List<string> FoodTruckNames { get; set; }
        public List<string> StageNames { get; set; }
    }
}
