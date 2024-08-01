using System.Windows;
using System.Windows.Controls;
using Crypto.App.ViewModels;

namespace Crypto.App.Views.Pages;

public partial class CurrencyDetailsPage : Page
{
    public CurrencyDetailsPage(string id)
    {
        InitializeComponent();
        Console.WriteLine(id);
        DataContext = new CurrencyDetailsViewModel(id);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new MainPage());
        });
    }
}