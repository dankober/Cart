using Cart.Api.Models;

namespace Cart.Api.Services;

public interface ICategoryAisleMappingService
{
    Task<IEnumerable<CategoryAisleMappingDto>> GetByStoreAsync(int storeId);
    Task<CategoryAisleMappingDto?> GetByIdAsync(int storeId, int mappingId);
    Task<CategoryAisleMappingDto?> CreateAsync(int storeId, CreateCategoryAisleMappingRequest request);
    Task<CategoryAisleMappingDto?> UpdateAsync(int storeId, int mappingId, CreateCategoryAisleMappingRequest request);
    Task<bool> DeleteAsync(int storeId, int mappingId);
}
