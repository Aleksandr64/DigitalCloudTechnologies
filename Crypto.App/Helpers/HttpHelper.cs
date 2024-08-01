using System.Net.Http;
using System.Net.Http.Headers;

namespace Crypto.App.Helpers;

public class HttpHelper
{
    private readonly HttpClient _httpClient;

    public HttpHelper()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetAsync(HttpRequestMessage request)
    {
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}