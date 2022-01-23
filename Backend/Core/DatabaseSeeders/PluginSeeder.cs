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
                    PluginDescription = "<div class=\"ql - editor\" data-gramm=\"false\" contenteditable=\"false\"><p>Forms is the plugin to get your form design game on point. Enjoy custom forms integrated with <strong>your design library</strong>. Pick from your own font styles, spacings and colors. Every form element will <strong>automatically</strong> generate variants for error, disabled, focus and hover <strong>states</strong> so you don’t have to! 😉 🎉</p><p><br></p><p><strong>Key features:</strong></p><p>— Custom forms</p><p>— All elements are generated with the elements primary states</p><p>— Apply your library styles or pick new ones</p><p>— Different component page so your workspace doesn't get cluttered</p><p>— Use inputs, textareas, checkboxes, buttons and more to come</p><p>— Free trial</p><p><br></p><p><strong>Customer support:</strong></p><p><a href=\"mailto:support@oneblinq.com\" rel=\"noreferrer noopener nofollow\" target=\"_blank\">support@oneblinq.com</a></p><p><br></p><p><strong>Buy your license:</strong></p><p><a href=\"https://gumroad.com/l/obforms\" rel=\"noreferrer noopener nofollow\" target=\"_blank\">https://gumroad.com/l/obforms</a></p></div>",
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
