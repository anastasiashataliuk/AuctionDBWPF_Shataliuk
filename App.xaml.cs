using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using AuctionManagerApp.Views;
using AuctionManagerApp.ViewModels;
using Microsoft.Extensions.Configuration;
using System.IO;
using AuctionManagerApp.BLL.Interfaces;
using AuctionManagerApp.BLL.Services;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;

namespace AuctionManagerApp
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }

        // This method registers all services, including view models and windows
        private void ConfigureServices(IServiceCollection services)
        {
            // Build the configuration to access the config.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Current directory
                .AddJsonFile("config.json", optional: false, reloadOnChange: true) // Load config.json
                .Build();

            // Retrieve connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Register windows
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<UserWindow>();
            services.AddSingleton<AuctionWindow>();
            services.AddSingleton<AuctionDetailsWindow>();  // Add AuctionDetailsWindow to DI container

            // Register view models
            services.AddSingleton<LoginViewModel>(sp =>
            {
                var authenticationService = sp.GetRequiredService<IAuthenticationService>();
                return new LoginViewModel(authenticationService, () =>
                {
                    // Navigate to MainWindow upon successful login
                    var mainWindow = sp.GetRequiredService<MainWindow>();
                    mainWindow.Show();

                    // Close LoginWindow
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LoginWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                });
            });

            services.AddSingleton<UserViewModel>();
            services.AddSingleton<AuctionViewModel>();
            services.AddSingleton<AuctionDetailsViewModel>();

            // Register services for authentication, etc.
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuctionService, AuctionService>();

            // Register other services like IUserService, etc.
            services.AddSingleton<IUserService>(sp => new UserService(connectionString));
        }
    }
}
