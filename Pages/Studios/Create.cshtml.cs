using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Studios;

[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Studio Studio { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return Page();

        _db.Studios.Add(Studio);
        await _db.SaveChangesAsync();

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect($"{returnUrl}?studioId={Studio.Id}");
        }

        return RedirectToPage("./Index");
    }
}