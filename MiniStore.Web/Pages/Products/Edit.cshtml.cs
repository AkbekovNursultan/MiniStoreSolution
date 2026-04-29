using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniStore.Application.DTOs.Product;
using MiniStore.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MiniStore.Web.Pages.Products;

public class EditModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public EditModel(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Введите название")]
    [MaxLength(100, ErrorMessage = "Максимум 100 символов")]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    [Range(0.01, 10000, ErrorMessage = "Цена должна быть от 0.01 до 10000")]
    public decimal Price { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Выберите категорию")]
    public int CategoryId { get; set; }

    public List<SelectListItem> CategoryOptions { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        var product = await _productService.GetByIdAsync(Id);
        if (product is null) return NotFound();

        Name = product.Name;
        Price = product.Price;
        CategoryId = product.CategoryId;

        await LoadCategoriesAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return Page();
        }

        await _productService.UpdateAsync(Id, new UpdateProductDto(Name, Price, CategoryId));
        return RedirectToPage("/Products/Index");
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        CategoryOptions = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
    }
}
