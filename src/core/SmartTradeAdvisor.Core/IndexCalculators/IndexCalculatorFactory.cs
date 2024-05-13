using Microsoft.Extensions.Configuration;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public class IndexCalculatorFactory(IConfiguration configuration, IndexDbContext indexDbContext)
    : IIndexCalculatorFactory
{
    public IIndexCalculator CreateIndexesCalculator(MarketIndex marketIndex)
    {
        return new IndexCalculator(configuration, indexDbContext, marketIndex);
    }
}
