using Microsoft.Extensions.Options;
using SmartTradeAdvisor.Core.Configurations;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public class IndexCalculatorFactory(IOptions<AlgorithmsConfiguration> algorithmConfiguration, IndexDbContext indexDbContext)
    : IIndexCalculatorFactory
{
    public IIndexCalculator CreateIndexesCalculator(MarketIndex marketIndex)
    {
        return new IndexCalculator(algorithmConfiguration, indexDbContext, marketIndex);
    }
}
