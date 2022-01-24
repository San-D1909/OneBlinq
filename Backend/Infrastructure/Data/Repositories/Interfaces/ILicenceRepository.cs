using Backend.DTO.Out;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface ILicenceRepository : IGenericRepository<LicenseModel>
    {
        public Task<List<LicenseModel>> GetLicencesDb(int userID);
        public Task<LicenseModel> GetLicenseByIntent(string intentId);
        public Task<IEnumerable<LicenseModel>> GetLicensesForUser(string filter, string sort, UserModel user);
        public Task<IEnumerable<LicenseModel>> GetLicenses(string filter, string sort);
        public Task<IEnumerable<LicenseOutput>> GetLicenseOutputs(string filter, string sort, UserModel user);
        public Task<IEnumerable<LicenseModel>> GetLicensesForPlugin(string filter, string sort, PluginModel plugin);
        public Task<IEnumerable<LicenseModel>> GetLicensesForUserAndPlugin(string filter, string sort, PluginModel plugin, UserModel user);
    }
}
