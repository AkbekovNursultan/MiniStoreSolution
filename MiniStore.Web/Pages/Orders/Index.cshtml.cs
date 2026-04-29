using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.DTOs.Product;
using MiniStore.Web.Services;

namespace MiniStore.Web.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderApiClient _orderService;
    private readonly IProductApiClient _productService;

    public IndexModel(IOrderApiClient orderService, IProductApiClient productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    public IEnumerable<OrderDto> Orders { get; set; } = [];
    public Dictionary<int, ProductDto> Products { get; set; } = [];

    public async Task OnGetAsync()
    {
        var all = await _orderService.GetAllAsync();
        Orders = all.OrderByDescending(o => o.CreatedAt);

        var products = await _productService.GetAllAsync();
        Products = products.ToDictionary(p => p.Id);
    }
}
