using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities
{
    public class FoodTruck
    {
        [Key]
        public Guid FoodTruckId { get; set; }
        public string Naam { get; set; }
        [ForeignKey(nameof(Locatie))]
        public Guid LocatieId { get; set; }
        public Locatie Locatie { get; set; }

        public ICollection<Editie> Edities { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
