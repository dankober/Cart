using System.Reflection;
using Cart.Api.Data;
using Cart.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHelloService, HelloService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IGroceryService, GroceryService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IAisleService, AisleService>();
        services.AddScoped<ICategoryAisleMappingService, CategoryAisleMappingService>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbPath = configuration.GetValue<string>("Database:Path") ?? "data/cart.db";
        services.AddDbContext<CartDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}",
                b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Cart API", Version = "v1" });
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:7101")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        return services;
    }
}
