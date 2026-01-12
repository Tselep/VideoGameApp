using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Genres;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public List<Genre> Genres { get; set; } = new();

    public async Task OnGetAsync()
    {
        Genres = await _db.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();
    }
}