using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Data.Interfaces;

public interface IIndexRepository
{
    IEnumerable<MarketIndex> GetAll();
    MarketIndex GetById(Guid id);
    void Add(MarketIndex entity);
    void Update(MarketIndex entity);
    void Delete(MarketIndex entity);
    void Save();
}