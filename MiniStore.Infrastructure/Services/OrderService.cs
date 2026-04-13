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

    //public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    //{
    //    var order = new Order
    //    {
    //        CreatedAt = DateTime.UtcNow,
    //        OrderProducts = dto.Items
    //            .Select(i => new OrderProduct
    //            {
    //                ProductId = i.ProductId,
    //                Quantity = i.Quantity
    //            })
    //            .ToList()
    //    };

    //    _context.Orders.Add(order);
    //    await _context.SaveChangesAsync();

    //    return new OrderDto(
    //        order.Id,
    //        order.CreatedAt,
    //        dto.Items
    //    );
    //}
    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            CreatedAt = DateTime.UtcNow,
            OrderProducts = dto.Items
                .GroupBy(i => i.ProductId)
                .Select(g => new OrderProduct
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return new OrderDto(order.Id, order.CreatedAt, dto.Items);
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
            .Select(o => new OrderDto(
                o.Id,
                o.CreatedAt,
                o.OrderProducts
                    .Select(op => new OrderItemDto(
                        op.ProductId,
                        op.Quantity
                    ))
                    .ToList()
            ))
            .ToListAsync();
    }
}