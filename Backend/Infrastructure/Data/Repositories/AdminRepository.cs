using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context) { }


    }
}
