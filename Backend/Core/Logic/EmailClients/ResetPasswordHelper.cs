using Backend.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Backend.Core.Logic.EmailClients
{
    public class ResetPasswordHelper
    {
		
		private readonly IConfiguration _config;
		private readonly MailClient _mailClient;

		public ResetPasswordHelper(IConfiguration config, MailClient mailClient)
		{
			_config = config;
			_mailClient = mailClient;
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

		public void SendResetLink(string email) 
        {
			var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					   new Claim(ClaimTypes.Email, email)
				}),
				Expires = DateTime.Now.AddHours(1),
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			SendTokenizedMail(email, "Reset Password", "Press this link to reset your password", "resetpassword");
		}

    }
}
