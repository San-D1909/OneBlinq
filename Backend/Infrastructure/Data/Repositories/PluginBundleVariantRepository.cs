using Backend.Core.Logic;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Backend.Infrastructure.Data.Repositories
{
    public class PluginBundleVariantRepository : GenericRepository<PluginBundleVariantModel>, IPluginBundleVariantRepository
    {
        private ApplicationDbContext _context;
        public PluginBundleVariantRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PluginBundleVariantModel>> PluginBundleVariantsByFilter(string filter, string sort)
        {
            IEnumerable<PluginBundleVariantModel> plugins = _context.PluginBundleVariant;

            RequestSortFilterLogic filterLogic = new();

            plugins = filterLogic.FilterDatabaseModel(plugins, filter);

            return plugins.ToList();
        }
    }
}
