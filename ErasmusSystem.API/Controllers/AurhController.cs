using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ErasmusSystem.Business;
using ErasmusSystem.Entities.DTOs;
using System;

namespace ErasmusSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController()
        {
            // DI (Dependency Injection) yapısı Sprint 1 içerisinde kurulacaktır.
            _authService = new AuthService();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var token = await _authService.LoginAsync(request);
                return Ok(new { Token = token, Message = "Giriş başarılı." });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Hatalı girişte "Kullanıcı adı veya şifre hatalı" uyarısı dönmelidir.
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Sunucu tarafında bir hata oluştu." });
            }
        }
    }
}