using SmartTradeAdvisor.DataFetcher.Requests;

namespace SmartTradeAdvisor.DataFetcher.IndexService;

public interface IIndexService
{
    public DateTime? GetLatestRecordDate(string symbol);

    public bool CheckSymbolExists(string symbol);

    public void CreateSymbol(MarketIndex symbol);

    public List<MarketIndexValue> FetchDataFromNasdaq(string symbol);

    public List<MarketIndexValue> FetchDataFromBinance(string symbol, DateTime startDate, DateTime endDate);


    public void SendValuesToSmartTradeAdvisor(List<MarketIndexValue> marketIndexValues, string symbol);

    public void SendValueToSmartTradeAdvisor(MarketIndexValue marketIndexValue, string symbol);

    public string ParseData(string nasdaqData);
}
