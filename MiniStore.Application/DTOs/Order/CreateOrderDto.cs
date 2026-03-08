namespace MiniStore.Application.DTOs.Order;

public record CreateOrderDto(
    List<int> ProductIds
);