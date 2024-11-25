using System;

namespace FritFest.API.Dtos
{
    public class TicketTypeDto
    {
        public Guid TicketTypeId { get; set; }
        public string Naam { get; set; }
        public double Prijs { get; set; }

    }
}
