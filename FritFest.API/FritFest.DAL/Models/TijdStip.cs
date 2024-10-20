using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class TijdStip
    {
        public Guid TijdStipId { get; set; }
        public DateTime Tijd { get; set; }

        [ForeignKey("Artiest")]
        public Guid ArtiestId { get; set; }
        public Artiest Artiest { get; set; }

        [ForeignKey("Podium")]
        public Guid PodiumId { get; set; }
        public Podium Podium { get; set; }

    }
}
