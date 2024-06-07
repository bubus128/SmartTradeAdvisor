namespace SmartTradeAdvisor.Api.Mapper;

public class MarketIndexValueDto
{
    public DateTime Date { get; set; }
    public double LowValue { get; set; }
    public double HighValue { get; set; }
    public double ClosingValue { get; set; }
    public double Volume { get; set; }
}
