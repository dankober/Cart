using Cart.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Cart.Api.DependencyInjection;

public static class DataExtensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CartDbContext>();
        db.Database.Migrate();
        return app;
    }
}
