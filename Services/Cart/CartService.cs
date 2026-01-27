using Microsoft.AspNetCore.Http;
using VideoGameApp.Infrastructure.Session;
using VideoGameApp.Models.Cart;

namespace VideoGameApp.Services.Cart;

public sealed class CartService : ICartService
{
    private const string Key = "CART";
    private readonly IHttpContextAccessor _http;

    public CartService(IHttpContextAccessor http) => _http = http;

    private ISession Session => _http.HttpContext!.Session;

    private List<CartItemDto> Load()
        => Session.GetJson<List<CartItemDto>>(Key) ?? new List<CartItemDto>();

    private void Save(List<CartItemDto> items) => Session.SetJson(Key, items);

    public IReadOnlyList<CartItemDto> GetItems() => Load();

    public int GetTotalQuantity() => Load().Sum(x => x.Quantity);

    public decimal GetTotalPrice() => Load().Sum(x => x.UnitPrice * x.Quantity);

    public void AddItem(int gameId, string title, decimal unitPrice, string? imageUrl, int qty = 1)
    {
        if (qty <= 0) qty = 1;

        var items = Load();
        var item = items.FirstOrDefault(x => x.GameId == gameId);

        if (item is null)
        {
            items.Add(new CartItemDto
            {
                GameId = gameId,
                Title = title,
                UnitPrice = unitPrice,
                Quantity = qty,
                ImageUrl = imageUrl
            });
        }
        else
        {
            item.Quantity += qty;
        }

        Save(items);
    }

    public void UpdateQuantity(int gameId, int qty)
    {
        var items = Load();
        var item = items.FirstOrDefault(x => x.GameId == gameId);
        if (item is null) return;

        if (qty <= 0) items.Remove(item);
        else item.Quantity = qty;

        Save(items);
    }

    public void RemoveItem(int gameId)
    {
        var items = Load();
        items.RemoveAll(x => x.GameId == gameId);
        Save(items);
    }

    public void Clear() => Save(new List<CartItemDto>());
}