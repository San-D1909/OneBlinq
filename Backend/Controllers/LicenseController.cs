using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("[controller]/[action]")]
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
