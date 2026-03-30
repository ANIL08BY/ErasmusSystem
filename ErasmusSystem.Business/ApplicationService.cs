using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErasmusSystem.DataAccess.Repositories;
using ErasmusSystem.Entities;
using ErasmusSystem.Entities.DTOs;

namespace ErasmusSystem.Business
{
    public class ApplicationService
    {
        // Artık doğrudan veritabanı (DbContext) yok! İki tane Depo (Repository) var.
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserRepository _userRepository;

        // Dependency Injection ile DbContext yerine Repository entegrasyonu
        public ApplicationService(IApplicationRepository applicationRepository, IUserRepository userRepository)
        {
            _applicationRepository = applicationRepository;
            _userRepository = userRepository;
        }

        // 1. Yeni Başvuru Oluşturma (Insert)
        public async Task<ApplicationResponseDto> CreateApplicationAsync(ApplicationCreateDto dto)
        {
            // Kullanıcının sistemde var olup olmadığını kontrol eden entegrasyon noktası
            var userExists = await _userRepository.UserExistsAsync(dto.UserId);
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

            await _applicationRepository.AddAsync(application);

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
            // Veriyi depodan çekip burada DTO'ya dönüştür
            var applications = await _applicationRepository.GetByUserIdAsync(userId);

            return applications.Select(a => new ApplicationResponseDto
            {
                Id = a.Id,
                UserId = a.UserId,
                Term = a.Term,
                Status = a.Status,
                TotalScore = a.TotalScore,
                CreatedAt = a.CreatedAt
            }).ToList();
        }

        // 3. Başvuru Durumunu Güncelleme (Update - Koordinatör Onayı/Reddi için)
        public async Task<bool> UpdateApplicationStatusAsync(Guid applicationId, string newStatus)
        {
            var app = await _applicationRepository.GetByIdAsync(applicationId);
            if (app == null) return false;

            app.Status = newStatus;
            app.UpdatedAt = DateTime.UtcNow;

            await _applicationRepository.UpdateAsync(app);
            return true;
        }
    }
}