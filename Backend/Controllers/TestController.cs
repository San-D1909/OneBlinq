using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data;
using System.Text.Json;
using Backend.Models;

namespace Backend.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]

	public class TestController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public TestController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var license = _context.License.FirstOrDefault();
			var user = _context.User.FirstOrDefault();

			Console.WriteLine(user.FullName);
			Console.WriteLine(license.LicenseId);

			return Ok();
		}

		[HttpPost]
		public IActionResult Ping(GumroadResponse response)
		{
			License license = new License()
			{
				ExpirationDate = new DateTime(2021, 11, 05),
				LicenseType = "Test",
				IfActive = true,
				TimesActivated = 1,
				LicenseId = response.License_Key
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

			return Ok();
		}
	}
}
