using GroProduct.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers with JSON options to handle possible cycles
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        // ignore object reference loops instead of throwing
        opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "GroProduct API",
        Version = "v1"
    });
});

// SQL Server DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// automatically ensure database and schema exist on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // apply migrations when available or fall back to EnsureCreated
    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
    else
    {
        db.Database.EnsureCreated();
    }

    // even if the database already existed, make sure the Users table is present
    // (EnsureCreated won't add tables to an existing database)
}

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();
app.Map("/", () => "GroProduct API Running...");
// Map controllers
app.MapControllers();


app.Run();