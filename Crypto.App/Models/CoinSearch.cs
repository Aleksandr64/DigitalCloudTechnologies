using Newtonsoft.Json;

namespace Crypto.App.Models;

public class CoinSearch
{
    [JsonProperty("id")] 
    public string Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    
    [JsonProperty("thumb")]
    public string ImageUrl { get; set; }
}