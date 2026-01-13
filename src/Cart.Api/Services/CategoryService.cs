using Cart.Api.Data;
using Cart.Api.Mappers;
using Cart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly CartDbContext _db;

    public CategoryService(CartDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        return await _db.Categories
            .OrderBy(c => c.Name)
            .Select(c => c.ToDto())
            .ToListAsync();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        return category?.ToDto();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryRequest request)
    {
        var category = request.ToEntity();
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
        return category.ToDto();
    }

    public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category is null) return null;

        category.Name = request.Name;
        await _db.SaveChangesAsync();
        return category.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category is null) return false;

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
        return true;
    }
}
