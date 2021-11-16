using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginRepository
    {
        Task<IEnumerable<PluginModel>> GetPluginsByNameAsync(string searchString);

        void AddPlugin(PluginModel plugin);

        Task<int> SaveAsync();
    }
}
