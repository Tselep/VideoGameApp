using VideoGameApp.Models.Cart;

namespace VideoGameApp.Services.Cart;

public interface ICartService
{
    IReadOnlyList<CartItemDto> GetItems();
    int GetTotalQuantity();
    decimal GetTotalPrice();

    void AddItem(int gameId, string title, decimal unitPrice, string? imageUrl, int qty = 1);
    void UpdateQuantity(int gameId, int qty);
    void RemoveItem(int gameId);
    void Clear();
}