namespace FritFest.API.Dtos
{
    public class BoughtTicketDto
    {
        public Guid BoughtTicketId { get; set; }

        public string BuyerName { get; set; }
        public string BuyerMail { get; set; }
        public string HolderName { get; set; }
        public string HolderMail { get; set; }


        public Guid TicketId { get; set; }

        public string? EditionName { get; set; }
        public string? TicketTypeName { get; set; }

        public bool Payed { get; set; }
    }
}
