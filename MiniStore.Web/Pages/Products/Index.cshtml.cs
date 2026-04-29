using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Product;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public IndexModel(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public IEnumerable<ProductDto> Products { get; set; } = [];
    public Dictionary<int, string> CategoryNames { get; set; } = [];

    public async Task OnGetAsync()
    {
        Products = await _productService.GetAllAsync();
        var categories = await _categoryService.GetAllAsync();
        CategoryNames = categories.ToDictionary(c => c.Id, c => c.Name);
    }
}
