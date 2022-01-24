using Backend.Infrastructure.Data;
using Backend.Infrastructure.Data.Repositories;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oneblinq.Test
{
    [TestClass]
    public class UnitTest
    {
        PluginVariantRepository pluginVariantRepository;
        PluginRepository pluginRepository;
        ApplicationDbContext context;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new ApplicationDbContext(options);

            pluginRepository = new PluginRepository(context);
            pluginVariantRepository = new PluginVariantRepository(context);

            PluginModel plugin = new PluginModel()
            {
                Id = 1,
                PluginName = "Naam",
                PluginDescription = "Description",
            };

            PluginVariantModel pluginVariant = new PluginVariantModel()
            {
                Id = 1,
                PluginId = 1,
                Plugin = plugin,
                Description = "Description",
                Price = 2,
                MaxActivations = 5,
                IsSubscription = true
            };

            pluginRepository.Add(plugin);
            pluginVariantRepository.Add(pluginVariant);
            pluginRepository.Save();

        }

        [TestMethod]
        public async Task PluginVariant_PluginBundleVariantsByFilter_TrueIfNameExistsAsync()
        {
            List<PluginVariantModel> pluginVariants = (await pluginVariantRepository.PluginVariantsByFilter("{\"Description\":\"Description\"}", null)).ToList();

            var pluginSingle = pluginVariants.FirstOrDefault();

            Assert.AreEqual("Description", pluginSingle.Description);
        }
    }
}
