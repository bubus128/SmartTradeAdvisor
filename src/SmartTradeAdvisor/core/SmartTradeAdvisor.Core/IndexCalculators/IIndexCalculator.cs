using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public interface IIndexCalculator
{
    Task CalculateAll(MarketIndexValue marketIndexValue);
}
