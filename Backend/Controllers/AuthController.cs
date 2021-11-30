using Backend.Core.Logic;
using Backend.DTO;
using Backend.DTO.In;
using Backend.Infrastructure.Data;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly MailClient _mailClient;
        private PasswordEncrypter _encryptor;

        public AuthController(ApplicationDbContext context, IConfiguration config, MailClient mailClient)
        {
            _context = context;
            _config = config;
            _mailClient = mailClient;
            _encryptor = new PasswordEncrypter(config);
        }

        private async void SendTokenizedMail(string email, string subject, string body, string endpoint)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                       new Claim(ClaimTypes.Email, email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string fullBody = $"{body}: {_config["baseUrlFront"]}{endpoint}?email={email}&token={tokenHandler.WriteToken(token)}";
            if (endpoint == "verify")
			{
                fullBody = $"{body}: {_config["baseUrlFront"]}{endpoint}/{tokenHandler.WriteToken(token)}?email={email}";
            }

            MailMessage mail = new MailMessage
             (
                 "stuurmen@stuur.men",
                 email,
                 subject,
                 fullBody
             );

            await _mailClient.SendEmailAsync(mail);
        }


        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LoginInput credentials)
        {

            var user = await _context.User
                    .Where(u => u.Email == credentials.Email)
                    .FirstOrDefaultAsync();

            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            if (_encryptor.EncryptPassword(credentials.Password + user.Salt) == user.Password)
            {
                Claim[] claims = new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                      new Claim(ClaimTypes.Name, user.FullName),
                      new Claim(ClaimTypes.Role, ""+user.IsAdmin.ToString()+"")
                };

                var token = TokenHelper.CreateToken(claims, _config);

                return Ok(TokenHelper.WriteToken(token));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterInput credentials)
        {

            var findUser = await _context.User
                    .Where(u => u.Email == credentials.User.Email)
                    .FirstOrDefaultAsync();

            if (findUser != null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            if (credentials.User.Password == credentials.User.PasswordConfirmation)
            {   
                CompanyModel company = null;
                if (credentials.HasCompany)
                {
                    await _context.Company.AddAsync(new CompanyModel
                    {
                        CompanyName = credentials.Company.CompanyName,
                        ZipCode = credentials.Company.ZipCode,
                        Street = credentials.Company.Street,
                        HouseNumber = credentials.Company.HouseNumber,
                        Country = credentials.Company.Country,
                        BTWNumber = credentials.Company.BTWNumber,
                        KVKNumber = credentials.Company.KVKNumber,
                        PhoneNumber = credentials.Company.PhoneNumber
                    });

                    await _context.SaveChangesAsync();

                    company = await _context.Company
                    .Where(c => c.CompanyName == credentials.Company.CompanyName)
                    .FirstOrDefaultAsync();
                }

                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                UserModel user = new UserModel
                {
                    Email = credentials.User.Email,
                    Password = _encryptor.EncryptPassword(credentials.User.Password + salt),
                    FullName = credentials.User.FullName,
                    IsAdmin = false,
                    Salt = salt,
                    Company = company
                };
                var newUser = _context.User
                    .Add(user);

                SendTokenizedMail(credentials.User.Email, "Email verification", "Press this link to verify your email", "verify");

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(VerifyEmailDTO dto)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken validatedToken = TokenHelper.Verify(dto.Token, _config);

                string claim_email = TokenHelper.GetClaim(dto.Token, "email");
                if (claim_email == dto.Email)
                {
                    var user = await _context
                            .User
                            .FirstOrDefaultAsync(u => u.Email == dto.Email);

                    user.IsVerified = true;

                    await _context.SaveChangesAsync();
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginInput credentials)
        {

            //Check if user exists with given email
            var user = await _context.User
                    .Where(u => u.Email == credentials.Email)
                    .FirstOrDefaultAsync();

            if (user != null)
            {
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                       new Claim(ClaimTypes.Email, user.Email.ToString())
                    }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                SendTokenizedMail(user.Email, "Reset Password", "Press this link to reset your password", "resetpassword");
            }

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken validatedToken = TokenHelper.Verify(dto.Token, _config);

                string claim_email = TokenHelper.GetClaim(dto.Token, "email");

                if (claim_email == dto.Email && dto.Password == dto.PasswordConfirm)
                {
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                    //TODO: repo hier
                    var user = await _context
                        .User
                        .Where(u => u.Email == dto.Email)
                        .FirstOrDefaultAsync();

                    user.Password = _encryptor.EncryptPassword(dto.Password + salt);
                    user.Salt = salt; 

                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

            return Ok();
        }

    }
}
