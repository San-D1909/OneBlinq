﻿using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        Task<UserModel> GetUserById(int UserId);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> UpdateFullName(string FullName, int UserId);
        Task<CompanyModel> GetCompanyById(int? companyId);
        Task<IEnumerable<UserModel>> GetUsers(string filter, string sort);
        Task<IEnumerable<UserModel>> GetUsersByPlugin(string filter, string sort, PluginModel plugin);
        Task<UserModel> CreateUser(UserModel user);
        Task<UserModel> UpdateUser(int id, UserModel user);
        Task DeleteUser(int id);
    }
}
