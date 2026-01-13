using System.ComponentModel.DataAnnotations;

namespace Cart.Api.Models;

public record GroceryDto(int Id, string Name, int? CategoryId, string? CategoryName);

public record CreateGroceryRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    public int? CategoryId { get; init; }
}

public record UpdateGroceryRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    public int? CategoryId { get; init; }
}
