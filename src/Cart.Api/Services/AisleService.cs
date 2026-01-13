using Cart.Api.Data;
using Cart.Api.Mappers;
using Cart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.Services;

public class AisleService : IAisleService
{
    private readonly CartDbContext _db;

    public AisleService(CartDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AisleDto>> GetByStoreAsync(int storeId)
    {
        return await _db.Aisles
            .Where(a => a.StoreId == storeId)
            .OrderBy(a => a.SortOrder)
            .Select(a => a.ToDto())
            .ToListAsync();
    }

    public async Task<AisleDto?> GetByIdAsync(int storeId, int aisleId)
    {
        var aisle = await _db.Aisles
            .FirstOrDefaultAsync(a => a.StoreId == storeId && a.Id == aisleId);

        return aisle?.ToDto();
    }

    public async Task<AisleDto?> CreateAsync(int storeId, CreateAisleRequest request)
    {
        var storeExists = await _db.Stores.AnyAsync(s => s.Id == storeId);
        if (!storeExists) return null;

        var aisle = request.ToEntity(storeId);
        _db.Aisles.Add(aisle);
        await _db.SaveChangesAsync();
        return aisle.ToDto();
    }

    public async Task<AisleDto?> UpdateAsync(int storeId, int aisleId, UpdateAisleRequest request)
    {
        var aisle = await _db.Aisles
            .FirstOrDefaultAsync(a => a.StoreId == storeId && a.Id == aisleId);

        if (aisle is null) return null;

        aisle.Name = request.Name;
        aisle.SortOrder = request.SortOrder;
        await _db.SaveChangesAsync();
        return aisle.ToDto();
    }

    public async Task<bool> DeleteAsync(int storeId, int aisleId)
    {
        var aisle = await _db.Aisles
            .FirstOrDefaultAsync(a => a.StoreId == storeId && a.Id == aisleId);

        if (aisle is null) return false;

        _db.Aisles.Remove(aisle);
        await _db.SaveChangesAsync();
        return true;
    }
}
