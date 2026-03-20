namespace api.Models.Requests;

public class InventoryRequest
{
    public required Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
