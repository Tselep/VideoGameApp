using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Genres;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db) => _db = db;

    public IList<Genre> Genres { get; set; } = new List<Genre>();

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? q { get; set; }

    public async Task OnGetAsync(int pageNumber = 1)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;

        IQueryable<Genre> query = _db.Genres
            .OrderBy(g => g.Name)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(g => EF.Functions.Like(g.Name, $"%{term}%"));
        }

        var totalCount = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

        Genres = await query
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }
}