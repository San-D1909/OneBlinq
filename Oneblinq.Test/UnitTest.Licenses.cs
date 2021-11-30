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
    public class UnitTestLicenses
    {
        LicenceRepository licenseRepository;
        UserRepository userRepository;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_DB")
            .Options;

            var context = new ApplicationDbContext(options);

            licenseRepository = new LicenceRepository(context);
            userRepository = new UserRepository(context);

            UserModel user = new UserModel()
            {
                Id = 1,
                FullName = "Gary",
                Password = "test",
                Email = "t@t.nl"
            };
            userRepository.Add(user);
            userRepository.Save();
        }

        [TestMethod]
        public async Task License_GetLicencesDb_TrueIsUserLicense()
        {
            UserModel user = await userRepository.GetByIdAsync(1);

            LicenseModel license = new LicenseModel()
            {
                Id = 1,
                LicenseKey = "abc",
                User = user
            };

            licenseRepository.Add(license);

            var resultLicense = licenseRepository.GetLicencesDb(1);

            Assert.IsTrue(resultLicense != null);
        }
    }
}
