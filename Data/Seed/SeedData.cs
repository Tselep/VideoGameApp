using VideoGameApp.Models;

namespace VideoGameApp.Data.Seed;

public static class SeedData
{
    public static void EnsureSeeded(AppDbContext db)
    {
        // Seed Genres (only if empty)
        if (!db.Genres.Any())
        {
            db.Genres.AddRange(
                new Genre { Name = "Action" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Sports" }
            );
            db.SaveChanges();
        }

        // Seed Studios (only if empty)
        if (!db.Studios.Any())
        {
            db.Studios.AddRange(
                new Studio { Name = "Naughty Dog", Country = "USA" },
                new Studio { Name = "CD Projekt", Country = "Poland" },
                new Studio { Name = "EA Sports", Country = "USA" }
            );
            db.SaveChanges();
        }

        // Seed Games (only if empty)
        if (db.Games.Any())
            return;

        var action = db.Genres.First(g => g.Name == "Action").Id;
        var rpg = db.Genres.First(g => g.Name == "RPG").Id;
        var sports = db.Genres.First(g => g.Name == "Sports").Id;

        var naughtyDog = db.Studios.First(s => s.Name == "Naughty Dog").Id;
        var cdpr = db.Studios.First(s => s.Name == "CD Projekt").Id;
        var ea = db.Studios.First(s => s.Name == "EA Sports").Id;

        db.Games.AddRange(
            new Game
            {
                Title = "The Last of Us",
                GenreId = action,
                StudioId = naughtyDog,
                Price = 59.99m,
                ReleaseDate = new DateTime(2013, 6, 14)
            },
            new Game
            {
                Title = "Cyberpunk 2077",
                GenreId = rpg,
                StudioId = cdpr,
                Price = 39.99m,
                ReleaseDate = new DateTime(2020, 12, 10)
            },
            new Game
            {
                Title = "FC 25",
                GenreId = sports,
                StudioId = ea,
                Price = 69.99m,
                ReleaseDate = new DateTime(2024, 9, 27)
            }
        );

        db.SaveChanges();
    }
}
