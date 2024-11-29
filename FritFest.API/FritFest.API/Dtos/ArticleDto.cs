public class ArticleDto
{
    public Guid ArticleId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public Guid EditionId { get; set; }
    public string EditionName { get; set; } // Mapping Editie.EditieNaam to this field
}
