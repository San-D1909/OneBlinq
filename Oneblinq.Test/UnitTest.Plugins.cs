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
using Backend.Infrastructure.Data.Repositories.Interfaces;
using System.Security.Cryptography;

namespace Oneblinq.Test
{
    [TestClass]
    public class UnitTestPlugins
    {
        IPluginRepository pluginRepository;
        IPluginBundleRepository pluginBundleRepository;
        ApplicationDbContext context;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new ApplicationDbContext(options);

            pluginRepository = new PluginRepository(context);
            pluginBundleRepository = new PluginBundleRepository(context);
        }

        [TestMethod]
        public async Task Plugin_AddPlugin_TrueExistsById()
        {
            PluginModel plugin = new PluginModel()
            {
                Id = 1,
                PluginName = "Ronaldo's plugin",
                PluginDescription = "b",
            };
            pluginRepository.AddPlugin(plugin);
            await pluginRepository.SaveAsync();

            Assert.IsTrue(pluginRepository.GetById(1) != null);
        }


        [TestMethod]
        public async Task Plugin_GetPluginsByUser_TrueIfTwoOfTheThreePluginsAreGivenAsync()
        {
            PluginModel plugin1 = new PluginModel
            {
                Id = 1,
                PluginName = "TestPlugin 1",
                PluginDescription = "Just testing a plugin.",
            };
            this.context.Plugin.Add(plugin1);
            PluginModel plugin2 = new PluginModel
            {
                Id = 2,
                PluginName = "TestPlugin 2",
                PluginDescription = "Just testing a plugin.",
            };
            this.context.Plugin.Add(plugin1);
            PluginModel plugin3 = new PluginModel
            {
                Id = 3,
                PluginName = "TestPlugin 3",
                PluginDescription = "Just testing a plugin.",
            };
            this.context.Plugin.Add(plugin1);
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            UserModel user = new UserModel
            {
                Id = 1,
                Email = "test@gmail.com",
                Password = "Test128983",
                Salt = salt,
                FullName = "Test user",
                IsAdmin = true,
                IsVerified = true,
                Company = null
            };

            this.context.User.Add(user);

            this.context.SaveChanges();

            LicenseModel license = new LicenseModel
            {
                Id = 1,
                CreationTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddYears(1),
                IsActive = true,
                LicenseKey = "ERFQE-RGEQF-EFWAQF-EFWEFEW",
                TimesActivated = 3,
                User = user
            };

            context.License.Add(license);
            context.SaveChanges();

            this.context.PluginLicense.AddRange(
                new PluginLicenseModel
                {
                    Id = 1,
                    License = license,
                    LicenseId = 1,
                    Plugin = plugin1,
                    PluginId = 1,
                    PluginBundle = null,
                    PluginBundleId = null,
                    TimesActivated = 2
                },
                new PluginLicenseModel
                {
                    Id = 2,
                    License = license,
                    LicenseId = 1,
                    Plugin = plugin2,
                    PluginId = 2,
                    PluginBundle = null,
                    PluginBundleId = null,
                    TimesActivated = 2
                }
            );

            this.context.SaveChanges();

            IEnumerable<PluginModel> plugins = (IEnumerable<PluginModel>)await pluginRepository.GetPluginsByUser(null, null, user);
            Assert.IsTrue(plugins.Count() == 2);
        }

        [TestMethod]
        public async Task Plugin_GetPluginsByNameAsync_TrueIfNameIsCorrect()
        {
            PluginModel plugin = new PluginModel()
            {
                Id = 1,
                PluginName = "Ronaldo's plugin",
                PluginDescription = "b",
            };
            pluginRepository.AddPlugin(plugin);
            await pluginRepository.SaveAsync();
            string searchString = plugin.PluginName;
            var plugins = await pluginRepository.GetPluginsByNameAsync(searchString);

            Assert.IsTrue(plugins.First() != null);
        }
        [TestMethod]
        public async Task PluginBundle_GetAllPluginBundles_TrueIf2IsReturned()
        {
            PluginBundleModel pluginBundleModel1 = new PluginBundleModel()
            {
                Id= 1,
                BundleName = "bundle1",
                BundleDescription = "b",
                StripeProductId = "blabla",
            };
            PluginBundleModel pluginBundleModel2 = new PluginBundleModel()
            {
                Id= 2,
                BundleName = "bundle2",
                BundleDescription = "b",
                StripeProductId = "blabla",
            };
            context.PluginBundle.Add(pluginBundleModel1);
            context.PluginBundle.Add(pluginBundleModel2);
            await context.SaveChangesAsync();

            var bundlesAll = await pluginBundleRepository.GetAllPluginBundle();

            Assert.IsTrue(bundlesAll.Count() == 2);
        }
    }
}
