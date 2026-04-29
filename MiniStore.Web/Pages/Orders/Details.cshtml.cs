using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.DTOs.Product;
using MiniStore.Web.Services;

namespace MiniStore.Web.Pages.Orders;

public class DetailsModel : PageModel
{
    private readonly IOrderApiClient _orderService;
    private readonly IProductApiClient _productService;

    public DetailsModel(IOrderApiClient orderService, IProductApiClient productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public OrderDto? Order { get; set; }
    public Dictionary<int, ProductDto> Products { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        Order = await _orderService.GetByIdAsync(Id);
        if (Order is null) return NotFound();

        var all = await _productService.GetAllAsync();
        Products = all.ToDictionary(p => p.Id);
        return Page();
    }
}
