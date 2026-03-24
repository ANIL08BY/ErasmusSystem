using Microsoft.Playwright;
using System.Threading.Tasks;

namespace ErasmusSystem.Tests.Core
{
    public static class DriverFactory
    {
        public static async Task<IPage> CreatePageAsync()
        {
            // Playwright motorunu başlat
            var playwright = await Playwright.CreateAsync();

            // Chromium (Chrome) tarayıcısını aç
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // Testleri ekranda görmek için false yap
            });

            var context = await browser.NewContextAsync();
            return await context.NewPageAsync();
        }
    }
}