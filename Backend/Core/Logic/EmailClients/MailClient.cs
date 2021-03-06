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

        public async Task PurchaseConfirmationMail(string from, string to, string subject, string licensekey)
        {
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException(nameof(from));
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException(nameof(subject));
            if (string.IsNullOrEmpty(licensekey))
                throw new ArgumentNullException(nameof(licensekey));

            var message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = "The purchase has been confirmed!. The license key is:" + licensekey;

            await SendEmailAsync(message);
        }

        public async Task AccountCreationConfirmationMail(string from, string to, string subject, string body)
        {
            // TODO Naam en email van de klant uit de database halen voor de confirmation mail
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
            message.Subject = "Account creation on OneBlinq";
            message.Body = "Thanks you for signing up " + to + ". From now on you'll get regular updates on our plugins and sales. " +
                "\nIn the meantime you can check out our catalog on https://www.figma.com/@OneBlinq";

            await SendEmailAsync(message);
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
