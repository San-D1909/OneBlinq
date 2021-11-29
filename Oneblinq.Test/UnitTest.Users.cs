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
    public class UnitTest1
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
        public void User_UpdateFullName_TrueExistsById()
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

            Assert.IsTrue(userRepository.GetById(1) != null);
        }
    }
}
