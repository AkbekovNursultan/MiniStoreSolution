using Microsoft.EntityFrameworkCore;
using MiniStore.Application.Interfaces;
using MiniStore.Domain.Entities;
using MiniStore.Infrastructure.Persistence;

namespace MiniStore.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetAllWithItemsAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdWithItemsAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderProducts)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
