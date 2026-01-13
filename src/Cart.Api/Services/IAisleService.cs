using Cart.Api.Models;

namespace Cart.Api.Services;

public interface IAisleService
{
    Task<IEnumerable<AisleDto>> GetByStoreAsync(int storeId);
    Task<AisleDto?> GetByIdAsync(int storeId, int aisleId);
    Task<AisleDto?> CreateAsync(int storeId, CreateAisleRequest request);
    Task<AisleDto?> UpdateAsync(int storeId, int aisleId, UpdateAisleRequest request);
    Task<bool> DeleteAsync(int storeId, int aisleId);
}
