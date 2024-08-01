using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Crypto.App.Commands;
using Crypto.App.Models;
using Crypto.App.Services;
using Crypto.App.Views.Pages;

namespace Crypto.App.ViewModels;

public class MainViewModel : ViewModelBase
    {
        private readonly CryptoService _cryptoService;
        private Currency _selectedCurrency;
        private string _searchQuery;
        

        public MainViewModel()
        {
            _cryptoService = new CryptoService();
            LoadCurrenciesCommand = new RelayCommand(async () => await LoadCurrenciesAsync());
            SearchCommand = new RelayCommand(async () => await SearchCurrencyAsync());
            Currencies = new ObservableCollection<Currency>();
            _ = LoadCurrenciesAsync();
        }

        public ObservableCollection<Currency> Currencies { get; }

        public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
                if (_selectedCurrency != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new CurrencyDetailsPage(_selectedCurrency.Id));
                    });
                }
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        public ICommand LoadCurrenciesCommand { get; }
        public ICommand SearchCommand { get; }

        private async Task LoadCurrenciesAsync()
        {
            var currencies = await _cryptoService.GetTopCurrenciesAsync(10);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Currencies.Clear();
                foreach (var currency in currencies)
                {
                    Currencies.Add(currency);
                }
            });
        }

        private async Task SearchCurrencyAsync()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var currency = await _cryptoService.GetCurrencyDetailsAsync(SearchQuery);
                if (currency != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Currencies.Clear();
                        Currencies.Add(currency);
                    });
                }
            }
        }
    }