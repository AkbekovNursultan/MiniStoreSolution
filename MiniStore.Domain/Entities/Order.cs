namespace MiniStore.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Many-to-Many с Product через OrderProduct
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}