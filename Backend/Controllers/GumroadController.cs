using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System;
using Backend.Infrastructure.Data;
using System.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GumroadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GumroadController(ApplicationDbContext context)
        {
            _context = context;
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
                IsActive = true,
                TimesActivated = 1,
                LicenseId = response.License_Key
            };

            license.UserId = user.UserId;

            _context.License.Add(license);
            _context.SaveChanges();

            return Ok();
        }
    }
}
