namespace FritFest.API.Entities
{
    public class DayList
    {
        public Guid TicketId { get; set; }  // Changed to Guid
        public Guid DayId { get; set; }  // Changed to Guid
        public Ticket Ticket { get; set; }
        public Day Day { get; set; }
    }
}
