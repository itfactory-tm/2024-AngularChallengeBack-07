public class ArtikelDto
{
    public Guid ArtikelId { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public DateTime Datum { get; set; }
    public Guid EditieId { get; set; }
    public string EditieNaam { get; set; } // Mapping Editie.EditieNaam to this field
}
