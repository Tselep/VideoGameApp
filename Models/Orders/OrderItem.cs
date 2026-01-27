using System.ComponentModel.DataAnnotations;

namespace VideoGameApp.Models.Orders;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int GameId { get; set; }

    [Required]
    public string Title { get; set; } = "";

    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}