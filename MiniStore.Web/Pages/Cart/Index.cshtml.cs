using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;
using MiniStore.Web.Models;

namespace MiniStore.Web.Pages.Cart;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public List<CartItem> Items { get; set; } = [];

    public void OnGet()
    {
        Items = CartSessionHelper.GetCart(HttpContext.Session);
    }

    public IActionResult OnPostUpdateQuantity(int productId, int quantity)
    {
        CartSessionHelper.UpdateQuantity(HttpContext.Session, productId, quantity);
        return RedirectToPage();
    }

    public IActionResult OnPostIncrease(int productId)
    {
        var cart = CartSessionHelper.GetCart(HttpContext.Session);
        var item = cart.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
            CartSessionHelper.UpdateQuantity(HttpContext.Session, productId, item.Quantity + 1);
        return RedirectToPage();
    }

    public IActionResult OnPostDecrease(int productId)
    {
        var cart = CartSessionHelper.GetCart(HttpContext.Session);
        var item = cart.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
            CartSessionHelper.UpdateQuantity(HttpContext.Session, productId, item.Quantity - 1);
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int productId)
    {
        CartSessionHelper.RemoveItem(HttpContext.Session, productId);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostCheckoutAsync()
    {
        var items = CartSessionHelper.GetCart(HttpContext.Session);
        if (items.Count == 0)
            return RedirectToPage();

        var orderItems = items.Select(i => new OrderItemDto(i.ProductId, i.Quantity)).ToList();
        await _orderService.CreateAsync(new CreateOrderDto(orderItems));

        CartSessionHelper.Clear(HttpContext.Session);
        return RedirectToPage("/Orders/Index");
    }
}
