using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Models.Cart;
using VideoGameApp.Services.Cart;

namespace VideoGameApp.Pages.Cart;

public class IndexModel : PageModel
{
    private readonly ICartService _cart;

    public IndexModel(ICartService cart)
    {
        _cart = cart;
    }

    public IReadOnlyList<CartItemDto> Items { get; private set; } = new List<CartItemDto>();
    public decimal TotalPrice { get; private set; }
    public int TotalQuantity { get; private set; }

    public void OnGet()
    {
        Items = _cart.GetItems();
        TotalPrice = _cart.GetTotalPrice();
        TotalQuantity = _cart.GetTotalQuantity();
    }

    public IActionResult OnPostUpdate(int gameId, int qty)
    {
        _cart.UpdateQuantity(gameId, qty);
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int gameId)
    {
        _cart.RemoveItem(gameId);
        return RedirectToPage();
    }

    public IActionResult OnPostClear()
    {
        _cart.Clear();
        return RedirectToPage();
    }
}