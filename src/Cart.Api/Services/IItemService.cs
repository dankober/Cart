using Cart.Api.Models;

namespace Cart.Api.Services;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllAsync(bool? isCompleted = null);
    Task<ItemDto?> GetByIdAsync(int id);
    Task<ItemDto> CreateAsync(CreateItemRequest request);
    Task<ItemDto?> UpdateAsync(int id, UpdateItemRequest request);
    Task<bool> DeleteAsync(int id);
    Task<int> ClearCompletedAsync();
}
