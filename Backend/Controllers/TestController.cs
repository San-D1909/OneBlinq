using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data;

namespace Backend.Controllers
{
	[ApiController]
	[Route("[controller]")]

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
	}
}
