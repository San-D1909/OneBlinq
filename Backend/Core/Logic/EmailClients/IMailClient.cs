using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.Logic.EmailClients
{
    public interface IMailClient
    {
        Task SendEmailAsync(string from, string to, string subject, string body);
    }
}
