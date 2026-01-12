using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Genres;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Genre Genre { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var genre = await _db.Genres.FindAsync(id);
        if (genre is null) return NotFound();
        Genre = genre;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _db.Attach(Genre).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}