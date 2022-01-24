using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IPluginLicenseRepository
    {
        public void Add(PluginLicenseModel entity);
        public Task<PluginModel> GetPluginByStripePriceId(string stripePriceId);
        public Task<PluginBundleModel> GetPluginBundleByStripePriceId(string stripePriceId);
        public Task<int> SaveAsync();
    }
}
