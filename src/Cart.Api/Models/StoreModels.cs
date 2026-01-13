using System.ComponentModel.DataAnnotations;

namespace Cart.Api.Models;

public record StoreDto(int Id, string Name, string? Address);

public record StoreDetailDto(int Id, string Name, string? Address, IEnumerable<AisleDto> Aisles);

public record CreateStoreRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    [MaxLength(500)]
    public string? Address { get; init; }
}

public record UpdateStoreRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;

    [MaxLength(500)]
    public string? Address { get; init; }
}
