using Microsoft.Extensions.Configuration;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;

namespace API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendPaymentConfirmationEmail(string toEmail, string subject, string message)
        {
            var apiKey = _config["Mailjet:ApiKey"];
            var apiSecret = _config["Mailjet:ApiSecret"];
            var fromEmail = _config["Mailjet:FromEmail"];
            var fromName = _config["Mailjet:FromName"];

            var client = new MailjetClient(apiKey, apiSecret);

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(fromEmail, fromName))
                .WithSubject(subject)
                .WithHtmlPart(message)
                .WithTo(new SendContact(toEmail))
                .Build();

            var response = await client.SendTransactionalEmailAsync(email);

            if (!response.Messages[0].Status.Equals("success"))
            {
                Console.WriteLine($"❌ Error al enviar el correo: {response.Messages[0].Errors[0].ErrorMessage}");
            }
        }

        public async Task SendVerificationEmail(string toEmail, string subject, string message)
        {
            var apiKey = _config["Mailjet:ApiKey"];
            var apiSecret = _config["Mailjet:ApiSecret"];
            var fromEmail = _config["Mailjet:FromEmail"];
            var fromName = _config["Mailjet:FromName"];

            var client = new MailjetClient(apiKey, apiSecret);

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(fromEmail, fromName))
                .WithSubject(subject)
                .WithHtmlPart(message)
                .WithTo(new SendContact(toEmail))
                .Build();

            var response = await client.SendTransactionalEmailAsync(email);

            if (!response.Messages[0].Status.Equals("success"))
            {
                Console.WriteLine($"❌ Error al enviar el correo: {response.Messages[0].Errors[0].ErrorMessage}");
            }
        }
    }
}
