using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Crypto.App.Commands;
using Crypto.App.Models;
using Crypto.App.Services.Interfaces;
using Crypto.App.Views.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Crypto.App.ViewModels;

public class HeaderViewModel : ViewModelBase
    {
        private readonly ICryptoService _cryptoService;
        private readonly DispatcherTimer _searchDelayTimer;
        private string _searchText;
        private ObservableCollection<CoinSearch> _searchResults;
        private bool _isPopupOpen;
        private CornerRadius _searchBorderRadius;

        public ICommand NavigateToMainCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand SearchTextChangedCommand { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (value != "")
                {
                    _searchText = value;
                    OnPropertyChanged();
                    _searchDelayTimer.Stop();
                    _searchDelayTimer.Start();
                }
                else
                {
                    _searchText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<CoinSearch> SearchResults
        {
            get => _searchResults;
            set
            {
                if (_searchResults != value)
                {
                    _searchResults = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                if (_isPopupOpen != value)
                {
                    _isPopupOpen = value;
                    OnPropertyChanged();
                    SearchBorderRadius = IsPopupOpen ? new CornerRadius(15, 15, 0, 0) : new CornerRadius(15);
                }
            }
        }

        public CornerRadius SearchBorderRadius
        {
            get => _searchBorderRadius;
            set
            {
                if (_searchBorderRadius != value)
                {
                    _searchBorderRadius = value;
                    OnPropertyChanged();
                }
            }
        }

        public HeaderViewModel(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;

            _searchResults = new ObservableCollection<CoinSearch>();

            NavigateToMainCommand = new RelayCommand(NavigateToMain);
            ClearSearchCommand = new RelayCommand(ClearSearch);
            SearchTextChangedCommand = new RelayCommand(OnSearchTextChanged);

            _searchDelayTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.3)
            };
            _searchDelayTimer.Tick += OnSearchDelayTimerTick;

            SearchBorderRadius = new CornerRadius(15);
        }

        private void NavigateToMain()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new MainPage(App.ServiceProvider.GetRequiredService<MainViewModel>()));
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
            SearchResults.Clear();
            IsPopupOpen = false;
        }

        private void OnSearchTextChanged()
        {
            _searchDelayTimer.Stop();
            _searchDelayTimer.Start();
        }

        private async void OnSearchDelayTimerTick(object sender, EventArgs e)
        {
            _searchDelayTimer.Stop();
            await ExecuteSearch();
        }

        private async Task ExecuteSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                var response = await _cryptoService.GetListSearchCryptoCurrency(SearchText);
                SearchResults.Clear();
                if (response.Count > 0)
                {
                    foreach (var coin in response)
                    {
                        SearchResults.Add(coin);
                    }
                    IsPopupOpen = true; 
                }
                else
                {
                    IsPopupOpen = false;
                }
            }
            else
            {
                IsPopupOpen = false; 
            }
        }

        public void OnSelectionChanged(object selectedItem)
        {
            if (selectedItem is CoinSearch coinSearch)
            {
                SearchResults.Clear();
                IsPopupOpen = false;
                SearchText = "";
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.MainFrame.Navigate(new CurrencyDetailsPage(App.ServiceProvider.GetRequiredService<Func<string, CurrencyDetailsViewModel>>(), coinSearch.Id));
                
            }
        }
    }