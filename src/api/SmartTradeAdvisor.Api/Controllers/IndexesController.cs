using Microsoft.AspNetCore.Mvc;
using SmartTradeAdvisor.Data.DbContexts;

namespace SmartTradeAdvisor.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IndexesController(IndexDbContext indexDbContext) : ControllerBase
{
    private readonly IndexDbContext _indexDbContext = indexDbContext;

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_indexDbContext.MarketIndexes.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var item = _indexDbContext.MarketIndexes.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }
}
