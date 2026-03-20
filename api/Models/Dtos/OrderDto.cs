using api.Models.Entities;

namespace api.Models.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string? Status { get; set; }
    public string? Address { get; set; }
    public string? SubDistrict { get; set; }
    public string? District { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public decimal? TotalAmount { get; set; }

    public static OrderDto ToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Status = order.Status,
            Address = order.Address,
            SubDistrict = order.SubDistrict,
            District = order.District,
            Province = order.Province,
            PostalCode = order.PostalCode,
            TotalAmount = order.TotalAmount
        };
    }
}