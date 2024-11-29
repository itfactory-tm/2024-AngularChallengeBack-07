using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Sponsor
    {
        [Key]
        public Guid SponsorId { get; set; }
        public string SponsorName { get; set; }
        public int Amount { get; set; }
        public string SponsoredItem { get; set; }

        public ICollection<Edition> Editions { get; set; }
    }
}
