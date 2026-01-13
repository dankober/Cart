using System.ComponentModel.DataAnnotations;

namespace Cart.Api.Models;

public record AisleDto(int Id, int StoreId, string Name, int SortOrder);

public record CreateAisleRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    public int SortOrder { get; init; }
}

public record UpdateAisleRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    public int SortOrder { get; init; }
}
