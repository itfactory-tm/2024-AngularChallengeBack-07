namespace FritFest.API.Dtos
{
    public class ArtiestDto
    {
        public Guid ArtiestId { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Beschrijving { get; set; }
        public string SpotifyLink { get; set; }
        public string ApiCode { get; set; }
        public int? SpotifyPopularity { get; set; } // Spotify popularity
        public int? SpotifyFollowers { get; set; } // Spotify followers
        public string SpotifyPhoto { get; set; } // New - Spotify photo URL
        //public Guid GenreId { get; set; }
        public string Genre { get; set; } // Optional, if you want to include genre name
        public List<string> Edities { get; set; }
    }
}
