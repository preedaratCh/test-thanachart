using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models.Entities;

namespace api.Data.Seeders;

public static class InventorySeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var productIds = await context.Products
            .Select(p => p.Id)
            .ToListAsync();
        if (productIds.Count == 0)
        {
            return;
        }

        var existingInventoryProductIds = await context.Inventories
            .Select(i => i.ProductId)
            .ToHashSetAsync();

        var newInventories = productIds
            .Where(productId => !existingInventoryProductIds.Contains(productId))
            .Select(productId => new Inventory
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Product = null!,
                Quantity = 10,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
            })
            .ToList();

        if (newInventories.Count == 0)
        {
            return;
        }

        await context.Inventories.AddRangeAsync(newInventories);
        await context.SaveChangesAsync();
    }
}