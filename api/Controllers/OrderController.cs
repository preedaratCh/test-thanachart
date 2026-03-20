using api.Models.Dtos;
using api.Models.Requests;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var orders = await _orderService.GetAll();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById([FromRoute] Guid id)
    {
        var order = await _orderService.GetById(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpGet("customers/{customerId}")]
    public async Task<ActionResult<OrderDto>> GetByCustomerId([FromRoute] Guid customerId)
    {
        var order = await _orderService.GetByCustomerId(customerId);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] OrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var order = await _orderService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderDto>> Update([FromRoute] Guid id, [FromBody] OrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedOrder = await _orderService.Update(id, request);
            if (updatedOrder == null)
            {
                return NotFound();
            }
            return Ok(updatedOrder);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var existingOrder = await _orderService.GetById(id);
        if (existingOrder == null)
        {
            return NotFound();
        }

        await _orderService.Delete(id);
        return NoContent();
    }
}