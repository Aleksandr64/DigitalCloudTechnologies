using System.Net.Http;
using System.Windows;
using Crypto.App.Domain.Constants;
using Crypto.App.Helpers;
using Crypto.App.Helpers.Interfaces;
using Crypto.App.Services;
using Crypto.App.Services.Interfaces;
using Crypto.App.ViewModels;
using Crypto.App.Views.Components;
using Crypto.App.Views.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.App
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create MainWindow: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddHttpClient("CoinGecko", client =>
            {
                client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("x-cg-demo-api-key", "CG-UPNLTdzzQdcDJF1XubdH3PCb");
            });

            services.AddScoped<IHttpHelper, HttpHelper>();

            services.AddScoped<ICryptoService, CryptoService>();

            services.AddScoped<MainViewModel>();
            services.AddScoped<HeaderViewModel>();
            services.AddScoped<Func<string, CurrencyDetailsViewModel>>(provider => id =>
                new CurrencyDetailsViewModel(provider.GetRequiredService<ICryptoService>(), id));

            services.AddTransient<MainPage>();
            services.AddScoped<CurrencyDetailsPage>();
            services.AddSingleton<Header>();
            services.AddSingleton<MainWindow>();
        }
    }
}