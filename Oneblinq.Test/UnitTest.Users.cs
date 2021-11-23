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
        UserRepository userrepository;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_DB")
            .Options;

            var context = new ApplicationDbContext(options);

            userrepository = new UserRepository(context);
        }

        [TestMethod]
        public async Task User_UpdateFullName_TrueIsNewNameAsync()
        {
            User user = new User()
            {
                UserId = 1,
                FullName = "Ronaldo",
                Password = "b",
                Email = "t@t.nl",
                IsAdmin = false,
            };
            userrepository.
        }
    }
}
