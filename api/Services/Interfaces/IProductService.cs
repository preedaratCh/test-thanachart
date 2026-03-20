using api.Models.Dtos;
using api.Models.Requests;

namespace api.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetById(Guid id);
        Task<ProductDto?> GetBySKU(string sku);
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> Create(ProductRequest request);
        Task<ProductDto?> Update(Guid id, ProductRequest request);
        Task Delete(Guid id);
    }
}
