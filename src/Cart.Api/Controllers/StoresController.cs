using Cart.Api.Models;
using Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoreDto>>> GetAll()
    {
        var stores = await _storeService.GetAllAsync();
        return Ok(stores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreDto>> GetById(int id)
    {
        var store = await _storeService.GetByIdAsync(id);
        return store is null ? NotFound() : Ok(store);
    }

    [HttpGet("{id}/detail")]
    public async Task<ActionResult<StoreDetailDto>> GetDetail(int id)
    {
        var store = await _storeService.GetDetailAsync(id);
        return store is null ? NotFound() : Ok(store);
    }

    [HttpPost]
    public async Task<ActionResult<StoreDto>> Create(CreateStoreRequest request)
    {
        var store = await _storeService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = store.Id }, store);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StoreDto>> Update(int id, UpdateStoreRequest request)
    {
        var store = await _storeService.UpdateAsync(id, request);
        return store is null ? NotFound() : Ok(store);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _storeService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
