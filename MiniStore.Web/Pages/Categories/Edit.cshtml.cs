using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Category;
using MiniStore.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MiniStore.Web.Pages.Categories;

public class EditModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public EditModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Введите название")]
    [MaxLength(100, ErrorMessage = "Максимум 100 символов")]
    public string Name { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var category = await _categoryService.GetByIdAsync(Id);
        if (category is null) return NotFound();

        Name = category.Name;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _categoryService.UpdateAsync(Id, new UpdateCategoryDto(Name));
        return RedirectToPage("/Categories/Index");
    }
}
