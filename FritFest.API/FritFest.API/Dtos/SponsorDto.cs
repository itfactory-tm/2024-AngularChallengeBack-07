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
        public string SponsorMail {  get; set; }
        public string? SponsorLogoBase64 { get; set; }
        
        // public string SponsorLogo { get; set; }
        public Guid EditionId { get; set; }
        public string EditionName { get; set; }
    }
}
