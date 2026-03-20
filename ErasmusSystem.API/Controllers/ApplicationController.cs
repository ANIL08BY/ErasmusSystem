using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ErasmusSystem.Business;
using ErasmusSystem.Entities.DTOs;
using System;

namespace ErasmusSystem.API.Controllers
{
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

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string newStatus)
        {
            var success = await _applicationService.UpdateApplicationStatusAsync(id, newStatus);
            if (!success) return NotFound(new { Error = "Başvuru bulunamadı." });

            return Ok(new { Message = "Başvuru durumu güncellendi." });
        }
    }
}