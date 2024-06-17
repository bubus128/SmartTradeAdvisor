namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class MacdSignal : Indicator
{
    public static double Calculate(List<Macd> macdList, int signalPeriod)
    {
        var signal = CalculateEma(macdList.Select(x => x.Value).ToList(), signalPeriod);

        return signal;
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
