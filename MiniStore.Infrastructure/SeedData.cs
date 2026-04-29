using MiniStore.Domain.Entities;
using MiniStore.Infrastructure.Persistence;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Электроника" },
                new Category { Name = "Книги" },
                new Category { Name = "Одежда" },
                new Category { Name = "Спорт" },
                new Category { Name = "Дом" }
            );
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Ноутбук", Price = 1200, CategoryId = 1 },
                new Product { Name = "Смартфон", Price = 799, CategoryId = 1 },
                new Product { Name = "Наушники", Price = 149, CategoryId = 1 },
                new Product { Name = "Клавиатура", Price = 89, CategoryId = 1 },
                new Product { Name = "Монитор", Price = 349, CategoryId = 1 },

                new Product { Name = "Clean Code", Price = 35, CategoryId = 2 },
                new Product { Name = "C# для начинающих", Price = 25, CategoryId = 2 },
                new Product { Name = "Design Patterns", Price = 40, CategoryId = 2 },
                new Product { Name = "ASP.NET Core в действии", Price = 45, CategoryId = 2 },

                new Product { Name = "Футболка", Price = 19, CategoryId = 3 },
                new Product { Name = "Джинсы", Price = 59, CategoryId = 3 },
                new Product { Name = "Кроссовки", Price = 89, CategoryId = 3 },

                new Product { Name = "Коврик для йоги", Price = 29, CategoryId = 4 },
                new Product { Name = "Бутылка для воды", Price = 15, CategoryId = 4 },
                new Product { Name = "Гантели 5кг", Price = 39, CategoryId = 4 },

                new Product { Name = "Настольная лампа", Price = 49, CategoryId = 5 },
                new Product { Name = "Кофемашина", Price = 199, CategoryId = 5 },
                new Product { Name = "Органайзер для стола", Price = 22, CategoryId = 5 }
            );
            context.SaveChanges();
        }
    }
}
