using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace FritFest.API.Services
{
    public class MailService
    {

        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> SendMailAsync(string toName, string toEmail, string subject, string templatePath, string ticketTemplateUrl)
        {
            string body = await PopulateTemplateAsync(templatePath);

            string ticketTemplate = await PopulateTemplateAsync(ticketTemplateUrl);
            MemoryStream pdfStream = ConvertHtmlToPdf(ticketTemplate, ticketTemplateUrl);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("FritFest Tickets", _configuration["SMTP:FromEmail"]));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body 
            };

            bodyBuilder.Attachments.Add("ticket.pdf", pdfStream, ContentType.Parse("application/pdf"));

            email.Body = bodyBuilder.ToMessageBody();

            //Body generated via HTML
            //email.Body = new TextPart("html") { Text = body };

            try
            {
                using (var smtpclient = new SmtpClient())
                {
                    await smtpclient.ConnectAsync(_configuration["SMTP:Host"], int.Parse(_configuration["SMTP:Port"]), true);
                    await smtpclient.AuthenticateAsync(_configuration["SMTP:Username"], _configuration["SMTP:Password"]);
                    await smtpclient.SendAsync(email);
                    await smtpclient.DisconnectAsync(true);
                }

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        public async Task<string> PopulateTemplateAsync(string templateUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string templateContent = await client.GetStringAsync(templateUrl);

                    // Optionally, you can replace placeholders if needed (uncomment and customize)
                    /*
                    foreach (var placeholder in placeholders)
                    {
                        templateContent = templateContent.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
                    }
                    */

                    return templateContent;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching template: {ex.Message}");
                return string.Empty;  
            }

        }

        //Generating Tickets
        public MemoryStream ConvertHtmlToPdf(string htmlContent, string outputFilePath)
        {
            // Generate PDF from HTML content
            PdfDocument pdf = PdfGenerator.GeneratePdf(htmlContent, PdfSharp.PageSize.A4);
            var memoryStream = new MemoryStream();
            pdf.Save(memoryStream, true); // Save the PDF to the memory stream

            // Reset the stream position to the beginning before returning
            memoryStream.Position = 0;

            return memoryStream;

        }

    }
}
