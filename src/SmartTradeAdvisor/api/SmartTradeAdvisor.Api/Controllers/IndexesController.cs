using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartTradeAdvisor.Api.Mapper;
using SmartTradeAdvisor.Core.IndexService;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;

namespace SmartTradeAdvisor.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IndexesController(IndexDbContext indexDbContext, IIndexService indexService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(indexDbContext.MarketIndexes.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var item = indexDbContext.MarketIndexes.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet("{id}/values/{months}")]
    public IActionResult GetValues(string id, int months)
    {
        var values = indexDbContext.MarketIndexValues
            .Where(x => x.MarketIndexId == id && x.Date >= DateTime.Now.AddMonths(-months))
            .Select(x =>
            new ValueResponse()
            {
                Date = x.Date,
                Price = x.Price
            });
        if (values == null)
        {
            return NotFound();
        }
        return Ok(values);
    }

    [HttpGet("{id}/values/latest")]
    public IActionResult GetLatestValue(string id)
    {
        var index = indexDbContext.MarketIndexes.Find(id);
        if (index == null)
        {
            return NotFound();
        }
        var value = index.MarketIndexValues.MaxBy(x => x.Date);
        if (value == null)
        {
            return NotFound();
        }
        var marketIndexResponse = mapper.Map<MarketIndexValueDto>(value);
        return Ok(marketIndexResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Post(MarketIndexDto index)
    {
        try
        {
            var marketIndex = mapper.Map<MarketIndex>(index);
            await indexService.AddIndex(marketIndex).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
        return Ok();
    }

    [HttpPost("{indexName}")]
    public async Task<IActionResult> Post(string indexName, List<MarketIndexValueDto> indexes)
    {
        foreach (var index in indexes)
        {
            var marketIndex = await indexDbContext.MarketIndexes.FindAsync(indexName).ConfigureAwait(false);
            if (marketIndex == null)
            {
                return NotFound();
            }

            var lastValue = marketIndex.MarketIndexValues.MaxBy(x => x.Date);
            if (lastValue != null && lastValue.Date >= index.Date)
            {
                continue;
            }
            try
            {
                var indexValue = mapper.Map<MarketIndexValue>(index);
                indexValue.MarketIndexId = marketIndex.Id;
                await indexService.AddValue(indexValue).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        return Ok();
    }
}
