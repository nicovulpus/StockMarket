using System.Text.Json.Serialization;


public class Stock
{
    public int Id { get; set; } // PRIMARY KEY
    
    [JsonPropertyName("stock_symbol")]
    public string StockSymbol { get; set; }
    
    [JsonPropertyName("stock_name")]
    public string StockName { get; set; }
    
    [JsonPropertyName("stock_sector")]
    public string StockSector { get; set; }
    
    [JsonPropertyName("stock_industry")]
    public string StockIndustry { get; set; }

    [JsonPropertyName("stock_market")]
    public string StockMarket { get; set; }

    [JsonPropertyName("stock_company_name")]
    public string StockCompanyName { get; set; }

    [JsonPropertyName("stock_price")]
    public decimal StockPrice { get; set; }

    [JsonPropertyName("market_cap")]
    public decimal MarketCap { get; set; }
    public int? Volume { get; set; }
    public float? ChangePercent { get; set; }
    public decimal? PreviousClose { get; set; }
    public float? PERatio { get; set; }
    public long? SharesOutstanding { get; set; }
    public float? DividendYield { get; set; }
}
