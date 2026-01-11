using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC / Controllers (Web API)
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite (single-file DB, good for GitHub clone-and-run)
// Uses appsettings.json if present, otherwise falls back to a local file.
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? "Data Source=videogameapp.db";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(cs));

var app = builder.Build();

// Auto-create / update DB schema on startup (applies migrations if they exist)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

// Simple health endpoint
app.MapGet("/health", () => Results.Ok(new { status = "OK" }));

app.Run();

// NOTE: Day 2 will move entities + DbSets here.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
