using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class PluginBundleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPluginBundleRepository _pluginBundleRepository;
        private readonly IUserRepository _userRepository;


        public PluginBundleController(ApplicationDbContext context, IPluginBundleRepository pluginBundleRepository, IUserRepository userRepository)
        {
            _context = context;
            _pluginBundleRepository = pluginBundleRepository;
            _userRepository = userRepository;
        }

        [HttpPost("SearchForPluginBundle")]
        public async Task<ActionResult<IEnumerable<PluginBundleModel>>> SearchForPluginBundle([FromQuery(Name = "searchString")] string searchString)
        {
            IEnumerable<PluginBundleModel> pluginBundleResults = await _pluginBundleRepository.GetPluginBundleByName(searchString);
            return Ok(pluginBundleResults);
        }


        // GET: api/PluginBundle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PluginBundleOutput>>> GetPluginBundle()
        {
            List<PluginBundleOutput> pluginBundleOutputs = new List<PluginBundleOutput>();
            IEnumerable<PluginBundleModel> pluginBundles = await _pluginBundleRepository.GetAllPluginBundle();

            foreach (PluginBundleModel pluginBundle in pluginBundles)
            {
                IEnumerable<PluginModel> plugins = _context.PluginBundles.Where(pb => pb.PluginBundleId == pluginBundle.Id).Include(pb => pb.Plugin).Select(pb => pb.Plugin).ToList();
                PluginBundleImageModel image = _context.PluginBundleImage.Where(p => p.PluginBundle.Id == pluginBundle.Id).FirstOrDefault();
                pluginBundleOutputs.Add(new PluginBundleOutput
                {
                    Id = pluginBundle.Id,
                    BundleDescription = pluginBundle.BundleDescription,
                    BundleName = pluginBundle.BundleName,
                    Plugins = plugins,
                    Price = pluginBundle.Price,
                    Image = image
                });
            }

            return pluginBundleOutputs;
        }

        // GET: api/Plugins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginBundleOutput>> GetPlugin(int id)
        {
            var pluginBundleModel = await _context.PluginBundle.FindAsync(id);

            if (pluginBundleModel == null)
            {
                return NotFound();
            }

            IEnumerable<PluginModel> plugins = this._context.PluginBundles.Include(p => p.Plugin).Include(p => p.PluginBundle).Where(p => p.PluginBundleId == pluginBundleModel.Id).Select(p => p.Plugin).ToList();
            PluginBundleOutput pluginBundle = new PluginBundleOutput(pluginBundleModel, this._context.PluginBundles.Where(p => p.PluginBundleId == pluginBundleModel.Id).Select(p => p.Plugin).ToList());

            var image = _context.PluginBundleImage.Where(p => p.PluginBundle.Id == id).FirstOrDefault();
            if (image != null)
            {
                pluginBundle.Image = image;
            }


            return pluginBundle;
        }

        // PUT: api/Plugins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlugin(int id, PluginBundleModel pluginBundle)
        {
            if (id != pluginBundle.Id)
            {
                return BadRequest();
            }

            _context.Entry(pluginBundle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PluginBundleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(pluginBundle);
        }

        // POST: api/Plugins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PluginBundleModel>> PostPluginBundle(PluginBundleModel pluginBundle)
        {
            _context.PluginBundle.Add(pluginBundle);
            await _context.SaveChangesAsync();


            // TODO: create stripe product object
            // TODO: create stripe payment object

            return CreatedAtAction("GetPluginBundle", new { id = pluginBundle.Id }, pluginBundle);
        }

        // DELETE: api/Plugins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePluginBundle(int id)
        {
            var pluginBundle = await _context.PluginBundle.FindAsync(id);
            if (pluginBundle == null)
            {
                return NotFound();
            }

            _context.PluginBundle.Remove(pluginBundle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Plugins/5
        [HttpDelete]
        public async Task<IActionResult> DeletePlugins([FromQuery(Name = "filter")] string filter)
        {
            //"{\"id\":[4]}"
            List<PluginBundleModel> pluginBundles = (await _pluginBundleRepository.GetAllPluginBundle(filter, "")).ToList();

            int[] deletedPluginBundles = new int[pluginBundles.Count()];
            for (int i = 0; i < pluginBundles.Count(); i++)
            {

                if (pluginBundles[i] == null)
                {
                    return NotFound();
                }

                deletedPluginBundles[i] = pluginBundles[i].Id;
                _context.PluginBundle.Remove(pluginBundles[i]);
            }

            await _context.SaveChangesAsync();

            return Ok(deletedPluginBundles);
        }

        private bool PluginBundleExists(int id)
        {
            return _context.PluginBundle.Any(e => e.Id == id);
        }
    }
}
