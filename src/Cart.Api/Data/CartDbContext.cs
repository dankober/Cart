using System.Reflection;
using Cart.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.Data;

public class CartDbContext : DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Grocery> Groceries => Set<Grocery>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Aisle> Aisles => Set<Aisle>();
    public DbSet<CategoryAisleMapping> CategoryAisleMappings => Set<CategoryAisleMapping>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
