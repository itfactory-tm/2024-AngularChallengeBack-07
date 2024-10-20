using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public decimal Prijs { get; set; }
        [ForeignKey("TicketType")]
        public Guid TypeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String TelNr { get; set; }
        public String Email { get; set; }
        public TicketType Type { get; set; }
    }
}
