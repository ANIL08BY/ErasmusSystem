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
                PasswordHash = "test_sifre_hash",
                Role = "Student"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Test kullanıcısı başarıyla oluşturuldu!", UserId = newUser.Id });
        }
    }
}