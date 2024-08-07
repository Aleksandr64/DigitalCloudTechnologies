using Crypto.App.Models;
using Newtonsoft.Json;

namespace Crypto.App.DTO;

public class CryptoSearchDto
{
    [JsonProperty("coins")]
    public List<CoinSearch> Coins { get; set; }
}