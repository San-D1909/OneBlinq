using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class PluginImageSeeder
    {
        public static PluginImageModel PluginImageModel;
        public static void SeedData(ApplicationDbContext context)
        {
            if (context.PluginImage.ToList().Count == 0)
            {
                Byte[] bytes = System.IO.File.ReadAllBytes("./DefaultImages/Seeder/FormsThumbnail.png");
                String file = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);

                PluginImageModel = new PluginImageModel
                {
                    Id = 1,
                    ImageData = file,
                    Plugin = PluginSeeder.PLUGIN_FORMS
                };

                context.Add(PluginImageModel);
                context.SaveChanges();
            }
        }
    }
}
