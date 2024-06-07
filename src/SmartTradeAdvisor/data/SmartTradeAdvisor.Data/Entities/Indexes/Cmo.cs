namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class Cmo : Indicator
{
    public static double Calculate(List<MarketIndexValue> values, int periods = 14)
    {
        if (values.Count < periods)
        {
            throw new ArgumentException("Closing prices list must contain at least 'periods' elements.");
        }

        List<double> gains = [];
        List<double> losses = [];

        for (var i = 1; i < values.Count; i++)
        {
            var change = values[i].ClosingValue - values[i - 1].ClosingValue;
            if (change > 0)
            {
                gains.Add(change);
                losses.Add(0);
            }
            else
            {
                gains.Add(0);
                losses.Add(-change);
            }
        }

        var gainSum = gains.Take(periods).Sum();
        var lossSum = losses.Take(periods).Sum();

        var cmo = 100 * ((gainSum - lossSum) / (gainSum + lossSum));

        return cmo;
    }
}
