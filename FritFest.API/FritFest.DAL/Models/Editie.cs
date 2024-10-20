using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class Editie
    {
        [Key]
        public Guid EditieId { get; set; }
        public string EditieNaam { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string TelNr { get; set; }
        public string Email { get; set; }
    }
}
