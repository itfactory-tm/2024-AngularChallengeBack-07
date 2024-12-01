using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Ticket
    {
        [Key]
        public Guid TicketId { get; set; }

        public Edition Edition { get; set; }
        public TicketType TicketType { get; set; }

        [ForeignKey(nameof(Edition))]
        public Guid EditionId { get; set; }
        [ForeignKey(nameof(TicketType))]
        public Guid TicketTypeId { get; set; }

        public Day Day { get; set; }
        [ForeignKey(nameof(Day))]
        public Guid DayId { get; set; }
    }
}
