namespace SmartTradeAdvisor.Data.Entities.Indexes;
public class Adx : Indicator
{
    public static double Calculate(List<MarketIndexValue> values, int periods = 14)
    {
        if (values.Count < periods)
        {
            throw new ArgumentException("Price lists must contain at least 'periods' elements.");
        }

        var dmPlus = new List<double>();
        var dmMinus = new List<double>();
        var trList = new List<double>();

        for (var i = 1; i < values.Count; i++)
        {
            var highDiff = values[i].HighValue - values[i - 1].HighValue;
            var lowDiff = values[i - 1].LowValue - values[i].LowValue;

            dmPlus.Add(highDiff > lowDiff && highDiff > 0 ? highDiff : 0);
            dmMinus.Add(lowDiff > highDiff && lowDiff > 0 ? lowDiff : 0);

            var trueRange = Math.Max(values[i].HighValue - values[i].LowValue, Math.Max(Math.Abs(values[i].HighValue - values[i - 1].ClosingValue), Math.Abs(values[i].LowValue - values[i - 1].ClosingValue)));
            trList.Add(trueRange);
        }

        var smoothedTr = trList.Take(periods).Sum();
        var smoothedDmPlus = dmPlus.Take(periods).Sum();
        var smoothedDmMinus = dmMinus.Take(periods).Sum();

        var adxList = new List<double>();

        for (var i = periods; i < trList.Count; i++)
        {
            smoothedTr = smoothedTr - (smoothedTr / periods) + trList[i];
            smoothedDmPlus = smoothedDmPlus - (smoothedDmPlus / periods) + dmPlus[i];
            smoothedDmMinus = smoothedDmMinus - (smoothedDmMinus / periods) + dmMinus[i];

            var diPlus = 100 * (smoothedDmPlus / smoothedTr);
            var diMinus = 100 * (smoothedDmMinus / smoothedTr);
            var dx = 100 * Math.Abs((diPlus - diMinus) / (diPlus + diMinus));

            adxList.Add(dx);
        }

        var adx = adxList.Take(periods).Average();

        return adx;
    }
}
