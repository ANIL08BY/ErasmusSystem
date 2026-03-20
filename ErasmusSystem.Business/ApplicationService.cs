using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErasmusSystem.DataAccess;
using ErasmusSystem.Entities;
using ErasmusSystem.Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ErasmusSystem.Business
{
    public class ApplicationService
    {
        private readonly ErasmusDbContext _context;

        // Dependency Injection ile DbContext entegrasyonu
        public ApplicationService(ErasmusDbContext context)
        {
            _context = context;
        }

        // 1. Yeni Başvuru Oluşturma (Insert)
        public async Task<ApplicationResponseDto> CreateApplicationAsync(ApplicationCreateDto dto)
        {
            // Kullanıcının sistemde var olup olmadığını kontrol eden entegrasyon noktası
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
            {
                throw new Exception("Belirtilen ID'ye sahip bir kullanıcı bulunamadı.");
            }

            var application = new Application
            {
                UserId = dto.UserId,
                Term = dto.Term,
                Status = "DRAFT", // İlk başvurular taslak olarak başlar
                CreatedAt = DateTime.UtcNow
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return new ApplicationResponseDto
            {
                Id = application.Id,
                UserId = application.UserId,
                Term = application.Term,
                Status = application.Status,
                CreatedAt = application.CreatedAt
            };
        }

        // 2. Öğrencinin Kendi Başvurularını Listelemesi (Select)
        public async Task<List<ApplicationResponseDto>> GetApplicationsByUserAsync(Guid userId)
        {
            return await _context.Applications
                .Where(a => a.UserId == userId)
                .Select(a => new ApplicationResponseDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Term = a.Term,
                    Status = a.Status,
                    TotalScore = a.TotalScore,
                    CreatedAt = a.CreatedAt
                }).ToListAsync();
        }

        // 3. Başvuru Durumunu Güncelleme (Update - Koordinatör Onayı/Reddi için)
        public async Task<bool> UpdateApplicationStatusAsync(Guid applicationId, string newStatus)
        {
            var app = await _context.Applications.FindAsync(applicationId);
            if (app == null) return false;

            app.Status = newStatus;
            app.UpdatedAt = DateTime.UtcNow;

            _context.Applications.Update(app);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}