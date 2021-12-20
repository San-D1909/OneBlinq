using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;
using Backend.Core.Logic;
using Microsoft.Extensions.Configuration;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.DTO.Out;

namespace Backend.Controllers.UserDashboard
{
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public LicenseController(ApplicationDbContext context, IConfiguration config, IUserRepository userRepository)
        {
            _context = context;
            _config = config;
            _userRepository = userRepository;
        }

        // GET: api/LicenseModels
        [HttpGet("{jtoken}/[controller]/")]
        public async Task<ActionResult<IEnumerable<LicenseModel>>> GetLicense(string jtoken)
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
            int tokenid = Convert.ToInt32(tokenuser.Claims.First().Value);
            UserModel user = await _userRepository.GetUserById(tokenid);

            IEnumerable<LicenseModel> licenses = await _context.License.Where(l => l.User.Id == user.Id).ToListAsync();

            List<LicenseOutput> licenseOutputs = new List<LicenseOutput>();
            
            foreach(var license in licenses)
			{
                license.LicenseType = await _context.LicenseType.Where(lt => lt.Id == license.LicenseTypeId).FirstOrDefaultAsync();
                IEnumerable<DeviceModel> devices = await _context.Device.Where(d => d.LicenseId == license.Id).ToListAsync();
                PluginLicenseModel pluginLicense = await _context.PluginLicense.Where(pl => pl.LicenseId == license.Id).FirstOrDefaultAsync();
                pluginLicense.Plugin = await _context.Plugin.Where(p => p.Id == pluginLicense.PluginId).FirstOrDefaultAsync();

                licenseOutputs.Add(new LicenseOutput
                {
                    License = license,
                    Id = license.Id,
                    TimesActivated = license.TimesActivated,
                    LicenseType = license.LicenseType,
                    Plugin = pluginLicense.Plugin,
                    PluginBundle = pluginLicense.PluginBundle,
                    PluginBundleId = pluginLicense.PluginBundleId,
                    PluginId = pluginLicense.PluginId,
                    Devices = devices,
                    User = user
                });
			}

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "licenses 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(licenseOutputs);
        }

        // GET: api/LicenseModels/5
        [HttpGet("{jtoken}/[controller]/{id}")]
        public async Task<ActionResult<PluginLicenseModel>> GetLicenseModel(int id, string jtoken)
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
            int tokenid = Convert.ToInt32(tokenuser.Claims.First().Value);
            UserModel user = await _userRepository.GetUserById(tokenid);

            LicenseModel license = await _context.License.Where(l => l.Id == id).FirstOrDefaultAsync();
            PluginLicenseModel pluginLicense = await _context.PluginLicense.Where(pl => pl.LicenseId == license.Id).FirstOrDefaultAsync();
            pluginLicense.Plugin = await _context.Plugin.Where(p => p.Id == pluginLicense.PluginId).FirstOrDefaultAsync();
            IEnumerable<DeviceModel> devices = await _context.Device.Where(d => d.LicenseId == license.Id).ToListAsync();
            license.LicenseType = await _context.LicenseType.Where(lt => lt.Id == license.LicenseTypeId).FirstOrDefaultAsync();

            //PluginLicenseModel licenseModel = await _context.PluginLicense.Include(l => l.License).ThenInclude(f => f.LicenseType).Include(l => l.License).ThenInclude(u => u.User).Include(p => p.Plugin).Where(p => p.License.User.Id == user.Id && p.Id == id).FirstOrDefaultAsync();

            LicenseOutput licenseOutput = new LicenseOutput
            {
                License = license,
                Id = license.Id,
                TimesActivated = license.TimesActivated,
                LicenseType = license.LicenseType,
                Plugin = pluginLicense.Plugin,
                PluginBundle = pluginLicense.PluginBundle,
                PluginBundleId = pluginLicense.PluginBundleId,
                PluginId = pluginLicense.PluginId,
                Devices = devices,
                User = user
            };

            if (licenseOutput == null)
            {
                return NotFound();
            }

            return Ok(licenseOutput);
        }

    }
}
