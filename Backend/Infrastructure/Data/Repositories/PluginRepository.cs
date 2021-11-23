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
    public class PluginRepository : GenericRepository<Plugin>, IPluginRepository
    {
        public PluginRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Plugin>> GetPlugins(string filter, string sort)
        {
            IEnumerable<Plugin> plugins = _context.Plugin;

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();
            
            plugins = filterLogic.FilterDatabaseModel<Plugin>(plugins, filter);

            return plugins.ToList();
        }

        public async Task<IEnumerable<Plugin>> GetPluginsByNameAsync(string searchString)
        {
            return await _context.Plugin.Where(s => s.PluginName.Contains(searchString == null ? "" : searchString)).ToListAsync();
        }

        public virtual void AddPlugin(Plugin plugin)
        {
            _context.Plugin.Add(plugin);
        }

    }
}
