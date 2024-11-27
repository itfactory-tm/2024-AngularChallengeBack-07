using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class GekochteTicket
    {
        public Guid GekochteTicketId { get; set; }
        public string NaamVanKoper { get; set; }
        public string EmailVanKoper { get; set; }
        public string NaamVanHouder { get; set; }
        public string EmailVanHouder { get; set; }

        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public bool Betaald { get; set; }
    }
}
