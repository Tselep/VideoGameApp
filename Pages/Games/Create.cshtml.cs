using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Games;

[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;

    public CreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Game Game { get; set; } = new();

    public SelectList GenreOptions { get; set; } = default!;
    public SelectList StudioOptions { get; set; } = default!;

    // Accept query params so when we return from Add Genre/Studio we can preselect them
    public async Task OnGetAsync(int? genreId, int? studioId)
    {
        await LoadOptionsAsync();

        if (genreId.HasValue)
            Game.GenreId = genreId.Value;

        if (studioId.HasValue)
            Game.StudioId = studioId.Value;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadOptionsAsync();

        if (!ModelState.IsValid)
            return Page();

        _db.Games.Add(Game);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync()
    {
        var genres = await _db.Genres.OrderBy(g => g.Name).ToListAsync();
        var studios = await _db.Studios.OrderBy(s => s.Name).ToListAsync();

        GenreOptions = new SelectList(genres, "Id", "Name");
        StudioOptions = new SelectList(studios, "Id", "Name");
    }
}