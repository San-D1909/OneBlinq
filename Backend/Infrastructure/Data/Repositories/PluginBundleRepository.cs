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
    public class PluginBundleRepository : GenericRepository<PluginBundleModel>, IPluginBundleRepository
    {

        public PluginBundleRepository(ApplicationDbContext context) : base(context) { }

        public void AddPluginBundle(PluginBundleModel pluginBundle)
        {
            _context.PluginBundle.Add(pluginBundle);
        }

        public async Task<IEnumerable<PluginBundleModel>> GetAllPluginBundle(string filter = null, string sort = null)
        {
            IEnumerable<PluginBundleModel> pluginBundles = _context.PluginBundle;

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            if (filter != null)
            {
                pluginBundles = filterLogic.FilterDatabaseModel<PluginBundleModel>(pluginBundles, filter);
            }
            

            return pluginBundles.ToList();
        }
        public Task<PluginBundleModel> GetPluginBundle(int id)
        {
            return _context.PluginBundle.Where(p => p.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<PluginBundleModel>> GetPluginBundleByName(string searchString)
        {
            return await _context.PluginBundle.Where(s => s.BundleName.Contains(searchString == null ? "" : searchString)).ToListAsync();
        }

    }
}
