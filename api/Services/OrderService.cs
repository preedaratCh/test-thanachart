using api.Data;
using api.Models;
using api.Models.Dtos;
using api.Models.Entities;
using api.Models.Requests;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.DeletedAt == null)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => OrderDto.ToDto(o))
            .ToListAsync();
    }
        
    public async Task<OrderDto?> GetById(Guid id)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id && o.DeletedAt == null);

        return order == null ? null : OrderDto.ToDto(order);
    }

    public async Task<OrderDto?> GetByCustomerId(Guid customerId)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId && o.DeletedAt == null)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync();

        return order == null ? null : OrderDto.ToDto(order);
    }

    public async Task<OrderDto> Create(OrderRequest request)
    {
        var normalizedItems = NormalizeItems(request.Items);
        var totalAmount = await CalculateTotalAmount(normalizedItems);
        await ReserveInventoryForOrder(normalizedItems);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            Customer = null!,
            Status = AppConstant.OrderStatus.PENDING,
            Address = string.Empty,
            SubDistrict = string.Empty,
            District = string.Empty,
            Province = string.Empty,
            PostalCode = string.Empty,
            TotalAmount = totalAmount,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Orders.AddAsync(order);
        await _context.OrderItems.AddRangeAsync(
            normalizedItems.Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Order = null!,
                ProductId = item.ProductId,
                Product = null!,
                Quantity = item.Quantity,
                CreatedAt = DateTime.UtcNow
            })
        );

        await _context.SaveChangesAsync();
        return OrderDto.ToDto(order);
    }

    public async Task<OrderDto?> Update(Guid id, OrderRequest request)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id && o.DeletedAt == null);

        if (order == null)
        {
            return null;
        }

        var normalizedItems = NormalizeItems(request.Items);
        var totalAmount = await CalculateTotalAmount(normalizedItems);

        var existingItems = await _context.OrderItems
            .Where(oi => oi.OrderId == id)
            .ToListAsync();

        if (existingItems.Count > 0)
        {
            _context.OrderItems.RemoveRange(existingItems);
        }

        await _context.OrderItems.AddRangeAsync(
            normalizedItems.Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Order = null!,
                ProductId = item.ProductId,
                Product = null!,
                Quantity = item.Quantity,
                CreatedAt = DateTime.UtcNow
            })
        );

        order.CustomerId = request.CustomerId;
        order.TotalAmount = totalAmount;
        order.UpdatedAt = DateTime.UtcNow;
        order.UpdatedBy = "System";

        await _context.SaveChangesAsync();
        return OrderDto.ToDto(order);
    }

    public async Task Delete(Guid id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id && o.DeletedAt == null);

        if (order == null)
        {
            return;
        }

        var items = await _context.OrderItems
            .Where(oi => oi.OrderId == id)
            .ToListAsync();

        if (items.Count > 0)
        {
            _context.OrderItems.RemoveRange(items);
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    private async Task<decimal> CalculateTotalAmount(IEnumerable<OrderItemRequest> items)
    {
        var productIds = items.Select(i => i.ProductId).Distinct().ToList();

        var productPrices = await _context.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id) && p.DeletedAt == null)
            .Select(p => new { p.Id, p.Price })
            .ToDictionaryAsync(p => p.Id, p => p.Price);

        if (productPrices.Count != productIds.Count)
        {
            throw new InvalidOperationException("One or more products were not found.");
        }

        return items.Sum(item => productPrices[item.ProductId] * item.Quantity);
    }

    private async Task ReserveInventoryForOrder(IEnumerable<OrderItemRequest> items)
    {
        var productIds = items.Select(i => i.ProductId).Distinct().ToList();

        var inventories = await _context.Inventories
            .Where(i => productIds.Contains(i.ProductId) && i.DeletedAt == null)
            .ToDictionaryAsync(i => i.ProductId);

        if (inventories.Count != productIds.Count)
        {
            throw new InvalidOperationException("One or more products have no inventory record.");
        }

        foreach (var item in items)
        {
            var inventory = inventories[item.ProductId];
            if (inventory.Quantity < item.Quantity)
            {
                throw new InvalidOperationException(
                    $"Not enough stock for product {item.ProductId}. Available: {inventory.Quantity}"
                );
            }
        }

        foreach (var item in items)
        {
            var inventory = inventories[item.ProductId];
            inventory.Quantity -= item.Quantity;
            inventory.UpdatedAt = DateTime.UtcNow;
            inventory.UpdatedBy = "System";
        }
    }

    private static List<OrderItemRequest> NormalizeItems(IEnumerable<OrderItemRequest> items)
    {
        var normalized = items
            .GroupBy(i => i.ProductId)
            .Select(g => new OrderItemRequest
            {
                ProductId = g.Key,
                Quantity = g.Sum(x => x.Quantity)
            })
            .ToList();

        if (normalized.Count == 0)
        {
            throw new InvalidOperationException("Order must contain at least one item.");
        }

        if (normalized.Any(i => i.Quantity <= 0))
        {
            throw new InvalidOperationException("All item quantities must be greater than zero.");
        }

        return normalized;
    }

    private static string NormalizeStatus(string? status)
    {
        var normalized = string.IsNullOrWhiteSpace(status)
            ? AppConstant.OrderStatus.PENDING
            : status.Trim().ToLowerInvariant();

        var isValid = normalized is AppConstant.OrderStatus.PENDING
            or AppConstant.OrderStatus.PROCESSING
            or AppConstant.OrderStatus.COMPLETED
            or AppConstant.OrderStatus.CANCELLED;

        if (!isValid)
        {
            throw new InvalidOperationException("Invalid order status.");
        }

        return normalized;
    }
}
