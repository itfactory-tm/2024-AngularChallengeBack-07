using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class NieuwsArtikel
    {
        public Guid ArtikelId { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public DateTime Datum { get; set; }
    }
}
