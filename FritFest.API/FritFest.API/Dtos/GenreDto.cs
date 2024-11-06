using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class GenreDto
    {
        public Guid GenreId { get; set; }
        public string Naam { get; set; }
        public List<string> ArtiestNamen { get; set; } // List of artist names in this genre
    }
}
