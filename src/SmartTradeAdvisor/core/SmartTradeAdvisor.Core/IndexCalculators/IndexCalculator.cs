using Microsoft.Extensions.Options;
using SmartTradeAdvisor.Core.Configurations;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;
using SmartTradeAdvisor.Data.Entities.Indexes;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public class IndexCalculator : IIndexCalculator
{
    private readonly int _macdSignalLimit;
    private readonly IndexDbContext _indexDbContext;
    private readonly MarketIndex _marketIndex;
    private readonly int _memoryLimit;

    public IndexCalculator(IOptions<AlgorithmsConfiguration> algorithmConfiguration, IndexDbContext indexDbContext, MarketIndex marketIndex)
    {
        _marketIndex = marketIndex;
        _indexDbContext = indexDbContext;
        var configuration = algorithmConfiguration.Value;
        _macdSignalLimit = configuration.MacdSignalPeriod;
        _memoryLimit = configuration.ValuesMemoryLimit;
        _indexDbContext = indexDbContext;
    }

    public void CalculateAll(MarketIndexValue marketIndexValue)
    {
        var lastValues = _marketIndex.MarketIndexValues.OrderByDescending(x => x.Date).Take(_memoryLimit).ToList();
        if (lastValues.Count < _memoryLimit)
        {
            return;
        }

        _indexDbContext.Adx.Add(new Adx()
        {
            MarketIndexId = _marketIndex.Id,
            Value = Adx.Calculate(lastValues),
            MarketIndex = _marketIndex,
            Date = DateTime.Now,
        });

        _indexDbContext.Cmo.Add(new Cmo()
        {
            MarketIndexId = _marketIndex.Id,
            Value = Cmo.Calculate(lastValues),
            MarketIndex = _marketIndex,
            Date = DateTime.Now,
        });

        _indexDbContext.Mfi.Add(new Mfi()
        {
            MarketIndexId = _marketIndex.Id,
            Value = Mfi.Calculate(lastValues),
            MarketIndex = _marketIndex,
            Date = DateTime.Now,
        });

        _indexDbContext.Rsi.Add(new Rsi()
        {
            MarketIndexId = _marketIndex.Id,
            Value = Rsi.Calculate(lastValues),
            MarketIndex = _marketIndex,
            Date = DateTime.Now,
        });

        _indexDbContext.Ult.Add(new Ult()
        {
            MarketIndexId = _marketIndex.Id,
            Value = Ult.Calculate(lastValues),
            MarketIndex = _marketIndex,
            Date = DateTime.Now,
        });

        _indexDbContext.Macd.Add(new Macd()
        {
            MarketIndexId = _marketIndex.Id,
            Value = Macd.Calculate(lastValues),
            MarketIndex = _marketIndex,
            Date = DateTime.Now,
        });

        _indexDbContext.MacdSignal.Add(new MacdSignal()
        {
            MarketIndexId = _marketIndex.Id,
            Value = MacdSignal.Calculate([.. _indexDbContext.Macd.OrderBy(x => x.Date).Take(_macdSignalLimit)], _macdSignalLimit),
            MarketIndex = _marketIndex,
            Date = DateTime.Now,
        });

        _indexDbContext.SaveChanges();
    }
}
