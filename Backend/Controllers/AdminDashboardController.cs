using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Repositories;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class AdminDashboardController : Controller
    {
        private readonly ILicenceRepository LicenceRepository;
        private readonly IPluginRepository PluginRepository;
        public AdminDashboardController(ILicenceRepository licenceRepository, IPluginRepository pluginRepository)
        {
            LicenceRepository = licenceRepository;
            PluginRepository = pluginRepository;
        }

        [HttpGet("GetLicences")]
        public async Task<IActionResult> GetLicences(int userID)
        {
            if (userID != 0)
            {
                List<LicenseModel> licenceList = await LicenceRepository.GetLicencesDb(2);
                return Ok(licenceList);
            }
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
        [HttpGet("GetPlugins")]
        public async Task<IActionResult> GetPlugins(string searchString)
        {
            var plugins = await PluginRepository.GetPluginsByNameAsync(searchString);

            return Ok(plugins);
        }

        [HttpPost("AddPlugin")]
        public async Task<IActionResult> AddPlugin([Bind("PluginName","PluginDescription")] PluginModel plugin)
        {
            if (ModelState.IsValid)
            {
                PluginRepository.AddPlugin(plugin);
                await PluginRepository.SaveAsync();
                return Ok(plugin);
            }
            return Ok(plugin);
        }

    }
}
