using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginRepository
    {
        Task<IEnumerable<Plugin>> GetPlugins(string filter, string sort);
        Task<IEnumerable<Plugin>> GetPluginsByNameAsync(string searchString);

        void AddPlugin(Plugin plugin);

        Task<int> SaveAsync();
    }
}
