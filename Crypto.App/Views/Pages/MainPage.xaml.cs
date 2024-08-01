using System.Windows.Controls;
using Crypto.App.ViewModels;

namespace Crypto.App.Views.Pages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}