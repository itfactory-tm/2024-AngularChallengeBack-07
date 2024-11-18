using System;

namespace FritFest.API.Dtos
{
    public class TijdStipDto
    {
        public Guid TijdStipId { get; set; }
        public DateTime Tijd { get; set; }
        public Guid ArtiestId { get; set; }
        public string ArtiestNaam { get; set; } // The name of the artist (Artiest)

        public Guid PodiumId { get; set; }
        public string PodiumNaam { get; set; } // The name of the podium (Podium)
    }
}
