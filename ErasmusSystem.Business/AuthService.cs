using System;
using System.Threading.Tasks;
using ErasmusSystem.Entities;
using ErasmusSystem.Entities.DTOs;

namespace ErasmusSystem.Business
{
    public class AuthService
    {
        // Not: Gerçek projede burada IUserRepository üzerinden veritabanı kontrolü yapılmalıdır.
        // Şimdilik 10. hafta prototipi için temel mantık kurulmuştur.

        public async Task<string> LoginAsync(LoginRequestDto loginDto)
        {
            // Domain validasyonu: Mail uzantısı kontrolü
            if (!loginDto.Email.EndsWith("@belek.edu.tr"))
            {
                throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı");
            }

            // TODO: Veritabanından kullanıcıyı e-posta ile bul
            // TODO: BCrypt.Verify ile şifre eşleşmesini kontrol et

            // Örnek başarılı dönüş (Gerçekte JWT Token dönülecektir)
            return "dummy-jwt-token-for-student";
        }
    }
}