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
		private readonly ApplicationDbContext _context;
        private readonly LicenseGenerator _generator;
        // TODO mail key

        public GumroadController(ApplicationDbContext context, LicenseGenerator generator)
        {
            _context = context;
            _generator = generator;
        }

        [HttpPost("Ping")]
        public IActionResult Ping(GumroadResponse response)
        {
            //TODO Check Domain

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

            LicenseModel license = new LicenseModel()
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

            return Ok(license);
        }
    }
}
