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
using Backend.DTO.In;

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
        public IActionResult Ping(GumroadResponseInput response)
        {
			var domain = HttpContext.Request.Host;
			if (domain.Host == "www.gumroad.com" && (_env.IsDevelopment() || _env.IsEnvironment("local")))
				return Ok(200);


			UserModel user = _context.User.Where(u => u.Email == response.Email).FirstOrDefault();
            if (user == null)
            {
                user = new UserModel()
                {
                    Email = response.Email,
                    FullName = response.Full_Name,
                    Password = "",
                };
                _context.User.Add(user);
                _context.SaveChanges();
            }

            LicenseTypeModel licenseType = new LicenseTypeModel
            {
                MaxAmount = 5,
                TypeName = "Test"
            };

            _context.LicenseType.Add(licenseType);

            LicenseModel license = new LicenseModel()
            {
                ExpirationTime = DateTime.Now.AddYears(1),
                LicenseType = licenseType,
                IsActive = true,
                TimesActivated = 0,
                LicenseKey = _generator.CreateLicenseKey(response.Email, "Forms", response.Variants),
                User = user
            };

            _context.License.Add(license);
            _context.SaveChanges();

            SendLicenseMail(user.Email, license.LicenseKey);

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
