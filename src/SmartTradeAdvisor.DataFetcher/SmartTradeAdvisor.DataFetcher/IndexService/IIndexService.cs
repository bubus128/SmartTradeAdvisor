using SmartTradeAdvisor.DataFetcher.Requests;

namespace SmartTradeAdvisor.DataFetcher.IndexService;

public interface IIndexService
{
    public Task<DateTime?> GetLatestRecordDate(string symbol);

    public Task<bool> CheckSymbolExists(string symbol);

    public Task CreateSymbol(MarketIndex symbol);

    public Task<List<MarketIndexValue>> FetchDataFromNasdaq(string symbol, DateTime startDate, DateTime endDate);

    public Task<List<MarketIndexValue>> FetchDataFromBinance(string symbol, DateTime startDate, DateTime endDate);


    public Task SendValuesToSmartTradeAdvisor(List<MarketIndexValue> marketIndexValues, string symbol);

    public Task SendValueToSmartTradeAdvisor(MarketIndexValue marketIndexValue, string symbol);

    public string ParseData(string nasdaqData);
}
