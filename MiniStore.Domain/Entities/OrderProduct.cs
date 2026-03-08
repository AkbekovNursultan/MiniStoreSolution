namespace MiniStore.Domain.Entities;

public class OrderProduct
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }
}