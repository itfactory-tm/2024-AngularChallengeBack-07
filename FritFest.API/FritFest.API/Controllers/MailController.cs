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

            string templatePath = "https://raw.githubusercontent.com/itfactory-tm/2024-AngularChallengeBack-07/refs/heads/main/FritFest.API/FritFest.API/MailTemplates/BoughtTicketsMail.html?token=GHSAT0AAAAAAC3BJ36QC5LKEBWT5HDX34KUZ2SAWUQ";

            var result = await _mailService.SendMailAsync(emailRequest.NameReceiver, emailRequest.EmailReceiver, emailRequest.Subject, templatePath);
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
