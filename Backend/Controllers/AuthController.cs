using Backend.Core.Logic;
using Backend.Infrastructure.Data;
using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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


		[HttpPost("LogIn")]
		public async Task<IActionResult> LogIn([FromBody] Login credentials)
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
					  new Claim(ClaimTypes.Name, user.FullName)
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
				if(credentials.Company.CompanyName != "")
				{
					var newCompany = await _context.Company.AddAsync(new RegisterCompanyModel
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

				if(company == null || company.CompanyName == "") 
				{
					id = null;
				}
				else 
				{
					id = company.CompanyId;
				}

				var newUser = await _context.User
					.AddAsync(new User
					{
						Email = credentials.User.Mail,
						Password = _encryptor.EncryptPassword(credentials.User.Password),
						FullName = credentials.User.FullName,
						IsAdmin = false,
						Company = id
					});

				await _context.SaveChangesAsync();

				return Ok();
			}
			else
			{
				return StatusCode(StatusCodes.Status401Unauthorized);
			}

		}

		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword([FromBody] Login credentials)
		{

			//Check if user exists with given email
			var user = await _context.User
					.Where(u => u.Email == credentials.Email)
					.FirstOrDefaultAsync();

			if (user != null)
			{
				Claim[] claims = new Claim[]
				{
					 new Claim(ClaimTypes.Email, user.Email.ToString())
				};

				var token = TokenHelper.CreateToken(claims, _config);

				MailMessage mail = new MailMessage
				 (
					 "stuurmen@stuur.men",
					 "test@gmail.com",
					 "Reset Password",
					 $"Press this link to reset your password: " + "localhost:29616/resetpassword/" + TokenHelper.WriteToken(token)
				 );

				await _mailClient.SendEmailAsync(mail);

				//TODO add token to database
			}

			return Ok();
		}

		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPassword()
		{
			//TODO create model for resetting password
			//TODO verify token

			return Ok();
		}
	}
}
