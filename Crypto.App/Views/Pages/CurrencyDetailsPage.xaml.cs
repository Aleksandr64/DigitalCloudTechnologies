using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Crypto.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.App.Views.Pages;

public partial class CurrencyDetailsPage : Page
{
    private readonly Func<string, CurrencyDetailsViewModel> _viewModelFactory;

    public CurrencyDetailsPage(Func<string, CurrencyDetailsViewModel> viewModelFactory, string id)
    {
        InitializeComponent();
        _viewModelFactory = viewModelFactory;
        DataContext = _viewModelFactory(id);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new MainPage(App.ServiceProvider.GetRequiredService<MainViewModel>()));
        });
    }
}