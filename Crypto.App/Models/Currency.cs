using Newtonsoft.Json;

namespace Crypto.App.Models;

public class Currency
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("current_price")]
    public decimal CurrentPrice { get; set; }

    [JsonProperty("market_cap")]
    public long MarketCap { get; set; }

    [JsonProperty("market_cap_rank")]
    public int MarketCapRank { get; set; }

    [JsonProperty("total_volume")]
    public long TotalVolume { get; set; }

    [JsonProperty("price_change_percentage_24h")]
    public decimal PriceChangePercentage24h { get; set; }

    [JsonProperty("ath")]
    public decimal Ath { get; set; }

    [JsonProperty("ath_date")]
    public string AthDate { get; set; }
}