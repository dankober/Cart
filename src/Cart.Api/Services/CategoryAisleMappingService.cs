using Cart.Api.Data;
using Cart.Api.Mappers;
using Cart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.Services;

public class CategoryAisleMappingService : ICategoryAisleMappingService
{
    private readonly CartDbContext _db;

    public CategoryAisleMappingService(CartDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CategoryAisleMappingDto>> GetByStoreAsync(int storeId)
    {
        return await _db.CategoryAisleMappings
            .Include(m => m.Category)
            .Include(m => m.Aisle)
            .Where(m => m.StoreId == storeId)
            .OrderBy(m => m.Aisle.SortOrder)
            .ThenBy(m => m.Category.Name)
            .Select(m => m.ToDto())
            .ToListAsync();
    }

    public async Task<CategoryAisleMappingDto?> GetByIdAsync(int storeId, int mappingId)
    {
        var mapping = await _db.CategoryAisleMappings
            .Include(m => m.Category)
            .Include(m => m.Aisle)
            .FirstOrDefaultAsync(m => m.StoreId == storeId && m.Id == mappingId);

        return mapping?.ToDto();
    }

    public async Task<CategoryAisleMappingDto?> CreateAsync(int storeId, CreateCategoryAisleMappingRequest request)
    {
        var storeExists = await _db.Stores.AnyAsync(s => s.Id == storeId);
        if (!storeExists) return null;

        var aisleBelongsToStore = await _db.Aisles
            .AnyAsync(a => a.Id == request.AisleId && a.StoreId == storeId);
        if (!aisleBelongsToStore) return null;

        var mapping = request.ToEntity(storeId);
        _db.CategoryAisleMappings.Add(mapping);
        await _db.SaveChangesAsync();

        await _db.Entry(mapping).Reference(m => m.Category).LoadAsync();
        await _db.Entry(mapping).Reference(m => m.Aisle).LoadAsync();
        return mapping.ToDto();
    }

    public async Task<CategoryAisleMappingDto?> UpdateAsync(int storeId, int mappingId, CreateCategoryAisleMappingRequest request)
    {
        var mapping = await _db.CategoryAisleMappings
            .FirstOrDefaultAsync(m => m.StoreId == storeId && m.Id == mappingId);

        if (mapping is null) return null;

        var aisleBelongsToStore = await _db.Aisles
            .AnyAsync(a => a.Id == request.AisleId && a.StoreId == storeId);
        if (!aisleBelongsToStore) return null;

        mapping.CategoryId = request.CategoryId;
        mapping.AisleId = request.AisleId;
        await _db.SaveChangesAsync();

        await _db.Entry(mapping).Reference(m => m.Category).LoadAsync();
        await _db.Entry(mapping).Reference(m => m.Aisle).LoadAsync();
        return mapping.ToDto();
    }

    public async Task<bool> DeleteAsync(int storeId, int mappingId)
    {
        var mapping = await _db.CategoryAisleMappings
            .FirstOrDefaultAsync(m => m.StoreId == storeId && m.Id == mappingId);

        if (mapping is null) return false;

        _db.CategoryAisleMappings.Remove(mapping);
        await _db.SaveChangesAsync();
        return true;
    }
}
