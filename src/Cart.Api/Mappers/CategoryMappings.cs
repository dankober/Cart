using Cart.Api.Data.Entities;
using Cart.Api.Models;

namespace Cart.Api.Mappers;

public static class CategoryMappings
{
    public static CategoryDto ToDto(this Category category)
        => new(category.Id, category.Name);

    public static Category ToEntity(this CreateCategoryRequest request)
        => new() { Name = request.Name };
}
