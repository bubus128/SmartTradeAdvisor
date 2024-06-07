namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class MacdSignal : Macd
{
    public static double Calculate(List<double> macdList, int signalPeriod)
    {
        var signal = CalculateEma(macdList, signalPeriod);

        return signal;
    }
}
