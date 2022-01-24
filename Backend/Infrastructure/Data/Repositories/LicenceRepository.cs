using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Logic;
using Backend.DTO.Out;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories
{
    public class LicenceRepository : GenericRepository<LicenseModel>, ILicenceRepository
    {
        public LicenceRepository(ApplicationDbContext context) : base(context) { }

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

        public async Task<IEnumerable<LicenseOutput>> GetLicenseOutputs(string filter, string sort, UserModel user)
        {
            IEnumerable<PluginLicenseModel> pluginLicenses = _context.PluginLicense.Include(pl => pl.Plugin).Include(pl => pl.License).ThenInclude(l => l.User).Where(l => l.License.User == user);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            pluginLicenses = filterLogic.FilterDatabaseModel<PluginLicenseModel>(pluginLicenses, filter).ToList();


            List<LicenseOutput> licenseOutputs = new List<LicenseOutput>();

            foreach (PluginLicenseModel pluginLicense in pluginLicenses)
            {
                //license.LicenseType = await _context.LicenseType.Where(lt => lt.Id == license.LicenseTypeId).FirstOrDefaultAsync();
                IEnumerable<DeviceModel> devices = await _context.Device.Where(d => d.LicenseId == pluginLicense.License.Id).ToListAsync();
                //PluginLicenseModel pluginLicense = await _context.PluginLicense.Where(pl => pl.LicenseId == license.Id).FirstOrDefaultAsync();
                //pluginLicense.Plugin = await _context.Plugin.Where(p => p.Id == pluginLicense.PluginId).FirstOrDefaultAsync();

                licenseOutputs.Add(new LicenseOutput
                {
                    License = pluginLicense.License,
                    Id = pluginLicense.License.Id,
                    TimesActivated = pluginLicense.TimesActivated,
                    Variant = pluginLicense.License.Variant,
                    Plugin = pluginLicense.Plugin,
                    PluginBundle = pluginLicense.PluginBundle,
                    PluginBundleId = pluginLicense.PluginBundleId,
                    PluginId = pluginLicense.PluginId,
                    Devices = devices,
                    User = pluginLicense.License.User
                });
            }

            return licenseOutputs;
;
        }

        public async Task<IEnumerable<LicenseModel>> GetLicenses(string filter, string sort)
        {
            IEnumerable<PluginLicenseModel> pluginLicenses = _context.PluginLicense.Include(pl => pl.Plugin).Include(pl => pl.License).ThenInclude(l => l.User);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            pluginLicenses = filterLogic.FilterDatabaseModel<PluginLicenseModel>(pluginLicenses, filter);

            return pluginLicenses.Select(pl => pl.License).ToList();
        }

        public async Task<IEnumerable<LicenseModel>> GetLicensesForPlugin(string filter, string sort, PluginModel plugin)
        {
            IEnumerable<LicenseModel> licenses = _context.PluginLicense.Include(pl => pl.Plugin).Include(pl => pl.License).Where(pl => pl.Plugin == plugin).Select(pl => pl.License);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            licenses = filterLogic.FilterDatabaseModel<LicenseModel>(licenses, filter);

            return licenses.ToList();
        }

        public async Task<IEnumerable<LicenseModel>> GetLicensesForUser(string filter, string sort, UserModel user)
        {
            IEnumerable<LicenseModel> licenses = _context.License.Include(l => l.User).Where(l => l.User == user);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            licenses = filterLogic.FilterDatabaseModel<LicenseModel>(licenses, filter);

            return licenses.ToList();
        }

        public async Task<IEnumerable<LicenseModel>> GetLicensesForUserAndPlugin(string filter, string sort, PluginModel plugin, UserModel user)
        {
            IEnumerable<LicenseModel> licenses = _context.PluginLicense.Include(pl => pl.Plugin)
                .Include(pl => pl.License).ThenInclude(l => l.User)
                .Where(pl => pl.Plugin == plugin || pl.License.User == user).Select(pl => pl.License);

            RequestSortFilterLogic filterLogic = new RequestSortFilterLogic();

            licenses = filterLogic.FilterDatabaseModel<LicenseModel>(licenses, filter);

            return licenses.ToList();
        }
    }
}
