using MiniStore.Domain.Entities;
using MiniStore.Infrastructure.Persistence;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Electronics" },
                new Category { Name = "Books" }
            );
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Laptop", Price = 1200, CategoryId = 1 },
                new Product { Name = "Book: C# Basics", Price = 30, CategoryId = 2 }
            );
            context.SaveChanges();
        }
    }
}