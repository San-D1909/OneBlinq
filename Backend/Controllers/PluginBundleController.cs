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

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PluginBundleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPluginBundleRepository _pluginBundleRepository;


        public PluginBundleController(ApplicationDbContext context, IPluginBundleRepository pluginBundleRepository)
        {
            _context = context;
            _pluginBundleRepository = pluginBundleRepository;
        }

        [HttpPost("SearchForPluginBundle")]
        public async Task<ActionResult<IEnumerable<PluginModel>>> SearchForPlugin([FromQuery(Name = "searchString")] string searchString)
        {
            IEnumerable<PluginBundleModel> pluginBundleResults = await _pluginBundleRepository.GetPluginBundleByName(searchString);
            return Ok(pluginBundleResults);
        }

        //[HttpPost("SearchForBundle")]
        //public async Task<ActionResult<List<PluginModel>>> SearchForBundle([FromQuery(Name = "searchString")] string searchString)
        //{
        //    List<PluginBundleModel> bundles = await _context.PluginBundle.Where(s => s.BundleName.Contains(searchString == null ? "" : searchString)).ToListAsync();
        //    return Ok(bundles);
        //}

        // GET: api/PluginBundle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PluginBundleModel>>> GetPluginBundle()
        {
            return await _context.PluginBundle.ToListAsync();
        }

        // GET: api/PluginBundle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginBundleModel>> GetPluginBundleModel(int id)
        {
            var pluginBundleModel = await _context.PluginBundle.FindAsync(id);

            if (pluginBundleModel == null)
            {
                return NotFound();
            }

            return pluginBundleModel;
        }

        // PUT: api/PluginBundle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPluginBundleModel(int id, PluginBundleModel pluginBundleModel)
        {
            if (id != pluginBundleModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(pluginBundleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PluginBundleModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PluginBundle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PluginBundleModel>> PostPluginBundleModel(PluginBundleModel pluginBundleModel)
        {
            _context.PluginBundle.Add(pluginBundleModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPluginBundleModel", new { id = pluginBundleModel.Id }, pluginBundleModel);
        }

        // DELETE: api/PluginBundle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePluginBundleModel(int id)
        {
            var pluginBundleModel = await _context.PluginBundle.FindAsync(id);
            if (pluginBundleModel == null)
            {
                return NotFound();
            }

            _context.PluginBundle.Remove(pluginBundleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PluginBundleModelExists(int id)
        {
            return _context.PluginBundle.Any(e => e.Id == id);
        }
    }
}
