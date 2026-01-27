using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Models;

namespace VideoGameApp.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Studio> Studios => Set<Studio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>()
            .Property(g => g.Title)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Genre>()
            .Property(g => g.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Studio>()
            .Property(s => s.Name)
            .HasMaxLength(150)
            .IsRequired();
    }
}