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

        // Allow showing success message after order even though cart is now empty
        if (Items.Count == 0 && TempData.ContainsKey("Success") == false)
        {
            TempData["Info"] = "Your cart is empty.";
            return RedirectToPage("/Cart/Index");
        }

        return Page();
    }

    public IActionResult OnPostPlaceOrder()
    {
        var items = _cart.GetItems();
        if (items.Count == 0)
        {
            TempData["Info"] = "Your cart is empty.";
            return RedirectToPage("/Cart/Index");
        }

        // Complete purchase (temporary implementation)
        _cart.Clear();

        TempData["Success"] = "Order placed successfully! Thank you for your purchase.";
        return RedirectToPage("/Checkout/Index");
    }
}