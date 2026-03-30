using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ErasmusSystem.Business;
using ErasmusSystem.Entities.DTOs;
using System;

namespace ErasmusSystem.API.Controllers
{
    [Authorize] // Artık bu sınıftaki hiçbir işleme Token olmadan ulaşılamaz.
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _applicationService;

        public ApplicationController(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationCreateDto request)
        {
            try
            {
                var result = await _applicationService.CreateApplicationAsync(request);
                return Ok(new { Message = "Başvuru başarıyla oluşturuldu.", Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserApplications(Guid userId)
        {
            var applications = await _applicationService.GetApplicationsByUserAsync(userId);
            return Ok(applications);
        }

        // Koordinatörün Başvuruyu Onaylaması veya Reddetmesi (Tam Entegrasyon)
        [HttpPatch("{applicationId}/status")]
        [Authorize(Roles = "Coordinator")] // Sadece Koordinatör
        public async Task<IActionResult> UpdateApplicationStatus(Guid applicationId, [FromBody] string newStatus)
        {
            // İzin verilen statüler: APPROVED (Onaylandı), REJECTED (Reddedildi)
            if (newStatus != "APPROVED" && newStatus != "REJECTED")
            {
                return BadRequest(new { Error = "Geçersiz statü. Sadece APPROVED veya REJECTED gönderilebilir." });
            }

            var success = await _applicationService.UpdateApplicationStatusAsync(applicationId, newStatus);

            if (!success)
                return NotFound(new { Error = "Belirtilen ID'ye ait başvuru bulunamadı." });

            return Ok(new { Message = $"Başvuru durumu başarıyla '{newStatus}' olarak güncellendi." });
        }

        [HttpGet("test-error")]
        [AllowAnonymous] // Kilitli bir sınıfta, sadece bu metoda herkesin girmesine izin vermek için bu kullanılır.
        public IActionResult TestError()
        {
            // Middleware'i test etmek için kasıtlı olarak sistem hatası
            throw new Exception("Bu, hata yakalayıcıyı denemek için fırlatılmış kasıtlı bir hatadır.");
        }
    }
}