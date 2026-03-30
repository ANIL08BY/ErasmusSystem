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
using ErasmusSystem.DataAccess.Repositories;

namespace ErasmusSystem.Business
{
    public class AuthService
    {
        // ARTIK DOĞRUDAN VERİTABANI(DbContext) YOK! SADECE INTERFACE VAR.
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        // Constructor: Hem veritabanı bağlantısını hem de appsettings.json'ı okumak için IConfiguration'ı alıyor
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginRequestDto loginDto)
        {
            if (loginDto.Email.EndsWith("@belek.edu.tr"))
            {
                // 1.Adım: Artık veritabanı sorgusunu Repository yapıyor.
                var user = await _userRepository.GetByEmailAsync(loginDto.Email);

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
                                new Claim(ClaimTypes.Email, user.Email),

                                // Kullanıcının veritabanındaki rolünü Token'a mühürle
                                new Claim(ClaimTypes.Role, user.Role)
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