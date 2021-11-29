using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories
{
    public class CompanyRepository : GenericRepository<CompanyModel>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context) { }
    }
}
