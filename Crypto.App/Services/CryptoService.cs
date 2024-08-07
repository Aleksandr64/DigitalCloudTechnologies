using System.Net.Http;
using Crypto.App.Domain.Constants;
using Crypto.App.Domain.Models;
using Crypto.App.DTO;
using Crypto.App.Helpers;
using Crypto.App.Helpers.Interfaces;
using Crypto.App.Models;
using Crypto.App.Services.Interfaces;
using Newtonsoft.Json;

namespace Crypto.App.Services;

public class CryptoService : ICryptoService
{
    private readonly IHttpHelper _httpHelper;

    public CryptoService(IHttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    public async Task<List<Currency>?> GetTopCurrenciesAsync(int count)
    {
        try
        {
            string url = $"coins/markets?vs_currency=usd&per_page={count}&page=1";
            return await _httpHelper.GetAsync<List<Currency>>(url, ApiType.CoinGecko);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
            return new List<Currency>();
        }
    }

    public async Task<CurrencyDetails> GetCurrencyDetailsAsync(string id)
    {
        try
        {
            string url = $"coins/{id}?localization=false&tickers=true&market_data=true&community_data=false&developer_data=false&sparkline=false";
            return await _httpHelper.GetAsync<CurrencyDetails>(url, "CoinGecko");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
            return new CurrencyDetails();
        }
    }

    public async Task<List<CoinSearch>?> GetListSearchCryptoCurrency(string searchText)
    {
        try
        {
            string url = $"search?query={searchText}";
            var result = await _httpHelper.GetAsync<CryptoSearchDto>(url, ApiType.CoinGecko);
            return result?.Coins;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<CoinSearch>();
        }
    }
}