﻿using Backend.Core.Logic;
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

		public AuthController(ApplicationDbContext context, IConfiguration config, MailClient mailClient)
		{
			_context = context;
			_config = config;
			_mailClient = mailClient;
		}


		[HttpPost("LogIn")]
		public async Task<IActionResult> LogIn([FromBody] LoginModel credentials)
		{
			var user = await _context.User
					.Where(u => u.Email == credentials.Email && u.Password == credentials.Password)
					.FirstOrDefaultAsync();

			if (user != null)
			{
				var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));

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
		public async Task<IActionResult> Register([FromBody] RegisterModel credentials)
		{
			var findUser = await _context.User
					.Where(u => u.Email == credentials.user.Mail && u.Password == credentials.user.Password)
					.FirstOrDefaultAsync();

			if (findUser != null)
			{
				return StatusCode(StatusCodes.Status401Unauthorized);
			}

			if (credentials.user.Password == credentials.user.PasswordConfirmation)
			{
				var newCompany = await _context.Company.AddAsync(new RegisterCompanyModel
				{
					CompanyName = credentials.company.CompanyName,
					ZipCode = credentials.company.ZipCode,
					Street = credentials.company.Street,
					HouseNumber = credentials.company.HouseNumber,
					Country = credentials.company.Country,
					BTWNumber = credentials.company.BTWNumber,
					KVKNumber = credentials.company.KVKNumber,
					PhoneNumber = credentials.company.PhoneNumber
				});
				var newUser = await _context.User
					.AddAsync(new UserModel
					{
						Email = credentials.user.Mail,
						Password = credentials.user.Password,
						FullName = credentials.user.FullName,
						IsAdmin = false,
						Company = credentials.company.CompanyId	
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
		public async Task<IActionResult> ForgotPassword([FromBody] LoginModel credentials)
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
					Expires = DateTime.UtcNow.AddDays(7),
					SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
				};

				var token = tokenHandler.CreateToken(tokenDescriptor);

				MailMessage mail = new MailMessage
				 (
					 "stuurmen@stuur.men",
					 "test@gmail.com",
					 "Reset Password",
					 $"Press this link to reset your password: " + "localhost:29616/resetpassword/" + tokenHandler.WriteToken(token)
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
