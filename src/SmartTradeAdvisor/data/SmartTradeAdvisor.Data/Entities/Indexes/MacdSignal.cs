namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class MacdSignal : Macd
{
    public static double Calculate(List<Macd> macdList, int signalPeriod)
    {
        var signal = CalculateEma(macdList.Select(x => x.Value).ToList(), signalPeriod);

        return signal;
    }
}
