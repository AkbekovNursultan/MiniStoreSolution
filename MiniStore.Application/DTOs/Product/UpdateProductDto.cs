namespace MiniStore.Application.DTOs.Product;

public record UpdateProductDto(
    string Name,
    decimal Price,
    int CategoryId
);