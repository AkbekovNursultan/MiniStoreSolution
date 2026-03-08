namespace MiniStore.Application.DTOs.Product;

public record ProductDto(
    int Id,
    string Name,
    decimal Price,
    int CategoryId
);