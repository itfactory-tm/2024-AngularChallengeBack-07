using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class DagList
    {
        [Key]
        public Guid TicketListId { get; set; }
        [ForeignKey(nameof(Dag))]
        public Guid DagId { get; set; }
        [ForeignKey(nameof(Ticket))]
        public Guid TicketId { get; set; }
    }
}
