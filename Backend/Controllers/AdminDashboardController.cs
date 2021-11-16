using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Repositories;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class AdminDashboardController : Controller
    {
        private readonly ILicenceRepository LicenceRepository;
        public AdminDashboardController(ILicenceRepository licenceRepository)
        {
            LicenceRepository = licenceRepository;
        }

        [HttpGet("GetLicences")]
        public async Task<IActionResult> GetLicences(int userID)
        {
            if (userID != 0)
            {
                List<LicenseModel> licenceList = await LicenceRepository.GetLicencesDb(2);
                return Ok(licenceList);
            }
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
    }
}
