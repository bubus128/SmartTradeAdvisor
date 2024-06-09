using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SmartTradeAdvisor.DataFetcher.Requests;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartTradeAdvisor.DataFetcher.IndexService;

public class IndexService(HttpClient client, IConfiguration configuration) : IIndexService
{
    private string? _binanceApiKey = configuration["Keys:BinanceApiKey"];
    private string? _nasdaqApiKey = configuration["Keys:NasdaqApiKey"];
    private const string SmartTradeAdvisorBaseUrl = "http://localhost:8080";
    private const string BinanceApiUrl = "https://api.binance.com/api/v3/klines";

    public async Task<DateTime?> GetLatestRecordDate(string symbol)
    {
        var response = await client.GetAsync($"{SmartTradeAdvisorBaseUrl}/indexes/{symbol}/values/latest");
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var latestRecord = JsonSerializer.Deserialize<MarketIndexValue>(responseData);
            return latestRecord?.Date;
        }

        if (response.StatusCode.Equals(HttpStatusCode.NotFound))
        {
            DateTime dateFiveYearsAgo = DateTime.Now.AddYears(-5);
            return dateFiveYearsAgo;
        }
        return null;
    }

    public async Task<bool> CheckSymbolExists(string symbol)
    {
        var response = await client.GetAsync($"{SmartTradeAdvisorBaseUrl}/indexes/{symbol}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
        {
            return false;
        }
        throw new Exception();
    }

    public async Task CreateSymbol(MarketIndex marketIndex)
    {
        try
        {
            var indexString = JsonSerializer.Serialize(marketIndex);
            var response = await client.PostAsync($"{SmartTradeAdvisorBaseUrl}/indexes", new StringContent(indexString, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occured while creating a symbol {marketIndex.Name}: {ex.Message}");
        }
    }

    public async Task<List<MarketIndexValue>> FetchDataFromNasdaq(string symbol, DateTime startDate, DateTime endDate)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _nasdaqApiKey);
        var response = await client.GetStringAsync($"https://api.nasdaq.com/api/v1/{symbol}/values?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}");

        var parsed = new List<MarketIndexValue>();
        return parsed;
    }

    public async Task<List<MarketIndexValue>> FetchDataFromBinance(string symbol, DateTime startDate, DateTime endDate)
    {
        string interval = "1d"; // You can change the interval as needed
        string startTime = ((DateTimeOffset)startDate).ToUnixTimeMilliseconds().ToString();
        string endTime = ((DateTimeOffset)endDate).ToUnixTimeMilliseconds().ToString();

        string url = $"{BinanceApiUrl}?symbol={symbol}&interval={interval}&startTime={startTime}&endTime={endTime}";

        HttpResponseMessage response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Unable to fetch data from Binance API.");
        }

        string responseBody = await response.Content.ReadAsStringAsync();
        JArray klineData = JArray.Parse(responseBody);

        List<MarketIndexValue> marketIndexValues = new List<MarketIndexValue>();

        foreach (var kline in klineData)
        {
            MarketIndexValue marketIndexValue = new MarketIndexValue
            {
                Date = DateTimeOffset.FromUnixTimeMilliseconds((long)kline[0]).DateTime,
                LowValue = (double)kline[3],
                HighValue = (double)kline[2],
                ClosingValue = (double)kline[4],
                Volume = (double)kline[5]
            };

            marketIndexValues.Add(marketIndexValue);
        }

        return marketIndexValues;
    }

    public string ParseData(string nasdaqData)
    {
        // Implement your parsing logic here
        return $"Parsed Data from Nasdaq: {nasdaqData}";
    }

    public async Task SendValuesToSmartTradeAdvisor(List<MarketIndexValue> marketIndexValues, string symbol)
    {
        var data = JsonSerializer.Serialize(marketIndexValues);
        var content = new StringContent(data, Encoding.UTF8, "application/json");
        await client.PostAsync($"{SmartTradeAdvisorBaseUrl}/indexes/{symbol}", content);
    }

    public async Task SendValueToSmartTradeAdvisor(MarketIndexValue marketIndexValue, string symbol)
    {
        var data = JsonSerializer.Serialize(marketIndexValue);
        var content = new StringContent(data, Encoding.UTF8, "application/json");
        await client.PostAsync($"{SmartTradeAdvisorBaseUrl}/indexes/{symbol}", content);
    }
}

