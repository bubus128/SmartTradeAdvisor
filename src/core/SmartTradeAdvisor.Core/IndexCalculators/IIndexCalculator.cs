using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public interface IIndexCalculator
{
    void CalculateAll(double high, double low);
    void CalculateAll(MarketIndexValue marketIndexValue) =>
        CalculateAll(high: marketIndexValue.HighValue, low: marketIndexValue.LowValue);
}
