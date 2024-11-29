using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Photo
    {
        [Key]
        public Guid PhotoId { get; set; }
        public string File { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Edition))]
        public Guid EditionId { get; set; }
        public Edition Edition { get; set; }

        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }

        [ForeignKey(nameof(Stage))]
        public Guid StageId { get; set; }
        public Stage Stage { get; set; }
    }
}
