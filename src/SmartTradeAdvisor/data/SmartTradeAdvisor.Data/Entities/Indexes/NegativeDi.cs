namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class NegativeDi : Indicator
{
    public static double Calculate(LimitedList<double> highs, LimitedList<double> lows, double prevValue, double smoothingFactor)
    {
        var highDifference = highs[^1] - highs[^2];
        var lowDifference = lows[^2] - lows[^1];

        var negativeDifference = lowDifference > highDifference && lowDifference > 0 ? lowDifference : 0;

        var negativeDi = prevValue == 0 ? negativeDifference :
            (negativeDifference - prevValue) * smoothingFactor + prevValue;

        return negativeDi;
    }
}
