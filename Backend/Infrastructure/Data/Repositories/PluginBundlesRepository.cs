using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories
{
    public class PluginBundlesRepository : GenericRepository<PluginBundlesModel>, IPluginBundlesRepository
    {
        public PluginBundlesRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<PluginModel>> GetPluginsFromBundle(int pluginBundleId)
        {
            return this._context.PluginBundles.Where(pbs => pbs.PluginBundleId == pluginBundleId).Select(pbs => pbs.Plugin);
        }
    }
}
