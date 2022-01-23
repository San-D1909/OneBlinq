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
        public static PluginVariantModel PLUGIN_VARIANT_FORMS1;
        public static PluginVariantModel PLUGIN_VARIANT_FORMS2;

        public static void SeedData(ApplicationDbContext context)
        {
            if (context.PluginVariant.ToList().Count == 0)
            {
                PLUGIN_VARIANT_FORMS1 = new PluginVariantModel
                {
                    Id = 1,
                    StripePriceId = "price_1KL7GWLEIzrCONviC522d94Q",
                    Description = "Variant",
                    IsSubscription = false,
                    MaxActivations = 10,
                    Plugin = PluginSeeder.PLUGIN_FORMS,
                    PluginId = PluginSeeder.PLUGIN_FORMS.Id,
                    Price = 10
                };                
                PLUGIN_VARIANT_FORMS2 = new PluginVariantModel
                {
                    Id = 2,
                    StripePriceId = "price_1KL7GWLEIzrCONviC522d94Q",
                    Description = "Variant",
                    IsSubscription = false,
                    MaxActivations = 100,
                    Plugin = PluginSeeder.PLUGIN_LINEHEIGHT,
                    PluginId = PluginSeeder.PLUGIN_LINEHEIGHT.Id,
                    Price = 10
                };

                context.Add(PLUGIN_VARIANT_FORMS1);
                context.Add(PLUGIN_VARIANT_FORMS2);
                context.SaveChanges();
            }
        }
    }
}
