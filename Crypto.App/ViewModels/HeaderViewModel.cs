using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Crypto.App.Commands;
using Crypto.App.Models;
using Crypto.App.Views.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.App.ViewModels;

public class HeaderViewModel : ViewModelBase
{
    private ObservableCollection<CoinSearch> _searchResults;
    private CoinSearch _selectedCoinSearch;
    public ICommand NavigateToMainCommand { get; }
    public ObservableCollection<CoinSearch> SearchResults
    {
        get { return _searchResults; }
        set { _searchResults = value; OnPropertyChanged(); }
    }

    public HeaderViewModel()
    {
        SearchResults = new ObservableCollection<CoinSearch>();
        NavigateToMainCommand = new RelayCommand(NavigateToMain);
    }
    
    private void NavigateToMain()
    {
        var mainWindow = Application.Current.MainWindow as MainWindow;
        mainWindow?.MainFrame.Navigate(new MainPage(App.ServiceProvider.GetRequiredService<MainViewModel>()));
    }
}