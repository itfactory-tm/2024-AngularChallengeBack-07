using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities
{
    public class Locatie
    {
        [Key]
        public Guid LocatieId { get; set; }
        public string Naam { get; set; }
        public string Coordinaten { get; set; }

        public ICollection<FoodTruck> FoodTrucks { get; set; }
        public ICollection<Podium> Podia { get; set; }
    }
}
