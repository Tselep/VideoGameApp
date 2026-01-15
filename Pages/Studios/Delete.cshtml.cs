using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Studios;

[Authorize(Roles = "Admin")]
public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public DeleteModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Studio Studio { get; set; } = default!;

    public bool IsInUse { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var studio = await _db.Studios.FindAsync(id);
        if (studio is null) return NotFound();

        Studio = studio;
        IsInUse = await _db.Games.AnyAsync(g => g.StudioId == id);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var inUse = await _db.Games.AnyAsync(g => g.StudioId == id);
        if (inUse)
        {
            TempData["Error"] = "Δεν μπορεί να διαγραφεί αυτό το Studio γιατί χρησιμοποιείται από υπάρχοντα Games.";
            return RedirectToPage("./Index");
        }

        var studio = await _db.Studios.FindAsync(id);
        if (studio is null) return NotFound();

        _db.Studios.Remove(studio);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Το Studio διαγράφηκε επιτυχώς.";
        return RedirectToPage("./Index");
    }
}