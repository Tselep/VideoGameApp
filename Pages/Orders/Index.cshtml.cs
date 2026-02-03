using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VideoGameApp.Data;
using VideoGameApp.Models.Orders;

namespace VideoGameApp.Pages.Orders;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db) => _db = db;

    public List<Order> Orders { get; private set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? OrderId { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? From { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? To { get; set; }

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        IQueryable<Order> query = _db.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAtUtc)
            .AsNoTracking();

        if (OrderId.HasValue)
        {
            query = query.Where(o => o.Id == OrderId.Value);
        }

        if (From.HasValue)
        {
            var fromUtc = DateTime.SpecifyKind(From.Value.Date, DateTimeKind.Local).ToUniversalTime();
            query = query.Where(o => o.CreatedAtUtc >= fromUtc);
        }

        if (To.HasValue)
        {
            // include the entire 'To' day
            var toUtc = DateTime.SpecifyKind(To.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime();
            query = query.Where(o => o.CreatedAtUtc < toUtc);
        }

        Orders = await query.ToListAsync();
    }
}