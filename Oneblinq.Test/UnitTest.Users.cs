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
    public class UnitTestUsers
    {
        IUserRepository userRepository;
        CompanyRepository companyRepository;
        ApplicationDbContext context;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new ApplicationDbContext(options);

            userRepository = new UserRepository(context);
            companyRepository = new CompanyRepository(context);
        }

        [TestMethod]
        public async Task User_GetUsersByPlugin_TrueIfTwoOfTheThreeUsersAreGiven()
        {
            PluginModel plugin1 = new PluginModel
            {
                Id = 1,
                PluginName = "TestPlugin 1",
                PluginDescription = "Just testing a plugin.",
                Price = 8
            };
            this.context.Plugin.Add(plugin1);
            PluginModel plugin2 = new PluginModel
            {
                Id = 2,
                PluginName = "TestPlugin 2",
                PluginDescription = "Just testing a plugin.",
                Price = 13
            };
            this.context.Plugin.Add(plugin2);
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            UserModel user1 = new UserModel
            {
                Id = 1,
                Email = "test1@gmail.com",
                Password = "Test128983",
                Salt = salt,
                FullName = "Test user",
                IsAdmin = true,
                IsVerified = true,
                Company = null
            };

            UserModel user2 = new UserModel
            {
                Id = 2,
                Email = "test2@gmail.com",
                Password = "Test128983",
                Salt = salt,
                FullName = "Test user",
                IsAdmin = true,
                IsVerified = true,
                Company = null
            };

            UserModel user3 = new UserModel
            {
                Id = 3,
                Email = "test3@gmail.com",
                Password = "Test128983",
                Salt = salt,
                FullName = "Test user",
                IsAdmin = true,
                IsVerified = true,
                Company = null
            };

            this.context.User.Add(user1);
            this.context.User.Add(user2);
            this.context.User.Add(user3);

            LicenseTypeModel type = new LicenseTypeModel
            {
                Id = 1,
                CreationTime = DateTime.Now,
                MaxAmount = 5,
                TypeName = "ShortLicenseType"
            };

            this.context.LicenseType.Add(type);
            this.context.SaveChanges();

            LicenseModel license1 = new LicenseModel
            {
                Id = 1,
                CreationTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddYears(1),
                IsActive = true,
                LicenseKey = "ERFQE-RGEQF-EFWAQF-EFWEFEW",
                TimesActivated = 3,
                LicenseType = type,
                User = user1
            };

            LicenseModel license2 = new LicenseModel
            {
                Id = 2,
                CreationTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddYears(1),
                IsActive = true,
                LicenseKey = "ERFQE-RGEQF-EFWAQF-FSDJAOF",
                TimesActivated = 4,
                LicenseType = type,
                User = user2
            };

            LicenseModel license3 = new LicenseModel
            {
                Id = 3,
                CreationTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddYears(1),
                IsActive = true,
                LicenseKey = "ERFQE-RGEQF-EFWAQF-SFDEHJ",
                TimesActivated = 2,
                LicenseType = type,
                User = user3
            };

            context.License.Add(license1);
            context.License.Add(license2);
            context.License.Add(license3);

            context.SaveChanges();

            this.context.PluginLicense.AddRange(
                new PluginLicenseModel
                {
                    Id = 1,
                    License = license1,
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
                    License = license2,
                    LicenseId = 1,
                    Plugin = plugin1,
                    PluginId = 1,
                    PluginBundle = null,
                    PluginBundleId = null,
                    TimesActivated = 3
                },
                new PluginLicenseModel
                {
                    Id = 3,
                    License = license3,
                    LicenseId = 3,
                    Plugin = plugin2,
                    PluginId = 2,
                    PluginBundle = null,
                    PluginBundleId = null,
                    TimesActivated = 4
                }
            );

            this.context.SaveChanges();

            IEnumerable<UserModel> users = await userRepository.GetUsersByPlugin(null, null, plugin1);
            Assert.IsTrue(users.Count() == 2);
        }

        [TestMethod]
        public void User_Add_TrueExistsById()
        {
            CompanyModel company = new CompanyModel
            {
                Id = 1,
                BTWNumber = "892389432",
                CompanyName = "TestCompany",
                Country = "Netherlands",
                HouseNumber = "23",
                KVKNumber = "324324342",
            };

            context.Company.Add(company);
            context.SaveChanges();

            UserModel user = new UserModel()
            {
                Id = 1,
                FullName = "Ronaldo",
                Password = "b",
                Email = "t@t.nl",
                IsAdmin = false,
                Company = company
            };
            userRepository.Add(user);
            context.SaveChanges();

            Assert.IsTrue(userRepository.GetById(1) != null);
        }

        [TestMethod]
        public async Task User_UpdateFullName_TrueIsNewFullName()
        {
            CompanyModel company = new CompanyModel
            {
                Id = 1,
                BTWNumber = "892389432",
                CompanyName = "TestCompany",
                Country = "Netherlands",
                HouseNumber = "23",
                KVKNumber = "324324342",
            };

            context.Company.Add(company);
            context.SaveChanges();

            UserModel userdb = new UserModel()
            {
                Id = 1,
                FullName = "Ronaldo",
                Password = "b",
                Email = "t@t.nl",
                IsAdmin = false,
                Company = company
            };
            userRepository.Add(userdb);
            context.SaveChanges();

            const string newName = "Janneman";

            await userRepository.UpdateFullName(newName, 1);
            await userRepository.SaveAsync();

            UserModel newNameUser = await userRepository.GetByIdAsync(1);

            Assert.IsTrue(newNameUser.FullName == newName);
        }

        [TestMethod]
        public async Task User_GetByCompanyId_TrueExistsById()
        {
            CompanyModel company = new CompanyModel
            {
                Id = 1,
                BTWNumber = "892389432",
                CompanyName = "TestCompany",
                Country = "Netherlands",
                HouseNumber = "23",
                KVKNumber = "324324342",
            };

            context.Company.Add(company);
            context.SaveChanges();

            CompanyModel checkCompany = await userRepository.GetCompanyById(1);

            Assert.IsTrue(company == checkCompany);
        }

        [TestMethod]
        public async Task User_GetUserByEmail_TrueEmailExists()
        {
            CompanyModel company = new CompanyModel
            {
                Id = 1,
                BTWNumber = "892389432",
                CompanyName = "TestCompany",
                Country = "Netherlands",
                HouseNumber = "23",
                KVKNumber = "324324342",
            };

            context.Company.Add(company);
            context.SaveChanges();

            UserModel userdb = new UserModel()
            {
                Id = 1,
                FullName = "Ronaldo",
                Password = "b",
                Email = "t@t.nl",
                IsAdmin = false,
                Company = company
            };
            userRepository.Add(userdb);
            context.SaveChanges();

            UserModel user = await userRepository.GetUserByEmail("t@t.nl");

            Assert.IsTrue(user != null);
        }

    }
}
