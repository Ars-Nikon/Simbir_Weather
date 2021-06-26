using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Services
{
    public class MailNotificationSender : INotificationSender
    {
        private readonly SmtpClientConfiguration _configuration;

        public MailNotificationSender(IOptions<SmtpClientConfiguration> options)
        {
            _configuration = options.Value;
        }

        public async Task SendNotificationAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_configuration.Name, _configuration.MailboxAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration.Host, _configuration.Port, _configuration.UseSsl);
                await client.AuthenticateAsync(_configuration.MailboxAddress, _configuration.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
