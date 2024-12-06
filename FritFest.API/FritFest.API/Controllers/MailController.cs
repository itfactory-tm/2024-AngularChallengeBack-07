using FritFest.API.Entities.MailEntities;
using FritFest.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {

        private readonly MailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] MailData emailRequest)
        {

            string templatePath = "https://raw.githubusercontent.com/itfactory-tm/2024-AngularChallengeBack-07/refs/heads/main/FritFest.API/FritFest.API/MailTemplates/BoughtTicketsMail.html?token=GHSAT0AAAAAACX4V2HBX6N6ACDJK6A47PV2Z2S4PIA";

            string TicketUrl = "https://raw.githubusercontent.com/itfactory-tm/2024-AngularChallengeBack-07/refs/heads/main/FritFest.API/FritFest.API/MailTemplates/TicketTemplates/Ticket.html?token=GHSAT0AAAAAACX4V2HA2EHPT7PBBH2CLJP4Z2S4DVA";

            var result = await _mailService.SendMailAsync(emailRequest.NameReceiver, emailRequest.EmailReceiver, emailRequest.Subject, templatePath, TicketUrl);
            if (result)
            {
                return Ok(new { message = "Email sent successfully!" });
            }
            else
            {
                return StatusCode(500, new { error = "Failed to send email." });
            }

        }
    }
}
