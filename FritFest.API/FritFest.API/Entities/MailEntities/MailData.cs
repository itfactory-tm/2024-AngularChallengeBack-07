namespace FritFest.API.Entities.MailEntities
{
    public class MailData
    {
        public string NameReceiver { get; set; }
        public string EmailReceiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BoughtTicketId { get; set; }
    }
}
