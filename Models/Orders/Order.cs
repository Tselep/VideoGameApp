using System.ComponentModel.DataAnnotations;

namespace VideoGameApp.Models.Orders;

public class Order
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = "";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public decimal Total { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}