using api.Models.Dtos;
using api.Models.Requests;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace api.Controllers;

[ApiController]
[Route("api/inventories")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAll()
    {
        var inventories = await _inventoryService.GetAll();
        return Ok(inventories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryDto>> GetById([FromRoute] Guid id)
    {
        var inventory = await _inventoryService.GetById(id);
        if (inventory == null)
        {
            return NotFound();
        }
        return Ok(inventory);
    }

    [HttpPost]
    public async Task<ActionResult<InventoryDto>> Create([FromBody] InventoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var inventory = await _inventoryService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = inventory.Id }, inventory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InventoryDto>> Update([FromRoute] Guid id, [FromBody] InventoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedInventory = await _inventoryService.Update(id, request);
        if (updatedInventory == null)
        {
            return NotFound();
        }
        return Ok(updatedInventory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var existingInventory = await _inventoryService.GetById(id);
        if (existingInventory == null)
        {
            return NotFound();
        }

        await _inventoryService.Delete(id);
        return NoContent();
    }
}