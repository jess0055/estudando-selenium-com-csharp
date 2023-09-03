using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; 

class Program
{
static void Main(string[] args)
        {
            // Configuração do contêiner de injeção de dependência
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AluraDbContext>(options =>
                    options.UseSqlServer("Server=localhost;Database=CursosAluraDB;Trusted_Connection=True; Encrypt=False;TrustServerCertificate=True"))
                .AddScoped<IWebDriver>(_ =>
                {
                    var driver = new ChromeDriver();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    return driver;
                })
                .AddScoped<IScraper, AluraScraper>()
                .AddScoped<RpaService>()
                .BuildServiceProvider();

            // Iniciar o RPA
            using (var scope = serviceProvider.CreateScope())
            {
                var rpaService = scope.ServiceProvider.GetRequiredService<RpaService>();
                rpaService.Run();
            }
        }
}

