using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniStore.Application.DTOs.Product;
using MiniStore.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MiniStore.Web.Pages.Products;

public class CreateModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public CreateModel(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

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

    public async Task OnGetAsync()
    {
        await LoadCategoriesAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return Page();
        }

        await _productService.AddAsync(new CreateProductDto(Name, Price, CategoryId));
        return RedirectToPage("/Products/Index");
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        CategoryOptions = categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
    }
}
