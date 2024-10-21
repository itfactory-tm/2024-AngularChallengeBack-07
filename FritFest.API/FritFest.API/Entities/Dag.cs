using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Dag
    {
        [Key]
        public Guid DagId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<DagList> DagList { get; set; }
    }
}
