using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Services.Cart;

namespace VideoGameApp.Pages.Cart;

public class AddModel : PageModel
{
    private readonly ICartService _cart;

    public AddModel(ICartService cart)
    {
        _cart = cart;
    }

    public IActionResult OnPost(int gameId, string title, decimal unitPrice, string? imageUrl, int qty = 1)
    {
        if (qty <= 0) qty = 1;

        _cart.AddItem(gameId, title, unitPrice, imageUrl, qty);
        return RedirectToPage("/Cart/Index");
    }
}