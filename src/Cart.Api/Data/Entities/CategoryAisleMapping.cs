using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Api.Data.Entities;

public class CategoryAisleMapping
{
    public int Id { get; set; }

    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int AisleId { get; set; }
    public Aisle Aisle { get; set; } = null!;

    internal class Configuration : IEntityTypeConfiguration<CategoryAisleMapping>
    {
        public void Configure(EntityTypeBuilder<CategoryAisleMapping> builder)
        {
            builder.HasOne(m => m.Store)
                .WithMany(s => s.CategoryAisleMappings)
                .HasForeignKey(m => m.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Category)
                .WithMany(c => c.CategoryAisleMappings)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Aisle)
                .WithMany(a => a.CategoryAisleMappings)
                .HasForeignKey(m => m.AisleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => new { m.StoreId, m.CategoryId, m.AisleId })
                .IsUnique();
        }
    }
}
