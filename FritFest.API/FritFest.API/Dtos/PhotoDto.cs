using System;

namespace FritFest.API.Dtos
{
    public class PhotoDto
    {
        public Guid PhotoId { get; set; }
        public string File { get; set; }
        public string Description { get; set; }

        public Guid EditionId { get; set; }
        public string EditionName { get; set; }   // Mapped from Editie.EditieNaam

        public Guid ArticleId { get; set; }
        public string ArticleTitle { get; set; } // Mapped from Article.Titel

        public Guid StageId { get; set; }
        public string StageName { get; set; }   // Mapped from Podium.Naam
    }
}
