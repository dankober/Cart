using Cart.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Cart API", Version = "v1" });
});

// Configure SQLite
var dbPath = builder.Configuration.GetValue<string>("Database:Path") ?? "cart.db";
builder.Services.AddDbContext<CartDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Configure CORS for development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:7101")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

// API endpoints
app.MapGet("/api/hello", () => new { message = "Hello from Cart API" })
   .WithName("Hello")
   .WithOpenApi();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }))
   .WithName("Health")
   .WithOpenApi();

// Fallback to index.html for SPA routing (must be after API routes)
app.MapFallbackToFile("index.html");

app.Run();
