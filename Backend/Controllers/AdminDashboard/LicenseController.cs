using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;

namespace Backend.Controllers.AdminDashboard
{
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LicenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LicenseModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenseModel>>> GetLicense()
        {
            IEnumerable<LicenseModel> licenses = await _context.License.Include(t => t.LicenseType).Include(t => t.User).ToListAsync();

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(licenses);
        }

        // GET: api/LicenseModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenseModel>> GetLicenseModel(int id)
        {
            var licenseModel =  _context.License.Include(t => t.LicenseType).Include(t => t.User).ToList().Find(p => p.Id == id);

            if (licenseModel == null)
            {
                return NotFound();
            }

            return licenseModel;
        }

        // PUT: api/LicenseModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLicenseModel(int id, LicenseModel licenseModel)
        {
            if (id != licenseModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(licenseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseModelExists(id))
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

        // POST: api/LicenseModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LicenseModel>> PostLicenseModel(LicenseModel licenseModel)
        {
            _context.License.Add(licenseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLicenseModel", new { id = licenseModel.Id }, licenseModel);
        }

        // DELETE: api/LicenseModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicenseModel(int id)
        {
            var licenseModel = await _context.License.FindAsync(id);
            if (licenseModel == null)
            {
                return NotFound();
            }

            _context.License.Remove(licenseModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LicenseModelExists(int id)
        {
            return _context.License.Any(e => e.Id == id);
        }
    }
}
