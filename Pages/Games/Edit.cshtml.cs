using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Games;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Game Game { get; set; } = default!;

    public SelectList GenreOptions { get; set; } = default!;
    public SelectList StudioOptions { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (game is null) return NotFound();

        Game = game;
        await LoadOptionsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadOptionsAsync();

        if (!ModelState.IsValid)
            return Page();

        _db.Attach(Game).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return RedirectToPage("Index");
    }

    private async Task LoadOptionsAsync()
    {
        GenreOptions = new SelectList(
            await _db.Genres.OrderBy(g => g.Name).ToListAsync(),
            "Id", "Name", Game?.GenreId);

        StudioOptions = new SelectList(
            await _db.Studios.OrderBy(s => s.Name).ToListAsync(),
            "Id", "Name", Game?.StudioId);
    }
}