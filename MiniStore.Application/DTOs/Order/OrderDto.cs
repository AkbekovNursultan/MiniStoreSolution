
namespace MiniStore.Application.DTOs.Order;
public record OrderDto(
    int Id,
    DateTime CreatedAt,
    List<int> ProductIds
);

