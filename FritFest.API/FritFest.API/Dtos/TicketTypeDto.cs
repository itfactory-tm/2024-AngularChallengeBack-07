using System;

namespace FritFest.API.Dtos
{
    public class TicketTypeDto
    {
        public Guid TicketTypeId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
