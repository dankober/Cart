using Cart.Api.Data.Entities;
using Cart.Api.Models;

namespace Cart.Api.Mappers;

public static class StoreMappings
{
    public static StoreDto ToDto(this Store store)
        => new(store.Id, store.Name, store.Address);

    public static StoreDetailDto ToDetailDto(this Store store)
        => new(
            store.Id,
            store.Name,
            store.Address,
            store.Aisles.Select(a => a.ToDto()));

    public static Store ToEntity(this CreateStoreRequest request)
        => new() { Name = request.Name, Address = request.Address };
}
