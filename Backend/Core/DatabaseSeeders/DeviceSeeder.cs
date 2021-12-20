using Backend.Core.Logic;
using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Backend.Core.DatabaseSeeders
{
    public class DeviceSeeder
    {
        public static DeviceModel Device1;

        public static void SeedData(ApplicationDbContext context)
        {
            if (context.Device.ToList().Count == 0)
            {
                Device1 = new DeviceModel
                {
                    Id = 1,
                    DeviceToken = "12345",
                    LicenseId = LicenseSeeder.LICENSE_BUSINESS_USER.Id,
                };

                context.Add(Device1);
                context.SaveChanges();
            }
        }
    }
}
