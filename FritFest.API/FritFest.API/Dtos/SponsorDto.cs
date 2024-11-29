using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class SponsorDto
    {
        public Guid SponsorId { get; set; }
        public string SponsorName { get; set; }
        public int Amount { get; set; }
        public string SponsoredItem { get; set; }
        public List<string> Editions { get; set; } // Assuming EditieDto exists
    }
}
