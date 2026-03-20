namespace api.Models.Requests;

public class OrderItemRequest
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
}