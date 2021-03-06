using Backend.Core.Logic;
using Backend.Infrastructure.Data;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class PluginBundleVariantSeeder
    {
        public static void SeedData(ApplicationDbContext context, IConfiguration configuration)
        {
            if (context.PluginBundleVariant.ToList().Count == 0)
            {
                StripeConfiguration.ApiKey = configuration.GetValue<string>("STRIPE_SECRET_KEY");

                var service = new PriceService();
                StripeList<Stripe.Price> pricelist = service.List();

                foreach (var price in pricelist.Data)
                {
                    if (price.Deleted == null && price.Active == true)
                    {
                        if (!price.Metadata.ContainsKey("PluginBundleId") || !price.Metadata.ContainsKey("MaxActivations") || !price.Metadata.ContainsKey("Description") || !price.Metadata.ContainsKey("IsSubscription"))
                        {
                            continue;
                        }
                        var pluginVariant = new PluginBundleVariantModel()
                        {
                            PluginBundleId = Convert.ToInt32(price.Metadata["PluginBundleId"]),
                            MaxActivations = Convert.ToInt32(price.Metadata["MaxActivations"]),
                            Description = price.Metadata["Description"],
                            IsSubscription = Convert.ToBoolean(price.Metadata["IsSubscription"]),
                            StripePriceId = price.Id.ToString(),
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
