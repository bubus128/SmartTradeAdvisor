using Microsoft.AspNetCore.Mvc;
using SmartTradeAdvisor.Data.Interfaces;

namespace SmartTradeAdvisor.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IndexesController : ControllerBase
{
    private readonly IIndexesRepository _indexesRepository;
    public IndexesController(IIndexesRepository indexesRepository)
    {
        _indexesRepository = indexesRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_indexesRepository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var item = _indexesRepository.GetById(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }
}
