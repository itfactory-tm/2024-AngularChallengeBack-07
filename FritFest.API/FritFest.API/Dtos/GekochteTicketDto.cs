namespace FritFest.API.Dtos
{
    public class GekochteTicketDto
    {
        public Guid GekochteTicketId { get; set; }

        public string NaamVanKoper { get; set; }
        public string EmailVanKoper { get; set; }
        public string NaamVanHouder { get; set; }
        public string EmailVanHouder { get; set; }


        public Guid TicketId { get; set; }

        public Guid TicketTypeId { get; set; }
        public string Naam {  get; set; }

        public bool Betaald { get; set; }
    }
}
