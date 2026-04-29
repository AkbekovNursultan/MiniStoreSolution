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
                new Product { Name = "Ноутбук", Price = 89990, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=400&q=80" },
                new Product { Name = "Смартфон", Price = 64990, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=400&q=80" },
                new Product { Name = "Наушники", Price = 12990, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400&q=80" },
                new Product { Name = "Клавиатура", Price = 4990, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=400&q=80" },
                new Product { Name = "Монитор", Price = 28990, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=400&q=80" },

                new Product { Name = "Clean Code", Price = 1890, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=400&q=80" },
                new Product { Name = "C# для начинающих", Price = 1290, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400&q=80" },
                new Product { Name = "Design Patterns", Price = 2190, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1481627834876-b7833e8f5570?w=400&q=80" },
                new Product { Name = "ASP.NET Core в действии", Price = 2490, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1524995997946-a1c2e315a42f?w=400&q=80" },

                new Product { Name = "Футболка", Price = 990, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=400&q=80" },
                new Product { Name = "Джинсы", Price = 3990, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1542272604-787c3835535d?w=400&q=80" },
                new Product { Name = "Кроссовки", Price = 5990, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400&q=80" },

                new Product { Name = "Коврик для йоги", Price = 1990, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=400&q=80" },
                new Product { Name = "Бутылка для воды", Price = 890, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1602143407151-7111542de6e8?w=400&q=80" },
                new Product { Name = "Гантели 5кг", Price = 2490, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=400&q=80" },

                new Product { Name = "Настольная лампа", Price = 3490, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=400&q=80" },
                new Product { Name = "Кофемашина", Price = 14990, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=400&q=80" },
                new Product { Name = "Органайзер для стола", Price = 1290, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1586281380349-632531db7ed4?w=400&q=80" }
            );
            context.SaveChanges();
        }
    }
}
