using System.ComponentModel.DataAnnotations;

namespace MiniStore.Application.DTOs.Product;

public record UpdateProductDto(
    [Required, MaxLength(100)] string Name,
    [Range(0.01, 10000)] decimal Price,
    [Required] int CategoryId
);
