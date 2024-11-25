using System;

namespace FritFest.API.Dtos
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }
        public decimal Prijs { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelNr { get; set; }
        public string Email { get; set; }

        public Guid EditieId { get; set; }
        public string EditieNaam { get; set; } // Editie.Name field to display the name in the DTO

        public Guid TicketTypeId { get; set; }
        public double TicketPrijs { get; set; }
        public Guid DagId { get; set; }
        public string DagNaam { get; set; } // Dag.Name field to display the name in the DTO
    }
}
