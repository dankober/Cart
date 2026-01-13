using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Api.Data.Entities;

public class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Address { get; set; }

    public ICollection<Aisle> Aisles { get; set; } = new List<Aisle>();
    public ICollection<CategoryAisleMapping> CategoryAisleMappings { get; set; } = new List<CategoryAisleMapping>();

    internal class Configuration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Address)
                .HasMaxLength(500);
        }
    }
}
