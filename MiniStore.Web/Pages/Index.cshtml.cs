using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Category;
using MiniStore.Application.DTOs.Product;
using MiniStore.Web.Models;
using MiniStore.Web.Services;

namespace MiniStore.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IProductApiClient _productService;
    private readonly ICategoryApiClient _categoryService;

    public IndexModel(IProductApiClient productService, ICategoryApiClient categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public IEnumerable<ProductDto> Products { get; set; } = [];
    public IEnumerable<CategoryDto> Categories { get; set; } = [];
    public int? SelectedCategoryId { get; set; }

    public async Task OnGetAsync(int? categoryId)
    {
        SelectedCategoryId = categoryId;
        Categories = await _categoryService.GetAllAsync();
        var all = await _productService.GetAllAsync();
        Products = categoryId.HasValue ? all.Where(p => p.CategoryId == categoryId) : all;
    }

    public IActionResult OnPostAddToCart(int productId, string name, decimal price, string? imageUrl)
    {
        CartSessionHelper.AddItem(HttpContext.Session, new CartItem
        {
            ProductId = productId,
            Name = name,
            Price = price,
            ImageUrl = imageUrl,
            Quantity = 1
        });
        return RedirectToPage();
    }
}
