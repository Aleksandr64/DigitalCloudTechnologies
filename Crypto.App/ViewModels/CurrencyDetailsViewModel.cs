using System.Windows;
using System.Windows.Input;
using Crypto.App.Commands;
using Crypto.App.Domain.Models;
using Crypto.App.Models;
using Crypto.App.Services;
using Crypto.App.Services.Interfaces;
using Crypto.App.Views.Pages;

namespace Crypto.App.ViewModels;

public class CurrencyDetailsViewModel : ViewModelBase
{
    private readonly ICryptoService _cryptoService;
    private CurrencyDetails _currencyDetails;
    public ICommand OpenMarketCommand { get; }

    public CurrencyDetailsViewModel(ICryptoService cryptoService, string id)
    {
        _cryptoService = cryptoService;
        LoadCurrencyDetailsCommand = new RelayCommand(async () => await LoadCurrencyDetailsAsync(id));
        LoadCurrencyDetailsCommand.Execute(null);
        OpenMarketCommand = new RelayCommand<string>(OpenMarket);
    }

    public CurrencyDetails CurrencyDetails
    {
        get => _currencyDetails;
        set
        {
            _currencyDetails = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadCurrencyDetailsCommand { get; }

    private async Task LoadCurrencyDetailsAsync(string id)
    {
        CurrencyDetails = await _cryptoService.GetCurrencyDetailsAsync(id);
    }
    
    private void OpenMarket(string url)
    {
        if (string.IsNullOrEmpty(url))
            return;

        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
        catch (System.ComponentModel.Win32Exception noBrowser)
        {
            if (noBrowser.ErrorCode == -2147467259)
                MessageBox.Show(noBrowser.Message);
        }
        catch (System.Exception other)
        {
            MessageBox.Show(other.Message);
        }
    }
}