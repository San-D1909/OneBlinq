using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.DTO.Out;
using Stripe;
using Backend.Infrastructure.Data.Repositories;

namespace Backend.Controllers.AdminDashboard
{
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PluginVariantController : ControllerBase
    {
        private IPluginVariantRepository _pluginVariantRepository;
        private IPluginRepository _pluginRepository;
        public PluginVariantController(IPluginVariantRepository pluginVariantRepository, IPluginRepository pluginRepository)
        {
            _pluginVariantRepository = pluginVariantRepository;
            _pluginRepository = pluginRepository;
        }

        // GET: api/Plugins
        [HttpGet]
        // <summary>
        // Get plugins
        // </summary>
        // <param name="filter" example='"pluginIds": [1,21321, 2312]'>The filter for the plugins</param>
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PluginVariantModel>>> GetPluginVariants([FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        {
            IEnumerable<PluginVariantModel> plugins = await _pluginVariantRepository.PluginVariantsByFilter(filter, sort);

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(plugins);
        }

        // GET: api/Plugins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PluginVariantModel>> GetPlugin(int id)
        {
            var plugin = await _pluginVariantRepository.GetByIdAsync(id);
            if (plugin == null)
            {
                return NotFound();
            }

            return Ok(plugin);
        }

        // PUT: api/Plugins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlugin(int id, PluginVariantModel plugin)
        {
            if (id != plugin.Id)
            {
                return BadRequest();
            }

            _pluginVariantRepository.Update(plugin);

            try
            {
                await _pluginVariantRepository.SaveAsync();
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
        public async Task<ActionResult<PluginVariantModel>> PostPlugin(PluginVariantModel pluginVariant)
        {
            var plugin = _pluginRepository.GetById(pluginVariant.PluginId);
            if(plugin == null) return NotFound();

            var options = new PriceCreateOptions();

            if(pluginVariant.IsSubscription)
            {
                options = new PriceCreateOptions
                {
                    UnitAmountDecimal = pluginVariant.Price * 100,
                    Currency = "eur",
                    Product = plugin.StripeProductId,
                    BillingScheme = "per_unit",
                    TaxBehavior = "exclusive",
                    Recurring = new PriceRecurringOptions
                    {
                        Interval = "month",
                        IntervalCount = 1,
                        AggregateUsage = null,
                        UsageType = "licensed"

                    }
                };
            }
            else
            {
                options = new PriceCreateOptions
                {
                    UnitAmountDecimal = pluginVariant.Price * 100,
                    Currency = "eur",
                    Product = plugin.StripeProductId,
                    BillingScheme = "per_unit",
                    TaxBehavior = "exclusive",
                };
            }


            var service = new PriceService();
            var price = service.Create(options);

            pluginVariant.StripePriceId = price.Id;
            _pluginVariantRepository.Add(pluginVariant);

            await _pluginVariantRepository.SaveAsync();

            return CreatedAtAction("GetPluginVariants", new { id = pluginVariant.Id }, pluginVariant);
        }

        // DELETE: api/Plugins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlugin(int id)
        {
            var plugin = await _pluginVariantRepository.GetByIdAsync(id);
            var service = new ProductService();
            if (plugin == null)
            {
                return NotFound();
            }

            if (plugin.StripePriceId == null)
            {
                service.Delete(plugin.StripePriceId);
            }
            _pluginVariantRepository.Remove(plugin);
            await _pluginVariantRepository.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Plugins/5
        [HttpDelete]
        public async Task<IActionResult> DeletePlugins([FromQuery(Name = "filter")] string filter)
        {
            //"{\"id\":[4]}"
            List<PluginVariantModel> plugins = (await _pluginVariantRepository.PluginVariantsByFilter(filter, "")).ToList();

            int[] deletedPlugins = new int[plugins.Count()];
            for (int i = 0; i < plugins.Count(); i++)
            {
                
                if (plugins[i] == null)
                {
                    return NotFound();
                }

                deletedPlugins[i] = plugins[i].Id;
                _pluginVariantRepository.Remove(plugins[i]);
            }

            await _pluginVariantRepository.SaveAsync();

            return Ok(deletedPlugins);
        }

        private bool PluginExists(int id)
        {
            return _pluginVariantRepository.GetById(id) != null;
        }

    }
}
