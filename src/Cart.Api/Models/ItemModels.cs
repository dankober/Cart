using System.ComponentModel.DataAnnotations;

namespace Cart.Api.Models;

public record ItemDto(
    int Id,
    int GroceryId,
    string GroceryName,
    int? CategoryId,
    string? CategoryName,
    int Quantity,
    bool IsCompleted
);

public record CreateItemRequest
{
    [Required]
    public int GroceryId { get; init; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; init; } = 1;
}

public record UpdateItemRequest
{
    [Range(1, int.MaxValue)]
    public int Quantity { get; init; } = 1;

    public bool IsCompleted { get; init; }
}
