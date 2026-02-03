using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Studios;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public IList<Studio> Studios { get; set; } = new List<Studio>();

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? q { get; set; }

    public async Task OnGetAsync(int pageNumber = 1)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;

        IQueryable<Studio> query = _db.Studios
            .OrderBy(s => s.Name)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(s =>
                EF.Functions.Like(s.Name, $"%{term}%") ||
                EF.Functions.Like(s.Country, $"%{term}%")
            );
        }

        var totalCount = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

        Studios = await query
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }
}