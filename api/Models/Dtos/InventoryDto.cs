using api.Models.Entities;

namespace api.Models.Dtos;

public class InventoryDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public static InventoryDto ToDto(Inventory inventory)
    {
        return new InventoryDto
        {
            Id = inventory.Id,
            ProductId = inventory.ProductId,
            Quantity = inventory.Quantity
        };
    }
}