using api.Models.Dtos;
using api.Models.Requests;

namespace api.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<InventoryDto?> GetById(Guid id);
        Task<InventoryDto?> GetByProductId(Guid productId);
        Task<IEnumerable<InventoryDto>> GetAll();
        Task<InventoryDto> Create(InventoryRequest request);
        Task<InventoryDto?> Update(Guid id, InventoryRequest request);
        Task Delete(Guid id);
    }
}
