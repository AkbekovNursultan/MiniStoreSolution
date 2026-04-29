using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniStore.Application.DTOs.Category;
using MiniStore.Application.Interfaces;

namespace MiniStore.Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public IEnumerable<CategoryDto> Categories { get; set; } = [];

    public async Task OnGetAsync()
    {
        Categories = await _categoryService.GetAllAsync();
    }
}
