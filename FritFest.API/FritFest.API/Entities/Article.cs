using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Article
    {
        [Key]
        public Guid ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Edition))]
        public Guid EditionId { get; set; }
        public Edition Edition { get; set; }
    }
}
