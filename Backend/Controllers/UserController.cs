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
            if (userbyid is null)
            {
                return NotFound();
            }
            if (userbyid != userInfo.User)
            {
                await _userRepository.UpdateUser(userInfo.User);
            }
            // var updatedUser = await _userRepository.UpdateUser(userModel);
            return Ok();
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUser()
        {
            IEnumerable<UserModel> users = await _context.User.Include(c => c.Company).ToListAsync();

            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(int id)
        {
            var userModel = await _context.User.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return userModel;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel(int id, UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return BadRequest();
            }


            _context.Entry(userModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            userModel.Salt = salt;
            userModel.Password = _encryptor.EncryptPassword(userModel.Password + salt);
            _context.User.Add(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel(int id)
        {
            var userModel = await _context.User.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            _context.User.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserModelExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
