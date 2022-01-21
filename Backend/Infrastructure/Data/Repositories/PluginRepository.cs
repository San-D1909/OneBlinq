using Backend.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Core.Logic;

namespace Backend.Infrastructure.Data.Repositories
{
    public class PluginRepository : GenericRepository<PluginModel>, IPluginRepository
    {
        public PluginRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<PluginModel>> GetPlugins(string filter = null, string sort = null)
        {
            IEnumerable<PluginModel> plugins = _context.Plugin;

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();
            
            if (filter != null)
            {
                plugins = filterLogic.FilterDatabaseModel<PluginModel>(plugins, filter);
            }

            return plugins.ToList();
        }

        public async Task<IEnumerable<PluginModel>> GetPluginsByNameAsync(string searchString)
        {
            return await _context.Plugin.Where(s => s.PluginName.Contains(searchString == null ? "" : searchString)).ToListAsync();
        }

        public virtual void AddPlugin(PluginModel plugin)
        {
            _context.Plugin.Add(plugin);
        }

        public async Task<IEnumerable<PluginModel>> GetPluginsByUser(string filter, string sort, UserModel user)
        {
            IEnumerable<PluginLicenseModel> plugins = _context.PluginLicense.Include(p => p.Plugin).Include(l => l.License).ThenInclude(u => u.User).Where(p => p.License.User.Id == user.Id);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            plugins = filterLogic.FilterDatabaseModel<PluginLicenseModel>(plugins, filter);

            return plugins.Select(p => p.Plugin).ToList();
        }

        public PluginModel UpdatePlugin(int id, PluginModel plugin)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlugin(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PluginModel> GetPlugin(int id)
        {
            return _context.Plugin.Where(p => p.Id == id).FirstAsync();
        }
    }
}
