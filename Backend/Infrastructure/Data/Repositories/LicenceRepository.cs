using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories
{
    public class LicenceRepository : GenericRepository<LicenseModel>, ILicenceRepository
    {
        public LicenceRepository(ApplicationDbContext context) : base(context) { }

        public Task<LicenseModel> ActivateLicense(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LicenseModel> DeactivateLicense(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LicenseModel>> GetLicencesDb(int userID)

        {
            bool admin = _context.User.Select(s => s.Id == userID).FirstOrDefault();
            List<LicenseModel> results = new();
            if (admin == true)
            {
                results = _context.License.Where(s => s.IsActive == true).ToList();
            }
            else
            {
                results = _context.License.Where(s => s.IsActive == true && s.User.Id == userID).ToList();
            }
            return Task.FromResult(results);
        }

        public Task<IEnumerable<LicenseModel>> GetLicensesForPlugin(string filter, string sort, PluginModel user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LicenseModel>> GetLicensesForUser(string filter, string sort, UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LicenseModel>> GetLicensesForUserAndPlugin(string filter, string sort, PluginModel plugin, UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
