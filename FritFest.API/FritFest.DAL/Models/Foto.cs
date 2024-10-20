using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class Foto
    {
        public Guid FotoId { get; set; }
        public string Bestand { get; set; }
        public string? Beschrijving { get; set; }
    }
}
