using System;

namespace FritFest.API.Dtos
{
    public class DagDto
    {
        public Guid DagId { get; set; }
        public string Naam { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public int TicketCount { get; set; } // Optional: Number of tickets related to the day
    }
}
