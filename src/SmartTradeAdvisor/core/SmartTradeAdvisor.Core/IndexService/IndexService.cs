using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Core.Exceptions;
using SmartTradeAdvisor.Core.IndexCalculators;
using SmartTradeAdvisor.Core.WalletCalculator;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexService;
public class IndexService : IIndexService
{
    private readonly Dictionary<string, IIndexCalculator> _indexCalculators = [];

    private readonly IndexDbContext _indexDbContext;
    private readonly IIndexCalculatorFactory _indexCalculatorFactory;
    private readonly IWalletService _walletService;

    public IndexService(IndexDbContext indexDbContext, IIndexCalculatorFactory indexCalculatorFactory, IWalletService walletService)
    {
        _indexDbContext = indexDbContext;
        _indexCalculatorFactory = indexCalculatorFactory;
        _walletService = walletService;
        var marketIndexes = indexDbContext.MarketIndexes.Include(x => x.MarketIndexValues);
        foreach (var marketIndex in marketIndexes)
        {
            _indexCalculators.Add(marketIndex.Id, indexCalculatorFactory.CreateIndexesCalculator(marketIndex));
        }
    }

    public async Task AddIndex(MarketIndex index)
    {
        ArgumentNullException.ThrowIfNull(index);

        if (_indexDbContext.MarketIndexes.Contains(index))
        {
            throw new DuplicatedRecordException($"Index {index.Name} already exists");
        }

        // Add the market index
        _indexDbContext.MarketIndexes.Add(index);
        await _indexDbContext.SaveChangesAsync().ConfigureAwait(false);

        // Create index calculator for market index
        _indexCalculators.Add(index.Id, _indexCalculatorFactory.CreateIndexesCalculator(index));
    }

    public async Task AddValue(MarketIndexValue indexValue)
    {
        // Update wallets based on yesterday indicators
        await _walletService.CalculateAll(indexValue).ConfigureAwait(false);

        // Add index value
        _indexDbContext.MarketIndexValues.Add(indexValue);
        await _indexDbContext.SaveChangesAsync().ConfigureAwait(false);

        // Update indicators
        await _indexCalculators[indexValue.MarketIndexId].CalculateAll(indexValue).ConfigureAwait(false);
    }
}
