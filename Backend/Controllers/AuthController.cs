using Backend.Infrastructure.Data;
using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
		private readonly ApplicationDbContext _context;

		public AuthController(ApplicationDbContext context)
		{
            _context = context;
		}


		[HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel credentials)
        {
            var user =  await _context.User
                    .Where(u => u.Email == credentials.Mail && u.Password == credentials.Password)
                    .FirstOrDefaultAsync();

            if (user != null)
            {
                var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                       new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                       new Claim(ClaimTypes.Name, user.FullName),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(tokenHandler.WriteToken(token));
            }
            else
			{
                return StatusCode(StatusCodes.Status401Unauthorized);
			}
        }

        [HttpPost("Register")]
        public IActionResult Register()
        {
           return Ok();
        }
    }
}
