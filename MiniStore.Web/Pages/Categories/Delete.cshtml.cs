using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public DeleteModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

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
        await _categoryService.DeleteAsync(Id);
        return RedirectToPage("/Categories/Index");
    }
}
