namespace MiniStore.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    // One-to-Many: Category → Products
    public ICollection<Product> Products { get; set; } = new List<Product>();
}