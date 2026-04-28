using System.ComponentModel.DataAnnotations;

namespace MiniStore.Application.DTOs.Order;

public record UpdateOrderDto(
    [Required, MinLength(1)] List<OrderItemDto> Items
);
