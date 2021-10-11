using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("LogIn")]
        public IActionResult LogIn()
        {
            throw new NotImplementedException();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            throw new NotImplementedException();
        }
    }
}
