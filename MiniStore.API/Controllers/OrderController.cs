using Microsoft.AspNetCore.Mvc;
using MiniStore.Application.DTOs.Order;
using MiniStore.Application.Interfaces;

namespace MiniStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService service, ILogger<OrderController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("HTTP GET all orders");
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation("HTTP GET order {OrderId}", id);
        var order = await _service.GetByIdAsync(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        _logger.LogInformation("HTTP POST create order");
        var order = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
    {
        _logger.LogInformation("HTTP PUT update order {OrderId}", id);
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("HTTP DELETE order {OrderId}", id);
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
