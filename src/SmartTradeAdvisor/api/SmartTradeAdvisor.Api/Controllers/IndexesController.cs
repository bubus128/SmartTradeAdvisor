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

    [HttpGet("{id}/values")]
    public IActionResult GetValues(string id)
    {
        var values = indexDbContext.MarketIndexValues.Where(x => x.MarketIndexId == id);
        if (values == null)
        {
            return NotFound();
        }
        return Ok(values);
    }

    [HttpGet("{id}/values/latest")]
    public IActionResult GetLatestValue(string id)
    {
        var value = indexDbContext.MarketIndexValues.OrderBy(x => x.Date).LastOrDefault(x => x.MarketIndexId == id);
        if (value == null)
        {
            return NotFound();
        }
        return Ok(value);
    }

    [HttpPost]
    public IActionResult Post(MarketIndexDto index)
    {
        try
        {
            var marketIndex = mapper.Map<MarketIndex>(index);
            indexService.AddIndex(marketIndex);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok();
    }

    [HttpPost("{indexName}")]
    public IActionResult Post(string indexName, List<MarketIndexValueDto> indexes)
    {
        foreach (var index in indexes)
        {
            var marketIndex = indexDbContext.MarketIndexes.Find(indexName);
            if (marketIndex == null)
            {
                return NotFound();
            }

            try
            {
                var indexValue = mapper.Map<MarketIndexValue>(index);
                indexValue.MarketIndexId = marketIndex.Id;
                indexService.AddValue(indexValue);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        return Ok();
    }
}
