using ErasmusSystem.Entities;
using System;
using System.Threading.Tasks;

namespace ErasmusSystem.DataAccess.Repositories
{
    // Veritabanı işlemlerinin "neler" yapacağını söyler, "nasıl" yapacağına karışmaz.
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(Guid id);
    }
}