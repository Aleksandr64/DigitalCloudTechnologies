using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Crypto.App.Models;
using Crypto.App.Views.Pages;

namespace Crypto.App.ViewModels;

public class HeaderViewModel : ViewModelBase
{
    private ObservableCollection<CoinSearch> _searchResults;
    private CoinSearch _selectedCoinSearch;
    public ObservableCollection<CoinSearch> SearchResults
    {
        get { return _searchResults; }
        set { _searchResults = value; OnPropertyChanged(); }
    }

    public HeaderViewModel()
    {
        SearchResults = new ObservableCollection<CoinSearch>();
    }
}