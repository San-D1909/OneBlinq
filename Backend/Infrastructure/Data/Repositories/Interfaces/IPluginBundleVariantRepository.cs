using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginBundleVariantRepository : IGenericRepository<PluginBundleVariantModel>
    {
        Task<IEnumerable<PluginBundleVariantModel>> PluginBundleVariantsByFilter(string filter, string sort);
        Task<PluginBundleVariantModel> GetPluginBundleVariantByPriceId(string stripePriceId);
    }
}
