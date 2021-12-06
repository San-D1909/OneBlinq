using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.DTO.Out;
using System.Net.NetworkInformation;
using System;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPluginRepository _pluginRepository;
        private readonly IUserRepository _userRepository;

        public PluginController(ApplicationDbContext context, IPluginRepository pluginRepository, IUserRepository userRepository)
        {
            _context = context;
            _pluginRepository = pluginRepository;
            _userRepository = userRepository;
        }


        // GET: api/Plugins
        [HttpGet]
        /// <summary>
        /// Get plugins
        /// </summary>
        /// <param name="filter" example='"pluginIds": [1,21321, 2312]'>The filter for the plugins</param>
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PluginOutput>>> GetPlugins([FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        {
            List<PluginOutput> pluginOutput = new List<PluginOutput>();
            IEnumerable<PluginModel> plugins = await _pluginRepository.GetPlugins(filter, sort);

            foreach (PluginModel plugin in plugins)
            {
                IEnumerable<UserModel> users = await _userRepository.GetUsersByPlugin(null, null, plugin);
                pluginOutput.Add(new PluginOutput
                {
                    Id = plugin.Id,
                    PluginName = plugin.PluginName,
                    PluginDescription = plugin.PluginDescription,
                    Price = plugin.Price,
                    Users = users
                });
            }


            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(pluginOutput);
        }


        [HttpGet("macaddress")]
        public IActionResult MacTest()
        {
            try
            {
                var firstMacAddress = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => nic.GetPhysicalAddress().ToString())
                    .FirstOrDefault();

                return Ok(firstMacAddress);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }

        private bool PluginExists(int id)
        {
            return _context.Plugin.Any(e => e.Id == id);
        }
    }
}
