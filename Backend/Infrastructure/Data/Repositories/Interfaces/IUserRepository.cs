using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        Task<UserModel> GetUserById(int UserId);
        Task<CompanyModel> UpdateCompany(CompanyModel company);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> UpdateUser(UserModel user);
        Task<CompanyModel> GetCompanyById(int? companyId);
        Task<IEnumerable<UserModel>> GetUsersByPlugin(string filter, string sort, PluginModel plugin);
        Task<IEnumerable<UserModel>> GetUsersByPluginBundle(string filter, string sort, PluginBundleModel pluginBundle);
    }
}
