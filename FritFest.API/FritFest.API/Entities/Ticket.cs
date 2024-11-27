using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Ticket
    {
        [Key]
        public Guid TicketId { get; set; }

        public Editie Editie { get; set; }
        public TicketType TicketType { get; set; }

        [ForeignKey(nameof(Editie))]
        public Guid EditieId { get; set; }
        [ForeignKey(nameof(TicketType))]
        public Guid TicketTypeId { get; set; }

        public Dag Dag { get; set; }
        [ForeignKey(nameof(Dag))]
        public Guid DagId { get; set; }
    }
}
