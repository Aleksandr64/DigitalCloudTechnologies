using System.Windows.Controls;
using Crypto.App.ViewModels;

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
}