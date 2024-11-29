using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Artist
    {
        [Key]
        public Guid ArtistId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        public string Description { get; set; }
        public string SpotifyLink { get; set; }
        public string ApiCode { get; set; }
        public string SpotifyPhoto { get; set; }

        public string Genre { get; set; } // Optional, if you want to include genre name

        //[ForeignKey(nameof(Genre))]
        //public Guid GenreId { get; set; }
        //public Genre Genre { get; set; }

        public ICollection<Edition> Edition { get; set; }
       
    }
}
