using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VideoGameApp.Data;
using VideoGameApp.Models.Orders;

namespace VideoGameApp.Pages.Orders;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;

    public DetailsModel(AppDbContext db) => _db = db;

    public Order? Order { get; private set; }

    public IActionResult OnGet(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Order = _db.Orders
            .Include(o => o.Items)
            .FirstOrDefault(o => o.Id == id && o.UserId == userId);

        if (Order is null) return NotFound();
        return Page();
    }
}