using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameApp.Data;
using VideoGameApp.Models;

namespace VideoGameApp.Pages.Studios;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public List<Studio> Studios { get; set; } = new();

    public async Task OnGetAsync()
    {
        Studios = await _db.Studios
            .OrderBy(s => s.Name)
            .ToListAsync();
    }
}