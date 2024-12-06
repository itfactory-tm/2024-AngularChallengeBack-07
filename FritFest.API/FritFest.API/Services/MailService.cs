using MimeKit;
using MailKit.Net.Smtp;
using QRCoder;
using System.Drawing;
using Microsoft.AspNetCore.Routing.Template;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


namespace FritFest.API.Services
{
    public class MailService
    {

        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> SendMailAsync(string toName, string toEmail, string subject, string templatePath)
        {
            var placeholders = new Dictionary<string, string>
            {
                { "name", toName },
            };


            string htmlBody = await PopulateTemplateAsync(templatePath, placeholders);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("FritFest Tickets", _configuration["SMTP:FromEmail"]));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = subject;

            byte[] qrCodeImage = GenerateQRCode(toEmail);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlBody; // HTML body content
            bodyBuilder.Attachments.Add("qr_code.png", qrCodeImage, ContentType.Parse("image/png")); // Add the QR code as attachment

            // 4. Set the email body (HTML)
            email.Body = bodyBuilder.ToMessageBody();

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

        public async Task<string> PopulateTemplateAsync(string templateUrl, Dictionary<string, string> placeholders)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string templateContent = await client.GetStringAsync(templateUrl);
                    var templateContent = File.ReadAllText("MailTemplates/BoughtTicketsMail.html");

                    // Optionally, you can replace placeholders if needed (uncomment and customize)

                    foreach (var placeholder in placeholders)
                    {
                        templateContent = templateContent.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
                    }

                    return templateContent;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching template: {ex.Message}");
                return string.Empty;
            }

        }

        //QRCodeGeneration
        public byte[] GenerateQRCode(string text, string format = "png")
        {
            // Create a QR code generator object
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.L); // Lower error correction level for smaller QR code

            var qrCode = new QRCode(qrCodeData);
            using (var ms = new MemoryStream())
            {
                System.Drawing.Color black = System.Drawing.Color.Black;
                System.Drawing.Color white = System.Drawing.Color.White;

                // Generate the QR code with a smaller pixel size (2 or 3)
                var qrBitmap = qrCode.GetGraphic(20, black, white, true); // Smaller pixel size, 2 or 3

                // Save the QR code image in the specified format (PNG/JPEG)
                if (format.ToLower() == "jpeg")
                {
                    // Save as JPEG
                    qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    // Default to PNG
                    qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }

                return ms.ToArray(); // Return the byte array of the image (PNG/JPEG)
            }
        }

        public byte[] OptimizeQRCode(byte[] qrCodeBytes)
        {

            using (var ms = new MemoryStream(qrCodeBytes))
            {
                using (var image = SixLabors.ImageSharp.Image.Load(ms))
                {
                    using (var optimizedMs = new MemoryStream())
                    {
                        // Apply the PngEncoder with best compression
                        var encoder = new PngEncoder
                        {
                            CompressionLevel = PngCompressionLevel.BestCompression
                        };
                        image.Save(optimizedMs, encoder);

                        // Further optimize using the ImageOptimizer
                        optimizedMs.Position = 0; // Reset stream position before optimizing.
                        var optimizedBytes = optimizedMs.ToArray();
                        return optimizedBytes;
                    }
                }
            }

        }

    }
}