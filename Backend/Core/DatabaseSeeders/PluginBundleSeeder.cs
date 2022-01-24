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
    public class PluginBundleSeeder
    {
        public static PluginBundleModel BundleModel;
        public static void SeedData(ApplicationDbContext context, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("STRIPE_SECRET_KEY");

            if (context.PluginBundle.ToList().Count == 0)
            {
                var service = new ProductService();

                BundleModel = new PluginBundleModel
                {
                    Id = 1,
                    BundleName = "All in one deal",
                    BundleDescription = "This is a one in a life time deal!!!"
                };
                var optionBundles = new ProductCreateOptions
                {
                    Name = BundleModel.BundleName,
                    Description = BundleModel.BundleDescription,
                    TaxCode = "txcd_10000000",
                    // TODO: Add image field
                };

                var productBundleId = service.Create(optionBundles);

                BundleModel.StripeProductId = productBundleId.Id;

                context.Add(BundleModel);
                context.SaveChanges();
            }
        }
    }
}
