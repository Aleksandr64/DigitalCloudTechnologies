using System.Windows;
using System.Windows.Input;
using Crypto.App.Commands;
using Crypto.App.Models;
using Crypto.App.Services;
using Crypto.App.Views.Pages;

namespace Crypto.App.ViewModels;

public class CurrencyDetailsViewModel : ViewModelBase
{
    private readonly CryptoService _cryptoService;
    private const string Token = "CG-UPNLTdzzQdcDJF1XubdH3PCb";

    public CurrencyDetailsViewModel(string id)
    {
        _cryptoService = new CryptoService();
        LoadCurrencyDetailsCommand = new RelayCommand(async () => await LoadCurrencyDetailsAsync(id));
        LoadCurrencyDetailsCommand.Execute(null);
    }

    public Currency? Currency { get; private set; }
    public ICommand LoadCurrencyDetailsCommand { get; }

    private async Task LoadCurrencyDetailsAsync(string id)
    {
        Currency = await _cryptoService.GetCurrencyDetailsAsync(id);
        OnPropertyChanged(nameof(Currency));
    }
}