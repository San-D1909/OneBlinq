using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class PluginBundlesSeeder
    {
        public static PluginBundlesModel BundleModel1;
        public static PluginBundlesModel BundleModel2;
        public static void SeedData(ApplicationDbContext context)
        {
            if (context.PluginBundles.ToList().Count == 0)
            {
                BundleModel1 = new PluginBundlesModel
                {
                    Id = 1,
                    PluginBundleId = PluginBundleSeeder.BundleModel.Id,
                    PluginId = PluginSeeder.PLUGIN_FORMS.Id,
                };
                BundleModel2 = new PluginBundlesModel
                {
                    Id = 2,
                    PluginBundleId = PluginBundleSeeder.BundleModel.Id,
                    PluginId = PluginSeeder.PLUGIN_LINEHEIGHT.Id,
                };

                context.Add(BundleModel1);
                context.Add(BundleModel2);
                context.SaveChanges();
            }
        }
    }
}
