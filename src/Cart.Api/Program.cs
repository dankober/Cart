using Cart.Api.Data;
using Cart.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddPresentation();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

// Configure Kestrel to use port 7100
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7100);
});

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CartDbContext>();
    db.Database.EnsureCreated();
}

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart API v1");
});

app.UseCors();

// Serve static files (React app in production)
app.UseDefaultFiles();
app.UseStaticFiles();

// Map controllers
app.MapControllers();

// Health check endpoint (keep as minimal API)
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }))
   .WithName("Health")
   .WithOpenApi();

// Fallback to index.html for SPA routing (must be after API routes)
app.MapFallbackToFile("index.html");

app.Run();
