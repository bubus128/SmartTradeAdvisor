namespace SmartTradeAdvisor.DataFetcher.Requests;

public class MarketIndexValue
{
    public DateTime Date { get; set; }
    public double LowValue { get; set; }
    public double HighValue { get; set; }
    public double ClosingValue { get; set; }
    public double Volume { get; set; }
}
