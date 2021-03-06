using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginBundleRepository
    {
        Task<IEnumerable<PluginBundleModel>> GetAllPluginBundle(string filter = null, string sort = null);
        Task<PluginBundleModel> GetPluginBundle(int id);
        Task<IEnumerable<PluginBundleModel>> GetPluginBundleByName(string searchString);
        void AddPluginBundle(PluginBundleModel pluginBundle);
    }
}
