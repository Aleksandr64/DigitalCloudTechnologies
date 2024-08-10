using System.Net.Http;
using Crypto.App.Helpers.Interfaces;
using Crypto.App.Services.Interfaces;
using Newtonsoft.Json;

namespace Crypto.App.Helpers;

public class HttpHelper : IHttpHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IErrorHandler _errorHandler;

    public HttpHelper(IHttpClientFactory httpClientFactory, IErrorHandler errorHandler)
    {
        _httpClientFactory = httpClientFactory;
        _errorHandler = errorHandler;
    }

    public async Task<T?> GetAsync<T>(string url, string apiType, Action<HttpRequestMessage> configureRequest = null)
    {
        try
        {
            var response = await SendRequestAsync(HttpMethod.Get, url, null, apiType, configureRequest);
            return JsonConvert.DeserializeObject<T>(response);
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Error occurred during GET request.");
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string url, HttpContent content, string apiType, Action<HttpRequestMessage> configureRequest = null)
    {
        try
        {
            var response = await SendRequestAsync(HttpMethod.Post, url, content, apiType, configureRequest);
            return JsonConvert.DeserializeObject<T>(response);
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Error occurred during POST request.");
            return default;
        }
    }

    private async Task<string> SendRequestAsync(HttpMethod method, string url, HttpContent content, string apiType, Action<HttpRequestMessage> configureRequest = null)
    {
        try
        {
            var client = _httpClientFactory.CreateClient(apiType);
            var request = new HttpRequestMessage(method, url);

            if (content != null)
            {
                request.Content = content;
            }

            configureRequest?.Invoke(request);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Error occurred during HTTP request.");
            throw;
        }
    }
}