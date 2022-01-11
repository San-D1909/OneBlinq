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
using Microsoft.Extensions.Configuration;
using Backend.Infrastructure.Data.Repositories.Interfaces;

namespace Backend.Controllers.UserDashboard
{
    [Route("api/v{version:apiVersion}/user/")]
    [ApiVersion("1")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPluginRepository _pluginRepository;
        private readonly IConfiguration _config;

        public PluginController(ApplicationDbContext context, IConfiguration config, IUserRepository userRepository, IPluginRepository pluginRepository)
        {
            _config = config;
            _context = context;
            _userRepository = userRepository;
            _pluginRepository = pluginRepository;
        }

        // GET: api/PluginModels
        [HttpGet("{jtoken}/[controller]/")]
        public async Task<ActionResult<IEnumerable<PluginModel>>> GetPlugin(string jtoken, [FromQuery(Name = "filter")] string filter, [FromQuery(Name = "sort")] string sort)
        {
            if (jtoken is null)
            {
                return NotFound();
            }
            var tokenuser = TokenHelper.Verify(jtoken, _config);

            if (tokenuser is null)
            {
                return NotFound();
            }
            int id = Convert.ToInt32(tokenuser.Claims.First().Value);
            UserModel user = await _userRepository.GetUserById(id);

            //IEnumerable<PluginModel> plugins  = await _context.PluginLicense.Include(l => l.License).ThenInclude(u => u.User).Include(p => p.Plugin).Where(p => p.License.User.Id == user.Id).Select(p => p.Plugin).ToListAsync();
            IEnumerable<PluginModel> plugins = await this._pluginRepository.GetPluginsByUser(filter, sort, user);
            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Request.HttpContext.Response.Headers.Add("Content-Range", "plugins 0-5/1");
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");

            return Ok(plugins);
        }

        // GET: api/PluginModels/5
        [HttpGet("{jtoken}/[controller]/{id}")]
        public async Task<ActionResult<PluginModel>> GetPluginModel(int id, string jtoken)
        {
            if (jtoken is null)
            {
                return NotFound();
            }
            var tokenuser = TokenHelper.Verify(jtoken, _config);

            if (tokenuser is null)
            {
                return NotFound();
            }
            int tokenid = Convert.ToInt32(tokenuser.Claims.First().Value);
            UserModel user = await _userRepository.GetUserById(tokenid);

            var plugin = await _context.PluginLicense.Include(l => l.License).ThenInclude(u => u.User).Include(p => p.Plugin).Where(p => p.License.User.Id == user.Id && p.Id == id).Select(p => p.Plugin).FirstOrDefaultAsync();

            if (plugin == null)
            {
                return NotFound();
            }

            return plugin;
        }
    }
}
