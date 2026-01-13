using Cart.Api.Models;
using Cart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/stores/{storeId}/category-mappings")]
public class CategoryAisleMappingsController : ControllerBase
{
    private readonly ICategoryAisleMappingService _mappingService;

    public CategoryAisleMappingsController(ICategoryAisleMappingService mappingService)
    {
        _mappingService = mappingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryAisleMappingDto>>> GetByStore(int storeId)
    {
        var mappings = await _mappingService.GetByStoreAsync(storeId);
        return Ok(mappings);
    }

    [HttpGet("{mappingId}")]
    public async Task<ActionResult<CategoryAisleMappingDto>> GetById(int storeId, int mappingId)
    {
        var mapping = await _mappingService.GetByIdAsync(storeId, mappingId);
        return mapping is null ? NotFound() : Ok(mapping);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryAisleMappingDto>> Create(int storeId, CreateCategoryAisleMappingRequest request)
    {
        var mapping = await _mappingService.CreateAsync(storeId, request);
        return mapping is null
            ? NotFound()
            : CreatedAtAction(nameof(GetById), new { storeId, mappingId = mapping.Id }, mapping);
    }

    [HttpPut("{mappingId}")]
    public async Task<ActionResult<CategoryAisleMappingDto>> Update(int storeId, int mappingId, CreateCategoryAisleMappingRequest request)
    {
        var mapping = await _mappingService.UpdateAsync(storeId, mappingId, request);
        return mapping is null ? NotFound() : Ok(mapping);
    }

    [HttpDelete("{mappingId}")]
    public async Task<IActionResult> Delete(int storeId, int mappingId)
    {
        var deleted = await _mappingService.DeleteAsync(storeId, mappingId);
        return deleted ? NoContent() : NotFound();
    }
}
