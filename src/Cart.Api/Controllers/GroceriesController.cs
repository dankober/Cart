using Cart.Api.Models;
using Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroceriesController : ControllerBase
{
    private readonly IGroceryService _groceryService;

    public GroceriesController(IGroceryService groceryService)
    {
        _groceryService = groceryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroceryDto>>> GetAll([FromQuery] int? categoryId = null)
    {
        var groceries = await _groceryService.GetAllAsync(categoryId);
        return Ok(groceries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryDto>> GetById(int id)
    {
        var grocery = await _groceryService.GetByIdAsync(id);
        return grocery is null ? NotFound() : Ok(grocery);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<GroceryDto>>> Search([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return Ok(Array.Empty<GroceryDto>());
        }
        var groceries = await _groceryService.SearchAsync(q);
        return Ok(groceries);
    }

    [HttpPost]
    public async Task<ActionResult<GroceryDto>> Create(CreateGroceryRequest request)
    {
        var grocery = await _groceryService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = grocery.Id }, grocery);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GroceryDto>> Update(int id, UpdateGroceryRequest request)
    {
        var grocery = await _groceryService.UpdateAsync(id, request);
        return grocery is null ? NotFound() : Ok(grocery);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _groceryService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
