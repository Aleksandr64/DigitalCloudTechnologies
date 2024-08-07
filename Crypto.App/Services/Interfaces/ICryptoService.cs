using Crypto.App.Domain.Models;
using Crypto.App.Models;

namespace Crypto.App.Services.Interfaces;

public interface ICryptoService
{
    public Task<List<Currency>?> GetTopCurrenciesAsync(int count);
    public Task<CurrencyDetails> GetCurrencyDetailsAsync(string id);
    public Task<List<CoinSearch>?> GetListSearchCryptoCurrency(string searchText);
}