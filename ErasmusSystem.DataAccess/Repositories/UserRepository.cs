using ErasmusSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ErasmusSystem.DataAccess.Repositories
{
    // Bu sınıf, IUserRepository gerçek veritabanı işlemlerini yapar.
    public class UserRepository : IUserRepository
    {
        private readonly ErasmusDbContext _context;

        public UserRepository(ErasmusDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}