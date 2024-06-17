using SmartTradeAdvisor.ChartDrawer.Models;

namespace SmartTradeAdvisor.ChartDrawer.Services;
public interface IDataService
{
    public Task<List<IndexValueDto>> GetIndexValuesAsync(string index, int months);

    public Task<List<TransactionDto>> GetTransactionsAsync(string index, string strategy, int months);
}

