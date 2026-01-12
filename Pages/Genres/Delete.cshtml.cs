using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Genres;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public DeleteModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Genre Genre { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var genre = await _db.Genres.FindAsync(id);
        if (genre is null) return NotFound();
        Genre = genre;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var genre = await _db.Genres.FindAsync(id);
        if (genre is null) return NotFound();

        _db.Genres.Remove(genre);
        await _db.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}