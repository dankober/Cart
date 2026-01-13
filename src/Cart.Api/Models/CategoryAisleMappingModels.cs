using System.ComponentModel.DataAnnotations;

namespace Cart.Api.Models;

public record CategoryAisleMappingDto(
    int Id,
    int StoreId,
    int CategoryId,
    string CategoryName,
    int AisleId,
    string AisleName
);

public record CreateCategoryAisleMappingRequest
{
    [Required]
    public int CategoryId { get; init; }

    [Required]
    public int AisleId { get; init; }
}
