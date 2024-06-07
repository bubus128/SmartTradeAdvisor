namespace SmartTradeAdvisor.Data.Entities.Indexes;

public class Mfi : Indicator
{
    public static double Calculate(List<MarketIndexValue> values, int periods = 14)
    {
        if (values.Count < periods)
        {
            throw new ArgumentException("Price and volume lists must contain at least 'periods' elements.");
        }

        List<double> typicalPrices = [];
        List<double> moneyFlow = [];

        for (var i = 0; i < values.Count; i++)
        {
            var typicalPrice = (values[i].HighValue + values[i].LowValue + values[i].ClosingValue) / 3;
            typicalPrices.Add(typicalPrice);
            moneyFlow.Add(typicalPrice * values[i].Volume);
        }

        List<double> positiveMoneyFlow = [];
        List<double> negativeMoneyFlow = [];

        for (var i = 1; i < typicalPrices.Count; i++)
        {
            if (typicalPrices[i] > typicalPrices[i - 1])
            {
                positiveMoneyFlow.Add(moneyFlow[i]);
                negativeMoneyFlow.Add(0);
            }
            else
            {
                positiveMoneyFlow.Add(0);
                negativeMoneyFlow.Add(moneyFlow[i]);
            }
        }

        var positiveMoneyFlowSum = positiveMoneyFlow.Take(periods).Sum();
        var negativeMoneyFlowSum = negativeMoneyFlow.Take(periods).Sum();

        var moneyFlowRatio = positiveMoneyFlowSum / negativeMoneyFlowSum;
        var mfi = 100 - (100 / (1 + moneyFlowRatio));

        return mfi;
    }
}
