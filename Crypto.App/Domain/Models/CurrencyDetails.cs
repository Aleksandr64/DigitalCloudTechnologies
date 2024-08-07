using Newtonsoft.Json;

namespace Crypto.App.Domain.Models;

public class CurrencyDetails
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    [JsonProperty("market_data")]
    public MarketData MarketData { get; set; }
    public List<Ticker> Tickers { get; set; }
}

public class MarketData
{
    [JsonProperty("current_price")]
    public Dictionary<string, decimal> CurrentPrice { get; set; }

    [JsonProperty("price_change_percentage_24h")]
    public decimal PriceChangePercentage24h { get; set; }

    [JsonProperty("total_volume")]
    public Dictionary<string, decimal> TotalVolume { get; set; }

    [JsonProperty("market_cap")]
    public Dictionary<string, decimal> MarketCap { get; set; }

    [JsonProperty("fully_diluted_valuation")]
    public Dictionary<string, decimal> FullyDilutedValuation { get; set; }

    [JsonProperty("circulating_supply")]
    public decimal CirculatingSupply { get; set; }

    [JsonProperty("total_supply")]
    public decimal TotalSupply { get; set; }

    [JsonProperty("max_supply")]
    public decimal? MaxSupply { get; set; }
}

public class Ticker
{
    public string Base { get; set; }
    public string Target { get; set; }
    public Market Market { get; set; }
    public decimal Last { get; set; }
    [JsonProperty("trade_url")]
    public string TradeUrl { get; set; }
}

public class Market
{
    public string Name { get; set; }
    public string Identifier { get; set; }
    [JsonProperty("has_trading_incentive")]
    public bool HasTradingIncentive { get; set; }
}