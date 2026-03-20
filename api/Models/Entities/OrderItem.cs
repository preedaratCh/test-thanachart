namespace api.Models.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public required Guid OrderId { get; set; }
    public required Order Order { get; set; }
    public required Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; } = 0;
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}