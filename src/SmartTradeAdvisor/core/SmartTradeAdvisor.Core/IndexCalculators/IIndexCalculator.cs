using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public interface IIndexCalculator
{
    void CalculateAll(MarketIndexValue marketIndexValue);
}
