using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.DTO.Out;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PluginsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPluginRepository _pluginRepository;

        public PluginsController(ApplicationDbContext context, IPluginRepository pluginRepository)
        {
            _context = context;
            _pluginRepository = pluginRepository;
        }

        // GET: api/Plugins
        [HttpGet]
        /// <summary>
        /// Get plugins
        /// </summary>
        /// <param name="filter" example='"pluginIds": [1,21321, 2312]'>The filter for the plugins</param>
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Plugin>>> GetPlugin([FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        {

          IEnumerable<Plugin> plugins = await _pluginRepository.GetPlugins(filter, sort);

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(plugins);
        }

        // GET: api/Plugins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginOut>> GetPlugin(int id)
        {
            var plugin = await _context.Plugin.FindAsync(id);

            if (plugin == null)
            {
                return NotFound();
            }

            return Ok(plugin);
        }

        // PUT: api/Plugins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlugin(int id, Plugin plugin)
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
        public async Task<ActionResult<Plugin>> PostPlugin(Plugin plugin)
        {
            _context.Plugin.Add(plugin);
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
            List<Plugin> plugins = (await _pluginRepository.GetPlugins(filter, "")).ToList();

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
