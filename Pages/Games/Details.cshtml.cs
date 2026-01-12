using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Games;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;
    public DetailsModel(AppDbContext db) => _db = db;

    public Game Game { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var game = await _db.Games
            .Include(g => g.Genre)
            .Include(g => g.Studio)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game is null) return NotFound();

        Game = game;
        return Page();
    }
}