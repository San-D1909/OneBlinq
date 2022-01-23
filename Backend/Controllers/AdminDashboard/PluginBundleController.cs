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
        private readonly IPluginBundleRepository _pluginBundleRepository;
        public PluginBundleController(ApplicationDbContext context, IUserRepository userRepository, IPluginBundleRepository pluginBundleRepository)
        {
            _context = context;
            this._userRepository = userRepository;
            this._pluginBundleRepository = pluginBundleRepository;
        }

        // GET: api/PluginBundle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PluginBundleOutput>>> GetPluginBundle([FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        {
            List<PluginBundleOutput> pluginBundleOutputs = new List<PluginBundleOutput>();
            IEnumerable<PluginBundleModel> pluginBundles = await _pluginBundleRepository.GetAllPluginBundle(filter, sort);

            foreach (PluginBundleModel pluginBundle in pluginBundles)
            {
                IEnumerable<UserModel> users = await _userRepository.GetUsersByPluginBundle(null, null, pluginBundle);
                IEnumerable<PluginModel> plugins = _context.PluginBundles.Where(pb => pb.PluginBundleId == pluginBundle.Id).Include(pb => pb.Plugin).Select(pb => pb.Plugin).ToList();
                PluginBundleImageModel image = _context.PluginBundleImage.Where(p => p.PluginBundle.Id == pluginBundle.Id).FirstOrDefault();
                pluginBundleOutputs.Add(new PluginBundleOutput
                {
                    Id = pluginBundle.Id,
                    BundleDescription = pluginBundle.BundleDescription,
                    BundleName = pluginBundle.BundleName,
                    Plugins = plugins,
                    StripeProductId = pluginBundle.StripeProductId,
                    Image = image
                });
            }

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "pluginbundles 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return pluginBundleOutputs;
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
            PluginBundleOutput pluginBundle = new PluginBundleOutput(pluginBundleModel, this._context.PluginBundles.Where(p => p.PluginBundleId == pluginBundleModel.Id).Select(p => p.Plugin).ToList(), users);

            var image = _context.PluginBundleImage.Where(p => p.PluginBundle.Id == id).FirstOrDefault();
            if (image != null)
            {
                pluginBundle.Image = image;
            }
            

            return pluginBundle;
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

            PluginBundleImageModel image = new PluginBundleImageModel
            {
                ImageData = pluginBundleInput.EncodedFileContent,
                PluginBundle = pluginBundleModel
            };

            foreach (int pluginId in pluginBundleInput.PluginIds)
            {
                PluginBundlesModel pluginBundles = new PluginBundlesModel
                {
                    PluginBundleId = pluginBundleModel.Id,
                    PluginId = pluginId
                };
                _context.PluginBundles.Add(pluginBundles);
            }

            _context.PluginBundleImage.Add(image);
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
