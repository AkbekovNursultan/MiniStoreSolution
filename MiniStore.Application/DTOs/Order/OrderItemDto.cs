using System.ComponentModel.DataAnnotations;

namespace MiniStore.Application.DTOs.Order;

public record OrderItemDto(
    [Range(1, int.MaxValue)] int ProductId,
    [Range(1, int.MaxValue)] int Quantity
);
