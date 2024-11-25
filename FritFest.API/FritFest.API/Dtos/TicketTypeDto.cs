using System;

namespace FritFest.API.Dtos
{
    public class TicketTypeDto
    {
        public Guid TicketTypeId { get; set; }
        public string Naam { get; set; }
        public string FirstName { get; set; } // Mapping for Ticket's FirstName
        public string LastName { get; set; }  // Mapping for Ticket's LastName
        public string TelNr { get; set; }     // Mapping for Ticket's TelNr
        public string Email { get; set; }     // Mapping for Ticket's Email
    }
}
