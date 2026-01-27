using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Cart;

public class AddModel : PageModel
{
    private readonly AppDbContext _db;

    public AddModel(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet(int gameId)
    {
        var game = _db.Games.FirstOrDefault(g => g.Id == gameId);
        if (game == null)
        {
            return NotFound();
        }

        // Add game to cart logic here

        return RedirectToPage("/Cart/Index");
    }
}