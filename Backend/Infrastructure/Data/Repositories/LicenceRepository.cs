using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories
{
    public class LicenceRepository : GenericRepository<License>, ILicenceRepository
    {
        public LicenceRepository(ApplicationDbContext context) : base(context) { }

        public Task<List<License>> GetLicencesDb(int userID)
        {
            bool admin = _context.User.Select(s => s.UserId == userID).FirstOrDefault();
            List<License> results = new();
            if (admin == true)
            {
                results = _context.License.Where(s => s.IsActive == true).ToList();
            }
            else
            {
                results = _context.License.Where(s => s.IsActive == true && s.UserId == userID).ToList();
            }
            return Task.FromResult(results);
        }
    }
}
