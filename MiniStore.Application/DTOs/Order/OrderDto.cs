
namespace MiniStore.Application.DTOs.Order;
public record OrderDto(
    int Id,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);

