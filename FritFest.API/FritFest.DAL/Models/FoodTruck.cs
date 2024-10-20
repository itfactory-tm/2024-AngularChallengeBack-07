using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class FoodTruck
    {
        public Guid FoodTruckId { get; set; }
        public string Naam { get; set; }
        public Guid LocatieId { get; set; }
    }
}
