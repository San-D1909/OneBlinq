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

namespace Backend.Controllers.AdminDashboard
{
    [Route("api/v{version:apiVersion}/admin/[controller]")]
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
        public async Task<ActionResult<UserModel>> PostUserModel(AdminCreateInput createInput)
        {
            if (createInput.Password == createInput.PasswordConfirmation)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                UserModel userModel = new UserModel
                {
                    FullName = createInput.FullName,
                    Salt = salt,
                    Password = _encryptor.EncryptPassword(createInput.Password + salt),
                    IsAdmin = true,
                    Email = createInput.Email,
                    IsVerified = false
                };

                _context.User.Add(userModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction("PostUserModel", userModel);
            }

            return BadRequest();
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
