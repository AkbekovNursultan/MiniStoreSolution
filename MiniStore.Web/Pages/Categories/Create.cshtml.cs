using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Category;
using MiniStore.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MiniStore.Web.Pages.Categories;

public class CreateModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public CreateModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [BindProperty]
    [Required(ErrorMessage = "Введите название")]
    [MaxLength(100, ErrorMessage = "Максимум 100 символов")]
    public string Name { get; set; } = string.Empty;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _categoryService.CreateAsync(new CreateCategoryDto(Name));
        return RedirectToPage("/Categories/Index");
    }
}
