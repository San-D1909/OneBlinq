using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Core.Logic;
using Backend.Infrastructure.Data.Repositories;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System;
using System.Linq;
using Backend.DTO.In;

namespace Backend.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public UserController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
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
        public async Task<ActionResult> UpdateData([FromBody] RegisterInput updatedUser)
        {
            UserModel userbyid = await _userRepository.GetUserById(updatedUser.User.UserId);
            if (userbyid is null)
            {
                return NotFound();
            }
            if (userbyid.FullName != updatedUser.User.FullName)
            {
                await _userRepository.UpdateFullName(updatedUser.User.FullName, updatedUser.User.UserId);
            }
            // var updatedUser = await _userRepository.UpdateUser(userModel);
            return Ok();
        }
    }
}
