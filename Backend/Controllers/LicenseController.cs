using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        [HttpGet]
        public IActionResult Verify()
        {
            throw new NotImplementedException();
        }
    }
}
