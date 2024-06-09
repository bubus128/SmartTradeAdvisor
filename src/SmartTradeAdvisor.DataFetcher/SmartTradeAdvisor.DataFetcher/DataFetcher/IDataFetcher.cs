namespace SmartTradeAdvisor.DataFetcher.DataFetcher;
public interface IDataFetcher
{
    public Task Init();
    public Task FetchAndSendData();
}

