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

        public async Task<UserModel> UpdateFullName(string FullName, int userId)
        {
            _context.User.Where(x => x.Id == userId).FirstOrDefault().FullName = FullName;
            UserModel userModel = await _context.User.Where(x => x.Id == userId).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();
            return userModel;
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
/*            try
            {
                var item = await _context.Set<UserModel>()
                    .Where(x => x.Id == UserId)
                    .FirstOrDefaultAsync();

                if (item == null)
                {
                    throw new Exception($"Couldn't find entity with id={UserId}");
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity with id={UserId}: {ex.Message}");
            }*/
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
                    throw new Exception($"Couldn't find entity with email={Email}");
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
    }
}
