using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;
using SmartTradeAdvisor.Data.Entities.Wallet;

namespace SmartTradeAdvisor.Core.WalletCalculator;

public class WalletService : IWalletService
{
    private static List<string> _strategies = ["adx", "cmo", "macd", "mfi", "rsi", "ult"];
    private readonly IndexDbContext _indexDbContext;
    private readonly Dictionary<string, Func<string, bool>> _strategiesDelegates;

    public WalletService(IndexDbContext indexDbContext)
    {
        _indexDbContext = indexDbContext;

        _strategiesDelegates = new Dictionary<string, Func<string, bool>>
        {
            { "adx", AdxStrategy },
            { "cmo", CmoStrategy },
            { "macd", MacdStrategy },
            { "mfi", MfiStrategy },
            { "rsi", RsiStrategy },
            { "ult", UltStrategy }
        };
    }

    public async Task CalculateAll(MarketIndexValue value)
    {
        foreach (var strategy in _strategies)
        {
            var wallet = await _indexDbContext.Wallets
                .Include(x => x.MarketIndex)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(x => (x.Strategy == strategy && x.MarketIndex.Name == value.MarketIndexId)).ConfigureAwait(false);
            if (wallet is null)
            {
                wallet = new Wallet()
                {
                    MarketIndex = value.MarketIndex,
                    MarketIndexId = value.MarketIndexId,
                    Strategy = strategy,
                    Transactions = []
                };
                await _indexDbContext.Wallets.AddAsync(wallet).ConfigureAwait(false);
                await _indexDbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            var lastTransaction = wallet.Transactions.MaxBy(x => x.Date);

            var strategyResult = _strategiesDelegates[strategy].Invoke(value.MarketIndexId);
            if (!strategyResult)
            {
                int a = 1;
                a++;
            }

            // Buy
            if (strategyResult && (lastTransaction == null || lastTransaction.Seal))
            {
                var transaction = new Transaction()
                {
                    Date = value.Date,
                    Seal = false,
                    Wallet = wallet,
                    WalletId = wallet.Id
                };
                await _indexDbContext.Transactions.AddAsync(transaction).ConfigureAwait(false);
                await _indexDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Sel
            else if (!strategyResult && (lastTransaction != null && !lastTransaction.Seal))
            {
                var transaction = new Transaction()
                {
                    Date = value.Date,
                    Seal = true,
                    Wallet = wallet,
                    WalletId = wallet.Id
                };
                await _indexDbContext.Transactions.AddAsync(transaction).ConfigureAwait(false);
                await _indexDbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            await _indexDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
    private bool AdxStrategy(string marketIndexId)
    {
        var pDi = _indexDbContext.PositiveDis
            .Where(a => a.MarketIndexId == marketIndexId)
            .OrderByDescending(a => a.Date)
            .FirstOrDefault()?.Value;
        var nDi = _indexDbContext.NegativeDis
            .Where(a => a.MarketIndexId == marketIndexId)
            .OrderByDescending(a => a.Date)
            .FirstOrDefault()?.Value;
        var adxValue = _indexDbContext.Adx
            .Where(a => a.MarketIndexId == marketIndexId)
            .OrderByDescending(a => a.Date)
            .FirstOrDefault()?.Value;

        return adxValue is > 25 && pDi > nDi;
    }

    private bool CmoStrategy(string marketIndexId)
    {
        var cmoValue = _indexDbContext.Cmo
            .Where(c => c.MarketIndexId == marketIndexId)
            .OrderByDescending(c => c.Date)
            .FirstOrDefault()?.Value;

        return cmoValue is > 0;
    }

    private bool MacdStrategy(string marketIndexId)
    {
        var macdValue = _indexDbContext.Macd
            .Where(m => m.MarketIndexId == marketIndexId)
            .OrderByDescending(m => m.Date)
            .FirstOrDefault()?.Value;

        var macdSignalValue = _indexDbContext.MacdSignal
            .Where(ms => ms.MarketIndexId == marketIndexId)
            .OrderByDescending(ms => ms.Date)
            .FirstOrDefault()?.Value;

        return macdSignalValue < macdValue;
    }

    private bool MfiStrategy(string marketIndexId)
    {
        var mfiValue = _indexDbContext.Mfi
            .Where(m => m.MarketIndexId == marketIndexId)
            .OrderByDescending(m => m.Date)
            .FirstOrDefault()?.Value;

        return mfiValue is > 50;
    }

    private bool RsiStrategy(string marketIndexId)
    {
        var rsiValue = _indexDbContext.Rsi
            .Where(r => r.MarketIndexId == marketIndexId)
            .OrderByDescending(r => r.Date)
            .FirstOrDefault()?.Value;

        return rsiValue is < 30;
    }

    private bool UltStrategy(string marketIndexId)
    {
        var ultValue = _indexDbContext.Ult
            .Where(u => u.MarketIndexId == marketIndexId)
            .OrderByDescending(u => u.Date)
            .FirstOrDefault()?.Value;

        return ultValue is < 50;
    }
}
