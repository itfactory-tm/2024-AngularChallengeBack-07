using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class Sponsor
    {
        public Guid SponsorId { get; set; }
        public string Naam { get; set; }
        public string Logo { get; set; }
    }
}
