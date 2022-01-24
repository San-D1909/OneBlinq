using Backend.Infrastructure.Data;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class PluginSeeder
    {
        public static PluginModel PLUGIN_FORMS;
        public static PluginModel PLUGIN_LINEHEIGHT;

        public static void SeedData(ApplicationDbContext context, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("STRIPE_SECRET_KEY");

            if (context.Plugin.ToList().Count == 0)
            {
                var service = new ProductService();

                PLUGIN_FORMS = new PluginModel
                {
                    Id = 1,
                    PluginName = "Forms",
                    PluginDescription = "Forms is the plugin to get your form design game on point. Enjoy custom forms integrated with your design library. Pick from your own font styles, spacings and colors. Every form element will automatically generate variants for error, disabled, focus and hover states so you don’t have to! 😉 🎉",
                };
                var optionForms = new ProductCreateOptions
                {
                    Name = PLUGIN_FORMS.PluginName,
                    Description = PLUGIN_FORMS.PluginDescription,
                    TaxCode = "txcd_10000000",
                    // TODO: Add image field
                };
                
                var productFormId = service.Create(optionForms);
                PLUGIN_FORMS.StripeProductId = productFormId.Id;

                PLUGIN_LINEHEIGHT = new PluginModel
                {
                    Id = 2,
                    PluginName = "Lineheight",
                    PluginDescription = "Test",
                };
                var optionLineHeight = new ProductCreateOptions
                {
                    Name = PLUGIN_LINEHEIGHT.PluginName,
                    Description = PLUGIN_LINEHEIGHT.PluginDescription,
                    TaxCode = "txcd_10000000",
                    // TODO: Add image field
                };

                var productLineHeightId = service.Create(optionLineHeight);
                PLUGIN_LINEHEIGHT.StripeProductId = productLineHeightId.Id;

                context.Add(PLUGIN_FORMS);
                context.Add(PLUGIN_LINEHEIGHT);
                context.SaveChanges();
            }
        }
    }
}
