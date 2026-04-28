namespace MiniStore.Application.DTOs.Order;

public record UpdateOrderDto(
    List<OrderItemDto> Items
);
