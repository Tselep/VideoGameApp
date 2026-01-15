using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Data.Seed;
using VideoGameApp.Infrastructure;
using VideoGameApp.Models;

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

// Identity (Authentication/Authorization)
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

// Simple health endpoint
app.MapGet("/health", () => Results.Ok(new { status = "OK" }));

app.Run();
