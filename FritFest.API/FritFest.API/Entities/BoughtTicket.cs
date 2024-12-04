using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class BoughtTicket
    {
        public Guid BoughtTicketId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerMail { get; set; }
        public string HolderName { get; set; }
        public string HolderMail { get; set; }

        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public bool Payed { get; set; }
    }
}
