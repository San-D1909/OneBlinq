using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginRepository: IGenericRepository<PluginModel>
    {
        Task<IEnumerable<PluginModel>> GetPluginsByUser(string filter, string sort, UserModel user);
        Task<IEnumerable<PluginModel>> GetPlugins(string filter = null, string sort = null);
        Task<PluginModel> GetPlugin(int id);
        Task<IEnumerable<PluginModel>> GetPluginsByNameAsync(string searchString);
        PluginModel UpdatePlugin(int id, PluginModel plugin);
        Task DeletePlugin(int id);
        void AddPlugin(PluginModel plugin);
    }
}
