using Cart.Api.Models;

namespace Cart.Api.Services;

public interface IStoreService
{
    Task<IEnumerable<StoreDto>> GetAllAsync();
    Task<StoreDto?> GetByIdAsync(int id);
    Task<StoreDetailDto?> GetDetailAsync(int id);
    Task<StoreDto> CreateAsync(CreateStoreRequest request);
    Task<StoreDto?> UpdateAsync(int id, UpdateStoreRequest request);
    Task<bool> DeleteAsync(int id);
}
