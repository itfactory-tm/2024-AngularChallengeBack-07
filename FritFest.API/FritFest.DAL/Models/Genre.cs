using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FritFest.DAL.Models
{
    public class Genre
    {
        [Key]
        public Guid GenreId { get; set; }
        public string Naam { get; set; }
    }
}
