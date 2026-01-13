using Cart.Api.Data.Entities;
using Cart.Api.Models;

namespace Cart.Api.Mappers;

public static class ItemMappings
{
    public static ItemDto ToDto(this Item item)
        => new(
            item.Id,
            item.GroceryId,
            item.Grocery.Name,
            item.Grocery.CategoryId,
            item.Grocery.Category?.Name,
            item.Quantity,
            item.IsCompleted);

    public static Item ToEntity(this CreateItemRequest request)
        => new()
        {
            GroceryId = request.GroceryId,
            Quantity = request.Quantity,
            IsCompleted = false
        };
}
