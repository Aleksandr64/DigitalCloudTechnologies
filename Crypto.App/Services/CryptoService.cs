using System.Net.Http;
using Crypto.App.Helpers;
using Crypto.App.Models;
using Newtonsoft.Json;

namespace Crypto.App.Services;

public class CryptoService
{
    private readonly HttpHelper _httpHelper;
    private const string BaseUrl = "https://api.coingecko.com/api/v3";
    private const string Token = "CG-UPNLTdzzQdcDJF1XubdH3PCb";

    public CryptoService()
    {
        _httpHelper = new HttpHelper();
    }

    public async Task<List<Currency>?> GetTopCurrenciesAsync(int count)
    {
        try
        {
            string url = $"{BaseUrl}/coins/markets?vs_currency=usd&per_page={count}&page=1";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("x-cg-demo-api-key", Token);

                var response = await _httpHelper.GetAsync(request);
                return JsonConvert.DeserializeObject<List<Currency>>(response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
            return new List<Currency>();
        }
    }

    public async Task<Currency?> GetCurrencyDetailsAsync(string id)
    {
        try
        {
            var url = $"{BaseUrl}/coins/markets?vs_currency=usd&ids={id}";
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("x-cg-demo-api-key", Token);

                var response = await _httpHelper.GetAsync(request);
                var currency = JsonConvert.DeserializeObject<List<Currency>>(response);
                return currency.FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
            return new Currency();
        }
    }
}