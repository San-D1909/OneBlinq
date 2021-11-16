using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System;
using Backend.Infrastructure.Data;
using System.Linq;
using Backend.Core.Logic;
using Microsoft.Extensions.Configuration;

namespace Backend.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GumroadController : ControllerBase
    {
		private readonly IConfiguration _config;
		private readonly ApplicationDbContext _context;
        private readonly LicenseGeneration _generator;

        public GumroadController(ApplicationDbContext context, LicenseGeneration generator)
        {
            _context = context;
            _generator = generator;
        }

        [HttpPost("Ping")]
        public IActionResult Ping(GumroadResponse response)
        {
            User user = _context.User.Where(u => u.Email == response.Email).FirstOrDefault();
            if (user == null)
            {
                user = new User()
                {
                    Email = response.Email,
                    FullName = response.Full_Name,
                    Password = "",
                    UserName = ""
                };
                _context.User.Add(user);
                _context.SaveChanges();
            }

            License license = new License()
            {
                ExpirationDate = new DateTime(2021, 11, 05),
                LicenseType = "Test",
                IfActive = true,
                TimesActivated = 1,
                LicenseId = _generator.CreateLicenseKey(response.Email, "Forms", response.Variants),
                UserId = user.UserId
            };

            _context.License.Add(license);
            _context.SaveChanges();

            return Ok(license);
        }
    }
}
