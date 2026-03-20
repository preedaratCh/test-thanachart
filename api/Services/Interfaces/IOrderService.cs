using api.Models.Dtos;
using api.Models.Requests;

namespace api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> GetById(Guid id);
        Task<OrderDto?> GetByCustomerId(Guid customerId);
        Task<IEnumerable<OrderDto>> GetAll();
        Task<OrderDto> Create(OrderRequest request);
        Task<OrderDto?> Update(Guid id, OrderRequest request);
        Task Delete(Guid id);
    }
}
