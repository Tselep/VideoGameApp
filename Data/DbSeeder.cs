using VideoGameApp.Data;
using VideoGameApp.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameApp.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Αν έχει ήδη αρκετά games, μην ξανασπέρνεις
        if (await db.Games.CountAsync() >= 20) return;

        // Genres
        if (!await db.Genres.AnyAsync())
        {
            db.Genres.AddRange(
                new Genre { Name = "Action" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Sports" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Racing" }
            );
            await db.SaveChangesAsync();
        }

        // Studios
        if (!await db.Studios.AnyAsync())
        {
            db.Studios.AddRange(
                new Studio { Name = "CD Projekt" },
                new Studio { Name = "Naughty Dog" },
                new Studio { Name = "EA Sports" },
                new Studio { Name = "Ubisoft" },
                new Studio { Name = "Rockstar Games" },
                new Studio { Name = "FromSoftware" }
            );
            await db.SaveChangesAsync();
        }

        var genres = await db.Genres.ToListAsync();
        var studios = await db.Studios.ToListAsync();

        // +20 Games (δεν πειράζω τα 3 που έχεις, απλά προσθέτω)
        var newGames = new List<Game>
        {
            new() { Title="God of War", Price=49.99m, ReleaseDate=new DateTime(2018,4,20), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"Sony Santa Monica") },
            new() { Title="Elden Ring", Price=59.99m, ReleaseDate=new DateTime(2022,2,25), GenreId=Pick(genres,"RPG"), StudioId=Pick(studios,"FromSoftware") },
            new() { Title="Red Dead Redemption 2", Price=39.99m, ReleaseDate=new DateTime(2018,10,26), GenreId=Pick(genres,"Adventure"), StudioId=Pick(studios,"Rockstar Games") },
            new() { Title="Assassin's Creed Odyssey", Price=29.99m, ReleaseDate=new DateTime(2018,10,5), GenreId=Pick(genres,"Action"), StudioId=Pick(studios,"Ubisoft") },
            new() { Title="F1 24", Price=69.99m, ReleaseDate=new DateTime(2024,5,31), GenreId=Pick(genres,"Racing"), StudioId=Pick(studios,"EA Sports") },
            new() { Title="Gran Turismo 7", Price=59.99m, ReleaseDate=new DateTime(2022,3,4), GenreId=Pick(genres,"Racing"), StudioId=Pick(studios,"Sony Santa Monica") },

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

        // Σημείωση: Αν δεν υπάρχει "Sony Santa Monica" κλπ, το Pick θα κάνει fallback.
        // Πρόσθεσε/άλλαξε titles/studios όπως θες.

        // Πρόσθεσε μόνο όσα δεν υπάρχουν ήδη (με βάση Title)
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
        // Προσπαθώ να βρω Name property αν υπάρχει
        var prop = typeof(T).GetProperty("Name");
        var match = list.FirstOrDefault(x => (string?)prop?.GetValue(x) == name);
        if (match != null)
            return (int)typeof(T).GetProperty("Id")!.GetValue(match)!;

        // fallback: πάρε το πρώτο
        return (int)typeof(T).GetProperty("Id")!.GetValue(list.First())!;
    }
}