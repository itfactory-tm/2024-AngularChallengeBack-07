using System;

namespace FritFest.API.Dtos
{
    public class DayDto
    {
        public Guid DayId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TicketCount { get; set; } // Optional: Number of tickets related to the day
    }
}
