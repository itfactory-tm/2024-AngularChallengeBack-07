using System;
using System.Collections.Generic;

namespace FritFest.API.Dtos
{
    public class EditionDto
    {
        public Guid EditionId { get; set; }
        public string EditionName { get; set; }
        public string Adres { get; set; }
        public string ZipCode { get; set; }
        public string Municipality { get; set; }
        public string PhoneNr { get; set; }
        public string Mail { get; set; }
        public int Year { get; set; }

        public int TicketCount { get; set; }
        public List<string> ArtistsNames { get; set; }
        public List<string> Photos { get; set; }
        public List<string> ArticleNames { get; set; }
        public List<string> SponsorNames { get; set; }
        public List<string> FoodtruckNames { get; set; }
        
    }
}
