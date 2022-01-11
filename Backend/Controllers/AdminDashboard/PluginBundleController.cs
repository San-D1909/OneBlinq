using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;
using Backend.DTO.In;
using Backend.DTO.Out;
using Backend.Infrastructure.Data.Repositories.Interfaces;

namespace Backend.Controllers.AdminDashboard
{
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PluginBundleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        public PluginBundleController(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            this._userRepository = userRepository;
        }

        // GET: api/PluginBundle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PluginBundleModel>>> GetPluginBundle([FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        {
            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "pluginbundles 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return await _context.PluginBundle.ToListAsync();
        }

        // GET: api/PluginBundle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginBundleOutput>> GetPluginBundleModel(int id)
        {
            var pluginBundleModel = await _context.PluginBundle.FindAsync(id);

            if (pluginBundleModel == null)
            {
                return NotFound();
            }

            IEnumerable<PluginModel> plugins = this._context.PluginBundles.Include(p => p.Plugin).Include(p => p.PluginBundle).Where(p => p.PluginBundleId == pluginBundleModel.Id).Select(p => p.Plugin).ToList();
            IEnumerable<UserModel> users = await this._userRepository.GetUsersByPluginBundle(null, null, pluginBundleModel);

            return new PluginBundleOutput(pluginBundleModel, this._context.PluginBundles.Where(p => p.PluginBundleId == pluginBundleModel.Id).Select(p => p.Plugin).ToList(), users);
        }

        // PUT: api/PluginBundle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPluginBundleModel(int id, PluginBundleInput pluginBundleInput)
        {
            PluginBundleModel pluginBundleModel = pluginBundleInput.GetPluginBundleModel();

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
        public async Task<ActionResult<PluginBundleModel>> PostPluginBundleModel(PluginBundleInput pluginBundleInput)
        {
            PluginBundleModel pluginBundleModel = pluginBundleInput.GetPluginBundleModel();
            _context.PluginBundle.Add(pluginBundleModel);
            await _context.SaveChangesAsync();

            foreach(int pluginId in pluginBundleInput.PluginIds)
            {
                PluginBundlesModel pluginBundles = new PluginBundlesModel
                {
                    PluginBundleId = pluginBundleModel.Id,
                    PluginId = pluginId
                };
                _context.PluginBundles.Add(pluginBundles);
            }

            _context.SaveChanges();
            

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
