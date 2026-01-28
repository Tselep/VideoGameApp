using System.ComponentModel.DataAnnotations;

namespace VideoGameApp.Models.Orders;

public class Order
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = "";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public decimal TotalPrice { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
}