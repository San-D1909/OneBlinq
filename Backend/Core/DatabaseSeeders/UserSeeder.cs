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
    public class UserSeeder
    {
        public static UserModel USER_ADMIN;
        public static UserModel USER_CUSTOMER;

        public static void SeedData(ApplicationDbContext context, PasswordEncrypter encryptor)
        {
            if (context.User.ToList().Count == 0)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                USER_ADMIN = new UserModel
                {
                    Id = 1,
                    FullName = "Stijn van Eekelen",
                    Company = null,
                    Email = "st.van.eekelen@gmail.com",
                    IsAdmin = true,
                    IsVerified = true,
                    Password = encryptor.EncryptPassword("Test123" + salt),
                    Salt = salt
                };

                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                USER_CUSTOMER = new UserModel
                {
                    Id = 2,
                    FullName = "Test von Tester",
                    Company = null,
                    Email = "test@test.nl",
                    IsAdmin = false,
                    IsVerified = true,
                    Password = encryptor.EncryptPassword("Test123" + salt),
                    Salt = salt
                };

                context.Add(USER_ADMIN);
                context.Add(USER_CUSTOMER);
                context.SaveChanges();
            }
        }
    }
}
