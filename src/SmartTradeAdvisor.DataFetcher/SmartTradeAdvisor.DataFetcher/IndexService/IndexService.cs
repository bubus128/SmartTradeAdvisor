using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SmartTradeAdvisor.DataFetcher.Requests;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartTradeAdvisor.DataFetcher.IndexService;

public class IndexService(HttpClient client, IConfiguration configuration) : IIndexService
{
    private string? _binanceApiKey = configuration["Keys:BinanceApiKey"];
    private string? _nasdaqApiKey = configuration["Keys:NasdaqApiKey"];
    private const string SmartTradeAdvisorBaseUrl = "https://localhost:44346";
    private const string BinanceApiUrl = "https://api.binance.com/api/v3/klines";

    public DateTime? GetLatestRecordDate(string symbol)
    {
        var task = Task.Run(() => client.GetAsync($"{SmartTradeAdvisorBaseUrl}/indexes/{symbol}/values/latest"));
        task.Wait();
        var response = task.Result;

        if (response.IsSuccessStatusCode)
        {
            var task2 = Task.Run(() => response.Content.ReadAsStringAsync());
            task2.Wait();
            var responseData = task2.Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var latestRecord = JsonSerializer.Deserialize<MarketIndexValue>(responseData, options);
            return latestRecord?.Date;
        }

        if (response.StatusCode.Equals(HttpStatusCode.NotFound))
        {
            DateTime dateFiveYearsAgo = DateTime.Now.AddYears(-5);
            return dateFiveYearsAgo;
        }
        return null;
    }

    public bool CheckSymbolExists(string symbol)
    {
        var task = Task.Run(() => client.GetAsync($"{SmartTradeAdvisorBaseUrl}/indexes/{symbol}"));
        task.Wait();
        var response = task.Result;
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

    public void CreateSymbol(MarketIndex marketIndex)
    {
        try
        {
            var indexString = JsonSerializer.Serialize(marketIndex);
            var task = Task.Run(() => client.PostAsync($"{SmartTradeAdvisorBaseUrl}/indexes", new StringContent(indexString, Encoding.UTF8, "application/json")));
            task.Wait();
            var response = task.Result;
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

    public List<MarketIndexValue> FetchDataFromNasdaq(string symbol)
    {
        string csvFilePath = $"D:\\MasterThesis\\Data\\{symbol}.csv";
        var marketIndexValues = new List<MarketIndexValue>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, config))
        {
            // Read the CSV header
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var marketIndexValue = new MarketIndexValue
                {
                    Date = DateTime.ParseExact(csv.GetField("Date"), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ClosingValue = double.Parse(csv.GetField("Close/Last").Trim('$'), CultureInfo.InvariantCulture),
                    Volume = double.Parse(csv.GetField("Volume"), CultureInfo.InvariantCulture),
                    LowValue = double.Parse(csv.GetField("Low").Trim('$'), CultureInfo.InvariantCulture),
                    HighValue = double.Parse(csv.GetField("High").Trim('$'), CultureInfo.InvariantCulture)
                };

                marketIndexValues.Add(marketIndexValue);
            }
        }

        return marketIndexValues;
    }

    public List<MarketIndexValue> FetchDataFromBinance(string symbol, DateTime startDate, DateTime endDate)
    {
        string interval = "1d"; // You can change the interval as needed
        string startTime = ((DateTimeOffset)startDate).ToUnixTimeMilliseconds().ToString();
        string endTime = ((DateTimeOffset)endDate).ToUnixTimeMilliseconds().ToString();

        string url = $"{BinanceApiUrl}?symbol={symbol}&interval={interval}&startTime={startTime}&endTime={endTime}";

        var task = Task.Run(() => client.GetAsync(url));
        task.Wait();
        var response = task.Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Unable to fetch data from Binance API.");
        }

        var task2 = Task.Run(() => response.Content.ReadAsStringAsync());
        task2.Wait();
        string responseBody = task2.Result;
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

    public void SendValuesToSmartTradeAdvisor(List<MarketIndexValue> marketIndexValues, string symbol)
    {
        var data = JsonSerializer.Serialize(marketIndexValues.OrderBy(x => x.Date));
        var content = new StringContent(data, Encoding.UTF8, "application/json");
        for (int i = 0; i < 10; i++)
        {
            var task = Task.Run(() => client.PostAsync($"{SmartTradeAdvisorBaseUrl}/indexes/{symbol}", content));
            task.Wait();
            var response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                break;
            }
            Task.Delay(1000);
        }


    }

    public void SendValueToSmartTradeAdvisor(MarketIndexValue marketIndexValue, string symbol)
    {
        throw new NotImplementedException();
    }
}

