using Backend.Infrastructure.Data;
using Backend.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using Backend.Models;

namespace Oneblinq.Test
{
    [TestClass]
    public class UnitTestUsers
    {
        UserRepository userRepository;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_DB")
            .Options;

            var context = new ApplicationDbContext(options);

            userRepository = new UserRepository(context);
        }

        [TestMethod]
        public void User_Add_TrueExistsById()
        {
            UserModel user = new UserModel()
            {
                Id = 1,
                FullName = "Ronaldo",
                Password = "b",
                Email = "t@t.nl",
                IsAdmin = false,
            };
            userRepository.Add(user);
            userRepository.Save();

            Assert.IsTrue(userRepository.GetById(1) != null);
        }

        [TestMethod]
        public async Task User_UpdateFullName_TrueIsNewFullName()
        {
            const string newName = "Janneman";

            await userRepository.UpdateFullName(newName, 1);
            await userRepository.SaveAsync();

            Assert.IsTrue(userRepository.GetById(1).FullName == newName);
        }

        [TestMethod]
        public void User_()
        {

        }

    }
}
