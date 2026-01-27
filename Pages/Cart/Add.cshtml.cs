using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Data;
using VideoGameApp.Services.Cart;

namespace VideoGameApp.Pages.Cart;

public class AddPageModel : PageModel
{
    private readonly ICartService _cart;
    private readonly AppDbContext _db;

    public AddPageModel(ICartService cart, AppDbContext db)
    {
        _cart = cart;
        _db = db;
    }

    // Prevent direct navigation to /Cart/Add
    public IActionResult OnGet() => RedirectToPage("/Games/Index");

    public IActionResult OnPost(int gameId, int qty = 1)
    {
        if (qty <= 0) qty = 1;

        var game = _db.Games.FirstOrDefault(g => g.Id == gameId);
        if (game is null)
            return NotFound();

        _cart.AddItem(game.Id, game.Title, game.Price, null, qty);
        return RedirectToPage("/Cart/Index");
    }
}