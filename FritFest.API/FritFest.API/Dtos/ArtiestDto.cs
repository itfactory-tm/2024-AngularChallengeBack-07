using FritFest.API.Entities;

namespace FritFest.API.Dtos
{
    public class ArtiestDto
    {
        public Guid ArtistId { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
