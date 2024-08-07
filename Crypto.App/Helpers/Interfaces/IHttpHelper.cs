using System.Net.Http;
using Crypto.App.Domain.Constants;

namespace Crypto.App.Helpers.Interfaces;

public interface IHttpHelper
{
    Task<T?> GetAsync<T>(string url, string apiType, Action<HttpRequestMessage> configureRequest = null);
    Task<T?> PostAsync<T>(string url, HttpContent content, string apiType, Action<HttpRequestMessage> configureRequest = null);
}