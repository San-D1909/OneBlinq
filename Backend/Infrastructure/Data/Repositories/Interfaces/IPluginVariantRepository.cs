using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginVariantRepository: IGenericRepository<PluginVariantModel>
    {
        Task<IEnumerable<PluginVariantModel>> PluginVariantsByFilter(string filter, string sort);
    }
}
