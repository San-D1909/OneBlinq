using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginBundleRepository
    {
        Task<IEnumerable<PluginBundleModel>> GetAllPluginBundle(string filter, string sort);
        Task<PluginBundleModel> GetPluginBundle(int id);
        Task<IEnumerable<PluginBundleModel>> GetPluginBundleByName(string searchString);
        PluginBundleModel UpdatePlugin(int id, PluginBundleModel pluginBundle);
        Task DeletePlugin(int id);
        void AddPluginBundle(PluginBundleModel pluginBundle);
    }
}
