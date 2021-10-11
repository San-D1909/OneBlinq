using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult LogIn()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Register()
        {
            throw new NotImplementedException();
        }
    }
}
