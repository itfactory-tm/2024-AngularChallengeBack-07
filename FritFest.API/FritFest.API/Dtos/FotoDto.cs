using System;

namespace FritFest.API.Dtos
{
    public class FotoDto
    {
        public Guid FotoId { get; set; }
        public string Bestand { get; set; }
        public string Beschrijving { get; set; }

        public Guid EditieId { get; set; }
        public string EditieNaam { get; set; }   // Mapped from Editie.EditieNaam

        public Guid ArtikelId { get; set; }
        public string ArtikelTitel { get; set; } // Mapped from Artikel.Titel

        public Guid PodiumId { get; set; }
        public string PodiumNaam { get; set; }   // Mapped from Podium.Naam
    }
}
