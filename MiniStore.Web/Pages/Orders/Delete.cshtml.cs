using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Orders;

public class DeleteModel : PageModel
{
    private readonly IOrderService _orderService;

    public DeleteModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var order = await _orderService.GetByIdAsync(Id);
        if (order is null) return NotFound();

        CreatedAt = order.CreatedAt;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _orderService.DeleteAsync(Id);
        return RedirectToPage("/Orders/Index");
    }
}
