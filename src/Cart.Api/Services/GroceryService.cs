using Cart.Api.Data;
using Cart.Api.Mappers;
using Cart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.Services;

public class GroceryService : IGroceryService
{
    private readonly CartDbContext _db;

    public GroceryService(CartDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<GroceryDto>> GetAllAsync(int? categoryId = null)
    {
        var query = _db.Groceries
            .Include(g => g.Category)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(g => g.CategoryId == categoryId.Value);
        }

        return await query
            .OrderBy(g => g.Name)
            .Select(g => g.ToDto())
            .ToListAsync();
    }

    public async Task<GroceryDto?> GetByIdAsync(int id)
    {
        var grocery = await _db.Groceries
            .Include(g => g.Category)
            .FirstOrDefaultAsync(g => g.Id == id);

        return grocery?.ToDto();
    }

    public async Task<IEnumerable<GroceryDto>> SearchAsync(string query)
    {
        return await _db.Groceries
            .Include(g => g.Category)
            .Where(g => g.Name.ToLower().Contains(query.ToLower()))
            .OrderBy(g => g.Name)
            .Select(g => g.ToDto())
            .ToListAsync();
    }

    public async Task<GroceryDto> CreateAsync(CreateGroceryRequest request)
    {
        var grocery = request.ToEntity();
        _db.Groceries.Add(grocery);
        await _db.SaveChangesAsync();

        await _db.Entry(grocery).Reference(g => g.Category).LoadAsync();
        return grocery.ToDto();
    }

    public async Task<GroceryDto?> UpdateAsync(int id, UpdateGroceryRequest request)
    {
        var grocery = await _db.Groceries.FindAsync(id);
        if (grocery is null) return null;

        grocery.Name = request.Name;
        grocery.CategoryId = request.CategoryId;
        await _db.SaveChangesAsync();

        await _db.Entry(grocery).Reference(g => g.Category).LoadAsync();
        return grocery.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var grocery = await _db.Groceries.FindAsync(id);
        if (grocery is null) return false;

        _db.Groceries.Remove(grocery);
        await _db.SaveChangesAsync();
        return true;
    }
}
