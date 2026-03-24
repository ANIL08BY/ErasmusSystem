using ErasmusSystem.Tests.Core;
using ErasmusSystem.Tests.Pages;
using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace ErasmusSystem.Tests.FunctionalTests
{
    public class LoginTests : IAsyncLifetime
    {
        private IPage _page;
        private LoginPage _loginPage;

        // Test başlamadan ÖNCE çalışacak asenkron hazırlık
        public async Task InitializeAsync()
        {
            _page = await DriverFactory.CreatePageAsync();
            _loginPage = new LoginPage(_page);
        }

        // Test bittikten SONRA çalışacak temizlik
        public async Task DisposeAsync()
        {
            if (_page != null)
            {
                await _page.CloseAsync();
            }
        }

        [Fact]
        public async Task UnsuccessfulLogin_WithInvalidEmail_ShouldShowError()
        {
            // Arrange (Hazırlık)
            await _loginPage.NavigateToAsync();

            // Act (Eylem)
            await _loginPage.LoginAsync("gecersiz@gmail.com", "yanlisSifre123");

            // Assert (Doğrulama)
            // Not: Frontend henüz hazır olmadığı için Assert kısımları şimdilik plan 
            // var errorMessage = await _loginPage.GetErrorMessageAsync();
            // Assert.Contains("Kullanıcı adı veya şifre hatalı", errorMessage);
        }
    }
}