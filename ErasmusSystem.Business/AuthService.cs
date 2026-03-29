using System;
using System.Threading.Tasks;
using ErasmusSystem.Entities;
using ErasmusSystem.Entities.DTOs;
using ErasmusSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ErasmusSystem.Business
{
    public class AuthService
    {
        private readonly ErasmusDbContext _context;
        private readonly IConfiguration _configuration;

        // Constructor: Hem veritabanı bağlantısını hem de appsettings.json'ı okumak için IConfiguration'ı alıyor
        public AuthService(ErasmusDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginRequestDto loginDto)
        {
            if (loginDto.Email.EndsWith("@belek.edu.tr"))
            {
                // 1. Adım: Veritabanından kullanıcıyı bul
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (user != null)
                {
                    // 2. Adım: BCrypt ile Şifre Doğrulaması
                    if (BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                    {
                        // 3. Adım: Şifre doğruysa GERÇEK JWT Token Üret
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]); // appsettings.json'dan okur

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                new Claim(ClaimTypes.Email, user.Email)
                            }),
                            Expires = DateTime.UtcNow.AddHours(2), // Token 2 saat geçerli olsun
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        return tokenHandler.WriteToken(token); // Üretilen şifreli metni geriye dön
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı");
            }
        }
    }
}