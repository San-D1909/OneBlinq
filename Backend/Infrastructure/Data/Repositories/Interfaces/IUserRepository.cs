using Backend.Models;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int UserId);
        Task<User> UpdateFullName(string FullName, int UserId);
    }
}
