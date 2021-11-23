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
        {
            var user = TokenHelper.Verify(jtoken, _config);
            if (user is null)
            {
                return NotFound();
            }
            int id = Convert.ToInt32(user.Claims.First().Value);
            UserModel userbyid = await _userRepository.GetUserById(id);
            CompanyModel userCompany = new CompanyModel();
            if (userbyid.Company != 0 && userbyid != null)
            {
                userCompany = await _userRepository.GetCompanyById(userbyid.Company);
            }           
            return Ok(new {userbyid,userCompany});
        }


        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUserData(int userId, UserModel updateUserModel)
        {
            if (userId != updateUserModel.UserId)
            {
                return BadRequest();
            }

            UserModel userbyid = await _userRepository.GetUserById(userId);
            if (userbyid is null)
            {
                return NotFound();
            }
            if (userbyid.FullName != updateUserModel.FullName)
            {
                await _userRepository.UpdateFullName(updateUserModel.FullName, userId);
            }
            // var updatedUser = await _userRepository.UpdateUser(userModel);
            return Ok();
        }
    }
}
