using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Core.IndexService;
public interface IIndexService
{
    public void AddIndex(MarketIndex index);
    public void AddValue(MarketIndexValue indexValue);
}
