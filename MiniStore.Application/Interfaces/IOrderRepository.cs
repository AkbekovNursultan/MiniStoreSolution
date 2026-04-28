using MiniStore.Domain.Entities;

namespace MiniStore.Application.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetAllWithItemsAsync();
    Task<Order?> GetByIdWithItemsAsync(int id);
}
