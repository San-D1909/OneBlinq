using Backend.Core.Logic;
using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class PluginVariantSeeder
    {
        public static PluginVariantModel PLUGIN_VARIANT_FORMS;

        public static void SeedData(ApplicationDbContext context)
        {
            if (context.PluginVariant.ToList().Count == 0)
            {
                PLUGIN_VARIANT_FORMS = new PluginVariantModel
                {
                    Id = 1,
                    StripePriceId = "adf",
                    Description = "Variant",
                    IsSubscription = true,
                    MaxActivations = 10,
                    Plugin = PluginSeeder.PLUGIN_FORMS,
                    PluginId = 1,
                    Price = 69
                };

                context.Add(PLUGIN_VARIANT_FORMS);
                context.SaveChanges();
            }
        }
    }
}
