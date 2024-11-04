namespace FritFest.API.Dtos
{
    public class EditieDto
    {
        public Guid EditieId { get; set; }
        public string EditieNaam { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string TelNr { get; set; }
        public string Email { get; set; }
        public int Jaar { get; set; }

    }
}
