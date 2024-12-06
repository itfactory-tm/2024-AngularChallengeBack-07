using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using DinkToPdf;

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

            var pdfStream = ConvertHtmlToPdf(ticketTemplate);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("FritFest Tickets", _configuration["SMTP:FromEmail"]));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = subject;

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(pdfStream),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "ticket.pdf"
            };

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
        public MemoryStream ConvertHtmlToPdf(string htmlContent)
        {
            var converter = new BasicConverter(new PdfTools());

            // Set the PDF generation options
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,            // Set color mode (color or grayscale)
                    Orientation = Orientation.Portrait,     // Set page orientation
                    PaperSize = PaperKind.A4,               // Set paper size (e.g., A4)
                },
                        Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,           // HTML content to be converted
                        WebSettings =
                        {
                            DefaultEncoding = "utf-8",      // Set encoding to UTF-8
                        }
                    }
                }
            };

            // Convert the HTML content to PDF and store it in a memory stream
            byte[] pdfBytes = converter.Convert(doc);
            var memoryStream = new MemoryStream(pdfBytes);

            // Return the generated PDF as a MemoryStream
            return memoryStream;

        }
    }
}
