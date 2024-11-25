using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class SponsorDto
    {
        public Guid SponsorId { get; set; }
        public string SponsorNaam { get; set; }
        public int Hoeveelheid { get; set; }
        public string GesponsordeItem { get; set; }
        public List<string> Edities { get; set; } // Assuming EditieDto exists
    }
}
