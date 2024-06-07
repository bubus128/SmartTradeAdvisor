using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Core.Exceptions;
using SmartTradeAdvisor.Core.IndexCalculators;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexService;
public class IndexService : IIndexService
{
    private readonly Dictionary<string, IIndexCalculator> _indexCalculators = [];

    private readonly IndexDbContext _indexDbContext;
    private readonly IIndexCalculatorFactory _indexCalculatorFactory;

    public IndexService(IndexDbContext indexDbContext, IIndexCalculatorFactory indexCalculatorFactory)
    {
        _indexDbContext = indexDbContext;
        _indexCalculatorFactory = indexCalculatorFactory;
        foreach (var marketIndex in indexDbContext.MarketIndexes.Include(x => x.MarketIndexValues))
        {
            _indexCalculators.Add(marketIndex.Id, indexCalculatorFactory.CreateIndexesCalculator(marketIndex));
        }
    }

    public void AddIndex(MarketIndex index)
    {
        ArgumentNullException.ThrowIfNull(index);

        if (_indexDbContext.MarketIndexes.Contains(index))
        {
            throw new DuplicatedRecordException($"Index {index.Name} already exists");
        }

        // Add the market index
        _indexDbContext.MarketIndexes.Add(index);
        _indexDbContext.SaveChanges();

        // Create index calculator for market index
        _indexCalculators.Add(index.Id, _indexCalculatorFactory.CreateIndexesCalculator(index));
    }

    public void AddValue(MarketIndexValue indexValue)
    {
        // Add index value
        _indexDbContext.MarketIndexValues.Add(indexValue);
        _indexDbContext.SaveChanges();

        _indexCalculators[indexValue.MarketIndexId].CalculateAll(indexValue);
    }
}
