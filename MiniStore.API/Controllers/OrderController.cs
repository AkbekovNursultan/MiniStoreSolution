using Microsoft.AspNetCore.Mvc;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;

namespace MiniStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var order = await _service.CreateAsync(dto);
        return Ok(order);
    }
}