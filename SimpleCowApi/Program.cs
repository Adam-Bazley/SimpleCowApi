using Microsoft.EntityFrameworkCore;
using SimpleCowApi.Data;
using SimpleCowApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=simpleCowApi.db"));  // SQLite connection string

// Register the controllers
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Simple Cow API",
        Version = "v1",
        Description = "API for managing farms and cows"
    });
});

var app = builder.Build();

// Use Swagger UI middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger JSON endpoint
    app.UseSwaggerUI(); // Enable Swagger UI
}

// Configure the HTTP request pipeline
app.MapControllers();

app.Run();

public partial class Program { }