using Cart.Api.Data;
using Cart.Api.Mappers;
using Cart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.Services;

public class StoreService : IStoreService
{
    private readonly CartDbContext _db;

    public StoreService(CartDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<StoreDto>> GetAllAsync()
    {
        return await _db.Stores
            .OrderBy(s => s.Name)
            .Select(s => s.ToDto())
            .ToListAsync();
    }

    public async Task<StoreDto?> GetByIdAsync(int id)
    {
        var store = await _db.Stores.FindAsync(id);
        return store?.ToDto();
    }

    public async Task<StoreDetailDto?> GetDetailAsync(int id)
    {
        var store = await _db.Stores
            .Include(s => s.Aisles.OrderBy(a => a.SortOrder))
            .FirstOrDefaultAsync(s => s.Id == id);

        return store?.ToDetailDto();
    }

    public async Task<StoreDto> CreateAsync(CreateStoreRequest request)
    {
        var store = request.ToEntity();
        _db.Stores.Add(store);
        await _db.SaveChangesAsync();
        return store.ToDto();
    }

    public async Task<StoreDto?> UpdateAsync(int id, UpdateStoreRequest request)
    {
        var store = await _db.Stores.FindAsync(id);
        if (store is null) return null;

        store.Name = request.Name;
        store.Address = request.Address;
        await _db.SaveChangesAsync();
        return store.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var store = await _db.Stores.FindAsync(id);
        if (store is null) return false;

        _db.Stores.Remove(store);
        await _db.SaveChangesAsync();
        return true;
    }
}
