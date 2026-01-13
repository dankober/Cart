using Cart.Api.Models;

namespace Cart.Api.Services;

public interface IGroceryService
{
    Task<IEnumerable<GroceryDto>> GetAllAsync(int? categoryId = null);
    Task<GroceryDto?> GetByIdAsync(int id);
    Task<IEnumerable<GroceryDto>> SearchAsync(string query);
    Task<GroceryDto> CreateAsync(CreateGroceryRequest request);
    Task<GroceryDto?> UpdateAsync(int id, UpdateGroceryRequest request);
    Task<bool> DeleteAsync(int id);
}
