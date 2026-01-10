using Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    private readonly IHelloService _helloService;

    public HelloController(IHelloService helloService)
    {
        _helloService = helloService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? name = null)
    {
        var message = _helloService.GetGreeting(name);
        return Ok(new { message });
    }
}
