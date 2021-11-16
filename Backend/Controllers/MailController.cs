using Backend.Core.Logic;
using Backend.Models.MailModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class MailController : Controller
    {

        private readonly MailClient _mailClient;

        public MailController(MailClient mailClient)
        {
            _mailClient = mailClient;
        }

        [HttpPost("sendMail")]
        public async Task<IActionResult> SendMail(MailModel mailModel)
        {
            await _mailClient.SendEmailAsync(mailModel.From, mailModel.To, mailModel.Subject, mailModel.Body);
            return Ok(mailModel);
        }
    }
}
