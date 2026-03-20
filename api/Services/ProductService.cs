using api.Data;
using api.Models.Dtos;
using api.Models.Entities;
using api.Models.Requests;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Create(ProductRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            SKU = request.SKU.Trim(),
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            Price = request.Price,
            ImageUrl = request.ImageUrl?.Trim(),
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System"
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return new ProductDto(product);
    }

    public async Task Delete(Guid id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

        if (product == null)
        {
            return;
        }

        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(i => i.ProductId == id && i.DeletedAt == null);

        if (inventory != null)
        {
            _context.Inventories.Remove(inventory);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetAll()
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.DeletedAt == null)
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto(p))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetById(Guid id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

        return product == null ? null : new ProductDto(product);
    }

    public async Task<ProductDto?> GetBySKU(string sku)
    {
        var normalizedSku = sku.Trim();

        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.SKU == normalizedSku && p.DeletedAt == null);

        return product == null ? null : new ProductDto(product);
    }

    public async Task<ProductDto?> Update(Guid id, ProductRequest request)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null);

        if (product == null)
        {
            return null;
        }

        product.SKU = request.SKU.Trim();
        product.Name = request.Name.Trim();
        product.Description = request.Description.Trim();
        product.Price = request.Price;
        product.ImageUrl = request.ImageUrl?.Trim();
        product.UpdatedAt = DateTime.UtcNow;
        product.UpdatedBy = "System";

        await _context.SaveChangesAsync();
        return new ProductDto(product!);
    }
}