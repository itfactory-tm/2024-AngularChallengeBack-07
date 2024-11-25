namespace FritFest.API.Dtos
{
    public class ArtiestDto
    {
        public Guid ArtiestId { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Beschrijving { get; set; }
        public string SpotifyApi { get; set; }
        public Guid GenreId { get; set; }
        public string GenreNaam { get; set; } // Optional, if you want to include genre name
        public ICollection<EditieDto> Edities { get; set; }
    }
}
