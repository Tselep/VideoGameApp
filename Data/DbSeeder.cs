using VideoGameApp.Data;
using VideoGameApp.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameApp.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {

        
        var genreNames = new[]
        {
            "Action", "RPG", "Sports", "Adventure", "Strategy",
            "Racing", "Shooter", "Horror", "Simulation", "Indie"
        };

        foreach (var name in genreNames)
        {
            if (!await db.Genres.AnyAsync(g => g.Name == name))
            {
                db.Genres.Add(new Genre { Name = name });
            }
        }
        await db.SaveChangesAsync();

        
        var studiosToSeed = new (string Name, string Country)[]
        {
            ("Naughty Dog", "USA"),
            ("EA Sports", "USA"),
            ("Ubisoft", "France"),
            ("Rockstar Games", "USA"),
            ("FromSoftware", "Japan"),
            ("Santa Monica Studio", "USA"),
            ("Insomniac Games", "USA"),
            ("Guerrilla Games", "Netherlands"),
            ("Bethesda", "USA")
        };

        foreach (var (name, country) in studiosToSeed)
        {
            var existing = await db.Studios.FirstOrDefaultAsync(s => s.Name == name);

            if (existing == null)
            {
                db.Studios.Add(new Studio { Name = name, Country = country });
            }
            else if (string.IsNullOrWhiteSpace(existing.Country))
            {
                existing.Country = country; 
            }
        }

        await db.SaveChangesAsync();

        var genres = await db.Genres.ToListAsync();
        var studios = await db.Studios.ToListAsync();

        
        var newGames = new List<Game>
        {
            new() { Title="God of War", Price=49.99m, ReleaseDate=new DateTime(2018,4,20), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"Santa Monica Studio") },
            new() { Title="Elden Ring", Price=59.99m, ReleaseDate=new DateTime(2022,2,25), GenreId=Pick(genres,"RPG"), StudioId=Pick(studios,"FromSoftware") },
            new() { Title="Red Dead Redemption 2", Price=39.99m, ReleaseDate=new DateTime(2018,10,26), GenreId=Pick(genres,"Adventure"), StudioId=Pick(studios,"Rockstar Games") },
            new() { Title="Assassin's Creed Odyssey", Price=29.99m, ReleaseDate=new DateTime(2018,10,5), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"Ubisoft") },
            new() { Title="F1 24", Price=69.99m, ReleaseDate=new DateTime(2024,5,31), GenreId=Pick(genres,"Racing"), StudioId=Pick(studios,"EA Sports") },
            new() { Title="Gran Turismo 7", Price=59.99m, ReleaseDate=new DateTime(2022,3,4), GenreId=Pick(genres,"Racing"), StudioId=Pick(studios,"Santa Monica Studio") },

            new() { Title="Horizon Zero Dawn", Price=19.99m, ReleaseDate=new DateTime(2017,2,28), GenreId=Pick(genres,"Adventure"), StudioId=Pick(studios,"Ubisoft") },
            new() { Title="Ghost of Tsushima", Price=49.99m, ReleaseDate=new DateTime(2020,7,17), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"Naughty Dog") },
            new() { Title="Sekiro: Shadows Die Twice", Price=39.99m, ReleaseDate=new DateTime(2019,3,22), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"FromSoftware") },
            new() { Title="Dark Souls III", Price=29.99m, ReleaseDate=new DateTime(2016,4,12), GenreId=Pick(genres,"RPG"), StudioId=Pick(studios,"FromSoftware") },

            new() { Title="FIFA 23", Price=24.99m, ReleaseDate=new DateTime(2022,9,30), GenreId=Pick(genres,"Sports"), StudioId=Pick(studios,"EA Sports") },
            new() { Title="NBA 2K24", Price=29.99m, ReleaseDate=new DateTime(2023,9,8), GenreId=Pick(genres,"Sports"), StudioId=Pick(studios,"EA Sports") },

            new() { Title="The Witcher 3", Price=14.99m, ReleaseDate=new DateTime(2015,5,19), GenreId=Pick(genres,"RPG"), StudioId=Pick(studios,"CD Projekt") },
            new() { Title="Cyberpunk: Phantom Liberty", Price=29.99m, ReleaseDate=new DateTime(2023,9,26), GenreId=Pick(genres,"RPG"), StudioId=Pick(studios,"CD Projekt") },

            new() { Title="GTA V", Price=19.99m, ReleaseDate=new DateTime(2013,9,17), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"Rockstar Games") },
            new() { Title="L.A. Noire", Price=14.99m, ReleaseDate=new DateTime(2011,5,17), GenreId=Pick(genres,"Adventure"), StudioId=Pick(studios,"Rockstar Games") },

            new() { Title="Civilization VI", Price=9.99m, ReleaseDate=new DateTime(2016,10,21), GenreId=Pick(genres,"Strategy"), StudioId=Pick(studios,"Ubisoft") },
            new() { Title="XCOM 2", Price=7.99m, ReleaseDate=new DateTime(2016,2,5), GenreId=Pick(genres,"Strategy"), StudioId=Pick(studios,"Ubisoft") },

            new() { Title="Uncharted 4", Price=19.99m, ReleaseDate=new DateTime(2016,5,10), GenreId=Pick(genres,"Adventure"), StudioId=Pick(studios,"Naughty Dog") },
            new() { Title="The Last of Us Part II", Price=29.99m, ReleaseDate=new DateTime(2020,6,19), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"Naughty Dog") },
        };

        
        
        var existingTitles = await db.Games.Select(g => g.Title).ToListAsync();
        var toAdd = newGames.Where(g => !existingTitles.Contains(g.Title)).ToList();

        if (toAdd.Count > 0)
        {
            db.Games.AddRange(toAdd);
            await db.SaveChangesAsync();
        }
    }

    private static int Pick<T>(List<T> list, string name) where T : class
    {
        
        var prop = typeof(T).GetProperty("Name");
        var match = list.FirstOrDefault(x => (string?)prop?.GetValue(x) == name);
        if (match != null)
            return (int)typeof(T).GetProperty("Id")!.GetValue(match)!;

        
        return (int)typeof(T).GetProperty("Id")!.GetValue(list.First())!;
    }
}