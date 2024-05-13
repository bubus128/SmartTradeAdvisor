using System.Globalization;
using Microsoft.Extensions.Configuration;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexCalculators;
public class IndexCalculator : IIndexCalculator
{
    private readonly LimitedList<double> _highs;
    private readonly LimitedList<double> _lows;
    private double _previousPositiveDi;
    private double _previousNegativeDi;
    private readonly int _period;
    private readonly double _smoothingFactor;
    private readonly IndexDbContext _indexDbContext;
    private readonly MarketIndex _marketIndex;

    public IndexCalculator(IConfiguration configuration, IndexDbContext indexDbContext, MarketIndex marketIndex)
    {
        _marketIndex = marketIndex;
        _indexDbContext = indexDbContext;
        _period = int.Parse(configuration["period"] ?? throw new InvalidOperationException(), new CultureInfo("en-US"));
        _highs = new(_period);
        _lows = new(_period);
        _previousPositiveDi = 0;
        _previousNegativeDi = 0;
        _smoothingFactor = 2.0 / (_period + 1);
        _indexDbContext = indexDbContext;
    }

    public void CalculateAll(double high, double low)
    {
        _highs.Add(high);
        _lows.Add(low);

        var positiveDi = new PositiveDi(_highs, _lows, _previousPositiveDi, _smoothingFactor)
        {
            MarketIndexId = _marketIndex.Id
        };
        var negativeDi = new NegativeDi(_highs, _lows, _previousNegativeDi, _smoothingFactor)
        {
            MarketIndexId = _marketIndex.Id
        };

        _previousPositiveDi = positiveDi.Value;
        _previousNegativeDi = negativeDi.Value;

        _indexDbContext.CalculatedIndexes.Add(positiveDi);
        _indexDbContext.CalculatedIndexes.Add(negativeDi);

    }

}
