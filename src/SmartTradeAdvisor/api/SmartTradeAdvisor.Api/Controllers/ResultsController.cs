using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTradeAdvisor.Api.Mapper;
using SmartTradeAdvisor.Data.DbContexts;

namespace SmartTradeAdvisor.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ResultsController(IndexDbContext indexDbContext) : ControllerBase
{
    private static List<string> _strategies = ["adx", "cmo", "macd", "mfi", "rsi", "ult"];

    [HttpGet("{symbol}/{months}")]
    public IActionResult Get(string symbol, int months)
    {
        var item = indexDbContext.MarketIndexes.Find(symbol);
        if (item == null)
        {
            return NotFound();
        }

        List<ResultResponse> results = [];
        foreach (var strategy in _strategies)
        {
            var wallet =
                indexDbContext.Wallets.Include(x => x.Transactions).FirstOrDefault(x => x.Strategy == strategy && x.MarketIndexId == symbol);
            if (wallet == null)
            {
                return NotFound();
            }

            ResultResponse result = new()
            {
                Strategy = strategy,
                Transactions = wallet.Transactions
                    .Where(x => x.Date >= DateTime.Now.AddMonths(-months))
                    .Select(x => new TransactionResponse()
                    {
                        Date = x.Date,
                        Seal = x.Seal
                    })
                    .OrderBy(x => x.Date).ToList()
            };
            results.Add(result);
        }
        return Ok(results);
    }

    [HttpGet("{symbol}/{strategy}/{months}")]
    public IActionResult Get(string symbol, string strategy, int months)
    {
        var item = indexDbContext.MarketIndexes.Find(symbol);
        if (item == null)
        {
            return NotFound();
        }

        var wallet =
            indexDbContext.Wallets.Include(x => x.Transactions).FirstOrDefault(x => x.Strategy == strategy && x.MarketIndexId == symbol);
        if (wallet == null)
        {
            return NotFound();
        }


        var transactions = wallet.Transactions
            .Where(x => x.Date >= DateTime.Now.AddMonths(-months))
            .Select(x =>
                new TransactionResponse()
                { Date = x.Date, Seal = x.Seal });

        if (transactions == null)
        {
            return NotFound();
        }

        return Ok(transactions);
    }
}
