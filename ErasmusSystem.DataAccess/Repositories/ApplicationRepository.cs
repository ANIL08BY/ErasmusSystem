using ErasmusSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErasmusSystem.DataAccess.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ErasmusDbContext _context;

        public ApplicationRepository(ErasmusDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Application>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Applications.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<Application> GetByIdAsync(Guid id)
        {
            return await _context.Applications.FindAsync(id);
        }

        public async Task UpdateAsync(Application application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
        }
    }
}