using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IEnumerable<OrderDto> Orders { get; set; } = [];

    public async Task OnGetAsync()
    {
        Orders = await _orderService.GetAllAsync();
    }
}
