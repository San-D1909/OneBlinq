using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System;
using Backend.Infrastructure.Data;
using System.Linq;
using Backend.Core.Logic;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Backend.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GumroadController : ControllerBase
    {
		private readonly ApplicationDbContext _context;
        private readonly LicenseGenerator _generator;
        private readonly MailClient _mailClient;
        private readonly IWebHostEnvironment _env;

        public GumroadController(ApplicationDbContext context, LicenseGenerator generator, MailClient mailClient, IWebHostEnvironment env)
        {
            _context = context;
            _generator = generator;
            _mailClient = mailClient;
            _env = env;
        }

        [HttpPost("Ping")]
        public IActionResult Ping(GumroadResponse response)
        {
			var domain = HttpContext.Request.Host;
			if (domain.Host == "www.gumroad.com" && (_env.IsDevelopment() || _env.IsEnvironment("local")))
				return Ok(200);


			User user = _context.User.Where(u => u.Email == response.Email).FirstOrDefault();
            if (user == null)
            {
                user = new User()
                {
                    Email = response.Email,
                    FullName = response.Full_Name,
                    Password = "",
                };
                _context.User.Add(user);
                _context.SaveChanges();
            }

            License license = new License()
            {
                ExpirationDate = DateTime.Now.AddYears(1),
                LicenseType = "Test",
                IsActive = true,
                TimesActivated = 0,
                LicenseId = _generator.CreateLicenseKey(response.Email, "Forms", response.Variants),
                UserId = user.UserId
            };

            _context.License.Add(license);
            _context.SaveChanges();

            SendLicenseMail(user.Email, license.LicenseId);

            return Ok();
        }

        private async void SendLicenseMail(string receiver, string key)
		{
            MailMessage mail = new MailMessage
            (
                "stuurmen@stuur.men",
                receiver,
                "Thank you for your purchase!",
                $"Here is your key to activate the plugin: {key}" 
            );

            await _mailClient.SendEmailAsync(mail);
		}
    }
}
