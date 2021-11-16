using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.Logic.EmailClients
{
    public interface IMailClient
    {
        Task SendEmailAsync(string from, string to, string subject, string body);

        Task PurchaseConfirmationMail(string from, string to, string subject, string licensekey);

        Task AccountCreationConfirmationMail(string from, string to, string subject, string body);
    }
}
