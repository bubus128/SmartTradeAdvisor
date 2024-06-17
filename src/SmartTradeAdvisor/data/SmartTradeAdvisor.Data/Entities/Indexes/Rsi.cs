namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class Rsi : Indicator
{
    public static double Calculate(List<MarketIndexValue> values, int periods = 14)
    {
        var closingPrices = values.Select(x => x.ClosingValue).ToList();
        if (closingPrices.Count < periods)
        {
            throw new ArgumentException("Closing prices list must contain at least 'periods' elements.");
        }

        List<double> gains = [];
        List<double> losses = [];

        for (var i = 1; i < closingPrices.Count; i++)
        {
            var change = closingPrices[i] - closingPrices[i - 1];
            if (change > 0)
            {
                gains.Add(change);
            }
            else
            {
                losses.Add(-change);
            }
        }

        var averageGain = gains.Take(periods).Sum() / periods;
        var averageLoss = losses.Take(periods).Sum() / periods;

        var rs = averageGain / averageLoss;
        var rsi = 100 - (100 / (1 + rs));

        return rsi;
    }
}
