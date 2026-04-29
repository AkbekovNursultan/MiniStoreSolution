using Microsoft.Extensions.Logging;
using MiniStore.Application.DTOs.Product;
using MiniStore.Application.Interfaces;
using MiniStore.Domain.Entities;

namespace MiniStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IRepository<Product> repository, ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all products");
        var products = await _repository.GetAllAsync();

        return products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.CategoryId, p.ImageUrl));
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching product {ProductId}", id);
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
        {
            _logger.LogWarning("Product {ProductId} was not found", id);
            return null;
        }

        return new ProductDto(product.Id, product.Name, product.Price, product.CategoryId, product.ImageUrl);
    }

    public async Task<ProductDto> AddAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId
        };

        await _repository.AddAsync(product);
        await _repository.SaveAsync();

        _logger.LogInformation("Created product {ProductId}", product.Id);
        return new ProductDto(product.Id, product.Name, product.Price, product.CategoryId, product.ImageUrl);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product is null)
        {
            _logger.LogWarning("Cannot update product {ProductId} because it was not found", id);
            return false;
        }

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        _repository.Update(product);
        await _repository.SaveAsync();

        _logger.LogInformation("Updated product {ProductId}", id);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product is null)
        {
            _logger.LogWarning("Cannot delete product {ProductId} because it was not found", id);
            return false;
        }

        _repository.Delete(product);
        await _repository.SaveAsync();

        _logger.LogInformation("Deleted product {ProductId}", id);
        return true;
    }
}
