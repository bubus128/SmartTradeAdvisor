namespace SmartTradeAdvisor.Data.Entities;
public class PositiveDi : CalculatedIndex
{
    public PositiveDi(LimitedList<double> highs, LimitedList<double> lows, double prevValue, double smoothingFactor) : base(nameof(PositiveDi))
    {
        var highDifference = highs[^1] - highs[^2];
        var lowDifference = lows[^2] - lows[^1];

        var positiveDifference = highDifference > lowDifference && highDifference > 0 ? highDifference : 0;

        var positiveDi = prevValue == 0 ? positiveDifference :
            (positiveDifference - prevValue) * smoothingFactor + prevValue;

        Value = positiveDi;
    }
}
