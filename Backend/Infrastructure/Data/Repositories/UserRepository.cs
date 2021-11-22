using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> GetUserById(int UserId)
        {
            try
            {
                var item = await _context.Set<UserModel>()
                    .Where(x => x.UserId == UserId)
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

        public async Task<UserModel> UpdateUser(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                _context.Set<UserModel>().Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(user)} could not be updated: {ex.Message}");
            }
        }
    }
}
