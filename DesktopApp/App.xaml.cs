using System.Windows;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DesktopApp.Data;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.ViewModels;
using DesktopApp.Views;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; } = null!;

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Database
                    services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(
                         "Server=DESKTOP-VLR55MR\\SQLEXPRESS;Database=DesktopAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));

                    // Identity (Core only - no SignInManager/cookies, this is a desktop app)
                    services.AddIdentityCore<ApplicationUser>(options =>
                        {
                            options.Password.RequiredLength = 6;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireUppercase = false;
                        })
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>();

                    // App services
                    services.AddSingleton<UserSession>();
                    services.AddScoped<IAuthService, AuthService>();

                    // ViewModels
                    services.AddTransient<LoginViewModel>();
                    services.AddTransient<RegisterViewModel>();

                    // Windows
                    services.AddTransient<LoginWindow>();
                    services.AddTransient<RegisterWindow>();
                    services.AddTransient<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            // Apply any pending EF Core migrations at startup
            using (var scope = AppHost.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            var loginWindow = AppHost.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}
