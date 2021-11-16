using Backend.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories
{
    public class PluginRepository : IPluginRepository
    {
        private readonly ApplicationDbContext _context;
        public PluginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PluginModel>> GetPluginsByNameAsync(string searchString)
        {
            return await _context.Plugin.Where(s => s.PluginName.Contains(searchString == null ? "" : searchString)).ToListAsync();
        }

        public virtual void AddPlugin(PluginModel plugin)
        {
            _context.Plugin.Add(plugin);
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
