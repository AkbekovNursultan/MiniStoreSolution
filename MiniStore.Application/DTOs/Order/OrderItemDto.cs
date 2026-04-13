using System;
using System.Collections.Generic;
using System.Text;

namespace MiniStore.Application.DTOs.Order;

public record OrderItemDto(
    int ProductId,
    int Quantity
);
