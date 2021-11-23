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

        public async Task<User> UpdateFullName(string FullName, int userId)
        {
            _context.User.Where(x => x.UserId == userId).FirstOrDefault().FullName = FullName;
            User userModel = await _context.User.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User> GetUserById(int UserId)
        {
            try
            {
                var item = await _context.Set<User>()
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
    }
}
