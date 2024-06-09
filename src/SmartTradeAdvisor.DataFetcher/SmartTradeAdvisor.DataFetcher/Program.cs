using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartTradeAdvisor.DataFetcher.DataFetcher;
using SmartTradeAdvisor.DataFetcher.IndexService;

class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<IDataFetcher, DataFetcher>();
        builder.Services.AddScoped<IIndexService, IndexService>();
        builder.Services.AddHttpClient<IIndexService, IndexService>();

        var host = builder.Build();

        using (var serviceScope = host.Services.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;

            var dataFetcher = serviceProvider.GetRequiredService<IDataFetcher>();

            await dataFetcher.Init();
            await dataFetcher.FetchAndSendData();

            using (var timer = new System.Timers.Timer(60000 * 60)) // Set the interval (e.g., 60000ms = 1 minute)
            {
                timer.Elapsed += async (sender, e) =>
                {
                    // Create a new scope for each timer tick
                    using var scope = host.Services.CreateScope();
                    var scopedProvider = scope.ServiceProvider;

                    var scopedDataFetcher = scopedProvider.GetRequiredService<IDataFetcher>();
                    await scopedDataFetcher.FetchAndSendData(); // Example symbol: aapl
                };

                timer.Start();

                Console.WriteLine("Data fetcher started. Press Enter to exit...");
                Console.ReadLine();

                // Stop the timer and wait for any running tasks to complete
                timer.Stop();
            }
        }

        await host.RunAsync();
    }
}