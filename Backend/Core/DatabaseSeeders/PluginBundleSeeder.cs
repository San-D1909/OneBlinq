using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class PluginBundleSeeder
    {
        public static PluginBundleModel BundleModel;
        public static void SeedData(ApplicationDbContext context)
        {
            if (context.PluginBundle.ToList().Count == 0)
            {
                BundleModel = new PluginBundleModel
                {
                    Id = 1,
                    BundleName = "All in one deal",
                    BundleDescription = "This is a one in a life time deal!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
                    Price = 4,
                };


                context.Add(BundleModel);
                context.SaveChanges();
            }
        }
    }
}
