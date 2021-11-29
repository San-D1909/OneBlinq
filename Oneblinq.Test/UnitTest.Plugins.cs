using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Data.Repositories;
using Backend.Models;

namespace Oneblinq.Test
{
    [TestClass]
    public class UnitTestPlugins
    {
        PluginRepository pluginRepository;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_DB")
            .Options;

            var context = new ApplicationDbContext(options);

            pluginRepository = new PluginRepository(context);
        }

        [TestMethod]
        public void Plugin_AddPlugin_TrueExistsById()
        {
            PluginModel plugin = new PluginModel()
            {
                Id = 1,
                PluginName = "Ronaldo's plugin",
                PluginDescription = "b",
                Price = 6
            };
            pluginRepository.AddPlugin(plugin);
            pluginRepository.Save();

            Assert.IsTrue(pluginRepository.GetById(1) != null);
        }

        //[TestMethod]
        //public void Plugin_GetPlugins_TrueIfPluginExists()
        //{
        //    PluginModel plugin = pluginRepository.GetPlugins("Ronaldo's", "v").Result.FirstOrDefault();

        //    Assert.IsTrue(plugin != null);
        //}

        [TestMethod]
        public async Task Plugin_GetPluginsByNameAsync_TrueIfNameIsCorrect()
        {
            PluginModel plugin = pluginRepository.GetById(1);
            string searchString = plugin.PluginName;
            var plugins = await pluginRepository.GetPluginsByNameAsync(searchString);

            Assert.IsTrue(plugins.First() != null);
        }
    }
}
