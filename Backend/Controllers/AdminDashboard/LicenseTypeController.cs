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
    public class LicenseTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LicenseTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LicenseType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenseTypeModel>>> GetLicenseType()
        {
            IEnumerable<LicenseTypeModel> licenseTypes = await _context.LicenseType.ToListAsync();


            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "licenseTypes 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(licenseTypes);
        }

        // GET: api/LicenseType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenseTypeModel>> GetLicenseTypeModel(int id)
        {
            var licenseTypeModel = await _context.LicenseType.FindAsync(id);

            if (licenseTypeModel == null)
            {
                return NotFound();
            }

            return licenseTypeModel;
        }

        // PUT: api/LicenseType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLicenseTypeModel(int id, LicenseTypeModel licenseTypeModel)
        {
            if (id != licenseTypeModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(licenseTypeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseTypeModelExists(id))
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

        // POST: api/LicenseType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LicenseTypeModel>> PostLicenseTypeModel(LicenseTypeModel licenseTypeModel)
        {
            _context.LicenseType.Add(licenseTypeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLicenseTypeModel", new { id = licenseTypeModel.Id }, licenseTypeModel);
        }

        // DELETE: api/LicenseType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicenseTypeModel(int id)
        {
            var licenseTypeModel = await _context.LicenseType.FindAsync(id);
            if (licenseTypeModel == null)
            {
                return NotFound();
            }

            _context.LicenseType.Remove(licenseTypeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LicenseTypeModelExists(int id)
        {
            return _context.LicenseType.Any(e => e.Id == id);
        }
    }
}
