using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Genre
    {
        [Key]
        public Guid GenreId { get; set; }
        public string Name { get; set; }

        //public ICollection<Artist> Artists { get; set; }
    }
}
