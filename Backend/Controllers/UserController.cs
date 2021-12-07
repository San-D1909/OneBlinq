using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Data;
using Backend.Models;
using Backend.Core.Logic;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Backend.DTO.In;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;
        private PasswordEncrypter _encryptor;

        public UserController(IUserRepository userRepository, IConfiguration config, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _config = config;
            _context = context;
            _encryptor = new PasswordEncrypter(config);
        }


        [HttpGet("GetUserByToken")]
        public async Task<ActionResult> GetUserByToken([FromQuery] string jtoken)
        {//Gets the user and company data by the token from the front end, used in the userinfo page to populate the data.
            var user = TokenHelper.Verify(jtoken, _config);
            if (user is null)
            {
                return NotFound();
            }
            int id = Convert.ToInt32(user.Claims.First().Value);
            UserModel userById = await _userRepository.GetUserById(id);
            CompanyModel userCompany = new CompanyModel();
            if (userById.Company != null && userById != null)
            {
                userCompany = await _userRepository.GetCompanyById(userById.Company.Id);
            }
            return Ok(new { userById, userCompany });
        }


        [HttpPost("UpdateData")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateData([FromBody]UserinfoDTO userInfo)
        {
            UserModel userbyid = await _userRepository.GetUserById(userInfo.User.Id);
            CompanyModel companybyid = await _userRepository.GetCompanyById(userInfo.Company.Id);
            if (userbyid is null)
            {
                return NotFound();
            }
            if (userbyid != userInfo.User)
            {
                await _userRepository.UpdateUser(userInfo.User);
            }
            if(companybyid != userInfo.Company)
            {
                await _userRepository.UpdateCompany(userInfo.Company);
            }
            return Ok();
        }
    }
}
