using Backend.Core.Logic;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserModel> UpdateUser(UserModel user)
        {
            UserModel userModel = await _context.User.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            _context.User.Update(userModel).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            userModel = await _context.User.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            return userModel;
        }
        public async Task<CompanyModel> UpdateCompany(CompanyModel company)
        {
            CompanyModel companyModel = await _context.Company.Where(x => x.Id == company.Id).FirstOrDefaultAsync();
            _context.Company.Update(companyModel).CurrentValues.SetValues(company);
            await _context.SaveChangesAsync();
            companyModel = await _context.Company.Where(x => x.Id == company.Id).FirstOrDefaultAsync();
            return companyModel;
        }
        public async Task<CompanyModel> GetCompanyById(int? companyId)
        {
            CompanyModel companyModel = await _context.Company.Where(x => x.Id == companyId).FirstOrDefaultAsync();
            return companyModel;
        }
            public async Task<UserModel> GetUserById(int UserId)
        {
          UserModel user = await _context.User.Include(c =>c.Company).Where(user => user.Id == UserId).FirstOrDefaultAsync();
            return user;
        }
        public async Task<UserModel> GetUserByEmail(string Email)
        {
            try
            {
                var item = await _context.Set<UserModel>()
                    .Where(x => x.Email == Email)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (item == null)
                {
                    return null;
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity with email={Email}: {ex.Message}");
            }
        }

        public Task<IEnumerable<UserModel>> GetUsers(string filter, string sort)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> GetUsersByPlugin(string filter, string sort, PluginModel plugin)
        {
            IEnumerable<PluginLicenseModel> plugins = _context.PluginLicense.Include(p => p.Plugin).Include(l => l.License).ThenInclude(u => u.User).Where(p => p.Plugin.Id == plugin.Id);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            plugins = filterLogic.FilterDatabaseModel<PluginLicenseModel>(plugins, filter);

            return plugins.Select(p => p.License.User).ToList();
        }

        public Task<UserModel> CreateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> UpdateUser(int id, UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> GetUsersByPluginBundle(string filter, string sort, PluginBundleModel pluginBundle)
        {
            IEnumerable<PluginLicenseModel> pluginbundles = _context.PluginLicense.Include(p => p.PluginBundle).Include(l => l.License).ThenInclude(u => u.User).Where(p => p.PluginBundle.Id == pluginBundle.Id);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            pluginbundles = filterLogic.FilterDatabaseModel<PluginLicenseModel>(pluginbundles, filter);

            return pluginbundles.Select(p => p.License.User).ToList();
        }
    }
}
