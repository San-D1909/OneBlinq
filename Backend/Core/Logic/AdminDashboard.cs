using Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.Logic
{
    public class AdminDashboard
    {
        private readonly ApplicationDbContext _context;
        public AdminDashboard(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<bool> InsertCircuits()
        {

                _context.License.Any(s => s.))
                _context.SaveChangesAsync();
            return Task.FromResult(true);
        }
    }
}
