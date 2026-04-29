using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public CreateModel(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    [BindProperty]
    public List<int> ProductIds { get; set; } = [];

    [BindProperty]
    public List<int> Quantities { get; set; } = [];

    public List<SelectListItem> ProductOptions { get; set; } = [];

    public async Task OnGetAsync()
    {
        await LoadProductsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var items = ProductIds
            .Zip(Quantities, (pid, qty) => new OrderItemDto(pid, qty))
            .Where(i => i.ProductId > 0 && i.Quantity > 0)
            .ToList();

        if (items.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Добавьте хотя бы один товар");
            await LoadProductsAsync();
            return Page();
        }

        await _orderService.CreateAsync(new CreateOrderDto(items));
        return RedirectToPage("/Orders/Index");
    }

    private async Task LoadProductsAsync()
    {
        var products = await _productService.GetAllAsync();
        ProductOptions = products.Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList();
    }
}
