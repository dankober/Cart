using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Api.Data.Entities;

public class Aisle
{
    public int Id { get; set; }

    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public ICollection<CategoryAisleMapping> CategoryAisleMappings { get; set; } = new List<CategoryAisleMapping>();

    internal class Configuration : IEntityTypeConfiguration<Aisle>
    {
        public void Configure(EntityTypeBuilder<Aisle> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(a => a.Store)
                .WithMany(s => s.Aisles)
                .HasForeignKey(a => a.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
