using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;

namespace Backend.Controllers.UserDashboard
{
    [Route("api/v{version:apiVersion}/user/")]
    [ApiVersion("1")]
    [ApiController]
    public class PluginLicenseModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PluginLicenseModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PluginLicenseModels
        [HttpGet("{jtoken}/[controller]/")]
        public async Task<ActionResult<IEnumerable<PluginLicenseModel>>> GetPluginLicense(string jtoken)
        {
            var test1 = _context.PluginLicense;
            return await _context.PluginLicense.ToListAsync();
        }

        // GET: api/PluginLicenseModels/5
        [HttpGet("{jtoken}/[controller]/{id}")]
        public async Task<ActionResult<PluginLicenseModel>> GetPluginLicenseModel(int id, string jtoken)
        {
            var pluginLicenseModel = await _context.PluginLicense.FindAsync(id);

            if (pluginLicenseModel == null)
            {
                return NotFound();
            }

            return pluginLicenseModel;
        }
    }
}
