using Cart.Api.Data.Entities;
using Cart.Api.Models;

namespace Cart.Api.Mappers;

public static class AisleMappings
{
    public static AisleDto ToDto(this Aisle aisle)
        => new(aisle.Id, aisle.StoreId, aisle.Name, aisle.SortOrder);

    public static Aisle ToEntity(this CreateAisleRequest request, int storeId)
        => new()
        {
            StoreId = storeId,
            Name = request.Name,
            SortOrder = request.SortOrder
        };
}
