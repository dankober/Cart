using Cart.Api.Models;
using Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetAll([FromQuery] bool? isCompleted = null)
    {
        var items = await _itemService.GetAllAsync(isCompleted);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetById(int id)
    {
        var item = await _itemService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> Create(CreateItemRequest request)
    {
        var item = await _itemService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ItemDto>> Update(int id, UpdateItemRequest request)
    {
        var item = await _itemService.UpdateAsync(id, request);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _itemService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("clear-completed")]
    public async Task<ActionResult<object>> ClearCompleted()
    {
        var count = await _itemService.ClearCompletedAsync();
        return Ok(new { cleared = count });
    }
}
