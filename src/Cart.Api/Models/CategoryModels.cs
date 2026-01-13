using System.ComponentModel.DataAnnotations;

namespace Cart.Api.Models;

public record CategoryDto(int Id, string Name);

public record CreateCategoryRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;
}

public record UpdateCategoryRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;
}
