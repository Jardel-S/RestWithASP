using Microsoft.AspNetCore.Mvc;
using RestWithASP.Model;
using RestWithASP.Services;

namespace RestWithASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    
    private readonly ILogger<PersonController> _logger;
    private IPersonService _personSevice;

    public PersonController(ILogger<PersonController> logger, IPersonService personService)
    {
        _logger = logger;
        _personSevice= personService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_personSevice.FindAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var person = _personSevice.FindById(id);
        if (person == null) return NotFound();
        return Ok(person);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        if (person == null) return BadRequest();
        return Ok(_personSevice.Create(person));
    }

    [HttpPut]
    public IActionResult Put([FromBody] Person person)
    {
        if (person == null) return BadRequest();
        return Ok(_personSevice.Create(person));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _personSevice.Delete(id);
        return NoContent();
    }
}
