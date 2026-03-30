using ErasmusSystem.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErasmusSystem.DataAccess.Repositories
{
    public interface IApplicationRepository
    {
        Task AddAsync(Application application);
        Task<IEnumerable<Application>> GetByUserIdAsync(Guid userId);
        Task<Application> GetByIdAsync(Guid id);
        Task UpdateAsync(Application application);
    }
}