using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class PluginLicenseSeeder
    {
        public static PluginLicenseModel PLUGINLICENSE_PREMIUM_USER;

        public static void SeedData(ApplicationDbContext context)
        {
            if (context.PluginLicense.ToList().Count == 0)
            {
                PLUGINLICENSE_PREMIUM_USER = new PluginLicenseModel
                {
                    Id = 1,
                    License = LicenseSeeder.LICENSE_PREMIUM_USER,
                    LicenseId = LicenseSeeder.LICENSE_PREMIUM_USER.Id,
                    Plugin = PluginSeeder.PLUGIN_FORMS,
                    PluginId = PluginSeeder.PLUGIN_FORMS.Id,
                    TimesActivated = 1
                };

                context.Add(PLUGINLICENSE_PREMIUM_USER);
                context.SaveChanges();
            }
        }
    }
}
