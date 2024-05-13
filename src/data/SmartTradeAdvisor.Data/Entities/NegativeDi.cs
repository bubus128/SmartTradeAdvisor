namespace SmartTradeAdvisor.Data.Entities;
public class NegativeDi : CalculatedIndex
{
    public NegativeDi(LimitedList<double> highs, LimitedList<double> lows, double prevValue, double smoothingFactor) : base(nameof(NegativeDi))
    {
        var highDifference = highs[^1] - highs[^2];
        var lowDifference = lows[^2] - lows[^1];

        var negativeDifference = lowDifference > highDifference && lowDifference > 0 ? lowDifference : 0;

        var negativeDi = prevValue == 0 ? negativeDifference :
            (negativeDifference - prevValue) * smoothingFactor + prevValue;

        Value = negativeDi;
    }
}
