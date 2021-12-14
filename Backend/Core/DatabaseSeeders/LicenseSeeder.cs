﻿using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    
    public class LicenseSeeder
    {
        public static LicenseModel LICENSE_PREMIUM_USER;
        public static LicenseModel LICENSE_BUSINESS_USER;

        public static void SeedData(ApplicationDbContext context)
        {
            if (context.License.ToList().Count == 0)
            {
                LICENSE_BUSINESS_USER = new LicenseModel
                {
                    Id = 1,
                    IsActive = true,
                    LicenseKey = "ABCD-EFGH-ABCD-EFGH",
                    LicenseType = LicenseTypeSeeder.LICENSETYPE_BUSINESS,
                    LicenseTypeId = LicenseTypeSeeder.LICENSETYPE_BUSINESS.Id,
                    CreationTime = DateTime.Now,
                    ExpirationTime = DateTime.Now.AddYears(4),
                    TimesActivated = 1,
                    User = UserSeeder.USER_CUSTOMER
                };

                LICENSE_PREMIUM_USER = new LicenseModel
                {
                    Id = 2,
                    IsActive = true,
                    LicenseKey = "ABCD-EFGH-ABCD-AOGH",
                    LicenseType = LicenseTypeSeeder.LICENSETYPE_PREMIUM,
                    LicenseTypeId = LicenseTypeSeeder.LICENSETYPE_PREMIUM.Id,
                    CreationTime = DateTime.Now,
                    ExpirationTime = DateTime.Now.AddYears(4),
                    TimesActivated = 1,
                    User = UserSeeder.USER_CUSTOMER
                };

                context.Add(LICENSE_BUSINESS_USER);
                context.Add(LICENSE_PREMIUM_USER);
                context.SaveChanges();
            }
        }
    }
}
