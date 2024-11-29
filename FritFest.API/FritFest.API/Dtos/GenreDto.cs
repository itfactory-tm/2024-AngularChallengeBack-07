using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class GenreDto
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; }
        public List<string> ArtistsNames { get; set; } // List of artist names in this genre
    }
}
