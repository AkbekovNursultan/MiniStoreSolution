using Microsoft.Extensions.Logging;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;
using MiniStore.Domain.Entities;

namespace MiniStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all orders");
        var orders = await _orderRepository.GetAllWithItemsAsync();
        return orders.Select(MapToDto);
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching order {OrderId}", id);
        var order = await _orderRepository.GetByIdWithItemsAsync(id);

        if (order is null)
        {
            _logger.LogWarning("Order {OrderId} was not found", id);
            return null;
        }

        return MapToDto(order);
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            CreatedAt = DateTime.UtcNow,
            OrderProducts = BuildOrderProducts(dto.Items)
        };

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveAsync();

        _logger.LogInformation("Created order {OrderId}", order.Id);
        return MapToDto(order);
    }

    public async Task<bool> UpdateAsync(int id, UpdateOrderDto dto)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(id);
        if (order is null)
        {
            _logger.LogWarning("Cannot update order {OrderId} because it was not found", id);
            return false;
        }

        order.OrderProducts.Clear();
        foreach (var orderProduct in BuildOrderProducts(dto.Items))
        {
            order.OrderProducts.Add(orderProduct);
        }

        _orderRepository.Update(order);
        await _orderRepository.SaveAsync();

        _logger.LogInformation("Updated order {OrderId}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null)
        {
            _logger.LogWarning("Cannot delete order {OrderId} because it was not found", id);
            return false;
        }

        _orderRepository.Delete(order);
        await _orderRepository.SaveAsync();

        _logger.LogInformation("Deleted order {OrderId}", id);
        return true;
    }

    private static List<OrderProduct> BuildOrderProducts(IEnumerable<OrderItemDto> items)
    {
        return items
            .GroupBy(i => i.ProductId)
            .Select(g => new OrderProduct
            {
                ProductId = g.Key,
                Quantity = g.Sum(x => x.Quantity)
            })
            .ToList();
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto(
            order.Id,
            order.CreatedAt,
            order.OrderProducts
                .Select(op => new OrderItemDto(op.ProductId, op.Quantity))
                .ToList()
        );
    }
}
