using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data;
using System.Text.Json;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Backend.Core.Logic;

namespace Backend.Controllers
{
	[ApiController]
	[ApiVersion("1")]
	[ApiVersion("2")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class TestController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public TestController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("Index")]
		public IActionResult Index()
		{
			var license = _context.License.FirstOrDefault();
			var user = _context.User.FirstOrDefault();

			Console.WriteLine(user.FullName);
			Console.WriteLine(license.LicenseId);

			return Ok();
		}

		[HttpGet("Auth")]
		[MapToApiVersion("1")]
		[Authorize(AuthenticationSchemes = "BasicAuthentication")]
		public IActionResult Auth()
        {
			return Ok("data");
        }

		[HttpGet("Auth")]
		[MapToApiVersion("2")]
		[ApiExplorerSettings(GroupName = "v2")]
		[Authorize(AuthenticationSchemes = "BasicAuthentication")]
		public IActionResult AuthV2()
		{
			return Ok("data V2");
		}

		[HttpPost("Ping")]
		public IActionResult Ping(GumroadResponse response)
		{
			LicenseModel license = new LicenseModel()
			{
				ExpirationDate = new DateTime(2021, 11, 05),
				LicenseType = "Test",
				IsActive = true,
				TimesActivated = 1,
				LicenseId = ""
			};

			User user = new User();
			var userFind = _context.User.Where(u => u.Email == response.Email).FirstOrDefault();

			if(userFind == null)
			{
				user.Email = response.Email;
				user.FullName = response.Full_Name;
				user.Password = "";
				user.UserName = "";;

				_context.User.Add(user);
				_context.SaveChanges();

			}
			else
			{
				user = userFind;
			}

			license.UserId  = _context.User.Where(u => u.Email == response.Email).FirstOrDefault().UserId;

			_context.License.Add(license);
			_context.SaveChanges();

			return Ok(license);
		}
	}
}
