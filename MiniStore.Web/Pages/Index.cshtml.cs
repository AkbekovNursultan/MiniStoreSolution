using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Category;
using MiniStore.Application.DTOs.Product;
using MiniStore.Application.Interfaces;
using MiniStore.Web.Models;

namespace MiniStore.Web.Pages;

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
    public IEnumerable<CategoryDto> Categories { get; set; } = [];
    public int? SelectedCategoryId { get; set; }

    public async Task OnGetAsync(int? categoryId)
    {
        SelectedCategoryId = categoryId;
        Categories = await _categoryService.GetAllAsync();
        var all = await _productService.GetAllAsync();
        Products = categoryId.HasValue ? all.Where(p => p.CategoryId == categoryId) : all;
    }

    public IActionResult OnPostAddToCart(int productId, string name, decimal price)
    {
        CartSessionHelper.AddItem(HttpContext.Session, new CartItem
        {
            ProductId = productId,
            Name = name,
            Price = price,
            Quantity = 1
        });
        return RedirectToPage();
    }
}
