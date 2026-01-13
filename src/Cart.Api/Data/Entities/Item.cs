using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Api.Data.Entities;

public class Item
{
    public int Id { get; set; }

    public int GroceryId { get; set; }
    public Grocery Grocery { get; set; } = null!;

    public int Quantity { get; set; } = 1;

    public bool IsCompleted { get; set; }

    internal class Configuration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(i => i.Grocery)
                .WithMany(g => g.Items)
                .HasForeignKey(i => i.GroceryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
