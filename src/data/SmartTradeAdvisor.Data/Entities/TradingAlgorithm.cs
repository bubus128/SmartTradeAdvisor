namespace SmartTradeAdvisor.Data.Entities;
public class TradingAlgorithm
{
    public required List<MarketIndex> MarketIndexes { get; set; }

    public required List<string> Operations { get; set; }
}
