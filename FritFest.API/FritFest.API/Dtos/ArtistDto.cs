namespace FritFest.API.Dtos
{
    public class ArtistDto
    {
        public Guid ArtistId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Description { get; set; }
        public string SpotifyLink { get; set; }
        public string ApiCode { get; set; }
        public string? SpotifyPhoto { get; set; } // New - Spotify photo URL
        public string? Genre { get; set; } // Optional, if you want to include genre name
        public Guid EditionId { get; set; }
        public string EditionName { get; set; }
    }
}
