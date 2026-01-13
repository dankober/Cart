using Cart.Api.Data;
using Cart.Api.Mappers;
using Cart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.Services;

public class ItemService : IItemService
{
    private readonly CartDbContext _db;

    public ItemService(CartDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ItemDto>> GetAllAsync(bool? isCompleted = null)
    {
        var query = _db.Items
            .Include(i => i.Grocery)
            .ThenInclude(g => g.Category)
            .AsQueryable();

        if (isCompleted.HasValue)
        {
            query = query.Where(i => i.IsCompleted == isCompleted.Value);
        }

        return await query
            .OrderBy(i => i.IsCompleted)
            .ThenBy(i => i.Grocery.Name)
            .Select(i => i.ToDto())
            .ToListAsync();
    }

    public async Task<ItemDto?> GetByIdAsync(int id)
    {
        var item = await _db.Items
            .Include(i => i.Grocery)
            .ThenInclude(g => g.Category)
            .FirstOrDefaultAsync(i => i.Id == id);

        return item?.ToDto();
    }

    public async Task<ItemDto> CreateAsync(CreateItemRequest request)
    {
        var item = request.ToEntity();
        _db.Items.Add(item);
        await _db.SaveChangesAsync();

        await _db.Entry(item).Reference(i => i.Grocery).LoadAsync();
        await _db.Entry(item.Grocery).Reference(g => g.Category).LoadAsync();
        return item.ToDto();
    }

    public async Task<ItemDto?> UpdateAsync(int id, UpdateItemRequest request)
    {
        var item = await _db.Items
            .Include(i => i.Grocery)
            .ThenInclude(g => g.Category)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item is null) return null;

        item.Quantity = request.Quantity;
        item.IsCompleted = request.IsCompleted;
        await _db.SaveChangesAsync();

        return item.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _db.Items.FindAsync(id);
        if (item is null) return false;

        _db.Items.Remove(item);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<int> ClearCompletedAsync()
    {
        var completedItems = await _db.Items
            .Where(i => i.IsCompleted)
            .ToListAsync();

        _db.Items.RemoveRange(completedItems);
        await _db.SaveChangesAsync();
        return completedItems.Count;
    }
}
