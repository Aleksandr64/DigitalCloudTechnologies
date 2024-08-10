using System.Windows;
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

        public App()
        {
            var services = new ServiceCollection();
            
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("CoinGecko", client =>
            {
                client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("x-cg-demo-api-key", "CG-UPNLTdzzQdcDJF1XubdH3PCb");
            });

            services.AddScoped<IHttpHelper, HttpHelper>();

            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IErrorHandler, ErrorHandler>();

            services.AddTransient<MainViewModel>();
            services.AddScoped<HeaderViewModel>();
            services.AddScoped<Func<string, CurrencyDetailsViewModel>>(provider => id =>
                new CurrencyDetailsViewModel(provider.GetRequiredService<ICryptoService>(), id));
            
            services.AddSingleton<Header>();
            
            services.AddScoped<CurrencyDetailsPage>();
            services.AddScoped<MainPage>();
            
            services.AddSingleton<MainWindow>();
        }
        
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true; 
        }

        private void HandleException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Виняток", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}