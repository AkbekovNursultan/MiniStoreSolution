using Microsoft.EntityFrameworkCore;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;
using MiniStore.Domain.Entities;
using MiniStore.Infrastructure.Persistence;

namespace MiniStore.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            CreatedAt = DateTime.UtcNow,
            OrderProducts = dto.ProductIds
                .Select(p => new OrderProduct { ProductId = p })
                .ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return new OrderDto(order.Id, order.CreatedAt, dto.ProductIds);
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
            .Select(o => new OrderDto(
                o.Id,
                o.CreatedAt,
                o.OrderProducts.Select(op => op.ProductId).ToList()
            ))
            .ToListAsync();
    }
}