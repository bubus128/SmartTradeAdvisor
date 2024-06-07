namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class Macd : Indicator
{
    public static double Calculate(List<MarketIndexValue> values)
    {
        if (values.Count < 26)
        {
            throw new ArgumentException("List of closing prices must contain at least 26 elements.");
        }

        var ema12 = CalculateEma(values.Select(x => x.ClosingValue).ToList(), 12);

        var ema26 = CalculateEma(values.Select(x => x.ClosingValue).ToList(), 26);

        var macd = ema12 - ema26;

        return macd;
    }

    protected static double CalculateEma(List<double> prices, int periods)
    {
        var k = 2.0 / (periods + 1);
        var ema = prices.Take(periods).Average(); // Początkowa wartość EMA jako średnia arytmetyczna z pierwszych 'periods' wartości

        for (var i = periods; i < prices.Count; i++)
        {
            ema = (prices[i] - ema) * k + ema;
        }

        return ema;
    }
}
