namespace VideoGameApp.Models.Cart;

public sealed class CartItemDto
{
    public int GameId { get; set; }
    public string Title { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
}