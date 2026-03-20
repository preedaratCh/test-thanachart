using api.Models.Entities;

namespace api.Models.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string? SKU { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }

    public ProductDto(){ }
    public ProductDto(Product entity)
    {
        Id = entity.Id;
        SKU = entity.SKU;
        Name = entity.Name;
        Description = entity.Description;
        Price = entity.Price;
        ImageUrl = entity.ImageUrl;
     }
}