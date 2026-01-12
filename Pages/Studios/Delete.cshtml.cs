using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Studios;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public DeleteModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Studio Studio { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var studio = await _db.Studios.FindAsync(id);
        if (studio is null) return NotFound();

        Studio = studio;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var studio = await _db.Studios.FindAsync(id);
        if (studio is null) return NotFound();

        _db.Studios.Remove(studio);
        await _db.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}