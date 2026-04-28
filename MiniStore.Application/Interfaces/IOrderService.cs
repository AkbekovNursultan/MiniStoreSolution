using MiniStore.Application.DTOs.Order;

namespace MiniStore.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync();
    Task<OrderDto?> GetByIdAsync(int id);
    Task<OrderDto> CreateAsync(CreateOrderDto dto);
    Task<bool> UpdateAsync(int id, UpdateOrderDto dto);
    Task<bool> DeleteAsync(int id);
}
