using Backend.Core.Logic;
using Backend.DTO;
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

        private async void SendVerificationMail(string Email)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                       new Claim(ClaimTypes.Email, Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // TODO: change site url from localhost to config["SITE_URL"] || env SITE_URL
            MailMessage mail = new MailMessage
             (
                 "stuurmen@stuur.men",
                 "test@gmail.com",
                 "Reset Password",
                 $"Press this link to confirm your account: localhost:29616/verify/{tokenHandler.WriteToken(token)}?email={Email}"
             );

            await _mailClient.SendEmailAsync(mail);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel credentials)
        {
            var encryptedPassword = _encryptor.EncryptPassword(credentials.Password);

            var user = await _context.User
                    .Where(u => u.Email == credentials.Email && u.Password == encryptedPassword)
                    .FirstOrDefaultAsync();

            if (user != null)
            {
                Claim[] claims = new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
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
        public async Task<IActionResult> Register([FromBody] RegisterModel credentials)
        {
            var findUser = await _context.User
                    .Where(u => u.Email == credentials.User.Mail)
                    .FirstOrDefaultAsync();

            if (findUser != null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            if (credentials.User.Password == credentials.User.PasswordConfirmation)
            {
                if (credentials.Company.CompanyName != "")
                {
                    var newCompany = await _context.Company.AddAsync(new CompanyModel
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
                }

                var company = await _context.Company
                    .Where(c => c.CompanyName == credentials.Company.CompanyName)
                    .FirstOrDefaultAsync();

                int? id = 0;

                if (company == null || company.CompanyName == "")
                {
                    id = null;
                }
                else
                {
                    id = company.CompanyId;
                }

                var newUser = await _context.User
                    .AddAsync(new UserModel
                    {
                        Email = credentials.User.Mail,
                        Password = _encryptor.EncryptPassword(credentials.User.Password),
                        FullName = credentials.User.FullName,
                        IsAdmin = false,
                        Company = id
                    });

                SendVerificationMail(credentials.User.Mail);

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
                tokenHandler.ValidateToken(dto.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                string claim_email = TokenHelper.GetClaim(dto.Token, ClaimTypes.Email);
                if (claim_email != dto.Email)
                {
                    var user = await _context
                        .User
                        .Where(u => u.Email == dto.Email)
                        .FirstOrDefaultAsync();

                    user.IsVerified = true;

                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginModel credentials)
        {
            //FIXME email wordt niet verzonden wanneer request wordt gemaakt via frontend ??

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
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                // TODO: change site url from localhost to config["SITE_URL"] || env SITE_URL
                MailMessage mail = new MailMessage
                 (
                     "stuurmen@stuur.men",
                     "test@gmail.com",
                     "Reset Password",
                     $"Press this link to reset your password: localhost:29616/resetpassword?email={credentials.Email}&token={tokenHandler.WriteToken(token)}"
                 );

                await _mailClient.SendEmailAsync(mail);
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
                tokenHandler.ValidateToken(dto.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                string claim_email = TokenHelper.GetClaim(dto.Token, ClaimTypes.Email);
                if (claim_email != dto.Email && dto.Password == dto.PasswordConfirm)
                {
                    //TODO: repo hier
                    var user = await _context
                        .User
                        .Where(u => u.Email == dto.Email)
                        .FirstOrDefaultAsync();

                    user.Password = _encryptor.EncryptPassword(dto.Password);

                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

    }
}
