using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class LicenseTypeSeeder
    {
        public static LicenseTypeModel LICENSETYPE_PREMIUM;
        public static LicenseTypeModel LICENSETYPE_BUSINESS;

        public static void SeedData(ApplicationDbContext context)
        {
            if (context.LicenseType.ToList().Count == 0)
            {
                LICENSETYPE_PREMIUM = new LicenseTypeModel
                {
                    Id = 1,
                    MaxAmount = 5,
                    TypeName = "Premium",
                    CreationTime = DateTime.Now
                };

                LICENSETYPE_BUSINESS = new LicenseTypeModel
                {
                    Id = 2,
                    MaxAmount = 20,
                    TypeName = "Business",
                    CreationTime = DateTime.Now
                };

                context.Add(LICENSETYPE_PREMIUM);
                context.Add(LICENSETYPE_BUSINESS);
                context.SaveChanges();
            }
        }
    }
}
