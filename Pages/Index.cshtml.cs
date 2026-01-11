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

    public List<Game> Games { get; private set; } = new();

    public async Task OnGetAsync()
    {
        Games = await _db.Games
            .Include(g => g.Genre)
            .Include(g => g.Studio)
            .OrderBy(g => g.Title)
            .ToListAsync();
    }
}
