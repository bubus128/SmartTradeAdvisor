namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class Ult : Indicator
{
    public static double Calculate(List<MarketIndexValue> values)
    {
        if (values.Count < 28)
        {
            throw new ArgumentException("Price lists must contain at least 28 elements.");
        }

        List<double> bp = [];
        List<double> tr = [];

        for (var i = 1; i < values.Count; i++)
        {
            var buyingPressure = values[i].ClosingValue - Math.Min(values[i].LowValue, values[i - 1].ClosingValue);
            bp.Add(buyingPressure);

            var trueRange = Math.Max(values[i].HighValue - values[i].LowValue,
                Math.Max(Math.Abs(values[i].HighValue - values[i - 1].ClosingValue),
                Math.Abs(values[i].LowValue - values[i - 1].ClosingValue)));
            tr.Add(trueRange);
        }

        var avg7BP = bp.Skip(bp.Count - 7).Take(7).Sum() / 7;
        var avg7TR = tr.Skip(tr.Count - 7).Take(7).Sum() / 7;
        var avg14BP = bp.Skip(bp.Count - 14).Take(14).Sum() / 14;
        var avg14TR = tr.Skip(tr.Count - 14).Take(14).Sum() / 14;
        var avg28BP = bp.Skip(bp.Count - 28).Take(28).Sum() / 28;
        var avg28TR = tr.Skip(tr.Count - 28).Take(28).Sum() / 28;

        var ult = 100 * ((4 * avg7BP / avg7TR) + (2 * avg14BP / avg14TR) + (avg28BP / avg28TR)) / 7;

        return ult;
    }
}
