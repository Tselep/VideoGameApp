using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameApp.Models.Cart;
using VideoGameApp.Services.Cart;
using System.Security.Claims;
using VideoGameApp.Data;
using VideoGameApp.Models.Orders;

namespace VideoGameApp.Pages.Checkout;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ICartService _cart;
    private readonly AppDbContext _db;

    public IndexModel(ICartService cart, AppDbContext db)
    {
        _cart = cart;
        _db = db;
    }

    public IReadOnlyList<CartItemDto> Items { get; private set; } = new List<CartItemDto>();
    public decimal TotalPrice { get; private set; }

    public IActionResult OnGet()
    {
        Items = _cart.GetItems();
        TotalPrice = _cart.GetTotalPrice();

        
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

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            return Challenge(); // should not happen because [Authorize]

        var order = new Order
        {
            UserId = userId,
            CreatedAtUtc = DateTime.UtcNow,
            TotalPrice = items.Sum(i => i.UnitPrice * i.Quantity),
            OrderItems = items.Select(i => new OrderItem
            {
                GameId = i.GameId,
                Title = i.Title,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };

        _db.Orders.Add(order);
        _db.SaveChanges();

        _cart.Clear();

        TempData["Success"] = $"Order #{order.Id} placed successfully!";
        return RedirectToPage("/Orders/Details", new { id = order.Id });
    }
}