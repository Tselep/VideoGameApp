using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public int GamesCount { get; private set; }
    public int GenresCount { get; private set; }
    public int StudiosCount { get; private set; }

    public List<Game> RecentGames { get; private set; } = new();

    public async Task OnGetAsync()
    {
        GamesCount = await _db.Games.CountAsync();
        GenresCount = await _db.Genres.CountAsync();
        StudiosCount = await _db.Studios.CountAsync();

        RecentGames = await _db.Games
            .Include(g => g.Genre)
            .Include(g => g.Studio)
            .OrderByDescending(g => g.Id)
            .Take(5)
            .ToListAsync();
    }
}
