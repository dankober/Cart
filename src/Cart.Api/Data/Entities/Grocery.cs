using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Api.Data.Entities;

public class Grocery
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();

    internal class Configuration : IEntityTypeConfiguration<Grocery>
    {
        public void Configure(EntityTypeBuilder<Grocery> builder)
        {
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(g => g.Category)
                .WithMany(c => c.Groceries)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
