namespace api.Models.Entities;

public class CustomerAddress
{
    public Guid Id { get; set; }
    public required Guid CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public required string Address { get; set; }
    public required string SubDistrict { get; set; }
    public required string District { get; set; }
    public required string Province { get; set; }
    public required string PostalCode { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}