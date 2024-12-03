using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class Sponsor
    {
        [Key]
        public Guid SponsorId { get; set; }
        public string SponsorName { get; set; }
        public int Amount { get; set; }
        public string SponsoredItem { get; set; }

        public string SponsorMail { get; set; }
        public string SponsorLogo { get; set; }

        public Edition Edition { get; set; }
        [ForeignKey(nameof(Edition))]
        public Guid EditionId { get; set; }

    }
}
