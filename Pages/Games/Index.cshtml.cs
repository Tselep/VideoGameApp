using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Games;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public IList<Game> Games { get; set; } = new List<Game>();

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? q { get; set; }

    public async Task OnGetAsync(int pageNumber = 1)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;

        IQueryable<Game> query = _db.Games
            .Include(g => g.Genre)
            .Include(g => g.Studio)
            .OrderBy(g => g.Title);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();

            query = query.Where(g =>
                EF.Functions.Like(g.Title, $"%{term}%") ||
                (g.Genre != null && EF.Functions.Like(g.Genre.Name, $"%{term}%")) ||
                (g.Studio != null && EF.Functions.Like(g.Studio.Name, $"%{term}%"))
            );
        }

        var totalCount = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

        Games = await query
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }
}
