using Cart.Api.Data.Entities;
using Cart.Api.Models;

namespace Cart.Api.Mappers;

public static class GroceryMappings
{
    public static GroceryDto ToDto(this Grocery grocery)
        => new(grocery.Id, grocery.Name, grocery.CategoryId, grocery.Category?.Name);

    public static Grocery ToEntity(this CreateGroceryRequest request)
        => new() { Name = request.Name, CategoryId = request.CategoryId };
}
