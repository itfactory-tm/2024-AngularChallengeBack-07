using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class Podium
    {
        public Guid PodiumId { get; set; }
        public string Naam { get; set; }
        [ForeignKey("Locatie")]
        public Guid LocatieId { get; set; }
        public Locatie Locatie { get; set; }
    }
}
