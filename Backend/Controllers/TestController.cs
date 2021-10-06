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

			return Ok(license);
		}

		[HttpPost]
		public IActionResult Ping(GumroadResponse response)
		{
			Console.WriteLine(response.License_Key);
			Console.WriteLine(response.Email);
			Console.WriteLine(response.Full_Name);
			Console.WriteLine(response.Variants);

			return Ok();
		}
	}
}
