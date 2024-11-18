using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Genre
    {
        [Key]
        public Guid GenreId { get; set; }
        public string Naam { get; set; }

        public ICollection<Artiest> Artiesten { get; set; }
    }
}
