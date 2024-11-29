using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class TicketType
    {
        [Key]
        public Guid TicketTypeId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
