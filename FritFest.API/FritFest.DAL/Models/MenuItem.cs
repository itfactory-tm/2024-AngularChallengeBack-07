using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class MenuItem
    {
        public Guid menuItemId { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public Guid FoodTruckId { get; set; }
    }
}
