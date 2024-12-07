using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Edition
    {
        [Key]
        public Guid EditionId { get; set; }
        public string EditionName { get; set; }
        public string Adres { get; set; }
        public string ZipCode { get; set; }
        public string Municipality { get; set; }
        public string PhoneNr { get; set; }
        public string Mail { get; set; }
        public int Year { get; set; }

        public ICollection<BoughtTicket> Tickets { get; set; }
        public ICollection<Artist> Artists { get; set; }
       
        public ICollection<Article> Articles { get; set; }
        public ICollection<Sponsor> Sponsors { get; set; }
        public ICollection<FoodTruck> Foodtrucks { get; set; }

    }
}
