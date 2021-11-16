using System;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Backend.Core.Logic.EmailClients;

namespace Backend.Core.Logic
{
    public class MailClient : IMailClient
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public MailClient(SmtpConfiguration smtpConfiguration)
        {
            if (smtpConfiguration == null)
                throw new ArgumentNullException(nameof(smtpConfiguration));

            _smtpConfiguration = smtpConfiguration;
        }


        public async Task SendEmailAsync(string from, string to, string subject, string body)
        {
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException(nameof(from));
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException(nameof(subject));
            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException(nameof(body));

            var message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            await SendEmailAsync(message);
        }

        public async Task SendEmailAsync(MailMessage mailMessage)
        {
            if (mailMessage == null)
                throw new ArgumentNullException(nameof(mailMessage));

            try
            {
                using (var smtpClient = new SmtpClient(_smtpConfiguration.Host, _smtpConfiguration.Port))
                {
                    if (!string.IsNullOrEmpty(_smtpConfiguration.Username) && !string.IsNullOrEmpty(_smtpConfiguration.Password))
                        smtpClient.Credentials = new NetworkCredential(_smtpConfiguration.Username, _smtpConfiguration.Password);

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            finally
            {
                mailMessage.Dispose();
            }
        }
    }
}
