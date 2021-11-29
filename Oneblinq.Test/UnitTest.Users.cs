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
    public class UnitTestUsers
    {
        UserRepository userRepository;
        CompanyRepository companyRepository;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_DB")
            .Options;

            var context = new ApplicationDbContext(options);

            userRepository = new UserRepository(context);
            companyRepository = new CompanyRepository(context);
        }

        [TestMethod]
        public void User_Add_TrueExistsById()
        {
            CompanyModel company = companyRepository.GetById(1);

            //UserModel user = new UserModel()
            //{
            //    Id = 1,
            //    FullName = "Ronaldo",
            //    Password = "b",
            //    Email = "t@t.nl",
            //    IsAdmin = false,
            //    Company = company
            //};
            //userRepository.Add(user);
            //userRepository.Save();

            Assert.IsTrue(userRepository.GetById(1) != null);
        }

        [TestMethod]
        public async Task User_UpdateFullName_TrueIsNewFullName()
        {
            const string newName = "Janneman";

            await userRepository.UpdateFullName(newName, 1);
            await userRepository.SaveAsync();

            UserModel newNameUser = await userRepository.GetByIdAsync(1);

            Assert.IsTrue(newNameUser.FullName == newName);
        }

        [TestMethod]
        public async Task User_GetByCompanyId_TrueExistsById()
        {
            CompanyModel company = companyRepository.GetById(1);

            CompanyModel checkCompany = await userRepository.GetCompanyById(1);

            Assert.IsTrue(company == checkCompany);
        }

        [TestMethod]
        public async Task User_GetUserByEmail_TrueEmailExists()
        {
            UserModel user = await userRepository.GetUserByEmail("t@t.nl");

            Assert.IsTrue(user != null);
        }

    }
}
