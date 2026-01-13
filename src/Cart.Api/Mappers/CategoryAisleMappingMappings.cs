using Cart.Api.Data.Entities;
using Cart.Api.Models;

namespace Cart.Api.Mappers;

public static class CategoryAisleMappingMappings
{
    public static CategoryAisleMappingDto ToDto(this CategoryAisleMapping mapping)
        => new(
            mapping.Id,
            mapping.StoreId,
            mapping.CategoryId,
            mapping.Category.Name,
            mapping.AisleId,
            mapping.Aisle.Name);

    public static CategoryAisleMapping ToEntity(this CreateCategoryAisleMappingRequest request, int storeId)
        => new()
        {
            StoreId = storeId,
            CategoryId = request.CategoryId,
            AisleId = request.AisleId
        };
}
