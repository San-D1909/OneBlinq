using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories
{
    public class PluginLicenseRepository : GenericRepository<PluginLicenseModel>, IPluginLicenseRepository
    {
        public PluginLicenseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PluginBundleModel> GetPluginBundleByStripePriceId(string stripePriceId)
        {
            return this._context.PluginBundleVariant.Where(pbv => pbv.StripePriceId == stripePriceId).Select(pbv => pbv.PluginBundle).FirstOrDefault();
        }

        public async Task<PluginBundleModel> GetPluginBundleByVariantId(int variantId)
        {
            return this._context.PluginBundleVariant.Where(pbv => pbv.Id == variantId).Select(pbv => pbv.PluginBundle).FirstOrDefault();
        }

        public async Task<PluginModel> GetPluginByStripePriceId(string stripePriceId)
        {
            return this._context.PluginVariant.Where(pbv => pbv.StripePriceId == stripePriceId).Select(pbv => pbv.Plugin).FirstOrDefault();
        }

        public async Task<PluginModel> GetPluginByVariantId(int variantId)
        {
            return this._context.PluginVariant.Where(pbv => pbv.Id == variantId).Select(pbv => pbv.Plugin).FirstOrDefault();
        }
    }
}
