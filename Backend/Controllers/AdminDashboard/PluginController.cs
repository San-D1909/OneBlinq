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
using Backend.DTO.In;
using Microsoft.AspNetCore.Http;

namespace Backend.Controllers.AdminDashboard
{
    [Route("api/v{version:apiVersion}/admin/[controller]")]
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
        // <summary>
        // Get plugins
        // </summary>
        // <param name="filter" example='"pluginIds": [1,21321, 2312]'>The filter for the plugins</param>
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PluginOutput>>> GetPlugins([FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        {
            List<PluginOutput> pluginOutput = new List<PluginOutput>();
            IEnumerable<PluginModel> plugins = await _pluginRepository.GetPlugins(filter, sort);

            foreach(PluginModel plugin in plugins)
            {
                IEnumerable<UserModel> users = await _userRepository.GetUsersByPlugin(null, null, plugin);
                PluginImageModel image = _context.PluginImage.Where(p => p.Plugin.Id == plugin.Id).FirstOrDefault();
                pluginOutput.Add(new PluginOutput
                {
                    Id = plugin.Id,
                    PluginName = plugin.PluginName,
                    PluginDescription = plugin.PluginDescription,
                    Users = users,
                    Image = image
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

            IEnumerable<UserModel> users = await _userRepository.GetUsersByPlugin(null, null, plugin);

            Byte[] bytes = System.IO.File.ReadAllBytes("./DefaultImages/HTMLPlaceholder.png");
            String file = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
            PluginImageModel image = _context.PluginImage.Where(p => p.Plugin.Id == id).FirstOrDefault() ?? new PluginImageModel { Id = 0, ImageData = file, Plugin = plugin};
            PluginOutput pluginOutput = new PluginOutput
            {
                Id = plugin.Id,
                PluginName = plugin.PluginName,
                PluginDescription = plugin.PluginDescription,
                Users = users,
                Image = image
            };
            return Ok(pluginOutput);
        }

        // PUT: api/Plugins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlugin(int id, [FromForm] PluginModel plugin)
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
        public async Task<ActionResult<PluginModel>> PostPlugin(PluginInput pluginInput)
        {
            var options = new ProductCreateOptions
            {
                Name = pluginInput.PluginName,
                Description = pluginInput.PluginDescription,
                TaxCode = "txcd_10000000",
                // TODO: Add image field
            };

            var service = new ProductService();
            var productId = service.Create(options);
            
            PluginModel plugin = pluginInput.GetPluginModel();
            PluginImageModel image = new PluginImageModel
            {
                ImageData = pluginInput.EncodedFileContent,
                Plugin = plugin
            };

            plugin.StripeProductId = productId.Id;

            _pluginRepository.Add(plugin);
            _context.PluginImage.Add(image);
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

            IEnumerable<PluginImageModel> images = _context.PluginImage.Where(p => p.Plugin.Id == id).ToList();
            _context.PluginImage.RemoveRange(images);
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
                IEnumerable<PluginImageModel> images = _context.PluginImage.Where(p => p.Plugin.Id == plugins[i].Id).ToList();
                _context.PluginImage.RemoveRange(images);
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
