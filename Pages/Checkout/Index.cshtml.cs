using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Models.Cart;
using VideoGameApp.Services.Cart;

namespace VideoGameApp.Pages.Checkout;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICartService _cart;

    public IndexModel(ICartService cart)
    {
        _cart = cart;
    }

    public IReadOnlyList<CartItemDto> Items { get; private set; } = new List<CartItemDto>();
    public decimal TotalPrice { get; private set; }

    public IActionResult OnGet()
    {
        Items = _cart.GetItems();
        TotalPrice = _cart.GetTotalPrice();

        if (!Items.Any())
        {
            TempData["Info"] = "Your cart is empty.";
            return RedirectToPage("/Cart/Index");
        }

        return Page();
    }

    public IActionResult OnPostPlaceOrder()
    {
        // (Για τώρα) απλά “ολοκληρώνουμε” την αγορά:
        _cart.Clear();
        TempData["Success"] = "Order placed successfully!";
        return RedirectToPage("/Cart/Index");
    }
}