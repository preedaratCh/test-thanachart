namespace api.Models.Entities;

public class Product
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public required string SKU { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}