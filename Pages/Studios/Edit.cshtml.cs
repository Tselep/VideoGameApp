using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Studios;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Studio Studio { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var studio = await _db.Studios.FindAsync(id);
        if (studio is null) return NotFound();

        Studio = studio;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _db.Attach(Studio).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}