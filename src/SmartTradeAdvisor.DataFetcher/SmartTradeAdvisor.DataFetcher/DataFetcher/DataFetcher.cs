using Microsoft.Extensions.Configuration;
using SmartTradeAdvisor.DataFetcher.IndexService;
using SmartTradeAdvisor.DataFetcher.Requests;


namespace SmartTradeAdvisor.DataFetcher.DataFetcher;

public class DataFetcher(IIndexService indexService, IConfiguration configuration) : IDataFetcher
{
    private readonly List<string> _nasdaqSymbolsList = configuration.GetRequiredSection("Symbols:NasdaqSymbols").Get<List<string>>();
    private readonly List<string> _binanceSymbolsList = configuration.GetRequiredSection("Symbols:BinanceSymbols").Get<List<string>>();


    public async Task Init()
    {
        foreach (var symbol in _nasdaqSymbolsList)
        {
            if (!await indexService.CheckSymbolExists(symbol))
            {
                var marketIndex = new MarketIndex()
                {
                    Name = symbol,
                    Description = string.Empty
                };
                await indexService.CreateSymbol(marketIndex);
            }
        }
        foreach (var symbol in _binanceSymbolsList)
        {
            if (!await indexService.CheckSymbolExists(symbol))
            {
                var marketIndex = new MarketIndex()
                {
                    Name = symbol,
                    Description = string.Empty
                };
                await indexService.CreateSymbol(marketIndex);
            }
        }
    }

    public async Task FetchAndSendData()
    {

        try
        {
            /*
            foreach (var symbol in _nasdaqSymbolsList)
            {
                var latestRecordDate = await indexService.GetLatestRecordDate(symbol);
                if (latestRecordDate != null)
                {
                    DateTime endDate = DateTime.Today.AddDays(-1); // yesterday's date

                    DateTime currentStartDate = (DateTime)latestRecordDate;
                    DateTime currentEndDate = currentStartDate.AddMonths(1);
                    while (currentStartDate < endDate)
                    {
                        var nasdaqData = await indexService.FetchDataFromNasdaq(symbol, currentStartDate, currentEndDate);
                        indexService.SendValuesToSmartTradeAdvisor(nasdaqData, symbol);
                        //await indexService.SendValueToSmartTradeAdvisor(parsedData);
                        Console.WriteLine("Data fetched and sent successfully.");

                        currentStartDate = currentStartDate.AddMonths(1);
                        currentEndDate = currentEndDate.AddMonths(1);
                    }

                }
            }
            */
            foreach (var symbol in _binanceSymbolsList)
            {
                var latestRecordDate = await indexService.GetLatestRecordDate(symbol);
                if (latestRecordDate != null)
                {
                    DateTime endDate = DateTime.Today.AddDays(-1); // yesterday's date

                    DateTime currentStartDate = (DateTime)latestRecordDate;
                    DateTime currentEndDate = currentStartDate.AddMonths(1);
                    while (currentStartDate < endDate)
                    {
                        var nasdaqData = await indexService.FetchDataFromBinance(symbol, currentStartDate, currentEndDate);
                        indexService.SendValuesToSmartTradeAdvisor(nasdaqData, symbol);
                        //await indexService.SendValueToSmartTradeAdvisor(parsedData);
                        Console.WriteLine("Data fetched and sent successfully.");

                        currentStartDate = currentStartDate.AddMonths(1);
                        currentEndDate = currentEndDate.AddMonths(1);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

