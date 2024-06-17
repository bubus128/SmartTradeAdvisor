using Microsoft.Extensions.Options;
using SmartTradeAdvisor.Core.Configurations;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;
using SmartTradeAdvisor.Data.Entities.Indexes;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public class IndexCalculator(IOptions<AlgorithmsConfiguration> algorithmConfiguration, IndexDbContext indexDbContext, MarketIndex marketIndex) : IIndexCalculator
{
    private readonly int _macdSignalLimit = algorithmConfiguration.Value.MacdSignalPeriod;
    private readonly int _memoryLimit = algorithmConfiguration.Value.ValuesMemoryLimit;

    public async Task CalculateAll(MarketIndexValue marketIndexValue)
    {
        var lastValues = marketIndex.MarketIndexValues.OrderByDescending(x => x.Date).Take(_memoryLimit).ToList();
        if (lastValues.Count < _memoryLimit)
        {
            return;
        }

        indexDbContext.Adx.Add(new Adx()
        {
            MarketIndexId = marketIndex.Id,
            Value = Adx.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        indexDbContext.PositiveDis.Add(new PositiveDi()
        {
            MarketIndexId = marketIndex.Id,
            Value = PositiveDi.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        indexDbContext.NegativeDis.Add(new NegativeDi()
        {
            MarketIndexId = marketIndex.Id,
            Value = NegativeDi.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        indexDbContext.Cmo.Add(new Cmo()
        {
            MarketIndexId = marketIndex.Id,
            Value = Cmo.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        indexDbContext.Mfi.Add(new Mfi()
        {
            MarketIndexId = marketIndex.Id,
            Value = Mfi.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        indexDbContext.Rsi.Add(new Rsi()
        {
            MarketIndexId = marketIndex.Id,
            Value = Rsi.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        indexDbContext.Ult.Add(new Ult()
        {
            MarketIndexId = marketIndex.Id,
            Value = Ult.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        indexDbContext.Macd.Add(new Macd()
        {
            MarketIndexId = marketIndex.Id,
            Value = Macd.Calculate(lastValues),
            MarketIndex = marketIndex,
            Date = DateTime.Now,
        });

        if (indexDbContext.Macd.Count() > 9)
        {
            indexDbContext.MacdSignal.Add(new MacdSignal()
            {
                MarketIndexId = marketIndex.Id,
                Value = MacdSignal.Calculate([.. indexDbContext.Macd.OrderByDescending(x => x.Date).Take(_macdSignalLimit)], _macdSignalLimit),
                MarketIndex = marketIndex,
                Date = DateTime.Now,
            });
        }
        await indexDbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
