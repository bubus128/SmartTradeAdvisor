using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public interface IIndexCalculatorFactory
{
    public IIndexCalculator CreateIndexesCalculator(MarketIndex marketIndex);
}
