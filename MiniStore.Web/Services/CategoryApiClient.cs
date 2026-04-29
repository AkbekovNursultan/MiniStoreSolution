using MiniStore.Application.DTOs.Category;

namespace MiniStore.Web.Services;

public class CategoryApiClient : ICategoryApiClient
{
    private readonly HttpClient _http;

    public CategoryApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<CategoryDto>>("api/category") ?? [];
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        using var response = await _http.GetAsync($"api/category/{id}");
        return await ApiClientResponse.ReadOrNullAsync<CategoryDto>(response);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        using var response = await _http.PostAsJsonAsync("api/category", dto);
        return await ApiClientResponse.ReadRequiredAsync<CategoryDto>(response);
    }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        using var response = await _http.PutAsJsonAsync($"api/category/{id}", dto);
        return ApiClientResponse.IsSuccessOrNotFound(response);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var response = await _http.DeleteAsync($"api/category/{id}");
        return ApiClientResponse.IsSuccessOrNotFound(response);
    }
}
