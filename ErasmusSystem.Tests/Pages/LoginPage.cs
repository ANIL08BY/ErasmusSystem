using Microsoft.Playwright;
using System.Threading.Tasks;

namespace ErasmusSystem.Tests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        // CSS Seçicileri (React Native Web için genel ID yapıları)
        private string EmailInput => "#email";
        private string PasswordInput => "#password";
        private string LoginButton => "#login-btn";
        private string ErrorMessage => "#error-message";

        public async Task NavigateToAsync()
        {
            // Frontend arayüzü adresi (Expo web genelde 8081 portunda çalışır)
            await _page.GotoAsync("http://localhost:8081/login");
        }

        public async Task LoginAsync(string email, string password)
        {
            await _page.FillAsync(EmailInput, email);
            await _page.FillAsync(PasswordInput, password);
            await _page.ClickAsync(LoginButton);
        }

        public async Task<string> GetErrorMessageAsync()
        {
            var element = _page.Locator(ErrorMessage);
            return await element.InnerTextAsync();
        }
    }
}