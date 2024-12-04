using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class DayList
    {
        [ForeignKey("TicketId")]
        public Guid TicketId { get; set; }  // Changed to Guid
        [ForeignKey(nameof(Day))]
        public Guid DayId { get; set; }  // Changed to Guid
        public Ticket Ticket { get; set; }
        public Day Day { get; set; }
    }
}
