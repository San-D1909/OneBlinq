using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
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
            return await _context.LicenseType.ToListAsync();
        }
    }
}
