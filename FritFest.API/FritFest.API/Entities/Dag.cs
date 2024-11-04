using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Dag
    {
        [Key]
        public Guid DagId { get; set; }
        public string Naam { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
