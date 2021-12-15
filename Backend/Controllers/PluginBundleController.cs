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
        public async Task<ActionResult<IEnumerable<PluginBundleModel>>> GetPluginBundle()
        {
            return await _context.PluginBundle.ToListAsync();
        }

        //// GET: api/PluginBundles
        ////
        //[HttpGet]
        //// <summary>
        //// Get plugins
        //// </summary>
        //// <param name="filter" example='"pluginIds": [1,21321, 2312]'>The filter for the plugins</param>
        //[Produces("application/json")]
        //public async Task<ActionResult<IEnumerable<PluginBundleOutput>>> GetPlugins([FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        //{
        //    List<PluginBundleOutput> pluginBundleOutput = new List<PluginBundleOutput>();
        //    IEnumerable<PluginBundleModel> pluginBundles = await _pluginBundleRepository.GetAllPluginBundle(filter, sort);
          

        //    foreach (PluginBundleModel pluginBundle in pluginBundles)
        //    {
        //        IEnumerable<UserModel> users = await _userRepository.GetUsersByPluginBundle(null, null, pluginBundle);
        //        pluginBundleOutput.Add(new PluginBundleOutput
        //        {
        //            Id = pluginBundle.Id,
        //            BundleName = pluginBundle.BundleName,
        //            BundleDescription = pluginBundle.BundleDescription,
        //            Price = pluginBundle.Price,
        //            Users = users
        //        });
        //    }

        //    Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
        //    Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
        //    Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

        //    return Ok(pluginBundleOutput);
        //}

        // GET: api/Plugins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginOutput>> GetPlugin(int id)
        {
            var pluginBundle = await _pluginBundleRepository.GetPluginBundle(id);

            if (pluginBundle == null)
            {
                return NotFound();
            }

            return Ok(pluginBundle);
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
