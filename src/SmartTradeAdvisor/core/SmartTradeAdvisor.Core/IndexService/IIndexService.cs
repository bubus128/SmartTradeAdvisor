using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexService;
public interface IIndexService
{
    public Task AddIndex(MarketIndex index);
    public Task AddValue(MarketIndexValue indexValue);
}
