namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class NegativeDi : Indicator
{
    private const int Period = 14;
    public static double Calculate(List<MarketIndexValue> values)
    {
        if (values == null || values.Count < Period)
            throw new ArgumentException("Insufficient data to calculate Negative DI");

        double negativeDM = 0;
        double trueRangeSum = 0;

        for (int i = 1; i < Period; i++)
        {
            double currentHigh = values[i].HighValue;
            double currentLow = values[i].LowValue;
            double previousHigh = values[i - 1].HighValue;
            double previousLow = values[i - 1].LowValue;

            double upMove = currentHigh - previousHigh;
            double downMove = previousLow - currentLow;

            if (downMove > upMove && downMove > 0)
            {
                negativeDM += downMove;
            }

            double trueRange = Math.Max(currentHigh - currentLow,
                Math.Max(Math.Abs(currentHigh - values[i - 1].ClosingValue),
                    Math.Abs(currentLow - values[i - 1].ClosingValue)));
            trueRangeSum += trueRange;
        }

        return (negativeDM / trueRangeSum) * 100;
    }
}
