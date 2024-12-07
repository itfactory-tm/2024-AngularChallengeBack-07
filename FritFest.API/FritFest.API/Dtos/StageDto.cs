using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class StageDto
    {
        public Guid StageId { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } // Maps Locatie.Naam to this property
        public List<string> TimeSlotRanges { get; set; }
        
    }
}
