using MiniStore.Application.DTOs.Product;

namespace MiniStore.Web.Services;

public class ProductApiClient : IProductApiClient
{
    private readonly HttpClient _http;

    public ProductApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("api/product") ?? [];
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        using var response = await _http.GetAsync($"api/product/{id}");
        return await ApiClientResponse.ReadOrNullAsync<ProductDto>(response);
    }

    public async Task<ProductDto> AddAsync(CreateProductDto dto)
    {
        using var response = await _http.PostAsJsonAsync("api/product", dto);
        return await ApiClientResponse.ReadRequiredAsync<ProductDto>(response);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        using var response = await _http.PutAsJsonAsync($"api/product/{id}", dto);
        return ApiClientResponse.IsSuccessOrNotFound(response);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var response = await _http.DeleteAsync($"api/product/{id}");
        return ApiClientResponse.IsSuccessOrNotFound(response);
    }
}
