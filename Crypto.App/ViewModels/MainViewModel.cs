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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow?.MainFrame.Navigate(new CurrencyDetailsPage(_selectedCurrency.Id));
                });
            }
        }
        
        public ICommand LoadCurrenciesCommand { get; }

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
    }