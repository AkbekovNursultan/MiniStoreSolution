using Microsoft.EntityFrameworkCore;
using MiniStore.Application.DTOs;
using MiniStore.Application.DTOs.Product;
using MiniStore.Application.Interfaces;
using MiniStore.Domain.Entities;

namespace MiniStore.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();

        return products.Select(p =>
            new ProductDto(p.Id, p.Name, p.Price, p.CategoryId));
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var p = await _repository.GetByIdAsync(id);

        if (p == null) return null;

        return new ProductDto(p.Id, p.Name, p.Price, p.CategoryId);
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

        return new ProductDto(product.Id, product.Name, product.Price, product.CategoryId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return false;

        _repository.Delete(product);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return false;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        _repository.Update(product);
        await _repository.SaveAsync();

        return true;
    }
}