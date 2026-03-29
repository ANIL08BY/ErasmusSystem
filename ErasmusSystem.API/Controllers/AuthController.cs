using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ErasmusSystem.Business;
using ErasmusSystem.Entities.DTOs;
using System;

namespace ErasmusSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        // Dependency Injection (Bağımlılık Enjeksiyonu) ile servisi otomatik al.
        // Artık "new AuthService()"  gerek yok .NET bunu arka planda dolduracak.
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDto);

                // Başarılı olursa 200 OK ile Token'ı dön
                return Ok(new { Token = token, Message = "Giriş başarılı." });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Hata durumunda 401 Unauthorized dön
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Beklenmeyen sistem hataları için 500
                return StatusCode(500, new { Message = "Sistemde beklenmeyen bir hata oluştu.", Details = ex.Message });
            }
        }
    }
}