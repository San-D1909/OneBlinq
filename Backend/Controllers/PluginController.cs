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
using Stripe;

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

        [HttpPost("SearchForPlugin")]
        public async Task<ActionResult<IEnumerable<PluginModel>>> SearchForPlugin([FromQuery(Name = "searchString")] string searchString)
        {
            IEnumerable<PluginModel> pluginResults = await _pluginRepository.GetPluginsByNameAsync(searchString);
            return Ok(pluginResults);
        }

        // GET: api/Plugins
        [HttpGet]
        // <summary>
        // Get plugins
        // </summary>
        // <param name="filter" example='"pluginIds": [1,21321, 2312]'>The filter for the plugins</param>
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
                    FullPrice = plugin.FullPrice,
                    MonthlyPrice = plugin.MonthlyPrice,
                    Users = users
                });
            }


            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(pluginOutput);
        }

        // GET: api/Plugins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginOutput>> GetPlugin(int id)
        {
            var plugin = await _pluginRepository.GetPlugin(id);

            if (plugin == null)
            {
                return NotFound();
            }

            return Ok(plugin);
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

        // PUT: api/Plugins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlugin(int id, PluginModel plugin)
        {
            if (id != plugin.Id)
            {
                return BadRequest();
            }

            _context.Entry(plugin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PluginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(plugin);
        }

        // POST: api/Plugins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PluginModel>> PostPlugin(PluginModel plugin)
        { 
            var options = new ProductCreateOptions
            {
                Name = plugin.PluginName,
                Description = plugin.PluginDescription,
                // TODO: Add image field
            };

            var service = new ProductService();
            var productId = service.Create(options);

            plugin.StripeProductId = productId.Id;

            _pluginRepository.Add(plugin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlugin", new { id = plugin.Id }, plugin);
        }

        // DELETE: api/Plugins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlugin(int id)
        {
            var plugin = await _context.Plugin.FindAsync(id);
            if (plugin == null)
            {
                return NotFound();
            }

            _context.Plugin.Remove(plugin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Plugins/5
        [HttpDelete]
        public async Task<IActionResult> DeletePlugins([FromQuery( Name = "filter" )] string filter)
        {
            //"{\"id\":[4]}"
            List<PluginModel> plugins = (await _pluginRepository.GetPlugins(filter, "")).ToList();

            int[] deletedPlugins = new int[plugins.Count()];
            for (int i = 0; i < plugins.Count(); i++)
            {
                
                if (plugins[i] == null)
                {
                    return NotFound();
                }

                deletedPlugins[i] = plugins[i].Id;
                _context.Plugin.Remove(plugins[i]);
            }

            await _context.SaveChangesAsync();

            return Ok(deletedPlugins);
        }

        private bool PluginExists(int id)
        {
            return _context.Plugin.Any(e => e.Id == id);
        }
    }
}
