using Backend.Models;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserById(int UserId);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> UpdateFullName(string FullName, int UserId);
        Task<CompanyModel> GetCompanyById(int? companyId);
    }
}
