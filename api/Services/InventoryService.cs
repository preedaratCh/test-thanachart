using api.Data;
using api.Models.Dtos;
using api.Models.Entities;
using api.Models.Requests;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class InventoryService : IInventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<InventoryDto> Create(InventoryRequest request)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.Id == request.ProductId && p.DeletedAt == null);

        if (!productExists)
        {
            throw new InvalidOperationException("Product not found.");
        }

        var inventory = new Inventory
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            Product = null!,
            Quantity = request.Quantity,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System"
        };

        await _context.Inventories.AddAsync(inventory);
        await _context.SaveChangesAsync();

        return InventoryDto.ToDto(inventory);
    }

    public async Task Delete(Guid id)
    {
        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (inventory == null)
        {
            return;
        }

        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<InventoryDto>> GetAll()
    {
        return await _context.Inventories
            .AsNoTracking()
            .Where(i => i.DeletedAt == null)
            .OrderBy(i => i.ProductId)
            .Select(i => InventoryDto.ToDto(i))
            .ToListAsync();
    }

    public async Task<InventoryDto?> GetById(Guid id)
    {
        var inventory = await _context.Inventories
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        return inventory == null ? null : InventoryDto.ToDto(inventory);
    }

    public async Task<InventoryDto?> GetByProductId(Guid productId)
    {
        var inventory = await _context.Inventories
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.ProductId == productId && i.DeletedAt == null);

        return inventory == null ? null : InventoryDto.ToDto(inventory);
    }

    public async Task<InventoryDto?> Update(Guid id, InventoryRequest request)
    {
        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

        if (inventory == null)
        {
            return null;
        }

        var productExists = await _context.Products
            .AnyAsync(p => p.Id == request.ProductId && p.DeletedAt == null);

        if (!productExists)
        {
            throw new InvalidOperationException("Product not found.");
        }

        inventory.ProductId = request.ProductId;
        inventory.Quantity = request.Quantity;
        inventory.UpdatedAt = DateTime.UtcNow;
        inventory.UpdatedBy = "System";

        await _context.SaveChangesAsync();
        return InventoryDto.ToDto(inventory);
    }
}