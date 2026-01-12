using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Genres;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public DeleteModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Genre Genre { get; set; } = default!;

    public bool IsInUse { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var genre = await _db.Genres.FindAsync(id);
        if (genre is null) return NotFound();

        Genre = genre;
        IsInUse = await _db.Games.AnyAsync(g => g.GenreId == id);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var inUse = await _db.Games.AnyAsync(g => g.GenreId == id);
        if (inUse)
        {
            TempData["Error"] = "Δεν μπορεί να διαγραφεί αυτό το Genre γιατί χρησιμοποιείται από υπάρχοντα Games.";
            return RedirectToPage("./Index");
        }

        var genre = await _db.Genres.FindAsync(id);
        if (genre is null) return NotFound();

        _db.Genres.Remove(genre);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Το Genre διαγράφηκε επιτυχώς.";
        return RedirectToPage("./Index");
    }
}