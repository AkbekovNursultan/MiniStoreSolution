using MiniStore.Application.DTOs.Order;

namespace MiniStore.Web.Services;

public class OrderApiClient : IOrderApiClient
{
    private readonly HttpClient _http;

    public OrderApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<IEnumerable<OrderDto>>("api/order") ?? [];
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        using var response = await _http.GetAsync($"api/order/{id}");
        return await ApiClientResponse.ReadOrNullAsync<OrderDto>(response);
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        using var response = await _http.PostAsJsonAsync("api/order", dto);
        return await ApiClientResponse.ReadRequiredAsync<OrderDto>(response);
    }

    public async Task<bool> UpdateAsync(int id, UpdateOrderDto dto)
    {
        using var response = await _http.PutAsJsonAsync($"api/order/{id}", dto);
        return ApiClientResponse.IsSuccessOrNotFound(response);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var response = await _http.DeleteAsync($"api/order/{id}");
        return ApiClientResponse.IsSuccessOrNotFound(response);
    }
}
