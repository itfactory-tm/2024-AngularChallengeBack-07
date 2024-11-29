using System;

namespace FritFest.API.Dtos
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }

        public Guid EditionId { get; set; }
        public string EditionName { get; set; } // Editie.Name field to display the name in the DTO

        public Guid TicketTypeId { get; set; }
        public double TicketPrice { get; set; }
        public Guid DayId { get; set; }
        public string DayName { get; set; } // Dag.Name field to display the name in the DTO
    }
}
