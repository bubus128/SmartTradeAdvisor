namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class PositiveDi : Indicator
{
    public static double Calculate(LimitedList<double> highs, LimitedList<double> lows, double prevValue, double smoothingFactor)
    {
        var highDifference = highs[^1] - highs[^2];
        var lowDifference = lows[^2] - lows[^1];

        var positiveDifference = highDifference > lowDifference && highDifference > 0 ? highDifference : 0;

        var positiveDi = prevValue == 0 ? positiveDifference :
            (positiveDifference - prevValue) * smoothingFactor + prevValue;

        return positiveDi;
    }
}
