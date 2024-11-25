using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class PodiumDto
    {
        public Guid PodiumId { get; set; }
        public string Naam { get; set; }
        public Guid locatieId { get; set; }
        public string LocatieNaam { get; set; } // Maps Locatie.Naam to this property
        public List<DateTime> TijdStippen { get; set; } // Assuming TijdStipDto exists
        public List<string> Fotos { get; set; } // Assuming FotoDto exists
    }
}
