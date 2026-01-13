using Cart.Api.Models;
using Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/stores/{storeId}/[controller]")]
public class AislesController : ControllerBase
{
    private readonly IAisleService _aisleService;

    public AislesController(IAisleService aisleService)
    {
        _aisleService = aisleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AisleDto>>> GetByStore(int storeId)
    {
        var aisles = await _aisleService.GetByStoreAsync(storeId);
        return Ok(aisles);
    }

    [HttpGet("{aisleId}")]
    public async Task<ActionResult<AisleDto>> GetById(int storeId, int aisleId)
    {
        var aisle = await _aisleService.GetByIdAsync(storeId, aisleId);
        return aisle is null ? NotFound() : Ok(aisle);
    }

    [HttpPost]
    public async Task<ActionResult<AisleDto>> Create(int storeId, CreateAisleRequest request)
    {
        var aisle = await _aisleService.CreateAsync(storeId, request);
        return aisle is null
            ? NotFound()
            : CreatedAtAction(nameof(GetById), new { storeId, aisleId = aisle.Id }, aisle);
    }

    [HttpPut("{aisleId}")]
    public async Task<ActionResult<AisleDto>> Update(int storeId, int aisleId, UpdateAisleRequest request)
    {
        var aisle = await _aisleService.UpdateAsync(storeId, aisleId, request);
        return aisle is null ? NotFound() : Ok(aisle);
    }

    [HttpDelete("{aisleId}")]
    public async Task<IActionResult> Delete(int storeId, int aisleId)
    {
        var deleted = await _aisleService.DeleteAsync(storeId, aisleId);
        return deleted ? NoContent() : NotFound();
    }
}
