using Backend.Core.Logic;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Backend.Infrastructure.Data.Repositories
{
    public class PluginVariantRepository : GenericRepository<PluginVariantModel>, IPluginVariantRepository
    {
        private ApplicationDbContext _context;
        public PluginVariantRepository(ApplicationDbContext context) : base(context) { 
            _context = context;
        }

        public async Task<IEnumerable<PluginVariantModel>> PluginVariantsByFilter(string filter, string sort)
        {
            IEnumerable<PluginVariantModel> plugins = _context.PluginVariant;

            RequestSortFilterLogic filterLogic = new();

            plugins = filterLogic.FilterDatabaseModel(plugins, filter);

            return plugins.ToList();
        }
    }
}
