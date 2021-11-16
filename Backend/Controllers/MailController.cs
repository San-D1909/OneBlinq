using Backend.Core.Logic;
using Backend.Models.MailModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    public class MailController : Controller
    {

        private readonly MailClient _mailClient;

        public MailController(MailClient mailClient)
        {
            _mailClient = mailClient;
        }


        //public async Task<IActionResult> SendMail(string from, string to, string subject, string body)
        //{
        //    var message = new MailModel();
        //    message.From = from;
        //    message.To = to;
        //    message.Subject = subject;
        //    message.Body = body;

        //    await MailClient.SendEmailAsync(message);
        //}
    }
}
