using Microsoft.AspNetCore.Mvc;
using SmartTradeAdvisor.Data.DbContexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartTradeAdvisor.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WalletsController(IndexDbContext indexDbContext) : ControllerBase
{
    // GET: api/<ValuesController>
    [HttpGet]
    public OkObjectResult Get()
    {
        return Ok(indexDbContext.Wallets.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var item = indexDbContext.Wallets.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }
}
