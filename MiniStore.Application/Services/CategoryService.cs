using Microsoft.Extensions.Logging;
using MiniStore.Application.DTOs.Category;
using MiniStore.Application.Interfaces;
using MiniStore.Domain.Entities;

namespace MiniStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(IRepository<Category> repository, ILogger<CategoryService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all categories");
        var categories = await _repository.GetAllAsync();

        return categories.Select(c => new CategoryDto(c.Id, c.Name));
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching category {CategoryId}", id);
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
        {
            _logger.LogWarning("Category {CategoryId} was not found", id);
            return null;
        }

        return new CategoryDto(category.Id, category.Name);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        await _repository.AddAsync(category);
        await _repository.SaveAsync();

        _logger.LogInformation("Created category {CategoryId}", category.Id);
        return new CategoryDto(category.Id, category.Name);
    }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
        {
            _logger.LogWarning("Cannot update category {CategoryId} because it was not found", id);
            return false;
        }

        category.Name = dto.Name;

        _repository.Update(category);
        await _repository.SaveAsync();

        _logger.LogInformation("Updated category {CategoryId}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
        {
            _logger.LogWarning("Cannot delete category {CategoryId} because it was not found", id);
            return false;
        }

        _repository.Delete(category);
        await _repository.SaveAsync();

        _logger.LogInformation("Deleted category {CategoryId}", id);
        return true;
    }
}
