﻿using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserModel> UpdateFullName(string FullName, int userId)
        {
            _context.User.Where(x => x.Id == userId).FirstOrDefault().FullName = FullName;
            UserModel userModel = await _context.User.Where(x => x.Id == userId).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();
            return userModel;
        }
        public async Task<CompanyModel> GetCompanyById(int? companyId)
        {
            CompanyModel companyModel = await _context.Company.Where(x => x.Id == companyId).FirstOrDefaultAsync();
            return companyModel;
        }
            public async Task<UserModel> GetUserById(int UserId)
        {
            try
            {
                var item = await _context.Set<UserModel>()
                    .Where(x => x.Id == UserId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (item == null)
                {
                    throw new Exception($"Couldn't find entity with id={UserId}");
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity with id={UserId}: {ex.Message}");
            }
        }
        public async Task<UserModel> GetUserByEmail(string Email)
        {
            try
            {
                var item = await _context.Set<UserModel>()
                    .Where(x => x.Email == Email)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (item == null)
                {
                    throw new Exception($"Couldn't find entity with email={Email}");
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity with email={Email}: {ex.Message}");
            }
        }
    }
}
