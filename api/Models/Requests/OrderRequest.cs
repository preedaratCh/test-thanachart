namespace api.Models.Requests;

public class OrderRequest
{
    public required Guid CustomerId { get; set; }
    public required List<OrderItemRequest> Items { get; set; }
}