using SmartTradeAdvisor.Core.IndexCalculators;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexService;
public class IndexService(IndexDbContext indexDbContext, IIndexCalculatorFactory indexCalculatorFactory) : IIndexService
{
    private readonly List<IIndexCalculator> _indexCalculators = [];

    public void AddIndex(MarketIndex index)
    {
        // Add the market index
        indexDbContext.MarketIndexes.Add(index);
        indexDbContext.SaveChanges();

        // Create index calculator for market index
        _indexCalculators.Add(indexCalculatorFactory.CreateIndexesCalculator(index));
    }

    public void AddValue(MarketIndexValue indexValue)
    {
        // Add index value
        indexDbContext.MarketIndexValues.Add(indexValue);
        indexDbContext.SaveChanges();

        // Calculate all additional indexes
        foreach (var indexCalculator in _indexCalculators)
        {
            indexCalculator.CalculateAll(indexValue.HighValue, indexValue.LowValue);
        }
    }
}
