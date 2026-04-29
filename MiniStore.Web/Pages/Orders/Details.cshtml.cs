using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Orders;

public class DetailsModel : PageModel
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public DetailsModel(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public OrderDto? Order { get; set; }
    public Dictionary<int, string> ProductNames { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        Order = await _orderService.GetByIdAsync(Id);
        if (Order is null) return NotFound();

        var products = await _productService.GetAllAsync();
        ProductNames = products.ToDictionary(p => p.Id, p => p.Name);
        return Page();
    }
}
