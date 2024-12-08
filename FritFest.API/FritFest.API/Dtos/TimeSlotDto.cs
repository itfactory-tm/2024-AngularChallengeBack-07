using System;

namespace FritFest.API.Dtos
{
    public class TimeSlotDto
    {
        public Guid TimeSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } // The name of the artist (Artiest)

        public Guid StageId { get; set; }
        public string StageName { get; set; } // The name of the podium (Podium)
        
        public Guid DayId { get; set; }
        public string DayName { get; set; }
    }
}
