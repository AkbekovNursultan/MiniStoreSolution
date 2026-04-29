using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Products;

public class DeleteModel : PageModel
{
    private readonly IProductService _productService;

    public DeleteModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var product = await _productService.GetByIdAsync(Id);
        if (product is null) return NotFound();

        Name = product.Name;
        Price = product.Price;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _productService.DeleteAsync(Id);
        return RedirectToPage("/Products/Index");
    }
}
