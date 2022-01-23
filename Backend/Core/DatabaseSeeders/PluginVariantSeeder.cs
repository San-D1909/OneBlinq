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
    public class PluginVariantSeeder
    {
        public static void SeedData(ApplicationDbContext context, IConfiguration configuration)
        {
            if (context.PluginVariant.ToList().Count == 0)
            {
                StripeConfiguration.ApiKey = configuration.GetValue<string>("STRIPE_SECRET_KEY");

                var service = new PriceService();
                StripeList<Stripe.Price> pricelist = service.List();
                
                foreach(var price in pricelist.Data)
                {
                    if (price.Deleted == null)
                    {
                        if (!price.Metadata.ContainsKey("PluginId") || !price.Metadata.ContainsKey("MaxActivations") || !price.Metadata.ContainsKey("Description") || !price.Metadata.ContainsKey("IsSubscription"))
                        {
                            continue;
                        }
                        var pluginVariant = new PluginVariantModel()
                        {
                            PluginId = Convert.ToInt32(price.Metadata["PluginId"]),
                            MaxActivations = Convert.ToInt32(price.Metadata["MaxActivations"]),
                            Description = price.Metadata["Description"],
                            IsSubscription = Convert.ToBoolean(price.Metadata["IsSubscription"]),
                            StripePriceId = price.ProductId.ToString(),
                            Price = (price.UnitAmountDecimal == null ? 0 : ((decimal)price.UnitAmountDecimal / 100))
                        };

                        context.Add(pluginVariant);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
