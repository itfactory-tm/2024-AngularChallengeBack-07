using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Day
    {
        [Key]
        public Guid DayId { get; set; }
        public string Name { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
