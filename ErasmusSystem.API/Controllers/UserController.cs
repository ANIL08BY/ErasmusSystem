using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ErasmusSystem.DataAccess;
using ErasmusSystem.Entities;

namespace ErasmusSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ErasmusDbContext _context;

        public UserController(ErasmusDbContext context)
        {
            _context = context;
        }

        // Test için hızlıca veritabanına yeni bir öğrenci ekler ve ID verir
        [HttpPost("create-test-user")]
        public async Task<IActionResult> CreateTestUser()
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Öğrenci",
                // E-posta çakışması olmasın diye rastgele bir mail üret
                Email = $"testogrenci_{Guid.NewGuid().ToString().Substring(0, 5)}@belek.edu.tr",

                // Şifremizi "123456" olarak belirle ve BCrypt ile şifreleyerek kaydet
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),

                Role = "Student"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Login olurken kullanılacak Email'i Postman ekranında görebilmek için response'a (Email) ekle
            return Ok(new
            {
                Message = "Test kullanıcısı başarıyla oluşturuldu!",
                UserId = newUser.Id,
                Email = newUser.Email
            });
        }
    }
}