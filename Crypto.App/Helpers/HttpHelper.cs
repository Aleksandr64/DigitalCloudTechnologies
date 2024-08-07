using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Crypto.App.Domain.Constants;
using Crypto.App.Helpers.Interfaces;
using Newtonsoft.Json;

namespace Crypto.App.Helpers;

public class HttpHelper : IHttpHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T?> GetAsync<T>(string url, string apiType, Action<HttpRequestMessage> configureRequest = null)
        {
            var response = await SendRequestAsync(HttpMethod.Get, url, null, apiType, configureRequest);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T?> PostAsync<T>(string url, HttpContent content, string apiType, Action<HttpRequestMessage> configureRequest = null)
        {
            var response = await SendRequestAsync(HttpMethod.Post, url, content, apiType, configureRequest);
            return JsonConvert.DeserializeObject<T>(response);
        }

        private async Task<string> SendRequestAsync(HttpMethod method, string url, HttpContent content, string apiType, Action<HttpRequestMessage> configureRequest = null)
        {
            var client = _httpClientFactory.CreateClient(apiType);
            
            var request = new HttpRequestMessage(method, url);
            if (content != null)
            {
                request.Content = content;
            }

            configureRequest?.Invoke(request);

            var response = await client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }
    }