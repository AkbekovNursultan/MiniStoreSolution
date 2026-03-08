using MiniStore.Application.DTOs.Order;

namespace MiniStore.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync();
    Task<OrderDto> CreateAsync(CreateOrderDto dto);
}