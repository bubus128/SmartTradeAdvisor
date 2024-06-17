using Newtonsoft.Json;
using SmartTradeAdvisor.ChartDrawer.Models;

namespace SmartTradeAdvisor.ChartDrawer.Services;

public class DataService(HttpClient httpClient) : IDataService
{
    public async Task<List<IndexValueDto>> GetIndexValuesAsync(string index, int months)
    {
        var response = await httpClient.GetStringAsync($"https://localhost:44346/indexes/{index}/values/{months}");
        return JsonConvert.DeserializeObject<List<IndexValueDto>>(response);
    }

    public async Task<List<TransactionDto>> GetTransactionsAsync(string index, string strategy, int months)
    {
        try
        {
            var response = await httpClient.GetStringAsync($"https://localhost:44346/results/{index}/{strategy}/{months}");
            var result = JsonConvert.DeserializeObject<dynamic>(response);
            return JsonConvert.DeserializeObject<List<TransactionDto>>(result.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

    }
}

