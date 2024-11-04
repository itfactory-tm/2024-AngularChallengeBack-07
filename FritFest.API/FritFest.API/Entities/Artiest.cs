using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Artiest
    {
        [Key]
        public Guid ArtistId { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Beschrijving { get; set; }
        public string SpotifyApi { get; set; }

        [ForeignKey(nameof(Genre))]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Editie> Edities { get; set; }
        public ICollection<TijdStip> TijdStippen { get; set; }
    }
}
