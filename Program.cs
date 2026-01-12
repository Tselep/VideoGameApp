using Microsoft.EntityFrameworkCore;
using System.IO;
using VideoGameApp.Data;
using VideoGameApp.Data.Seed;
using VideoGameApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// MVC / Controllers (Web API)
builder.Services.AddControllers().AddMvcOptions(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

// Razor Pages
builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite (single-file DB)
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? $"Data Source={Path.Combine(builder.Environment.ContentRootPath, "videogameapp.db")}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(cs));

var app = builder.Build();

// Auto-create / update DB schema on startup (applies migrations if they exist)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    SeedData.EnsureSeeded(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

// Simple health endpoint
app.MapGet("/health", () => Results.Ok(new { status = "OK" }));

app.Run();
