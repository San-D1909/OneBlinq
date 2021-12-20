using Backend.Core.Logic;
using Backend.DTO.In;
using Backend.DTO.Out;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Backend.Controllers.UserDashboard
{
	[Route("api/v{version:apiVersion}/user")]
	[ApiVersion("1")]
	[ApiController]
	public class DeviceController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _config;

		public DeviceController(ApplicationDbContext context, IConfiguration config, IUserRepository userRepository)
		{
			_context = context;
			_config = config;
			_userRepository = userRepository;
		}

		[HttpGet("{jtoken}/[controller]/")]
		public async Task<ActionResult<IEnumerable<PluginModel>>> GetDevice(string jtoken)
		{
			if (jtoken is null)
			{
				return NotFound();
			}
			var tokenuser = TokenHelper.Verify(jtoken, _config);

			if (tokenuser is null)
			{
				return NotFound();
			}
			int id = Convert.ToInt32(tokenuser.Claims.First().Value);
			UserModel user = await _userRepository.GetUserById(id);
			//var data = await _context.Device.Join(_context.PluginLicense, plugin => plugin.LicenseId, device => device.LicenseId, (plugin, device) => new { License = plugin.License }).Where(u => u.License.User.Id == user.Id).ToListAsync();
			//IEnumerable<DeviceModel> devices = await _context.PluginLicense.Include(l => l.License).ThenInclude(u => u.User).Include(p => p.Plugin).Where(p => p.License.User.Id == user.Id).Select(p => p.Plugin).ToListAsync();

			Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
			Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
			Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

			throw new NotImplementedException();
			//return Ok(devices);
		}

		[HttpPost("[controller]/validate/{id}")]
		public async Task<IActionResult> VerifyDevice([FromBody] DeviceRegisterInput data, int id)
		{
			var jtoken = data.Jtoken;
			var deviceInfo = data.DeviceInfo;

			if (jtoken is null || deviceInfo is null)
			{
				return NotFound();
			}
			var tokenuser = TokenHelper.Verify(jtoken, _config);

			if (tokenuser is null)
			{
				return NotFound();
			}

			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(deviceInfo + "_" + _config["Secret"]);
			var dtoken = Convert.ToBase64String(plainTextBytes);

			var databaseDevice = _context.Device.Where(d => d.DeviceToken == dtoken).FirstOrDefault();

			if(databaseDevice == null || data.LocalToken != databaseDevice.DeviceToken ) { return Unauthorized("New device recognized!"); }

			var license = await _context.License.FindAsync(databaseDevice.LicenseId);
			if(DateTime.Now > license.ExpirationTime) { return Unauthorized("License is not active!"); }

			return Ok();
		}

		[HttpPost("[controller]")]
		public async Task<IActionResult> RegisterNewDevice([FromBody] DeviceRegisterInput data)
		{
			var jtoken = data.Jtoken;
			var deviceInfo = data.DeviceInfo;

			if (jtoken is null || deviceInfo is null)
			{
				return NotFound();
			}
			var tokenuser = TokenHelper.Verify(jtoken, _config);

			if(tokenuser is null)
			{
				return NotFound();
			}

			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(deviceInfo + "_" + _config["Secret"]);
			var dtoken = Convert.ToBase64String(plainTextBytes);

			var databaseDevice = _context.Device.Where(d => d.DeviceToken == dtoken).FirstOrDefault();

			if(databaseDevice != null) { return Unauthorized("Device already registered!"); }

			LicenseModel license = await _context.License.Where(l => l.LicenseKey == data.LicenseKey).FirstOrDefaultAsync();
			if(!license.IsActive) { return NotFound(); }

			LicenseTypeModel licenseType = await _context.LicenseType.FindAsync(license.LicenseTypeId);

			if(license.TimesActivated >= licenseType.MaxAmount) { return NotFound(); }

			DeviceModel device = new DeviceModel
			{
				DeviceToken = dtoken,
				LicenseId = license.Id,
			};
			try
			{
				await _context.Device.AddAsync(device);
			}
			catch (Exception)
			{
				throw;
			}
			
			license.TimesActivated++;

			_context.Update(license);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}

			return Ok(dtoken);
		}

		[HttpDelete("{jtoken}/[controller]/{id}")]
		public async Task<IActionResult> DeleteDevice(int id, string jtoken)
		{
			if (jtoken is null)
			{
				return NotFound();
			}
			var tokenuser = TokenHelper.Verify(jtoken, _config);

			if (tokenuser is null)
			{
				return NotFound();
			}

			var device = await _context.Device.FindAsync(id);
			_context.Remove(device);
			await _context.SaveChangesAsync();

			var license = await _context.License.FindAsync(device.LicenseId);
			license.TimesActivated--;
			_context.License.Update(license);
			await _context.SaveChangesAsync();

			return Ok();
		}

	}
}
